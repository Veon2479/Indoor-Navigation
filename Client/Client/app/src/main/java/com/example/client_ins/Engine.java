package com.example.client_ins;

import static com.example.client_ins.Tools.*;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.StrictMode;
import android.util.Pair;
import android.widget.TextView;
import androidx.annotation.RequiresApi;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.*;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;
import java.nio.charset.CodingErrorAction;
import java.nio.charset.StandardCharsets;
import java.time.Instant;
import java.util.ArrayList;
import java.util.List;

@RequiresApi(api = Build.VERSION_CODES.O)
public class Engine implements Runnable{

    public int UserId = 0;

    public int QrId;
    public double ReceivedCrd1, receivedCrd2, Azimuth;  // received coordinates, they stores the same point all time the program runs
    public double accX, accY;

    public double Crd1, Crd2; //the resulting coordinates, they'll be sent to the server

    public double WiFiCrd1, WiFiCrd2; //coordinates, according to the triangulation - TODO: what we should do, if they aren't determined?
    //this flag should solve previous problem
    public boolean isWiFiDetermined = false;
    public double WiFiAccuracy = 1; // 1 - numbers ARE NOT ACCURATE AT ALL, 0 - they are accurate without 

  
    public boolean isAlive = false;
    public boolean isBLocked = false;

    public List<WiFi> WiFi_List = new ArrayList<>();

    private static Engine instance;
    public static Context context;

    public ClientMath clientMath;
    public Thread mathThread;
    public  DataSender dataSender;
    public WifiModule wifiModule;

    @RequiresApi(api = Build.VERSION_CODES.O)
    private Engine(Context context)
    {
        boolean flag = false;
        try {
            //MainActivity.textScroll.append("Initializing program state"+"\r\n");
            System.out.println("Initializing program state");
            readFromFile(context);


        } catch (Exception e) {
            System.out.println( "FATAL ERROR while reading settings file: " + e );
            flag = true;
        }

        if (!flag)
        {
            System.out.println("serverAddr is "+serverAddr);
            System.out.println("serverPortTcp is "+serverPortTcp);
            System.out.println("serverPortUdp is "+serverPortUdp);
            System.out.println("AttemptsToRegistrate is "+AttemptsToRegistrate);
            System.out.println("BufferSize is "+BufferSize);
            System.out.println("UdpPacketDelay is "+UdpPacketDelay);

        }
    }

    public static void setContext(Context sContext)
    {
        context = sContext;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static Engine getInstance()
    {
        if ( instance == null )
            instance = new Engine(context);
        return instance;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void startTracking()
    {

        if (!isAlive) {
            isBLocked = true;
            boolean flag = false;
            int i = 0;
            while (i < AttemptsToRegistrate && !flag) {
                System.out.println("Trying to registrate");
                flag = Registrate();
                if (!flag)
                    UserId = 0;
                i++;

            }
            if (flag) {
                flag = false;
                i = 0;
                while (i < AttemptsToRegistrate && !flag) {
                    System.out.println("Trying to get wifi info");
                    flag = getWiFiInfo();

                    i++;

                }
            }
            isAlive = flag;

            if (isAlive) {
                System.out.println("Registration was successful");





                //create 2 streams - first to compute coordinates
                //second - to send them
                //but they're don't working yet
            } else {
                System.out.println("Failed to registrate");
                isBLocked = false;
            }
        }
        if (isBLocked) {
            dataSender.EnableUdp();
            clientMath.Unpause();
        }
    }

    public void stopTracking(){
        clientMath.Pause();
        dataSender.DisableUdp();
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public boolean Registrate() {
        boolean RESULT = true;
        try
        {

            Socket clientTcp = new Socket();
            clientTcp.connect( new InetSocketAddress( serverAddr, serverPortTcp ) );
            if ( clientTcp.isConnected() )
                System.out.println("Connected!");
            else
                System.out.println("Not connected!");

            InputStream sock_ins = clientTcp.getInputStream();
            OutputStream sock_outs = clientTcp.getOutputStream();

            int[] regBuffer = {0, QrId};       //request for registration
            sock_outs.write( setCustomBufferWithInts(regBuffer) );

            byte[] buffer = new byte[ 4 + 2 * 8 ];
            sock_ins.read( buffer );

            getResponseBuffer( this, buffer );
            System.out.println( "new ID is "+UserId);
            System.out.println( "new crd1 is "+ReceivedCrd1);
            System.out.println( "new crd2 is "+receivedCrd2);


            if ( UserId < 0 ) {
                RESULT = false;
                System.out.println("Server refused to distribute UserId");
            }
            else
            {
                System.out.println("Calling background service to start");
                Intent intent = new Intent(context, ClientService.class);
                context.startForegroundService( intent );
            }

            try {
                sock_ins.close();
                sock_outs.close();
                clientTcp.close();
            } catch (Exception e)
            {
                System.out.println("Exception while registration: " + e );
            }
            System.out.println("Connection closed!");
        }
        catch  (Exception e)
        {
            System.out.println("While registrate: " + e );
            e.printStackTrace();
            RESULT = false;
        }

        return RESULT;
    }


    public boolean getWiFiInfo()
    {
        boolean RESULT = true;
        try
        {

            Socket clientTcp = new Socket();
            clientTcp.connect( new InetSocketAddress( serverAddr, serverPortTcp ) );
            if ( clientTcp.isConnected() )
                System.out.println("Connected!");
            else
                System.out.println("Not connected!");

            InputStream sock_ins = clientTcp.getInputStream();
            OutputStream sock_outs = clientTcp.getOutputStream();

            System.out.println("Getting additional info");

            int[] reqBuffer = { 1, UserId };       //request for Wifi's list
            sock_outs.write(setCustomBufferWithInts(reqBuffer));

            byte[] textBuffer = new byte[4096];
            String WiFi_infoBuffer = "";

            CharsetDecoder utf8Decoder = Charset.forName("UTF-8").newDecoder();
            utf8Decoder.onMalformedInput(CodingErrorAction.REPLACE);
            utf8Decoder.onUnmappableCharacter(CodingErrorAction.REPLACE);

            File path = context.getFilesDir();
            BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(new File( path.getPath() + WiFiFile) ), StandardCharsets.UTF_8));


            while (sock_ins.read(textBuffer) > 0)
            {
                bw.write( utf8Decoder.decode(ByteBuffer.wrap(textBuffer)).array() );
            }
            bw.close();
            fillWiFiInfo();


            try {
                sock_ins.close();
                sock_outs.close();
                clientTcp.close();
            } catch (Exception e)
            {
                System.out.println("Exception while closing sockets: " + e );
            }
            System.out.println("Connection closed!");
        }
        catch  (Exception e)
        {
            System.out.println("While getting wifi info: " + e );
            RESULT = false;
        }



        if ( UserId < 0 )
            RESULT = false;
        return RESULT;

    }

    @Override
    public void run() {

    }
}

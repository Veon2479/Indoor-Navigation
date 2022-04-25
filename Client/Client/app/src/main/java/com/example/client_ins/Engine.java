package com.example.client_ins;

import static com.example.client_ins.Tools.*;
import android.content.Context;
import android.os.Build;
import android.os.StrictMode;
import android.widget.TextView;
import androidx.annotation.RequiresApi;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.*;
import java.time.Instant;

public class Engine {

    public int UserId = 0;
    public double Crd1, Crd2;
    public double accX, accY;
    public boolean isAlive = false;

    private static Engine instance;
    public static Context context;

    public ClientMath clientMath;
    public Thread mathThread;
    public  DataSender dataSender;

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



            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);

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
        boolean flag = false;
        int i = 0;
        while ( i < AttemptsToRegistrate && !flag ) {
            System.out.println("Trying to registrate");
            flag = Registrate();
            if (!flag)
                UserId = 0;
            i++;
        }
        isAlive = flag;

        if (isAlive) {
            System.out.println("Registration was successful");


            dataSender.Enable();
      

            //create 2 streams - first to compute coordinates
            //second - to send them
            //but they're don't working yet
        } else {
            System.out.println("Failed to registrate");
        }

        clientMath.Unpause();
    }

    public void stopTracking(){
        clientMath.Pause();
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


            byte[] buffer = setInfoBufferWithLongs( UserId, 0, 0); //TODO: par1 is ID of qr of place


            System.out.println("Sending data");
            sock_outs.write(buffer);
            System.out.println("Receiving data");
            sock_ins.read(buffer);

            long timeStamp = getInfoBuffer( this, buffer );
            System.out.println("Now: "+ Instant.now().getEpochSecond()+", time of sending: "+timeStamp);
            System.out.println( "new ID is "+UserId);
            System.out.println( "new crd1 is "+Crd1);
            System.out.println( "new crd2 is "+Crd2);

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
            RESULT = false;
        }


        if ( UserId < 0 )
            RESULT = false;
        return RESULT;
    }
}

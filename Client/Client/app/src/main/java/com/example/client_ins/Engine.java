package com.example.client_ins;

import static com.example.client_ins.Tools.*;

import android.os.Build;
import android.os.StrictMode;

import androidx.annotation.RequiresApi;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.*;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.time.Instant;



public class Engine {

    public int UserId = 0;
    public double Crd1, Crd2;
    public boolean isAlive = false;

    private Socket clientTcp;

    @RequiresApi(api = Build.VERSION_CODES.O)
    public Engine()
    {
        int i = 0;
        boolean flag = false;
        System.out.println( "Try to registrate user!");


        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        while ( i < AttemptsToRegistrate && ! flag )
        {
            flag = Registrate();
            i++;
        }
        isAlive = flag;

        if ( isAlive )
        {
            DataSender dataSender = new DataSender(this);
            Thread udpSender = new Thread(dataSender);
            udpSender.start();
            //create 2 streams - first to compute coordinates
            //second - to send them
            //but they're don't working yet
        }
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public boolean Registrate() {
        boolean RESULT = true;
        try
        {

            clientTcp = new Socket();
            clientTcp.connect( new InetSocketAddress( serverAddr, serverPortTcp ) );
            //clientTcp = new Socket();
            //clientTcp.connect(new InetSocketAddress( serverAddr, serverPort ), 500 );
            if ( clientTcp.isConnected() )
                System.out.println("Connected!");
            else
                System.out.println("Not connected!");

            InputStream sock_ins = clientTcp.getInputStream();
            OutputStream sock_outs = clientTcp.getOutputStream();

            byte[] buffer = new byte[ ( 32 + 64 * 2 + 64) / 8 ];

            buffer = setInfoBuffer( UserId, 2.34d, 1.32e-10); //TODO: crd1 is ID of place

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
                System.out.println(e);
            }
            System.out.println("Connection closed!");
        }
        catch  (Exception e)
        {
          //  e.printStackTrace();
            System.out.println("While registrate: "+e);

            RESULT = false;
        }


        if ( UserId < 0 )
            RESULT = false;
        return RESULT;
    }
}

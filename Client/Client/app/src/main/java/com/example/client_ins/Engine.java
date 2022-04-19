package com.example.client_ins;

import static com.example.client_ins.Tools.*;

import android.os.Build;
import android.os.StrictMode;
import android.util.Log;

import androidx.annotation.RequiresApi;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.*;
import java.time.Instant;
import java.util.Scanner;


public class Engine {

    public int UserId;
    public double Crd1, Crd2;
    public boolean isAlive = false;

    private Socket clientTcp;

    @RequiresApi(api = Build.VERSION_CODES.O)
    public Engine()
    {
        int i = 0;
        boolean flag = false;
        Log.i("INFO: ", "try to registrate user!");
        System.out.println("AAAAAAAAAAAAAAAAA");

        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        while ( i < AttemptsToRegistrate && ! flag )
        {
            flag = Registrate();
            i++;
        }
        if ( i != AttemptsToRegistrate )
            isAlive = true;
        Log.i("INFO: ", "nCannot registrate user!");

        //create 2 streams - first to compute coordinates
        //second - to send them
        //but they're don't working yet
    }

    public void StartComputing()
    {

    }

    public void StopComputing()
    {

    }


    @RequiresApi(api = Build.VERSION_CODES.O)
    public boolean Registrate() {
        boolean RESULT = true;
        try
        {

            clientTcp = new Socket(serverAddr, serverPort);
            //clientTcp = new Socket();
            //clientTcp.connect(new InetSocketAddress( serverAddr, serverPort ), 500 );
            if ( clientTcp.isConnected() )
                System.out.println("Connected!");
            else
                System.out.println("Not connected!");

            InputStream sock_ins = clientTcp.getInputStream();
            OutputStream sock_outs = clientTcp.getOutputStream();
            byte[] buffer = new byte[ ( 32 + 64 * 2 + 64) / 8 ];

            buffer = setInfoBuffer( UserId, 0, 0); //TODO: crd1 is ID of place

            sock_outs.write(buffer);
            sock_ins.read(buffer);

            long timeStamp = getInfoBuffer( this, buffer );
            Log.i("INFO: ", "Now: "+ Instant.now().getEpochSecond()+", time of sending: "+timeStamp);
            Log.i("INFO: ", "new ID is "+UserId);
            Log.i("INFO: ", "new crd1 is "+Crd1);
            Log.i("INFO: ", "new crd2 is "+Crd2);


        }
        catch  (Exception e)
        {
          //  e.printStackTrace();
            System.out.println("While registrate: "+e);

            RESULT = false;
        }


        try
        {
            if (clientTcp != null)
                clientTcp.close();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }


        if ( UserId < 0 )
            RESULT = false;
        return RESULT;
    }
}

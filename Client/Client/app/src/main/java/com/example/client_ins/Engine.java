package com.example.client_ins;

import static com.example.client_ins.Tools.*;

import android.os.Build;

import androidx.annotation.RequiresApi;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.*;


public class Engine {

    public int UserId;
    public double Crd1, Crd2;
    public boolean isAlive = false;

    private Socket clientTcp;

    @RequiresApi(api = Build.VERSION_CODES.O)
    public Engine()
    {
        //init serverAddr:serverPort some way
        int i = 0;
        while ( i < AttemptsToRegistrate && ! Registrate() )
            i++;
        if ( i != AttemptsToRegistrate )
            isAlive = true;
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
            InputStream sock_ins = clientTcp.getInputStream();
            byte[] buffer = new byte[ ( 32 + 64 * 2 + 64) / 8 ];

            buffer = setInfoBuffer( UserId, 0, 0); //TODO: crd1 is ID of place
            sock_ins.read(buffer);
            OutputStream sock_outs;
            sock_outs = clientTcp.getOutputStream();
            sock_outs.write(buffer);
            long timeStamp = getInfoBuffer( this, buffer );


        }
        catch  (Exception e)
        {
            e.printStackTrace();
            RESULT = false;
        }


        try
        {
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

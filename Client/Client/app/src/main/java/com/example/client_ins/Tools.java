package com.example.client_ins;

import android.os.Build;

import androidx.annotation.RequiresApi;

import java.nio.ByteBuffer;
import java.time.Instant;

public class Tools {


    //these values will be read from file
    public static String serverAddr = "10.42.0.1";
    public static int serverPort = 50001;
    public static int AttemptsToRegistrate = 1;

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static byte[] setInfoBuffer(int userID, double crd1, double crd2)
    {
        byte[] result = new byte[ ( 32 + 64 * 2 + 64) / 8 ];
        ByteBuffer.wrap( result ).putInt( 0, userID );
        ByteBuffer.wrap( result ).putDouble( 4, crd1 );
        ByteBuffer.wrap( result ).putDouble( 12, crd2 );
        ByteBuffer.wrap( result ).putLong( 20, Instant.now().getEpochSecond() );
        return result;
    }

    public static long getInfoBuffer( Engine engine, byte[] buffer )
    {
        engine.UserId = ByteBuffer.wrap( buffer ).getInt( 0 );
        engine.Crd1 = ByteBuffer.wrap( buffer ).getDouble( 4 );
        engine.Crd2 = ByteBuffer.wrap( buffer ).getDouble( 12 );
        return ByteBuffer.wrap( buffer ).getLong( 20 );

    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static void updateInfoBuffer(byte[] buffer, int userID, double crd1, double crd2)
    {
        ByteBuffer.wrap( buffer ).putInt( 0, userID );
        ByteBuffer.wrap( buffer ).putDouble( 4, crd1 );
        ByteBuffer.wrap( buffer ).putDouble( 12, crd2 );
        ByteBuffer.wrap( buffer ).putLong( 20, Instant.now().getEpochSecond() );
    }
}

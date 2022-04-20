package com.example.client_ins;

import android.os.Build;
import android.util.Xml;

import androidx.annotation.RequiresApi;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.time.Instant;

public class Tools {


    //these values will be read from file


    public static String serverAddr = "10.144.52.41";


    public static int serverPortTcp = 4444;
    public static int serverPortUdp = 4445;
    public static int AttemptsToRegistrate = 1;

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static byte[] setInfoBuffer(int userID, double crd1, double crd2)
    {
        byte[] result = new byte[ ( 32 + 64 * 2 + 64) / 8 ];
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 4, crd1 );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 12, crd2 );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 20,  ( Instant.now().getEpochSecond() ) );//.order(ByteOrder.LITTLE_ENDIAN);
        return result;
    }

    public static long getInfoBuffer( Engine engine, byte[] buffer )
    {
        engine.UserId = ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).getInt( 0 );
        engine.Crd1 = ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).getDouble( 4 );
        engine.Crd2 = ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).getDouble( 12 );
        return ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).getLong( 20 );

    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static void updateInfoBuffer(byte[] buffer, int userID, double crd1, double crd2)
    {
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 4, crd1 );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 12, crd2 );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putLong( 20, Instant.now().getEpochSecond() );
    }
}

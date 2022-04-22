package com.example.client_ins;

import android.content.Context;
import android.os.Build;
import androidx.annotation.RequiresApi;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.time.Instant;
import java.util.Scanner;

public class Tools {


    //these values will be read from file

    public static String serverAddr;
    public static int serverPortTcp;
    public static int serverPortUdp;
    public static int AttemptsToRegistrate;
    public static int BufferSize;
    public static int UdpPacketDelay;

    public static void readFromFile(Context context) throws IOException {
        File path = context.getFilesDir();

        File file = new File(path, "configClient.txt");
        if(!file.exists()) {
           // serverAddr = "10.144.157.188";
            serverAddr = "192.168.50.145";
            serverPortTcp = 4444;
            serverPortUdp = 4445;
            AttemptsToRegistrate = 3;
            BufferSize = 28;
            UdpPacketDelay = 1000;
            writeToFile(context);
        }
        else {
            Scanner in = new Scanner(new FileInputStream(file));
            serverAddr = in.nextLine();
            AttemptsToRegistrate = Integer.parseInt(in.nextLine());
            BufferSize = Integer.parseInt(in.nextLine());
            UdpPacketDelay = Integer.parseInt(in.nextLine());
            serverPortTcp = Integer.parseInt(in.nextLine());
            serverPortUdp = Integer.parseInt(in.nextLine());
        }
    }


    public static void writeToFile(Context context) throws IOException {
        File path = context.getFilesDir();

        File file = new File(path, "configClient.txt");
        if(!file.exists()) {
            file.createNewFile();
        }
        OutputStreamWriter fl = new OutputStreamWriter(new FileOutputStream(file));
        fl.write(serverAddr+"\n");
        fl.write(serverPortTcp+"\n");
        fl.write(serverPortUdp+"\n");
        fl.write(AttemptsToRegistrate+"\n");
        fl.write(BufferSize+"\n");
        fl.write(UdpPacketDelay+"\n");
        fl.close();
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static byte[] setInfoBuffer(int userID, double crd1, double crd2)
    {
        byte[] result = new byte[ BufferSize ];
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 4, crd1 );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 12, crd2 );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 20,  ( Instant.now().getEpochSecond() ) );//.order(ByteOrder.LITTLE_ENDIAN);
        return result;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static byte[] setInfoBufferWithLongs(int userID, long par1, long par2) //par1 is QR id
    {
        byte[] result = new byte[ BufferSize ];
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 4, par1 );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 12, par2 );//.order(ByteOrder.LITTLE_ENDIAN);
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

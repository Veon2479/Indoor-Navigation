package com.example.client_ins;

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.IBinder;

import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;

public class ClientService extends Service {

    private Engine engine;

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public int onStartCommand(Intent intent, int flags, int startId){
        engine = Engine.getInstance();

        System.out.println("Background service is starting!");

        DataSender dataSender = new DataSender(engine);
        Thread udpSender = new Thread(dataSender);
        udpSender.start();

        ClientMath clientMath = new ClientMath(engine);
        Thread mathThread = new Thread(clientMath);
        mathThread.start();

        SensorReader sensorReader = new SensorReader(engine, getBaseContext(), clientMath);
        return START_STICKY;
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }
}

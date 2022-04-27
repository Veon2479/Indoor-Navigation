package com.example.client_ins;

import static androidx.core.app.ActivityCompat.requestPermissions;
import static androidx.lifecycle.Lifecycle.State.STARTED;

import android.Manifest;
import android.app.Activity;
import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.location.LocationManager;
import android.os.Build;
import android.os.IBinder;
import android.renderscript.RenderScript;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.core.app.ActivityCompat;
import androidx.core.app.NotificationCompat;
import androidx.core.content.ContextCompat;

public class ClientService extends Service {

    private Engine engine;

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void onCreate() {

        super.onCreate();
        engine = Engine.getInstance();
        // startForeground(101, new Notification() );
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O)
            startMyOwnForeground();
        else
            startForeground(1, new Notification());
    }


    @RequiresApi(api = Build.VERSION_CODES.O)
    private void startMyOwnForeground() {
        String NOTIFICATION_CHANNEL_ID = "com.example.simpleapp";
        String channelName = "My Background Service";
        NotificationChannel chan = new NotificationChannel(NOTIFICATION_CHANNEL_ID, channelName, NotificationManager.IMPORTANCE_NONE);
        chan.setLightColor(Color.BLUE);
        chan.setLockscreenVisibility(Notification.VISIBILITY_PRIVATE);
        NotificationManager manager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        assert manager != null;
        manager.createNotificationChannel(chan);

        NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);
        Notification notification = notificationBuilder.setOngoing(true)
                .setSmallIcon(R.drawable.ic_launcher_foreground)
                .setContentTitle("App is running in background")
                .setPriority(NotificationManager.IMPORTANCE_MIN)
                .setCategory(Notification.CATEGORY_SERVICE)
                .build();
        startForeground(2, notification);
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {

        //  engine.context.startService(Intent(context, MyService::class.java))
        //engine.context.startService(intent);
        //ContextCompat.startForegroundService(engine.context, intent);

        System.out.println("Background service is starting!");

        engine.clientMath = new ClientMath(engine);
        if (engine.mathThread != null)
            engine.mathThread.stop();
        engine.mathThread = new Thread(engine.clientMath);
        engine.mathThread.start();

        SensorReader sensorReader = new SensorReader(engine, getBaseContext(), engine.clientMath);


        engine.dataSender = new DataSender(engine);
        Thread udpSender = new Thread(engine.dataSender);
        udpSender.start();

        engine.wifiModule = new WifiModule(engine.context);
        engine.wifiModule.run();

        //engine.startTracking();
        return START_STICKY;

    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }
}

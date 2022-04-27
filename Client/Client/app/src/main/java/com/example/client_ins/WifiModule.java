package com.example.client_ins;

import android.annotation.SuppressLint;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Build;

import java.util.ArrayList;

public class WifiModule implements Runnable {
    WifiManager wifiManager;
    BroadcastReceiver wifiScanReceiver;
    Context context;

    ArrayList<String> arrWifiNames = new ArrayList<>();
    ArrayList<Integer> arrWifiPowers = new ArrayList<>();

    WifiModule(Context context) {
        this.context = context;
    }
    @Override
    public void run() {
        System.out.println("WiFi module is running..");
        wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
        wifiScanReceiver = new BroadcastReceiver() {
            @SuppressLint("MissingPermission")
            @Override
            public void onReceive(Context context, Intent intent) {
                wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
                int numberOfLevels = 5;
                wifiManager.startScan();
                String s;
                arrWifiPowers.clear();
                for (int j = 0; j < arrWifiNames.size(); j++) {
                    s = arrWifiNames.get(j);
                    for (int i = 0; i < wifiManager.getScanResults().size(); i++) {
                        if(s.equals(wifiManager.getScanResults().get(i).SSID)) arrWifiPowers.add(Math.abs(wifiManager.getScanResults().get(i).level));
                        else System.out.println(wifiManager.getScanResults().get(i).SSID);
                    }

                }
            }
        };
        wifiScanReceiver.onReceive(context,new Intent(context, MainActivity.class));
    }
}

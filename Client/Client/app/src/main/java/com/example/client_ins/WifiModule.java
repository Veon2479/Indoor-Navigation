package com.example.client_ins;

import static java.lang.Math.abs;

import android.annotation.SuppressLint;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Build;

import androidx.annotation.RequiresApi;

import java.util.ArrayList;

@RequiresApi(api = Build.VERSION_CODES.O)
public class WifiModule implements Runnable {
    WifiManager wifiManager;
    BroadcastReceiver wifiScanReceiver;
    Context context;

    ArrayList<String> arrWifiNames = new ArrayList<>();
    ArrayList<Integer> arrWifiPowers = new ArrayList<>();
    Engine engine = Engine.getInstance();

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
              //  int numberOfLevels = 5;
                wifiManager.startScan();
                String SSID = "", BSSID = "";
                double frequency = 0;
                double distance = 0;
                double exp = 0;
                double level = 0;
                arrWifiPowers.clear();
                for (int i = 0; i < engine.WiFi_List.size(); i++)
                {
                    engine.WiFi_List.get(i).setLevel(0);
                }
                for (int j = 0; j < wifiManager.getScanResults().size(); j++) {

                    SSID = wifiManager.getScanResults().get(j).SSID;
                    BSSID = wifiManager.getScanResults().get(j).BSSID;
                    frequency = wifiManager.getScanResults().get(j).frequency;
                    level = wifiManager.getScanResults().get(j).level;

                    System.out.println("Analyzing WiFi " + SSID + " with MAC " + BSSID);
                    System.out.println("Level is " + level);
                    for (int i = 0; i <  engine.WiFi_List.size() ; i++) {
                        if ( SSID.equals( engine.WiFi_List.get(i).getSSID() ) )
                        {

                            //if ( BSSID.equals( engine.WiFi_List.get(i).getBSSID() ) )
                            engine.WiFi_List.get(i).setLevel( abs( wifiManager.getScanResults().get(j).level ) );

                            exp =  (27.55 - (20.0 * Math.log10 (frequency)) + Math.abs( level ))/20.0;  //first 20.0 is spot power
                            distance = Math.pow(10.0, exp);
                            engine.WiFi_List.get(i).setDistance( distance );

                            System.out.println("WiFi "+ SSID + " matches");
                            System.out.println("Distance is " + distance );
                        }
                    }


                }
            }
        };
        wifiScanReceiver.onReceive(context,new Intent(context, MainActivity.class));
    }
}

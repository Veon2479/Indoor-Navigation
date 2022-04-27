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
import java.util.List;

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

                            exp =  (27.55 - (20.0 * Math.log10 (frequency)) + abs( level ))/20.0;  //first 20.0 is spot power
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

        //detect user position
        List<WiFi> Wifi_Active = new ArrayList<>();
        for (WiFi wiFi : engine.WiFi_List) {
            if(abs(wiFi.getLevel())>0.01) {
                Wifi_Active.add(wiFi);
            }
        }
        if(Wifi_Active.size()<2) engine.isWiFiDetermined = false;
        else {
            double x0 = Wifi_Active.get(0).getX();
            double x1 = Wifi_Active.get(1).getX();
            double y0 = Wifi_Active.get(0).getY();
            double y1 = Wifi_Active.get(1).getY();

            double distX = Math.abs(x1-x0);
            double distY = Math.abs(y1-y0);
            double d = Math.sqrt(distX*distX+distY*distY);
            double r0 = Wifi_Active.get(0).getDistance();
            double r1 = Wifi_Active.get(1).getDistance();
            if(d>r0+r1) {
                engine.isWiFiDetermined = false;
            }
            else {
                double b=(r1*r1-r0*r0+d*d)/(2*d);
                double a=d-b;
                double h=Math.sqrt(r1*r1-b*b);
                double x=x0+(x1-x0)/(d/a);
                double y=y0+(y1-y0)/(d/a);
                double x3=x-(y-y1)*h/b;
                double y3=y+(x-x1)*h/b;
                double x4=x+(y-y1)*h/b; //это если чё вторые координаты, на случай, если круг пересекается в нескольких точках
                double y4=y-(x-x1)*h/b; //это если чё вторые координаты, на случай, если круг пересекается в нескольких точках
                engine.isWiFiDetermined = true;
                engine.WiFiAccuracy = h;
                if(Wifi_Active.size()==2) {
                    engine.WiFiCrd1 = x;
                    engine.WiFiCrd2 = y;
                }
                else {
                    //берём данные с третьего источника
                    double xDop = Wifi_Active.get(2).getX();
                    double yDop = Wifi_Active.get(2).getY();
                    double rDop = Wifi_Active.get(2).getDistance();
                    //находим точку, которая лучше всего подходит к радиусу третьего источника
                    if(Math.abs( rDop - Math.sqrt( (xDop-x3)*(xDop-x3) + (yDop-y3)*(yDop-y3) )) < Math.abs( rDop - Math.sqrt( (xDop-x4)*(xDop-x4) + (yDop-y4)*(yDop-y4) )) ) {
                        engine.WiFiCrd1 = x3;
                        engine.WiFiCrd2 = y3;
                    }
                    else {
                        engine.WiFiCrd1 = x4;
                        engine.WiFiCrd2 = y4;
                    }
                }

            }
        }

    }
}

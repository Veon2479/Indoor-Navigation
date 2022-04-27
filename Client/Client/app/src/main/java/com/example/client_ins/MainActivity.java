package com.example.client_ins;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

import android.Manifest;
import android.annotation.SuppressLint;
import android.content.BroadcastReceiver;

import android.Manifest;
import android.app.Activity;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiConfiguration;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.net.wifi.WifiNetworkSuggestion;
import android.os.Build;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.StrictMode;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import static com.example.client_ins.Tools.*;



import java.util.Arrays;
import java.util.Iterator;
import java.util.List;


public class MainActivity extends AppCompatActivity {

    public static int REQUEST_FINE_LOCATION = 1;
    public static int REQUEST_BACKGROUND_LOCATION = 2;
    public static int REQUEST_COARSE_LOCATION = 3;

    TextView text1;
    TextView text2;
    TextView text3;

  
    Button buttonStart;
    Button buttonStop;
    Button buttonQR;

    EditText editTextQrID;
    EditText editTextServerAddr;

    Engine engine;


    double x=0, y=0, z=0; //correct when it possible

    double accX = 0, accY = 0, accZ = 0;
    double angleX = 0, angleY = 0, angleZ = 0;
    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        ActivityCompat.requestPermissions(this,
                new String[]{Manifest.permission.ACCESS_BACKGROUND_LOCATION},
                REQUEST_BACKGROUND_LOCATION);

        ActivityCompat.requestPermissions(this,
                new String[]{Manifest.permission.ACCESS_COARSE_LOCATION},
                REQUEST_COARSE_LOCATION);

        ActivityCompat.requestPermissions(this,
                new String[]{Manifest.permission.ACCESS_FINE_LOCATION},
                REQUEST_FINE_LOCATION);

        super.onCreate(savedInstanceState);
        Bundle arguments = getIntent().getExtras();
        setContentView(R.layout.activity_main);

        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        Engine.setContext(getApplicationContext());
        Engine engine = Engine.getInstance();
        Thread engineThread = new Thread(engine);
        engineThread.start();


        System.out.println("Calling background service to start");
        Intent intent = new Intent(this, ClientService.class);
        Context context = getApplicationContext();
        context.startForegroundService( intent );


        text1 = findViewById(R.id.text1);
        text2 = findViewById(R.id.text2);
        text3 = findViewById(R.id.text3);


        //WifiModule wifi = new WifiModule(getApplicationContext());



        editTextQrID = findViewById(R.id.editTextTextPersonName1);
        editTextServerAddr = findViewById(R.id.editTextTextPersonName2);
      
        if(arguments != null) {
            String qrcode = arguments.getString("1");
            System.out.println(qrcode);
            String[] helpStr = qrcode.split("[\n\\t]");
            System.out.println("Nice "+helpStr.length);
            try {
                Tools.serverAddr = helpStr[0];
                Tools.serverPortTcp = Integer.parseInt(helpStr[1]);
                Tools.serverPortUdp = Integer.parseInt(helpStr[2]);
                engine.Azimuth = Double.parseDouble(helpStr[3]);
                engine.QrId = Integer.parseInt(helpStr[4]);
                engine.ReceivedCrd1 = Double.parseDouble(helpStr[5]);
                engine.ReceivedCrd2 = Double.parseDouble(helpStr[6]);

            }
            catch (Exception ex) {
                Tools.serverAddr = "10.144.52.41";
                Tools.serverPortTcp = 4444;
                Tools.serverPortUdp = 4445;
                engine.Azimuth = 0;
                engine.QrId = 0;
                engine.ReceivedCrd1 = 0;
                engine.ReceivedCrd2 = 0;
            }
            finally {
                editTextQrID.setText( Integer.toString(engine.QrId));
                editTextServerAddr.setText( serverAddr );
            }
        }
        //editText1.getText(); //взять текст из первой строки
        //editText2.getText(); //взять текст из второй строки


        x = engine.ReceivedCrd1;
        y = engine.ReceivedCrd2;


        editTextQrID.setText("0");
        editTextServerAddr.setText(serverAddr);

        //editText1.getText(); //взять текст из первой строки
        //editText2.getText(); //взять текст из второй строки

        buttonStart = findViewById(R.id.button);
        buttonStart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //wifi.arrWifiNames.add(String.valueOf(editText1.getText()));
                //wifi.run();
                //context.startForegroundService( intent );
                buttonStart.setEnabled(false);
                engine.QrId = Integer.parseInt( editTextQrID.getText().toString() );
                serverAddr = editTextServerAddr.getText().toString();
                try {
                    writeToFile(getApplicationContext());
                } catch (Exception e) {
                    System.out.println("Failed to change settings file: " + e );
                }
                engine.startTracking();
                if (engine.isBLocked)
                    buttonStop.setEnabled(true);
            }
        });

        buttonStop = findViewById(R.id.button3);
        buttonStop.setEnabled(false);
        buttonStop.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                buttonStop.setEnabled(false);
                engine.stopTracking();
                //add method for second button
                buttonStart.setEnabled(true);
               
            }
        });

        buttonQR = findViewById(R.id.button2);
        buttonQR.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //System.out.println(wifi.arrWifiPowers.size());
                //add method for third button
                Intent intent = new Intent(getApplicationContext(), MainActivity2.class);
                startActivity(intent);
            }
        });
//
//        text1.setText("Coordinates\nX: "+x+"\nY: "+y+"\nZ: "+z);
//        text2.setText("Rotation\nX: "+angleX+"\nY: "+angleY+"\nZ: "+angleZ);
//        text3.setText( "Accelerometer\nX: "+accX+"\nY: "+accY+"\nZ: "+accZ);

        //Чтобы добавлять логи, просто textScroll.append("nessesary info"+"\n");

        CountDownTimer countDownTimer = new CountDownTimer(2000, 2000) {
            @Override
            public void onTick(long l) {

            }

            @SuppressLint("DefaultLocale")
            @Override
            public void onFinish() {
                x = engine.Crd1;
                y = engine.Crd2;
                accX = engine.accX;
                accY = engine.accY;
                if(engine.clientMath != null){
                    text1.setText(String.format("Coordinates\nX: %.2f\nVX: %.2f\nAX: %.2f\nY: %.2f\nVY: %.2f\nAY: %.2f",
                            engine.clientMath.currX.matrix[0][0], engine.clientMath.currX.matrix[1][0],
                            engine.clientMath.currX.matrix[2][0], engine.clientMath.currX.matrix[3][0],
                            engine.clientMath.currX.matrix[4][0], engine.clientMath.currX.matrix[5][0]));
                    text2.setText(String.format("P\npX: %.2f\npVX: %.2f\npAX: %.2f\npY: %.2f\npVY: %.2f\npAY: %.2f",
                            engine.clientMath.P.matrix[0][0], engine.clientMath.P.matrix[1][1],
                            engine.clientMath.P.matrix[2][2], engine.clientMath.P.matrix[3][3],
                            engine.clientMath.P.matrix[4][4], engine.clientMath.P.matrix[5][5]));
                    text3.setText( String.format("Accelerometer\nX: %.2f\nY: %.2f\nZ: %.2f\n" +
                            "GPS:\nLong: %.4f\nLat: %.4f\n"+
                            "MeasCoord:\nX:%.2f\nY: %.2f", accX, accY, accZ, engine.clientMath.Longitude, engine.clientMath.Latitude
                            , engine.clientMath.z.matrix[0][0], engine.clientMath.z.matrix[1][0]));
                }

          /*      text1.setText(String.format("Coordinates\nX: %.2f\nY: %.2f\nZ: %.2f", x, y, z));
                text3.setText( String.format("Accelerometer\nX: %.2f\nY: %.2f\nZ: %.2f", accX, accY, accZ));*/

                if ( engine.isBLocked == false )
                    buttonStart.setEnabled(true);
                this.start();
            }
        };

        countDownTimer.start();
    }

    @Override
    public void onRequestPermissionsResult(
            int requestCode, String[] permissions, int[] grantResults) {

        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        boolean granted = true;
        if (grantResults.length > 0) {
            for (int result : grantResults) {
                if (result != PackageManager.PERMISSION_GRANTED) {
                    granted = false;
                }
            }
        } else {
            granted = false;
        }
    }

}
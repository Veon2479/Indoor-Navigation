package com.example.client_ins;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

import android.Manifest;
import android.annotation.SuppressLint;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiConfiguration;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.net.wifi.WifiNetworkSuggestion;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.budiyev.android.codescanner.CodeScanner;
import com.budiyev.android.codescanner.CodeScannerView;
import com.budiyev.android.codescanner.DecodeCallback;
import com.google.zxing.Result;

import java.util.List;


public class MainActivity extends AppCompatActivity {

    TextView text1;
    TextView text2;
    TextView text3;
    public static TextView textScroll;

    Button button1;
    Button button2;
    Button button3;

    EditText editText1;
    EditText editText2;


    double x=0,y=0,z=0; //correct when it possible
    double accX = 0, accY = 0, accZ = 0;
    double angleX = 0, angleY = 0, angleZ = 0;
    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Bundle arguments = getIntent().getExtras();
        setContentView(R.layout.activity_main);
        text1 = findViewById(R.id.text1);
        text2 = findViewById(R.id.text2);
        text3 = findViewById(R.id.text3);
        textScroll = findViewById(R.id.textScroll);
        textScroll.setText("\rLog started!\r\n");

        Engine engine = new Engine(getApplicationContext());
        WifiModule wifi = new WifiModule(getApplicationContext());

        editText1 = findViewById(R.id.editTextTextPersonName1);
        editText2 = findViewById(R.id.editTextTextPersonName2);
        if(arguments!=null) {
            String qrcode = arguments.getString("1");
            editText1.setText(qrcode);
        }
        //editText1.getText(); //взять текст из первой строки
        //editText2.getText(); //взять текст из второй строки


        x = engine.Crd1;
        y = engine.Crd2;

        text1.setText("Coordinates\nX: "+x+"\nY: "+y+"\nZ: "+z);
        text2.setText("Rotation\nX: "+angleX+"\nY: "+angleY+"\nZ: "+angleZ);
        text3.setText( "Accelerometer\nX: "+accX+"\nY: "+accY+"\nZ: "+accZ);
        MainActivity.textScroll.append("Log ended!"+"\n");

        button1 = findViewById(R.id.button);
        button1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                wifi.arrWifiNames.add(String.valueOf(editText1.getText()));
                wifi.run();
            }
        });

        button2 = findViewById(R.id.button2);
        button2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //add method for second button
                Intent intent = new Intent(getApplicationContext(), MainActivity2.class);
                startActivity(intent);
            }
        });

        button3 = findViewById(R.id.button3);
        button3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                System.out.println(wifi.arrWifiPowers.size());
            }
        });


        //Чтобы добавлять логи, просто textScroll.append("nessesary info"+"\n");
    }

}
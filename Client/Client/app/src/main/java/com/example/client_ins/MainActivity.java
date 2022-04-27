package com.example.client_ins;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

import android.Manifest;
import android.app.Activity;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import static com.example.client_ins.Tools.*;


public class MainActivity extends AppCompatActivity {

    public static int REQUEST_FINE_LOCATION = 1;
    public static int REQUEST_BACKGROUND_LOCATION = 2;
    public static int REQUEST_COARSE_LOCATION = 3;

    TextView text1;
    TextView text2;
    TextView text3;
    public static TextView textScroll;

    Button buttonStart;
    Button buttonStop;

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
        setContentView(R.layout.activity_main);

        Engine.setContext(getApplicationContext());
        Engine engine = Engine.getInstance();

        System.out.println("Calling background service to start");
        //ClientService clientService = new ClientService(engine);
        Intent intent = new Intent(this, ClientService.class);
        Context context = getApplicationContext();
        context.startForegroundService( intent );


        text1 = findViewById(R.id.text1);
        text2 = findViewById(R.id.text2);
        text3 = findViewById(R.id.text3);
        textScroll = findViewById(R.id.textScroll);
      
        textScroll.setText("\rLog started!\r\n");
  
        editTextQrID = findViewById(R.id.editTextTextPersonName1);
        editTextServerAddr = findViewById(R.id.editTextTextPersonName2);

        editTextQrID.setText("0");
        editTextServerAddr.setText(serverAddr);

        //editText1.getText(); //взять текст из первой строки
        //editText2.getText(); //взять текст из второй строки

        buttonStart = findViewById(R.id.button);
        buttonStart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //context.startForegroundService( intent );
                serverAddr = editTextServerAddr.getText().toString();
                try {
                    writeToFile(getApplicationContext());
                } catch (Exception e) {
                    System.out.println("Failed to change settings file: " + e );
                }
                engine.startTracking();

            }
        });

        buttonStop = findViewById(R.id.button2);
        buttonStop.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                engine.stopTracking();
                //add method for second button
            }
        });

        x = engine.Crd1;
        y = engine.Crd2;


        text1.setText("Coordinates\nX: "+x+"\nY: "+y+"\nZ: "+z);
        text2.setText("Rotation\nX: "+angleX+"\nY: "+angleY+"\nZ: "+angleZ);
        text3.setText( "Accelerometer\nX: "+accX+"\nY: "+accY+"\nZ: "+accZ);
        MainActivity.textScroll.append("Log ended!"+"\n");

        //Чтобы добавлять логи, просто textScroll.append("nessesary info"+"\n");

        CountDownTimer countDownTimer = new CountDownTimer(2000, 2000) {
            @Override
            public void onTick(long l) {

            }

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
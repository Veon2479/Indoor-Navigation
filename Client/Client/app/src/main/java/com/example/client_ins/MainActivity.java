package com.example.client_ins;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import static com.example.client_ins.Tools.*;


public class MainActivity extends AppCompatActivity {

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

    }
}
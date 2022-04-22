package com.example.client_ins;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    TextView text1;
    TextView text2;
    TextView text3;
    TextView textScroll;

    Button button1;
    Button button2;

    EditText editText1;
    EditText editText2;

    double x=0,y=0,z=0; //correct when it possible
    double accX = 0, accY = 0, accZ = 0;
    double angleX = 0, angleY = 0, angleZ = 0;
    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Engine engine = new Engine(getApplicationContext());
        text1 = findViewById(R.id.text1);
        text2 = findViewById(R.id.text2);
        text3 = findViewById(R.id.text3);
        textScroll = findViewById(R.id.textScroll);

        editText1 = findViewById(R.id.editTextTextPersonName1);
        editText2 = findViewById(R.id.editTextTextPersonName2);

        //editText1.getText(); //взять текст из первой строки
        //editText2.getText(); //взять текст из второй строки

        button1 = findViewById(R.id.button);
        button1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //add method for first button
            }
        });

        button2 = findViewById(R.id.button2);
        button2.setOnClickListener(new View.OnClickListener() {
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

        textScroll.setText("Yes1\nYes2\nYes3\nYes4\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\n");
        //Чтобы добавлять логи, просто textScroll.append("nessesary info"+"\n");
    }
}
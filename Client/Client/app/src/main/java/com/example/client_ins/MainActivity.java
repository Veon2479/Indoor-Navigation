package com.example.client_ins;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Build;
import android.os.Bundle;
import android.widget.ScrollView;
import android.widget.TextView;

import java.time.Duration;
import com.example.client_ins.Engine.*;

public class MainActivity extends AppCompatActivity {

    TextView text1;
    TextView text2;
    TextView text3;
    double x=0,y=0,z=0; //correct when it possible
    double accX = 0, accY = 0, accZ = 0;
    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        //new Engine(getApplicationContext());
        text1 = findViewById(R.id.text1);
        text2 = findViewById(R.id.text2);
        text3 = findViewById(R.id.text3);
        text1.setText("Rotation\nX: "+x+"\nY: "+y+"\nZ: "+z);
        text2.setText( "Accelerometer\nX: "+accX+"\nY: "+accY+"\nZ: "+accZ);
        text3.setText("Yes1\nYes2\nYes3\nYes4\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\n");

    }
}
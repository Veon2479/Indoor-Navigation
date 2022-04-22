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
    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        //new Engine(getApplicationContext());
        text3 = findViewById(R.id.text3);
        text3.setText("Yes1\nYes2\nYes3\nYes4\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\nYes5\n");

    }
}
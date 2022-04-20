package com.example.client_ins;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.content.Context.*;

public class SensorReader implements Runnable{
    Context mContext;
    Engine mainEngine;
    ClientMath math;
    private boolean isActive;

    private SensorManager sensorManager;
    private Sensor sensorLinearAcceleration;
    private Sensor sensorRotation;

    public SensorReader(Engine engine, Context mContext, ClientMath math){
        this.mainEngine = engine;
        this.isActive = true;
        this.mContext = mContext;
        this.math = math;

        sensorManager = (SensorManager) mContext.getSystemService(Context.SENSOR_SERVICE);
        sensorRotation = sensorManager.getDefaultSensor(Sensor.TYPE_ROTATION_VECTOR);
        sensorLinearAcceleration = sensorManager.getDefaultSensor(Sensor.TYPE_LINEAR_ACCELERATION);

        SensorEventListener sensorListener = new SensorEventListener() {
            @Override
            public void onSensorChanged(SensorEvent sensorEvent) {
                switch(sensorEvent.sensor.getType())
                {
                    case Sensor.TYPE_ROTATION_VECTOR:
                        math.rotVecX = sensorEvent.values[0];
                        math.rotVecY = sensorEvent.values[1];
                        math.rotVecZ = sensorEvent.values[2];
                        break;

                    case Sensor.TYPE_LINEAR_ACCELERATION:
                        math.linAccX = sensorEvent.values[0];
                        math.linAccY = sensorEvent.values[1];
                        math.linAccZ = sensorEvent.values[2];

                }
            }

            @Override
            public void onAccuracyChanged(Sensor sensor, int i) {

            }
        };

        sensorManager.registerListener(sensorListener, sensorRotation, SensorManager.SENSOR_DELAY_FASTEST);
        sensorManager.registerListener(sensorListener, sensorLinearAcceleration, SensorManager.SENSOR_DELAY_FASTEST);
    }

    public void Disable(){
        isActive = false;
    }

    public void run()
    {
        while (isActive)
        {

        }
    }
}

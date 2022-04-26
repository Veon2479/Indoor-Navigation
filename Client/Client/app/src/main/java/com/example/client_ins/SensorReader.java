package com.example.client_ins;

import static android.content.Context.LOCATION_SERVICE;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.content.Context.*;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;

import androidx.annotation.NonNull;

public class SensorReader{
    Context mContext;
    Engine mainEngine;
    ClientMath math;

    private SensorManager sensorManager;
    private Sensor sensorLinearAcceleration;
    private Sensor sensorRotation;

    private LocationManager locationManager;

    public SensorReader(Engine engine, Context mContext, ClientMath math){
        this.mainEngine = engine;
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
                        math.rotQuat.x = sensorEvent.values[0];
                        math.rotQuat.y = sensorEvent.values[1];
                        math.rotQuat.z = sensorEvent.values[2];
                        math.rotQuat.w = sensorEvent.values[3];
                        break;

                    case Sensor.TYPE_LINEAR_ACCELERATION:
                        math.linAccQuat.x = sensorEvent.values[0];
                        math.linAccQuat.y = sensorEvent.values[1];
                        math.linAccQuat.z = sensorEvent.values[2];
                        math.fPhysics = true;
                    math.UpdateGlobalAcc();
                }
            }

            @Override
            public void onAccuracyChanged(Sensor sensor, int i) {

            }
        };

        sensorManager.registerListener(sensorListener, sensorRotation, SensorManager.SENSOR_DELAY_FASTEST);
        sensorManager.registerListener(sensorListener, sensorLinearAcceleration, SensorManager.SENSOR_DELAY_FASTEST);

        locationManager = (LocationManager) mContext.getSystemService(LOCATION_SERVICE);
    }

    private LocationListener locationListener = new LocationListener() {
        @Override
        public void onLocationChanged(@NonNull Location location) {

        }
    };
}

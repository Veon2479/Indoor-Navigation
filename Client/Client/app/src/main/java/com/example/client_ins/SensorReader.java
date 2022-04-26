package com.example.client_ins;

import static android.content.Context.LOCATION_SERVICE;

import android.Manifest;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.content.Context.*;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;

import androidx.annotation.NonNull;
import androidx.core.app.ActivityCompat;

public class SensorReader {
    Context mContext;
    Engine mainEngine;
    ClientMath math;

    private SensorManager sensorManager;
    private Sensor sensorLinearAcceleration;
    private Sensor sensorRotation;

    public LocationManager locationManager;

    public LocationListener locationListener = new LocationListener() {
        @Override
        public void onLocationChanged(@NonNull Location location) {
            math.Longitude = location.getLongitude();
            math.Latitude = location.getLatitude();
            math.itudeAccur = location.getAccuracy();
            math.CorrectCoordinates();
        }
    };

    public LocationListener initLocationListener = new LocationListener() {
        @Override
        public void onLocationChanged(@NonNull Location location) {
            math.initLongitude = location.getLongitude();
            math.initLatitude = location.getLatitude();
            locationManager.removeUpdates(this);
            math.CorrectInitCoordinates();
        }
    };

    public SensorReader(Engine engine, Context mContext, ClientMath math) {
        this.mainEngine = engine;
        this.mContext = mContext;
        this.math = math;

        sensorManager = (SensorManager) mContext.getSystemService(Context.SENSOR_SERVICE);
        sensorRotation = sensorManager.getDefaultSensor(Sensor.TYPE_ROTATION_VECTOR);
        sensorLinearAcceleration = sensorManager.getDefaultSensor(Sensor.TYPE_LINEAR_ACCELERATION);

        SensorEventListener sensorListener = new SensorEventListener() {
            @Override
            public void onSensorChanged(SensorEvent sensorEvent) {
                switch (sensorEvent.sensor.getType()) {
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

        if (ActivityCompat.checkSelfPermission(mContext, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(mContext, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }

        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER,
                1, 0.1f, initLocationListener);

        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER,
                1, 0.1f, locationListener);
    }

}

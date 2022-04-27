package com.example.client_ins;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.location.Location;
import android.os.Build;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.core.app.ActivityCompat;

import com.google.android.gms.location.LocationRequest;

import com.google.android.gms.location.FusedLocationProviderClient;
import com.google.android.gms.location.LocationCallback;
import com.google.android.gms.location.LocationResult;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.tasks.OnSuccessListener;

public class SensorReader {
    Context mContext;
    Engine mainEngine;
    ClientMath math;

    private SensorManager sensorManager;
    private Sensor sensorLinearAcceleration;
    private Sensor sensorRotation;

    private FusedLocationProviderClient fusedLocationProviderClient;

    private LocationRequest locationRequest;

    private LocationCallback locationCallback;

    @RequiresApi(api = Build.VERSION_CODES.P)
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

        locationRequest = new LocationRequest();
        locationRequest.setInterval(1000);
        locationRequest.setFastestInterval(100);
        locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);

        locationCallback = new LocationCallback() {
            @Override
            public void onLocationResult(@NonNull LocationResult locationResult) {
                super.onLocationResult(locationResult);

                Location location = locationResult.getLastLocation();

                math.Latitude = location.getLatitude();
                math.Longitude = location.getLongitude();
                if(location.hasAccuracy())
                    math.itudeAccur = location.getAccuracy();
                math.CorrectCoordinates();
            }
        };

        fusedLocationProviderClient = LocationServices.getFusedLocationProviderClient(mContext);

        if (ActivityCompat.checkSelfPermission(mContext, Manifest.permission.ACCESS_BACKGROUND_LOCATION) != PackageManager.PERMISSION_GRANTED
        && ActivityCompat.checkSelfPermission(mContext, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED
        && ActivityCompat.checkSelfPermission(mContext, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {

        }else{
            fusedLocationProviderClient.getLastLocation().addOnSuccessListener(mContext.getMainExecutor(), new OnSuccessListener<Location>() {
                @Override
                public void onSuccess(Location location) {
                    if(location != null){
                        math.initLongitude = location.getLongitude();
                        math.initLatitude = location.getLatitude();
                        math.Latitude = location.getLatitude();
                        math.Longitude = location.getLongitude();
                        math.CorrectInitCoordinates();
                        math.CorrectCoordinates();
                    }
                }
            });

                fusedLocationProviderClient.requestLocationUpdates(locationRequest, locationCallback, null);
        }
      //  fusedLocationProviderClient.requestLocationUpdates(locationRequest, locationCallback, null);
    }
}

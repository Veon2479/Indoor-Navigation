package com.example.client_ins;

import static java.lang.Math.sqrt;

public class ClientMath implements Runnable{

    Engine mainEngine;

    public double rotVecX;
    public double rotVecY;
    public double rotVecZ;

    public double linAccX;
    public double linAccY;
    public double linAccZ;

    private double angleSin;
    private double rotX;
    private double rotY;
    private double rotZ;

    private double accX;
    private double accY;
    private double accZ;

    public void updateGlobalAcc()
    {
        angleSin = sqrt(rotVecX*rotVecX + rotVecY*rotVecY + rotVecZ*rotVecZ);
        rotX = rotVecX/angleSin;
        rotY = rotVecY/angleSin;
        rotZ = rotVecZ/angleSin;

        //accX = rotX * linAccY + sqrt(1 - rotX * rotX);
        //accY = rotY * linAccY;
        //accZ = rotZ * linAccY;
    }

    public void Math(Engine engine)
    {
        this.mainEngine = engine;
    }

    public void run()
    {

    }
}

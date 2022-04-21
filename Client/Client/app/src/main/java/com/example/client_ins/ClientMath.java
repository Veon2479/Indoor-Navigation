package com.example.client_ins;

import static java.lang.Math.sqrt;

class Quaternion{
    public double x;
    public double y;
    public double z;
    public double w;

    public void Normalize(){
        double l = Math.sqrt(x*x + y*y + z*z + w*w);
        x /= l;
        y /= l;
        z /= l;
        w /= l;
    }

    public void Mul(Quaternion q){
        double tx = w*q.x + x*q.w + y*q.z - z*q.y;
        double ty = w*q.y - x*q.z + y*q.w + z*q.x;
        double tz = w*q.z + x*q.y - y*q.x + z*q.w;
        double tw = w*q.w - x*q.x - y*q.y - z*q.z;

        x = tx;
        y = ty;
        z = tz;
        w = tw;
    }

    public void MulWithInverted(Quaternion q){
        double tx = - w*q.x + x*q.w - y*q.z + z*q.y;
        double ty = - w*q.y + x*q.z + y*q.w - z*q.x;
        double tz = - w*q.z - x*q.y + y*q.x + z*q.w;
        double tw = w*q.w + x*q.x + y*q.y + z*q.z;

        x = tx;
        y = ty;
        z = tz;
        w = tw;
    }

    public void MulVec(Quaternion q){
        double tx = w*q.x + y*q.z - z*q.y;
        double ty = w*q.y - x*q.z + z*q.x;
        double tz = w*q.z + x*q.y - y*q.x;
        double tw = - x*q.x - y*q.y - z*q.z;

        x = tx;
        y = ty;
        z = tz;
        w = tw;
    }

    public void Invert(){
        x = -x;
        y = -y;
        z = -z;
    }
}

public class ClientMath implements Runnable{

    boolean isActive;
    Engine mainEngine;

    public Quaternion tempQuat = new Quaternion();
    public Quaternion rotQuat = new Quaternion();

    public Quaternion linAccQuat = new Quaternion();

    private double accX = 0;
    private double accY = 0;
    private double accZ = 0;

    private double velX = 0;
    private double velY = 0;
    private double velZ = 0;

    private double z = 0;

    private double time;
    private double deltaTime;

    public void UpdateGlobalAcc()
    {
        tempQuat.x = rotQuat.x;
        tempQuat.y = rotQuat.y;
        tempQuat.w = rotQuat.z;
        tempQuat.z = rotQuat.w;

        tempQuat.Invert();
        tempQuat.MulVec(linAccQuat);
        tempQuat.Mul(rotQuat);
        //tempQuat.MulWithInverted(rotQuat);

        accX = linAccQuat.x;
        accY = linAccQuat.y;
        accZ = linAccQuat.z;
    }

    void PhysicsProc(){
        mainEngine.Crd1 += velX * deltaTime + accX * deltaTime * deltaTime / 2;
        mainEngine.Crd2 += velY * deltaTime + accY * deltaTime * deltaTime / 2;
        z += velZ * deltaTime + accZ * deltaTime * deltaTime / 2;

        velX += accX * deltaTime;
        velY += accY * deltaTime;
        velZ += accZ * deltaTime;
    }

    public void Math(Engine engine)
    {
        this.mainEngine = engine;
        linAccQuat.w = 0;
    }

    public void run()
    {
        time = System.nanoTime();
        while(isActive)
        {
            deltaTime = System.nanoTime() / 1000000000 - time;
            time += deltaTime;
            PhysicsProc();
        }
    }
}

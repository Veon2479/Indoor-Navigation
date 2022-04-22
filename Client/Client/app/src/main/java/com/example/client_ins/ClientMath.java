package com.example.client_ins;

import static java.lang.Math.sqrt;

class Quaternion{
    public double x;
    public double y;
    public double z;
    public double w;

    public double Norm(){
        return x*x + y*y + z*z + w*w;
    }

    public void Normalize(){
        double l = Math.sqrt(x*x + y*y + z*z + w*w);
        this.x /= l;
        this.y /= l;
        this.z /= l;
        this.w /= l;
    }

    public void Mul(Quaternion q){
        double tx = w*q.x + x*q.w + y*q.z - z*q.y;
        double ty = w*q.y - x*q.z + y*q.w + z*q.x;
        double tz = w*q.z + x*q.y - y*q.x + z*q.w;
        double tw = w*q.w - x*q.x - y*q.y - z*q.z;

        this.x = tx;
        this.y = ty;
        this.z = tz;
        this.w = tw;
    }

    public void MulWithInverted(Quaternion q){
        double l = q.Norm();

        double tx = (- w*q.x + x*q.w - y*q.z + z*q.y) / l;
        double ty = (- w*q.y + x*q.z + y*q.w - z*q.x) / l;
        double tz = (- w*q.z - x*q.y + y*q.x + z*q.w) / l;
        double tw = (w*q.w + x*q.x + y*q.y + z*q.z) / l;

        this.x = tx;
        this.y = ty;
        this.z = tz;
        this.w = tw;
    }

    public void MulVec(Quaternion q){
        double tx = w*q.x + y*q.z - z*q.y;
        double ty = w*q.y - x*q.z + z*q.x;
        double tz = w*q.z + x*q.y - y*q.x;
        double tw = - x*q.x - y*q.y - z*q.z;

        this.x = tx;
        this.y = ty;
        this.z = tz;
        this.w = tw;
    }

    public void Invert(){
        double l = Norm();

        this.x = -x / l;
        this.y = -y / l;
        this.z = -z / l;
        this.w /= l;
    }
}

public class ClientMath implements Runnable{

    boolean isActive = true;
    public boolean fPhysics = true;
    Engine mainEngine;

    public ClientMath(Engine engine) {
        this.mainEngine = engine;
        linAccQuat.w = 0;
    }

    public Quaternion tempQuat = new Quaternion();
    public Quaternion rotQuat = new Quaternion();

    public Quaternion linAccQuat = new Quaternion();

    public double accX = 0;
    public double accY = 0;
    public double accZ = 0;

    public double velX = 0;
    public double velY = 0;
    public double velZ = 0;

    private double z = 0;

    private double time;
    private double deltaTime;

    public void UpdateGlobalAcc()
    {
        tempQuat.x = rotQuat.x;
        tempQuat.y = rotQuat.y;
        tempQuat.z = rotQuat.z;
        tempQuat.w = rotQuat.w;

        //tempQuat.Invert();
        //tempQuat.MulVec(linAccQuat);
        tempQuat.Mul(linAccQuat);
        tempQuat.MulWithInverted(rotQuat);

        accX = tempQuat.x;
        accY = tempQuat.y;
        accZ = tempQuat.z;
    }

    void PhysicsProc(){
        mainEngine.Crd1 += velX * deltaTime + accX * deltaTime * deltaTime / 2;
        mainEngine.Crd2 += velY * deltaTime + accY * deltaTime * deltaTime / 2;
        z += velZ * deltaTime + accZ * deltaTime * deltaTime / 2;

        velX += accX * deltaTime;
        velY += accY * deltaTime;
        velZ += accZ * deltaTime;
    }

    public void Disable(){
        isActive = false;
    }

    public void run()
    {
        time = System.nanoTime();
        while(isActive)
        {
            deltaTime = ((float)System.nanoTime()) / 1000000000 - time;
            time += deltaTime;
            if(fPhysics)
                PhysicsProc();
        }
    }
}

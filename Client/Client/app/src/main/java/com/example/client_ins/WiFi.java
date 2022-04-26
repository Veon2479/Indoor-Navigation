package com.example.client_ins;

public class WiFi {

    private String SSID;
    private String MAC;
    private double NominalPower;
    private double Power;
    private double X;
    private double Y;
    private double Distance;

    public WiFi( String SSID, String MAC, double NominalPower, double X, double Y)
    {
        this.setSSID(SSID);
        this.setMAC(MAC);
        this.setNominalPower(NominalPower);
        this.setPower(0);
        this.setX(X);
        this.setY(Y);
        this.setDistance(0);
    }

    public String getSSID() {
        return SSID;
    }

    public void setSSID(String SSID) {
        this.SSID = SSID;
    }

    public double getNominalPower() {
        return NominalPower;
    }

    public void setNominalPower(double nominalPower) {
        NominalPower = nominalPower;
    }

    public double getPower() {
        return Power;
    }

    public void setPower(double power) {
        Power = power;
    }

    public String getMAC() {
        return MAC;
    }

    public void setMAC(String MAC) {
        this.MAC = MAC;
    }

    public double getY() {
        return Y;
    }

    public void setY(double y) {
        Y = y;
    }

    public double getX() {
        return X;
    }

    public void setX(double x) {
        X = x;
    }

    public double getDistance() {
        return Distance;
    }

    public void setDistance(double distance) {
        Distance = distance;
    }
}

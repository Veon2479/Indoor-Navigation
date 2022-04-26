package com.example.client_ins;

public class WiFi {

    private String SSID;
    private String MAC;
    private double NominalPower;
    private double Power;

    public WiFi( String SSID, String MAC, double NominalPower)
    {
        this.setSSID(SSID);
        this.setMAC(MAC);
        this.setNominalPower(NominalPower);
        this.setPower(0);
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
}

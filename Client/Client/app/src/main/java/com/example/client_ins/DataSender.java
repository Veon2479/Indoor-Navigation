package com.example.client_ins;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;

import static com.example.client_ins.Tools.*;

import static java.lang.Thread.sleep;

import android.os.Build;
import android.util.Log;

import androidx.annotation.RequiresApi;

class DataSender implements Runnable {
    Engine mainEngine;
    private boolean isActive;
    private DatagramSocket clientSocket;

    public DataSender(Engine engine){
        this.mainEngine = engine;
        this.isActive = true;
    }

    public void Disable(){
        isActive = false;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void run(){

        try {
            clientSocket = new DatagramSocket();

            // Получение IP-адреса сервера
            InetAddress IPAddress = InetAddress.getByName(serverAddr);

            // Создание соответствующего буфера
            byte[] sendingDataBuffer = setInfoBuffer( mainEngine.UserId, mainEngine.Crd1, mainEngine.Crd2);

            // Создание UDP-пакет
            DatagramPacket sendingPacket = new DatagramPacket(sendingDataBuffer,sendingDataBuffer.length, IPAddress, serverPort);

            while(isActive){
                //*Помещение данных в буфер*
                updateInfoBuffer(sendingDataBuffer, mainEngine.UserId, mainEngine.Crd1, mainEngine.Crd2);

                // Отправьте UDP-пакет серверу
                clientSocket.send(sendingPacket);

                sleep(1000);
            }

            // Закрытие соединения с сервером через сокет
            clientSocket.close();
        }catch (Exception exception){
            System.out.println("Error UDP Sending");
        }
    }
}
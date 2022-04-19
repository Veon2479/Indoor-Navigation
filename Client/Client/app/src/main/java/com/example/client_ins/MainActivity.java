package com.example.client_ins;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Build;
import android.os.Bundle;

import java.time.Duration;

class DataSender implements Runnable {
    MainActivity main;
    boolean isActive;

    public DataSender(MainActivity mainActivity){
        this.main = mainActivity;
        this.isActive = true;
    }

    public void Disable(){
        isActive = false;
    }

    public final static int SERVICE_PORT=50001;

    public void run(){
        try {
            DatagramSocket clientSocket = new DatagramSocket();

            // Получение IP-адреса сервера
            InetAddress IPAddress = InetAddress.getByName("localhost");

            // Создание соответствующего буфера
            byte[] sendingDataBuffer = new byte[1024];

            // Создание UDP-пакет
            DatagramPacket sendingPacket = new DatagramPacket(sendingDataBuffer,sendingDataBuffer.length, IPAddress, SERVICE_PORT);

            while(isActive){
                //*Помещение данных в буфер*

                // Отправьте UDP-пакет серверу
                clientSocket.send(sendingPacket);

            }

            // Закрытие соединения с сервером через сокет
            clientSocket.close();
        }catch (Exception exception){
            System.out.println("Error UDP Sending");
        }
    }
}

import com.example.client_ins.Engine.*;

public class MainActivity extends AppCompatActivity {

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Engine engine = new Engine();
    }
}
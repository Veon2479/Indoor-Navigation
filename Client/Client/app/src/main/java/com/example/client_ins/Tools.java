package com.example.client_ins;

import android.os.Build;
import android.util.Xml;

import androidx.annotation.RequiresApi;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NamedNodeMap;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import java.io.File;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.time.Instant;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;


public class Tools {


    //these values will be read from file

   // public static String serverAddr = "10.144.50.145";
    public static String serverAddr = "192.168.50.145";

    public static int serverPortTcp = 4444;
    public static int serverPortUdp = 4445;
    public static int AttemptsToRegistrate = 3;
    public static int BufferSize = 28;
    public static int UdpPacketDelay = 1000;

    public static void readFromFile() throws IOException, SAXException, ParserConfigurationException {
        File file = new File("configClient.xml");
        if(!file.exists()) {
            serverAddr = "192.168.50.145";
            serverPortTcp = 4444;
            serverPortUdp = 4445;
            AttemptsToRegistrate = 3;
            BufferSize = 28;
            UdpPacketDelay = 1000;
        }
        else {
            DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
            DocumentBuilder db = dbf.newDocumentBuilder();
            Document doc = db.parse(file);
            doc.getDocumentElement().normalize();
            NodeList nodeList = doc.getElementsByTagName("root");
            Node node = nodeList.item(0);
            NamedNodeMap nm = node.getAttributes();
            AttemptsToRegistrate = Integer.parseInt(nm.item(0).getNodeValue());
            BufferSize = Integer.parseInt(nm.item(1).getNodeValue());
            UdpPacketDelay = Integer.parseInt(nm.item(2).getNodeValue());
            serverAddr = nm.item(3).getNodeValue();
            serverPortTcp = Integer.parseInt(nm.item(4).getNodeValue());
            serverPortUdp = Integer.parseInt(nm.item(5).getNodeValue());

        }
    }

    public static void writeToFile() throws ParserConfigurationException, TransformerException {
        DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
        factory.setNamespaceAware(true);
        Document doc = factory.newDocumentBuilder().newDocument();
        Element root = doc.createElement("root");
        root.setAttribute("serverAddr", serverAddr);
        root.setAttribute("serverPortTcp", String.valueOf(serverPortTcp));
        root.setAttribute("serverPortUdp", String.valueOf(serverPortUdp));
        root.setAttribute("AttemptsToRegistrate", String.valueOf(AttemptsToRegistrate));
        root.setAttribute("BufferSize", String.valueOf(BufferSize));
        root.setAttribute("UdpPacketDelay", String.valueOf(UdpPacketDelay));
        doc.appendChild(root);

        File file = new File("configClient.xml");
        Transformer transformer = TransformerFactory.newInstance().newTransformer();
        transformer.setOutputProperty(OutputKeys.INDENT, "yes");
        transformer.transform(new DOMSource(doc), new StreamResult(file));
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static byte[] setInfoBuffer(int userID, double crd1, double crd2)
    {
        byte[] result = new byte[ ( 32 + 64 * 2 + 64) / 8 ];
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 4, crd1 );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 12, crd2 );//.order(ByteOrder.LITTLE_ENDIAN);
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 20,  ( Instant.now().getEpochSecond() ) );//.order(ByteOrder.LITTLE_ENDIAN);
        return result;
    }

    public static long getInfoBuffer( Engine engine, byte[] buffer )
    {
        engine.UserId = ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).getInt( 0 );
        engine.Crd1 = ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).getDouble( 4 );
        engine.Crd2 = ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).getDouble( 12 );
        return ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).getLong( 20 );

    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static void updateInfoBuffer(byte[] buffer, int userID, double crd1, double crd2)
    {
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 4, crd1 );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 12, crd2 );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putLong( 20, Instant.now().getEpochSecond() );
    }
}

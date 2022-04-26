package com.example.client_ins;

import android.content.Context;
import android.os.Build;
import android.text.Editable;

import androidx.annotation.RequiresApi;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.StringReader;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.time.Instant;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;

@RequiresApi(api = Build.VERSION_CODES.O)
public class Tools {


    Engine engine = Engine.getInstance();
    //these values will be read from file

    public static String serverAddr;
    public static int serverPortTcp;
    public static int serverPortUdp;
    public static int AttemptsToRegistrate;
    public static int BufferSize;
    public static int UdpPacketDelay;
    public static int NumberOfWiFi;

    private static final String SettingFile = "Settings.xml";
    static final String WiFiFile = "WiFi.xml";

    public static void readFromFile(Context context) throws Exception {
        File path = context.getFilesDir();
        System.out.println("Path is: " + path.getPath() );

        try {
            DocumentBuilder builder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
            Document doc = builder.parse(new File(path.getPath() + SettingFile));
            doc.normalizeDocument();

            Element root = doc.getDocumentElement();
            System.out.println("Trying to read values");
            serverAddr = root.getElementsByTagName("serverAddr").item(0).getTextContent();
            AttemptsToRegistrate = Integer.parseInt(root.getElementsByTagName("AttemptsToRegistrate").item(0).getTextContent());
            BufferSize = Integer.parseInt(root.getElementsByTagName("BufferSize").item(0).getTextContent());
            UdpPacketDelay = Integer.parseInt(root.getElementsByTagName("UdpPacketDelay").item(0).getTextContent());
            serverPortTcp = Integer.parseInt(root.getElementsByTagName("ServerPortTcp").item(0).getTextContent());
            serverPortUdp = Integer.parseInt(root.getElementsByTagName("ServerPortUdp").item(0).getTextContent());
            NumberOfWiFi = Integer.parseInt(root.getElementsByTagName("NumberOfWiFi").item(0).getTextContent());
        }
        catch (Exception e)
        {
            serverAddr = "10.144.52.41";
            serverPortTcp = 4444;
            serverPortUdp = 4445;
            AttemptsToRegistrate = 9;
            BufferSize = 28;
            UdpPacketDelay = 1000;
            NumberOfWiFi = 64;
            writeToFile(context);
        }

    }


    public static void writeToFile(Context context) throws Exception {

        File path = context.getFilesDir();

        DocumentBuilder builder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
        Document doc = builder.newDocument();

        Element root = doc.createElement("Settings");
        doc.appendChild(root);

        Element elServerAddr = doc.createElement("serverAddr");
        elServerAddr.setTextContent(serverAddr);
        root.appendChild(elServerAddr);

        Element elServerPortTcp = doc.createElement("ServerPortTcp");
        elServerPortTcp.setTextContent(Integer.toString(serverPortTcp));
        root.appendChild(elServerPortTcp);

        Element elServerPortUdp = doc.createElement("ServerPortUdp");
        elServerPortUdp.setTextContent(Integer.toString(serverPortUdp));
        root.appendChild(elServerPortUdp);

        Element elAttemptsToRegistrate = doc.createElement("AttemptsToRegistrate");
        elAttemptsToRegistrate.setTextContent(Integer.toString(AttemptsToRegistrate));
        root.appendChild(elAttemptsToRegistrate);

        Element elBufferSize = doc.createElement("BufferSize");
        elBufferSize.setTextContent(Integer.toString(BufferSize));
        root.appendChild(elBufferSize);

        Element elUdpPacketDelay = doc.createElement("UdpPacketDelay");
        elUdpPacketDelay.setTextContent(Integer.toString(UdpPacketDelay));
        root.appendChild(elUdpPacketDelay);

        Element elNumberOfWiFi = doc.createElement("NumberOfWiFi");
        elUdpPacketDelay.setTextContent(Integer.toString(NumberOfWiFi));
        root.appendChild(elNumberOfWiFi);

        Transformer tr = TransformerFactory.newInstance().newTransformer();
        tr.setOutputProperty(OutputKeys.INDENT, "yes");
        tr.transform(new DOMSource(doc), new StreamResult( new File( path.getPath() + SettingFile )));

    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static byte[] setInfoBuffer(int userID, double crd1, double crd2)
    {
        byte[] result = new byte[ BufferSize ];
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 4, crd1 );
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 12, crd2 );
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 20,  ( Instant.now().getEpochSecond() ) );
        return result;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static byte[] setInfoBufferWithLongs(int userID, long par1, long par2) //par1 is QR id
    {
        byte[] result = new byte[ BufferSize ];
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 4, par1 );
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 12, par2 );
        ByteBuffer.wrap( result ).order(ByteOrder.LITTLE_ENDIAN).putLong( 20,  ( Instant.now().getEpochSecond() ) );
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
    public static byte[] setCustomBufferWithInts(int[] prms)
    {
        byte[] res = new byte[ prms.length * 4 + 8 ];
        ByteBuffer buff = ByteBuffer.wrap( res );
        int i;
        for ( i = 0; i < prms.length; i++ )
        {
            buff.order(ByteOrder.LITTLE_ENDIAN).putInt( i*4, prms[i] );
        }
        buff.order(ByteOrder.LITTLE_ENDIAN).putLong( i*4, Instant.now().getEpochSecond() );
        return res;
    }

    public static void getResponseBuffer(Engine engine, byte[] buff)
    {
        ByteBuffer tmpBuff = ByteBuffer.wrap(buff);
        engine.UserId = tmpBuff.order(ByteOrder.LITTLE_ENDIAN).getInt(0 );
        engine.Crd1 = tmpBuff.order(ByteOrder.LITTLE_ENDIAN).getDouble( 4 );
        engine.Crd2 = tmpBuff.order(ByteOrder.LITTLE_ENDIAN).getDouble( 12 );
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public static void updateInfoBuffer(byte[] buffer, int userID, double crd1, double crd2)
    {
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putInt( 0, userID );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 4, crd1 );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putDouble( 12, crd2 );
        ByteBuffer.wrap( buffer ).order(ByteOrder.LITTLE_ENDIAN).putLong( 20, Instant.now().getEpochSecond() );
    }



    public static void fillWiFiInfo() throws ParserConfigurationException, IOException, SAXException {

        Engine engine = Engine.getInstance();
        File path = engine.context.getFilesDir();

        DocumentBuilder builder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
        Document doc = builder.parse(new File(path.getPath() + WiFiFile));
        doc.normalizeDocument();

        Element root = doc.getDocumentElement();
        System.out.println("Parsing xml with wifi info");

        NodeList list = doc.getElementsByTagName("WIFISpot");

        for (int i = 0; i < list.getLength(); i++ )
        {
            Node node = list.item(i);
            if (node.getNodeType() == Node.ELEMENT_NODE)
            {
                Element element = (Element) node;
                String SSID = element.getAttribute("name");
                String MAC = element.getElementsByTagName("mac").item(0).getTextContent();
                double X = Double.parseDouble( element.getElementsByTagName("x").item(0).getTextContent() );
                double Y = Double.parseDouble( element.getElementsByTagName("y").item(0).getTextContent() );
                double Power = Double.parseDouble( element.getElementsByTagName("power").item(0).getTextContent() );

                System.out.println("Get info about spot " + SSID);
                engine.WiFi_List.add( new WiFi( SSID, MAC, Power, X, Y ) );
            }
        }
    }
}

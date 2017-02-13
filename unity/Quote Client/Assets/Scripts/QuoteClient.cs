using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuoteClient : MonoBehaviour
{
    TcpClient client;
    string receivedMessage;
    byte[] buf = new byte[49152];

    public Text text;
    public Button button;

    public string ipAddress = "127.0.0.1";
    public int port = 54010;

    public void GetQuote()
    {
        if (!client.Connected) return; // early out to stop the function from running if client is disconnected

        button.interactable = false;
        receivedMessage = "";

        // Set up async read
        var stream = client.GetStream();
        stream.BeginRead(buf, 0, buf.Length, Message_Received, null);

        // send message
        byte[] msg = Encoding.ASCII.GetBytes("QUOTE");
        stream.Write(msg, 0, msg.Length);
    }

    void Start()
    {
        client = new TcpClient();
        client.Connect(ipAddress, port);
    }

    void OnDestroy()
    {
        if (client.Connected)
        {
            client.Close();
        }
    }
	
	void Update ()
    {
		if (!string.IsNullOrEmpty(receivedMessage))
        {
            text.text = receivedMessage;
            receivedMessage = "";
            button.interactable = true;
        }
	}

    void Message_Received(IAsyncResult res)
    {
        if (res.IsCompleted && client.Connected)
        {
            var stream = client.GetStream();
            int bytesIn = stream.EndRead(res);

            receivedMessage = Encoding.ASCII.GetString(buf, 0, bytesIn);
        }
    }
}

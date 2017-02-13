using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class QuoteServer : MonoBehaviour
{
    Quotes quotes;
    TcpListener server;
    TcpClient client;
    IEnumerator doClient = null;
    

    public string wisdomFile = "wisdom";

    public string ipAddress = "127.0.0.1";
    public int port = 54010;

    void Client_Connected(IAsyncResult res)
    {
        client = server.EndAcceptTcpClient(res);
    }

    void Update()
    {
        if (client != null && doClient == null)
        {
            doClient = DoClient();
            StartCoroutine(doClient);
        }
    }

    IEnumerator DoClient()
    {
        int bytesReceived = 0;
        byte[] buf = new byte[49152];
        var stream = client.GetStream();

        do
        {
            bytesReceived = stream.Read(buf, 0, buf.Length);
            if (bytesReceived > 0)
            {
                string msg = Encoding.ASCII.GetString(buf, 0, bytesReceived);
                if (msg == "QUOTE")
                {
                    byte[] quoteOut = Encoding.ASCII.GetBytes(quotes.RandomQuote);
                    stream.Write(quoteOut, 0, quoteOut.Length);
                }
            }

            yield return null;
        }
        while (bytesReceived > 0);

        // Reset everything to defaults
        doClient = null;
        client.Close();
        client = null;

        // Accept a new client
        server.BeginAcceptTcpClient(Client_Connected, null);
    }

    void Start ()
    {
        quotes = new Quotes(wisdomFile);

        IPAddress ip = IPAddress.Parse(ipAddress);
        server = new TcpListener(ip, port);
        server.Start();

        server.BeginAcceptTcpClient(Client_Connected, null);
	}
}

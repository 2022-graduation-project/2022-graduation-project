using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Server : MonoBehaviour
{
    public TMP_InputField portInput;

    List<ServerClient> clients;
    List<ServerClient> disconnectList;

    TcpListener server;
    bool serverStarted;

    public void ServerCreate()
    {
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();

        try
        {
            int portNum = portInput.text == "" ? 7777 : int.Parse(portInput.text);
            // 자기 자신의 IP주소
            server = new TcpListener(IPAddress.Any, portNum);
            // Bind
            server.Start();

            StartListening();
            serverStarted = true;
            Chat.instance.ShowMessage($"서버가 {portNum}에서 시작되었습니다.");
        }
        catch (Exception e)
        {
            Chat.instance.ShowMessage($"Socket error: {e.Message}");
        }
    }

    void StartListening()
    {
        // Async (비동기) 듣기를 시작해놓고 다음 것 받아들이기
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        // 무한 반복 하며 다른 소켓도 계속 받을 수 있음
        StartListening();

        // 메시지를 연결된 모두에게 보냄
        
    }
    
}

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket)
    {
        clientName = "Guest";
        tcp = clientSocket;
    }
}
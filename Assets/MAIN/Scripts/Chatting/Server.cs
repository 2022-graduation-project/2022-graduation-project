using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.IO;
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
        Broadcast("%NAME", new List<ServerClient>() {   clients[clients.Count - 1]    });
    }
    
    bool IsConnected(TcpClient c)
    {
        try
        {
            if(c!=null && c.Client!=null && c.Client.Connected)
            {
                // Poll: 클라이언트에게 1바이트짜리 테스트 데이터 보냈다가 제대로 받으면 true
                if(c.Client.Poll(0, SelectMode.SelectRead))
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }
    }

    void Update() 
    {
        if(!serverStarted) return;

        foreach(ServerClient c in clients)
        {
            // 클라이언트 연결 안돼있으면, 닫기
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectList.Add(c);
                continue;
            }
            // 연결O: 클라이언트로부터 체크 메시지를 받는다
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if(s.DataAvailable)
                {
                    string data = new StreamReader(s, true).ReadLine();
                    if(data!=null)
                    {
                        OnIncomingData(c, data);
                    }
                }
            }
        }
    }

    void OnIncomingData(ServerClient c, string data)
    {
        if(data.Contains("&NAME"))
        {
            c.clientName = data.Split('|')[1];
            // 메시지를 연결된 모두에게 보냄
            Broadcast($"{c.clientName}이 연결되었습니다.", clients);
            return;
        }
        // 메시지를 연결된 모두에게 보냄
        Broadcast($"{c.clientName} : {data}", clients);
    }
    
    void Broadcast(string data, List<ServerClient> cl)
    {
        foreach(var c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Chat.instance.ShowMessage($"쓰기 에러 : {e.Message}를 클라이언트에게 {c.clientName}");
            }
        }
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
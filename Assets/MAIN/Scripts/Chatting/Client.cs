using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;
using TMPro;

public class Client : MonoBehaviour
{
    public TMP_InputField IPInput, PortInput;
    public TMP_Text NickInput;
    string clientName;

    bool socketReady;
    TcpClient socket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;

    public void ConnectToServer()
    {
        // 이미 연결됐다면 함수 무시
        if (socketReady) return;

        // 기본 호스트 / 포트번호
        string ip = IPInput.text == "" ? "127.0.0.1" : IPInput.text;
        int port = PortInput.text == "" ? 7777 : int.Parse(PortInput.text);

        // 소켓 생성
        try
        {
            socket = new TcpClient(ip, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Chat.instance.ShowMessage($"소켓 에러 : {e.Message}");
        }
    }

    void Update()
    {
        if(socketReady && stream.DataAvailable)
        {
            string data = reader.ReadLine();
            if(data!=null)
            {
                OnIncomingData(data);
            }
        }
    }

    void OnIncomingData(string data)
    {
        if(data == "%NAME")
        {
            clientName = NickInput.text == "" ? "Guest" + UnityEngine.Random.Range(1000, 10000) : NickInput.text;
            Send($"&NAME|{clientName}");
            return;
        }

        Chat.instance.ShowMessage(data);
    }

    // Server에 보내기
    void Send(string data)
    {
        if(!socketReady) return;

        writer.WriteLine(data);
        writer.Flush();
    }

    public void OnSendButton(TMP_InputField SendInput)
    {
        // Enter 아닐 때 return
        if(!Input.GetButtonDown("Submit")) return;
        // Focus
        SendInput.ActivateInputField();
        if(SendInput.text.Trim()=="") return;

        string message = SendInput.text;
        SendInput.text = "";
        Send(message);
    }

    private void OnApplicationQuit() 
    {
        CloseSocket();    
    }
    void CloseSocket()
    {
        if(!socketReady) return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }
}

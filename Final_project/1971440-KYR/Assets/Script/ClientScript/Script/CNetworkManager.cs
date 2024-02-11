using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using serverModule;
using ServerUnity;

public interface IMessageReceiver 
{
    void on_recv(CPacket msg);
}

public class CNetworkManager : MonoBehaviour
{
    string ipaddress;
    string port_number;


    Queue<CPacket> sending_queue;
    CServerUnityService server;

    public IMessageReceiver message_receiver;
    CStartMenu start;
    CGameUser user;

    void Awake()
    {
        this.server = gameObject.AddComponent<CServerUnityService>();
        this.server.appcallback_on_message += this.on_message;
        this.server.appcallback_on_status_changed += this.on_status_changed;

        this.sending_queue = new Queue<CPacket>();

        start = GameObject.Find("StartLoading").GetComponent<CStartMenu>();
        user = GameObject.Find("GameUser").GetComponent<CGameUser>();
    }

    public void connect()
    {
        this.sending_queue.Clear();

        if (!this.server.is_connected())
        {
            this.server.connect(this.ipaddress, int.Parse(this.port_number));

            user.enter();
        }
    }
    public void disconnect()
    {
        if (is_connected())
        {
            this.server.disconnect();
            return;
        }

        back_to_main();
    }
    void on_message(CPacket msg)
    {
        this.message_receiver.on_recv(msg);
    }
    void on_status_changed(NETWORK_EVENT status)
    {
        switch (status)
        {
            case NETWORK_EVENT.disconnected:
                back_to_main();
                break;
        }
    }
    void back_to_main()
    {
        start.startMenu();
    }
    public void send(CPacket msg)
    {
        this.sending_queue.Enqueue(msg);
    }
    private void Update()
    {
        if (!this.server.is_connected())
        {
            return;
        }
        while (this.sending_queue.Count > 0)
        {
            CPacket msg = this.sending_queue.Dequeue();
            this.server.send(msg);
        }
    }
    public bool is_connected()
    {
        if(this.server == null)
        {
            return false;
        }

        return this.server.is_connected();
    }
    public void getIpAddress(string ip, string port)
    {
        this.ipaddress = ip;
        this.port_number = port;

        connect();
    }
}

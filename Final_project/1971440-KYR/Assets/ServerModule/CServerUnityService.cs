using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using serverModule;

namespace ServerUnity
{
    // 서버 모듈과 유니티 어플리케이션을 이어주는 클래스
    // 서버에서 받은 이벤트, 메시지 수신 이벤트 등을 전달하는 역할
    // 모노비해비어를 상속받으며 유니티 어플리케이션과 동일한 스레드에서 작동

    public class CServerUnityService : MonoBehaviour
    {
        CServerEventManager event_manager;

        IPeer gameserver;

        CNetworkService service;

        public delegate void StatusChangeHandler(NETWORK_EVENT status);
        public StatusChangeHandler appcallback_on_status_changed;

        public delegate void MessageHandler(CPacket msg);
        public MessageHandler appcallback_on_message;

        private void Awake()
        {
            this.event_manager = new CServerEventManager();
        }

        public void connect(string host, int port)
        {
            if (this.service == null)
            {
                this.service = new CNetworkService();
            }

            CConnector connector = new CConnector(service);

            connector.connected_callback += on_connected_gameserver;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(host), port);
            connector.connect(endpoint);
        }
        public bool is_connected()
        {
            return this.gameserver != null;
        }
        void on_connected_gameserver(CUserToken server_token)
        {
            this.gameserver = new CRemoteServerPeer(server_token);
            ((CRemoteServerPeer)this.gameserver).set_eventmanager(this.event_manager);

            server_token.disable_auto_heartbeat();

            this.event_manager.enqueue_network_event(NETWORK_EVENT.connected);
        }
        // Update is called once per frame
        void Update()
        {
            if (this.event_manager.has_message())
            {
                CPacket msg = this.event_manager.dequeue_network_message();
                if (this.appcallback_on_message != null)
                {
                    this.appcallback_on_message(msg);
                }
            }

            if (this.event_manager.has_event())
            {
                NETWORK_EVENT status = this.event_manager.dequeue_network_event();
                appcallback_on_status_changed(status);
                if (this.appcallback_on_status_changed != null)
                {
                    this.appcallback_on_status_changed(status);
                }
            }

            // heartbeat...
            if(this.gameserver != null)
            {
                ((CRemoteServerPeer)this.gameserver).update_heartbeat(Time.deltaTime);
            }
        }
        void on_status_changed(NETWORK_EVENT status)
        {
            switch (status)
            {
                case NETWORK_EVENT.disconnected:
                    this.gameserver = null;
                    break;
            }
        }
        public void send(CPacket msg)
        {
            try
            {
                this.gameserver.send(msg);
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        private void OnApplicationQuit()
        {
            if(this.gameserver != null)
            {
                ((CRemoteServerPeer)this.gameserver).token.disconnect();
            }
        }
        public void disconnect()
        {
            if (this.gameserver != null)
            {
                this.gameserver.disconnect();
            }
        }
    }
}

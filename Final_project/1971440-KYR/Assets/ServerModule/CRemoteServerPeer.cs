using System;
using System.Collections;
using System.Collections.Generic;
using serverModule;


namespace ServerUnity
{
    public class CRemoteServerPeer : IPeer
    {
        public CUserToken token { get; private set; }
        WeakReference server_eventmanager;

        public CRemoteServerPeer(CUserToken token)
        {
            this.token = token;
            this.token.set_peer(this);
        }
        public void set_eventmanager(CServerEventManager event_manager)
        {
            this.server_eventmanager = new WeakReference(event_manager);
        }
        public void update_heartbeat(float time)
        {
            this.token.update_heartbeat_manually(time);
        }

        void IPeer.on_message(CPacket msg)
        {
            (this.server_eventmanager.Target as CServerEventManager).enqueue_network_message(msg);
        }
        void IPeer.on_removed()
        {
            (this.server_eventmanager.Target as CServerEventManager).enqueue_network_event(NETWORK_EVENT.disconnected);
        }
        void IPeer.send(CPacket msg)
        {
            this.token.send(msg);
        }
        void IPeer.disconnect()
        {
            this.token.disconnect();
        }
    }
}

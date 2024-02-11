using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using serverModule;

namespace ServerUnity
{
    public enum NETWORK_EVENT : byte
    {
        connected,

        disconnected,

        end
    }

    public class CServerEventManager
    {
        object cs_event;

        Queue<NETWORK_EVENT> network_events;

        Queue<CPacket> network_message_events;

        public CServerEventManager()
        {
            this.network_events = new Queue<NETWORK_EVENT>();
            this.network_message_events = new Queue<CPacket>();
            this.cs_event = new object();
        }

        public void enqueue_network_event(NETWORK_EVENT event_type)
        {
            lock (this.cs_event)
            {
                this.network_events.Enqueue(event_type);
            }
        }
        public bool has_event()
        {
            lock (this.cs_event)
            {
                return this.network_events.Count > 0;
            }
        }
        public NETWORK_EVENT dequeue_network_event()
        {
            lock (this.cs_event)
            {
                return this.network_events.Dequeue();
            }
        }
        public bool has_message()
        {
            lock (this.cs_event)
            {
                return this.network_message_events.Count > 0;
            }
        }
        public void enqueue_network_message(CPacket msg)
        {
            lock (this.cs_event)
            {
                this.network_message_events.Enqueue(msg);
            }
        }
        public CPacket dequeue_network_message()
        {
            lock (this.cs_event)
            {
                return this.network_message_events.Dequeue();
            }
        }
    }
}
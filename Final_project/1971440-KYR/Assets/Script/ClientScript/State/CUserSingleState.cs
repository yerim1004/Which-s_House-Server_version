using System.Collections;
using System;
using UnityEngine;
using serverModule;
using WHServer;

public class CUserSingleState : MonoBehaviour, IState
{
    void Awake()
    {
        //GetComponent<CStateManager>().register_message_handler(this, );
    }
    void IState.on_message(serverModule.CPacket msg)
    {

    }
    void IState.on_enter()
    {

    }
    void IState.on_exit()
    {

    }
}

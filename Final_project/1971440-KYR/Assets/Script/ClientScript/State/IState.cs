using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using serverModule;

public enum USER_STATE_TYPE
{
    PARTY,
    SINGLE
}
public interface IState
{
    // IUserState
    void on_message(CPacket msg);

    // IState
    void on_enter();
    void on_exit();
}

using System.Collections;
using System;
using UnityEngine;

public enum STATE_OBJECT_TYPE
{
    // �ڽ��� ������Ʈ�� ��� ������Ʈ ��ũ��Ʈ�� ���̴� ����...
    ATTACH_TO_SINGLE_OBJECT,

    // ���ο� ���� ������Ʈ�� �����ϰ� �ڽ����� ���̴� ����...
    CREATE_NEW_OBJECT
}

public interface IStateObjectGenerationType
{
    IState make_state_object<T>(GameObject parent, Enum Key) where T : Component, IState;
    void set_active(IState obj, bool flag);
}
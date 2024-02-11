using System.Collections;
using System;
using UnityEngine;

public class CStateSingleObject : IStateObjectGenerationType
{
    IState IStateObjectGenerationType.make_state_object<T>(GameObject parent, Enum Key)
    {
        return parent.AddComponent<T>();
    }

    void IStateObjectGenerationType.set_active(IState obj, bool flag)
    {
        ((MonoBehaviour)obj).StopAllCoroutines();
        ((MonoBehaviour)obj).enabled = flag;
    }
}

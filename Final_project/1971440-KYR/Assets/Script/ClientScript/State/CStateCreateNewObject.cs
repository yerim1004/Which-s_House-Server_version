using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateCreateNewObject : IStateObjectGenerationType
{
    IState IStateObjectGenerationType.make_state_object<T>(GameObject parent, System.Enum Key)
    {
        GameObject obj = new GameObject(Key.ToString());
        obj.transform.parent = parent.transform;
        return obj.AddComponent<T>();
    }
    void IStateObjectGenerationType.set_active(IState obj, bool flag)
    {
        ((MonoBehaviour)obj).gameObject.SetActive(flag);
    }
}

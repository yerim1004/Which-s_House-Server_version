using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIOnOff : MonoBehaviour
{
    public GameObject UI;
    
    public void ActiveSetting()
    {
        UI.SetActive(!UI.activeSelf);
    }

    public void LayoutSetting()
    {
        GameObject uiparent = UI.transform.parent.gameObject;

        uiparent.transform.SetAsLastSibling();
    }
}

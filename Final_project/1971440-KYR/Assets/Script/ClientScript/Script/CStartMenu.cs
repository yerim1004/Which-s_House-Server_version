using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CStartMenu : MonoBehaviour
{
    public InputField ip;
    public InputField port;

    string ipAdd;
    string portNum;
    CNetworkManager network_manager;

    public void Awake()
    {
        this.network_manager = GameObject.Find("NetworkManager").GetComponent<CNetworkManager>();
    }

    public void startBtn()
    {
        ipAdd = ip.text;
        portNum = port.text;

        if(ipAdd.Length <= 0 || portNum.Length <= 0)
        {
            return;
        }

        network_manager.getIpAddress(ipAdd, portNum);

        gameObject.SetActive(false);
    }
    public void startMenu()
    {
        gameObject.SetActive(true);
    }
}

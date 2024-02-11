using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using serverModule;
using WHServer;

public class CChatManager : MonoBehaviour
{
    string input_text;
    List<string> received_texts;
    CNetworkManager network_manager;

    public InputField input_field;
    Vector2 currentScrollPos = new Vector2();

    public GameObject text_prefab;
    public Canvas canvas;
    Vector3 contentPos = new Vector3();
    void Awake()
    {
        this.input_text = "";
        this.received_texts = new List<string>();
        this.network_manager = GameObject.Find("NetworkManager").GetComponent<CNetworkManager>();

        //text_prefab = Resources.Load("Prefabs/Text");
    }

    // Update is called once per frame
    void Update()
    {
        if(input_field.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            send();
        }
    }
    void send()
    {
        input_text = input_field.text;

        CPacket msg = CPacket.create((short)PROTOCOL.CHAT_MSG_REQ);
        msg.push(input_text);

        network_manager.send(msg);

        Debug.Log("send message : " + input_text);
        // 초기화
        input_field.text = "";
    }
    public void write_chat(USER_STATE_TYPE current_state, string text)
    {
        // 텍스트 받고
        // 파티인지 싱글인지 확인후 채팅 색 다르게
        switch (current_state)
        {
            case USER_STATE_TYPE.SINGLE:
                {
                    create_text(text);
                    break;
                }
            case USER_STATE_TYPE.PARTY:
                {
                    create_text(text);
                    break;
                }
        }
    }
    void create_text(string text)
    {
        GameObject print = Instantiate(text_prefab, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);

        print.GetComponent<Text>().text = text;
        print.transform.SetParent(GameObject.Find("Content").transform);
    }
}

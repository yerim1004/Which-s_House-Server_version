using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using serverModule;
using WHServer;

public class CGameUser : MonoBehaviour, IMessageReceiver
{
    // 네트워크 매니저에 적용될 메인 메시지 리시버
    
    CNetworkManager network_manager;
    USER_STATE_TYPE user_state;
    CChatManager chat_manager;

    GameObject player_object;
    CPlayer player;
    public string player_index { get; private set; }

    public CPlayerRenderer rendering { get; private set; }

    void Awake()
    {
        Debug.Log("start");
        // 싱글이 기본
        this.user_state = USER_STATE_TYPE.SINGLE;
        this.network_manager = GameObject.Find("NetworkManager").GetComponent<CNetworkManager>();
        this.user_state = USER_STATE_TYPE.SINGLE;

        this.chat_manager = GameObject.Find("Chat").GetComponent<CChatManager>();

        rendering = new CPlayerRenderer();
    }
    public void enter()
    {
        this.network_manager.message_receiver = this;

        CPacket msg = CPacket.create((short)PROTOCOL.ENTER_GAME_ROOM_REQ); 
        send(msg);

        //this.initialize();
    }
    void initialize()
    {
        // 프리팹 자식으로 붙이고
        player_object = Resources.Load("ChaWitch") as GameObject;
        

        Transform transform = new GameObject().GetComponent<Transform>();
        transform.position = new Vector3(-13, 0, 8);

        player_object = Instantiate(player_object, transform);
        player_object.transform.parent = this.transform;
        player_object.AddComponent<CPlayer>();
        player_object.GetComponent<CPlayer>().init(this, player_object);

        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<MouseAimCamera>().CameraInit(player_object);
    }
    void IMessageReceiver.on_recv(CPacket msg)
    {
        PROTOCOL protocol_id = (PROTOCOL)msg.pop_protocol_id();

        Debug.Log("Recv PROTOCOL : " + protocol_id);

        switch (protocol_id)
        {
            case PROTOCOL.ENTER_GAME_ROOM_ACK:
                {
                    // 이거 받으면 유저 전체 리스트를 받게 됨
                    List<string> playerlist = new List<string>();
                    Dictionary<string, Position> position_list = new Dictionary<string, Position>();

                    player_index = msg.pop_string();
                    Debug.Log("my index : " + player_index);

                    //player_object.GetComponent<CPlayer>().get_name(player_index);
                    initialize();

                    // 유저 리스트를 렌더러한테 보내줘서 전체 유저를 그릴 수 있도록하자
                    int count = msg.pop_int32();
                    
                    for(int i=0; i<count; i++)
                    {
                        string index = msg.pop_string();
                        playerlist.Add(index);
                    }
                    for(int i=0; i<count; i++)
                    {
                        string name = playerlist[i];
                        Position position;

                        float x = msg.pop_float();
                        float z = msg.pop_float();

                        position = new Position(x, z);

                        position_list.Add(name, position);
                    }

                    // 일단 그리고 위치는 나중에 서버한테 받는 걸로 갱신
                    if(playerlist != null)
                    {
                        rendering.start(playerlist, position_list, player_index);
                    }
                    
                    break;
                }
            case PROTOCOL.ENTER_PLAYER_REQ:
                {
                    string addPlayer = msg.pop_string();
                    if(addPlayer != player_index) { 
                        rendering.addPlayer(addPlayer); 
                    }
                    
                    break;
                }
            case PROTOCOL.CHAT_MSG_ACK:
                {
                    string text = msg.pop_string();
                    chat_manager.write_chat(USER_STATE_TYPE.SINGLE, text);
                    break;
                }

            case PROTOCOL.PARTY_MSG_ACK:
                {
                    string text = msg.pop_string();
                    chat_manager.write_chat(USER_STATE_TYPE.PARTY, text);
                    break;
                }
            case PROTOCOL.PLAYER_MOVED_ACK:
                {
                    string index = msg.pop_string();
                    //Debug.Log("받은 인덱스 :: " + index);
                    if (index == player_index)
                    {
                        //Debug.Log("내가 보낸거");
                        player_object.GetComponent<CPlayer>().recv_move(msg);
                    }
                    else
                    {
                        Debug.Log("남이 보낸거");
                        rendering.playerUpdate(index, msg, 0);
                    }
                    break;
                }
            case PROTOCOL.PLAYER_STAY_ACK:
                {
                    string index = msg.pop_string();
                    
                    if (index == player_index)
                    {
                        //Debug.Log("내가 보낸거");
                        player_object.GetComponent<CPlayer>().recv_stop();
                    }
                    else
                    {
                        Debug.Log("남이 보낸거");
                        rendering.playerUpdate(index, msg, 1);
                    }
                    break;
                }
            case PROTOCOL.PLAYER_CLOSE_REQ:
                {
                    string index = msg.pop_string();

                    rendering.removePlayer(index);
                }
                break;
            case PROTOCOL.PLAYER_ATK_ACK:
                {
                    string index = msg.pop_string();

                    rendering.playerAttack(index);
                }
                break;
        }
    }
    public void send(CPacket msg)
    {
        network_manager.send(msg);
    }
}

// 설치물, 몬스터 관리
//    맵
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using serverModule;
using WHServer;

public class CPlayer : MonoBehaviour
{
    CGameUser user;

    // 플레이어 오브젝트
    GameObject player;
    CharacterController controller;
    PlayerMove attack_script;

    private static Vector3 s_velocity;
    private static Vector3 c_velocity;
    private static Vector3 direction;
    private float xRotate = 0;

    private Animator m_animator;

    private CollectArea m_collectArea = null;
    public float m_moveSpeed = 2.0f;

    bool is_moving = false;


    private void Awake()
    {
       
    }
    void Update()
    {
        Move();
        PlayerLook();
    }
    private void FixedUpdate()
    {
        if (is_moving)
        {
            // Debug.Log("무빙");
            player.transform.position += player.transform.TransformDirection(s_velocity) * Time.deltaTime * m_moveSpeed;
            // 서버에서 받은 걸로 할거면 s속도 클라꺼는 클라에서 할거면 c속도
            
        }
    }
    public void init(CGameUser user, GameObject player)
    {
        this.player = player;
        this.user = user;

        controller = this.player.GetComponent<CharacterController>();

        this.player.AddComponent<PlayerMove>();
        attack_script = this.player.GetComponent<PlayerMove>();
        attack_script.init(this, this.player, user.player_index);
        player.transform.Find("PlayerName").GetComponent<TextMesh>().text = user.player_index;

        m_animator = this.player.GetComponent<Animator>();
        //m_collectArea = GetComponentInChildren<CollectArea>();
    }

    public void msgSend(CPacket msg)
    {
        user.send(msg);
    }
    public void get_position()
    {

    }
    public void get_name(string index)
    {
        player.transform.Find("PlayerName").GetComponent<TextMesh>().text = index;
    }


    private float old_h = 0;
    private float old_v = 0;
    private void Move()
    {
        //CharacterController controller = GetComponent<CharacterController>();

        float new_h = Input.GetAxisRaw("Horizontal");
        float new_v= Input.GetAxisRaw("Vertical");

        //입력이 없으면 0 0
        // 한쪽이라도 움직임이 있으면 전송

        //전 h v 랑 현 h v가 변화가 있으면 전송해야 댐 --- 수정 필요
        if((old_h != new_h || old_v != new_v) && !is_moving)
        {
            is_moving = true;
            send_move(new_h, new_v, m_moveSpeed);

            c_velocity = new Vector3(new_h, 0, new_v);
            c_velocity = c_velocity.normalized;

            c_velocity.y = 0;
        }
        else if(new_h == 0 && new_v == 0 && is_moving) // 움직임이 멈추면 전송
        {
            is_moving = false;
            stop();
        }

        old_h = new_h; old_v = new_v;
    }
    private void PlayerLook()
    {
        xRotate = xRotate + Input.GetAxisRaw("Mouse X") * 3.55f;

        direction = new Vector3(0, xRotate, 0);

        //player.transform.eulerAngles = direction;
        send direction 해야할거 같다
    }
    public void send_move(float h, float v, float speed)
    {
        CPacket moving_data = CPacket.create((short)PROTOCOL.PLAYER_MOVED_REQ);
        Vector3 rotation = direction;
        moving_data.push(user.player_index);
        //Debug.Log("send Index :: " + user.player_index);

        moving_data.push(h);
        moving_data.push(v);
        moving_data.push(speed);

        moving_data.push(rotation.x);
        moving_data.push(rotation.y);
        moving_data.push(rotation.z);

        msgSend(moving_data);
        //user.send(moving_data);
    }


    void stop()
    {
        CPacket moving_data = CPacket.create((short)PROTOCOL.PLAYER_STAY_REQ);
        
        float tX = player.transform.position.x;
        float tZ = player.transform.position.z;

        float rX = player.transform.rotation.x;
        float rY = player.transform.rotation.y;
        float rZ = player.transform.rotation.z;

        moving_data.push(user.player_index);

        moving_data.push(tX);
        moving_data.push(tZ);

        moving_data.push(rX);
        moving_data.push(rY);
        moving_data.push(rZ);

        msgSend(moving_data);
        //user.send(moving_data);
    }
    public void recv_move(CPacket data)
    {
        //string index = data.pop_string();
        float h = data.pop_float();
        float v = data.pop_float();
        float speed = data.pop_float();

        float rx = data.pop_float();
        float ry = data.pop_float();
        float rz = data.pop_float();

        s_velocity = new Vector3(h, 0, v);
        s_velocity = s_velocity.normalized;

        m_animator.SetFloat("Move", s_velocity.magnitude);

        s_velocity.y = 0; //점프없음

        Vector3 newDirection = new Vector3(rx, ry, rz);
        player.transform.rotation = Quaternion.Euler(newDirection);

        if (player == null)
        {
            Debug.Log("player NULL");
        }
    }
    public void recv_stop()
    {
        s_velocity = new Vector3(0, 0, 0);
        s_velocity = s_velocity.normalized;
        s_velocity.y = 0;

        m_animator.SetFloat("Move", s_velocity.magnitude);

        //controller.Move(transform.TransformDirection(s_velocity) * m_moveSpeed * Time.deltaTime);
        player.transform.position += s_velocity * m_moveSpeed * Time.deltaTime;
    }
    Vector3 playerLook()
    {
        Vector3 vec = new Vector3();

        vec = player.transform.eulerAngles;

        return vec;
    }
}

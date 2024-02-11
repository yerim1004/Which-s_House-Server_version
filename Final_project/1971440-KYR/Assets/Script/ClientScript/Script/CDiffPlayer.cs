using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDiffPlayer : MonoBehaviour
{
    // 다른 플레이어 클래스

    GameObject player_object;
    public string player_index { get; private set; }
    Animator animator;
    CharacterController controller;

    bool is_moving = false;

    float x;
    float z;
    float speed;
    static Vector3 velocity;
    static Vector3 rotation;

    public GameObject energy;
    public Transform spawnpos;

    // Update is called once per frame
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (is_moving)
        {
            player_object.transform.eulerAngles = rotation;
            player_object.transform.position += player_object.transform.TransformDirection(velocity) * Time.deltaTime * speed; //c_velocity or s_velocity
        }
    }
    public void getPlayer(GameObject player_object, string index)
    {
        this.player_object = player_object;
        this.player_index = index;

        animator = this.player_object.GetComponent<Animator>();
        controller = this.player_object.GetComponent<CharacterController>();

        energy = Resources.Load("Prefabs/Effect/AttackEffect") as GameObject;
        spawnpos = player_object.transform.Find("AttackArea");
    }
    public void moving(Vector3 direction, Vector3 angle, float speed)
    {
        // 움직이는 상태 direction == velocity
        velocity = direction.normalized;

        this.speed = speed;
        rotation = angle;

        animator.SetFloat("Move", velocity.magnitude);

        velocity.y = 0; //점프없음

        is_moving = true;

        //방향 받아오기

    }
    public void stop(float tx, float tz, float rx, float ry, float rz)
    {
        is_moving = false;

        Vector3 stop_position = new Vector3(tx, 0, tz);
        Vector3 stop_rotation = new Vector3(rx, ry, rz);

        player_object.transform.position = stop_position;
        player_object.transform.rotation = Quaternion.Euler(stop_rotation);
    }
    public void Remove()
    {
        Destroy(player_object);
    }

    public void attack()
    {
        animator.SetTrigger("Attack");
        Instantiate(energy, spawnpos.position, spawnpos.rotation);
    }
}

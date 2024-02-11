using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using serverModule;
using WHServer;

public class CPlayerRenderer : MonoBehaviour
{
    // ��� �÷��̾� ���¸� ������

    static List<GameObject> player_list;
    static Dictionary<string, Position> position_list;
    string myindex;
    public CPlayerRenderer()
    {
        player_list = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void start(List<string> players, Dictionary<string, Position> positions, string myindex)
    {
        this.myindex = myindex;
        Debug.Log("������");
        // ������ �������ִ� �÷��̾� ����Ʈ�� �޾Ƽ�
        // ��ġ�� �°� �����Ѵ�
        foreach (string player in players)
        {
            if (player.Equals(myindex))
            {
                continue;
            }

            GameObject playerObject = Resources.Load("ChaWitch") as GameObject;

            Transform pos = new GameObject().GetComponent<Transform>();

            if (positions[player].X == 0 && positions[player].Z == 0)
            {
                pos.position = new Vector3(-13, 0, 8);
            }
            else
            {
                pos.position = new Vector3(positions[player].X, 0, positions[player].Z);
            }

            // �÷��̾� ���� �� ���ӿ�����Ʈ�� �ε��� ����
            playerObject = Instantiate(playerObject, pos);

            playerObject.AddComponent<CDiffPlayer>();
            playerObject.GetComponent<CDiffPlayer>().getPlayer(playerObject, player);

            if (playerObject == null)
            {
                Debug.Log("DiffPlayer ���� ����");
            }
            else
            {
                player_list.Add(playerObject);
            }
        }

    }
    public void playerUpdate(string player_index, CPacket direction, int is_stop)
    {
        foreach (GameObject player in player_list)
        {
            if (player.GetComponent<CDiffPlayer>().player_index == player_index)
            {
                //�ִϸ��̼ǵ� �������!
                if (is_stop == 0)
                {
                    float h = direction.pop_float();
                    float v = direction.pop_float();
                    float speed = direction.pop_float();

                    float rx = direction.pop_float();
                    float ry = direction.pop_float();
                    float rz = direction.pop_float();

                    Vector3 velocity = new Vector3(h, 0, v);
                    velocity = velocity.normalized;

                    Vector3 rotation = new Vector3(rx, ry, rz);

                    player.GetComponent<CDiffPlayer>().moving(velocity, rotation, speed);
                }

                if (is_stop == 1)
                {
                    float tX = direction.pop_float();
                    float tZ = direction.pop_float();

                    float rX = direction.pop_float();
                    float rY = direction.pop_float();
                    float rZ = direction.pop_float();

                    player.GetComponent<CDiffPlayer>().stop(tX, tZ, rX, rY, rZ);
                }
            }
        }


    }
    //�߰� �� �Ǵ��� Ȯ���ϰ� �ٸ� �÷��̾� ������ ��ġ��
    public void addPlayer(string player_index)
    {
        GameObject playerObject = Resources.Load("ChaWitch") as GameObject;

        Transform pos = new GameObject().GetComponent<Transform>();
        pos.position = new Vector3(-13, 0, 8);

        // �÷��̾� ���� �� ���ӿ�����Ʈ�� �ε��� ����
        // ���� ��ġ ��������
        playerObject = Instantiate(playerObject, pos);

        playerObject.AddComponent<CDiffPlayer>();
        playerObject.GetComponent<CDiffPlayer>().getPlayer(playerObject, player_index);

        lock (player_list)
        {
            if (playerObject == null)
            {
                Debug.Log("DiffPlayer ���� ����");
            }
            else
            {
                player_list.Add(playerObject);
            }
        }
        // ���� �Ϸ� ack �ؾ��ұ�?

    }
    public void removePlayer(string player_index)
    {
        GameObject remove_player = new GameObject();

        lock (player_list)
        {
            foreach (GameObject player in player_list)
            {
                if (player.GetComponent<CDiffPlayer>().player_index == player_index)
                {
                    player.GetComponent<CDiffPlayer>().Remove();

                    remove_player = player;
                }
            }
        }
        player_list.Remove(remove_player);
    }
    public void playerAttack(string player_index)
    {
        foreach (GameObject player in player_list)
        {
            if (player.GetComponent<CDiffPlayer>().player_index == player_index)
            {
                player.GetComponent<CDiffPlayer>().attack();
            }
        }
    }
}

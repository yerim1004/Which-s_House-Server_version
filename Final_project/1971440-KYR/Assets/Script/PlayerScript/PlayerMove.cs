using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using serverModule;
using WHServer;

public class PlayerMove : MonoBehaviour
{
    private CPlayer player;

    private Animator m_animator;

    public GameObject energy; //발사체
    public Transform spawnpos;
    private string player_index;
    private Transform collectArea;
    private CollectArea collectScript;

    private GameObject inven;
    public ItemData collectItem;
    private static List<ItemInfo> itemlist; // = new List<ItemInfo>();
    private GameController GM = new GameController();

    void Start()
    {
        inven = GameObject.Find("InventoryUI");
        m_animator = GetComponent<Animator>();
        
        energy = Resources.Load("Prefabs/Effect/AttackEffect") as GameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && collectScript.collectAble) //채집가능 대상이 범위에 있으면서 G키를 누르면 채집
        {
            Collect();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }
    }
    public void init(CPlayer player, GameObject playerObj, string player_index)
    {
        this.player = player;

        spawnpos = playerObj.transform.Find("AttackArea");
        collectArea = playerObj.transform.Find("CollectArea");
        collectScript = collectArea.GetComponent<CollectArea>();

        this.player_index = player_index;
    }
    public void Collect()
    {
        m_animator.SetTrigger("Attack"); //채집과 사냥 모두 Attack 모션으로 통일

        if(collectScript.collect != null)
        {
            collectScript.collect.GetComponentInParent<CollectCreate>().StartCoroutine("ReCreate");
            GM.ItemAdd(collectItem, 1);

            Destroy(collectScript.collect);
            collectScript.collect.GetComponent<CollectEffect>().PlayEffect();
            collectScript.collect = null;
        }
        
    }
    private void attack()
    {
        CPacket msg = CPacket.create((short)PROTOCOL.PLAYER_ATK_REQ);
        msg.push(player_index);

        player.msgSend(msg);

        m_animator.SetTrigger("Attack");
        Instantiate(energy, spawnpos.position, spawnpos.rotation);
    }
    public void setItemlist()
    {
        Dictionary dict = new Dictionary();
        itemlist = dict.itemlist;
    }
 }

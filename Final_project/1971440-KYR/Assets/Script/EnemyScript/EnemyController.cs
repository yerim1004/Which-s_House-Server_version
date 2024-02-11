using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private int hp = 3;
    public GameObject destroyEffectPrefab;
    private GameObject inven;
    public ItemData rabbitItem;
    
    private Animator m_animator;

    private int rnd;
    private int ran_rotate;
    private float time = 0.0f;
    private float waitingTime = 2.0f;
    private bool isdead = false;

    private GameController GM = new GameController();
    void Start()
    {
        inven = GameObject.Find("InventoryUI");
        m_animator = GetComponent<Animator>();
        InvokeRepeating("setPlay", 0.5f, 2.5f);
    }

    private void Update()
    {
        
    }
    public void Damage()
    {
        hp--;
       
        if (hp <= 0)
        {
            m_animator.SetTrigger("Dead");
            isdead = true;
            //inven.transform.GetChild(0).GetComponent<ItemSave>().AddItem(rabbitItem, 1);
            GM.ItemAdd(rabbitItem, 1);
            

            Destroy(gameObject, 4);
        }
    }
    public int GetHP()
    {
        return hp;
    }
    public void PlayEffect()
    {
        Instantiate(destroyEffectPrefab, transform.localPosition, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        //트리거 공격 받으면 damage호출
        if (other.gameObject.tag == "Energy")
        {
            Damage();
            PlayEffect();
        }
    }
    private void Move(int rotate)
    {
        gameObject.transform.Rotate(new Vector3(0, 30 * rotate, 0));
        m_animator.SetFloat("Move", 1.0f);
    }
    private void Idle()
    {
        m_animator.SetFloat("Move", 0.0f);
    }

    private void setPlay()
    {
        rnd = Random.Range(0, 2); //적은 2초마다 랜덤으로 이동하기
        ran_rotate = Random.Range(0, 12);
        
        //랜덤으로 360도/30x12
        if (rnd == 0)
            Move(ran_rotate);
        else if (rnd == 1)
            Idle();
    }
}

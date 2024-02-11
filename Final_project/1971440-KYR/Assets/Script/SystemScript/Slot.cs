using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    [SerializeField] private Text text_count;
    [SerializeField] private GameObject go_CountImage;
    private GameObject m_Update;
    private ItemSave parent;

    private ItemData _item;
    public int itemcount = 1;
    public int myindex;
    public void Start()
    {
        m_Update = GameObject.Find("InventoryUI");
        parent = m_Update.transform.GetChild(0).GetComponent<ItemSave>();
    }
    public ItemData item
    {
        get { return _item; }
        set
        {
            _item = value;
            if(_item != null)
            {
                image.sprite = _item.itemImg;
                image.color = new Color(1, 1, 1, 1);
                go_CountImage.SetActive(true);
                text_count.text = itemcount.ToString();
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                go_CountImage.SetActive(false);
            }
        }
    }
    public void SetSlotCount(int _count)
    {
        itemcount += _count;

        if(itemcount <= 0)
        {
            DeleteSlot();
        }

        text_count.text = itemcount.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
               
                // 소비
                if (m_Update)
                {
                    m_Update.transform.GetChild(1).GetComponent<MonsterMake>().AddSlot(item, 1); //chile 0은 인벤토리
                }
                else return;

                SetSlotCount(-1);
            }
        }
    }
    void DeleteSlot() {
        image.sprite = null;
        image.color = new Color(1, 1, 1, 0);
        go_CountImage.SetActive(false);
        text_count.text = null;

        parent.SlotUpdate(myindex);
    }

}

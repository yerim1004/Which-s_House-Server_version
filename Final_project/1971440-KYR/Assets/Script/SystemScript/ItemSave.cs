using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSave : MonoBehaviour //인벤토리 스크립트
{
    public List<ItemData> items;//플레이어가 사용해야 하니까 public 

    public List<ItemData> invenlist
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
        }
    }

    //[SerializeField] private Transform slotParent;
    public Transform slotParent;
    //[SerializeField] private Slot[] slots;
    public Slot[] slots;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void SlotSetting()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = null;
            slots[i].myindex = i;
            
        }
    }
    public void AddItem(ItemData _item, int _count)
    {
        List<ItemData> mylist = invenlist;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                mylist.Add(_item);
                invenlist = mylist;

                slots[i].itemcount = _count;
                slots[i].item = items[i];
                return;
            }
        }
    }

    public void SlotUpdate(int index)
    {
        slots[index].item = null;

        items.RemoveAt(index);
    }
   
}

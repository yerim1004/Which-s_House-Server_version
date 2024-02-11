using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //private static GameObject player;
    private static GameObject inven;
    private static List<ItemInfo> itemlist;
    void Start()
    {
        inven = GameObject.Find("InventoryUI");

        ItemSave slot = inven.transform.GetChild(0).GetComponent<ItemSave>();
        Debug.Log(slot.slotParent);
        slot.SlotSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ItemAdd(ItemData item, int num)
    {
        inven.transform.GetChild(0).GetComponent<ItemSave>().AddItem(item, num);

        ItemInfo findItem = itemlist.Find(a => a.Name == item.itemName);

        if (findItem.GetFlag == false)
        {
            findItem.GetFlag = true;

            DictionaryItem dictitem = new DictionaryItem();
            dictitem.rePaint(findItem.Name);
        }

    }

    public void SetItemList(List<ItemInfo> list)
    {
        itemlist = list;
    }
}

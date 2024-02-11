using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMake : MonoBehaviour
{
    
    public List<ItemData> _items;
    
    [SerializeField] private Transform slotParent;
    [SerializeField] private Transform mslotParent;
    [SerializeField] private Slot[] slots;
    [SerializeField] private Slot[] makeslots;

    private string recipt1 = "tomato";
    private string recipt2 = "r_meet";

    public GameObject UI;
    public Transform success;
    public GameObject madeObj;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        makeslots = mslotParent.GetComponentsInChildren<Slot>();
    }


    void Start()
    {
        updateSlot();
    }

    void Update()
    {
        
    }
    
    public void updateSlot()
    {
        int i = 0;
        
        for (; i < _items.Count && i < makeslots.Length; i++)
        {
            makeslots[i].item = _items[i];
            
        }
        for (; i < makeslots.Length; i++)
        {
            makeslots[i].item = null;
        }
    }
    public void AddSlot(ItemData _item, int _count)
    {
        for (int i = 0; i < makeslots.Length; i++)
        {
            if (makeslots[i].item != null) 
            {
                if (makeslots[i].item.itemName == _item.itemName)
                {
                    makeslots[i].SetSlotCount(_count);
                    
                    updateSlot();
                    return;
                }
            }
        }
        for (int i = 0; i < makeslots.Length; i++)
        {
            if (makeslots[i].item == null)
            {
                _items.Add(_item);
                makeslots[i].itemcount = _count;
                
                updateSlot();
                return;
            }
        }
    }
    public void makeMonster() {
        for (int i = 0; i < makeslots.Length; i++)
        {
            if (makeslots[i].item != null)
            {
                if (makeslots[i].item.itemName == recipt1)
                {
                    for (int j = 0; j < makeslots.Length; j++)
                    {
                        if (makeslots[j].item != null)
                        {
                            if (makeslots[j].item.itemName == recipt2)
                            {
                                Debug.Log("생성 완료!!");
                                
                                UI.SetActive(true); //성공 알림 패널
                                
                                Instantiate(madeObj, success.position, success.rotation);
                                
                                return;
                            }
                        }
                    }
                    return;
                }
            }
        }
    }
}

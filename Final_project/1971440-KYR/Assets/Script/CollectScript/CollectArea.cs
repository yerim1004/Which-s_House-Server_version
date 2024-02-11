using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectArea : MonoBehaviour
{
    //플레이어에게 적용할 스크립트
    
    public bool collectAble;
    public GameObject collect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectableObject"))
        {
            
            collectAble = true;
            collect = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CollectableObject"))
        {
            //colliders.Remove(other);
            //Debug.Log("out");
            collectAble = false;
            collect = null;
        }
    }
    void Start()
    {

    }
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCreate : MonoBehaviour
{
    public Transform pos;
    public GameObject collect_model;
    private GameObject collect;

    float waitingTime = 5.0f;
    private bool destroy = false;
    

    void Start()
    {
        pos = gameObject.transform;
        StartCreate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ReCreate()
    {
        yield return new WaitForSeconds(5.0f); //5초 기다리고 인스턴스 생성
        
        StartCreate();
    }

    void StartCreate()
    {
        collect = Instantiate(collect_model, pos.position, pos.rotation);
        //부모를 지정해서 다른 스크립트에서 해당 스크립스 변수 참조를 가능하게 함
        collect.transform.SetParent(pos, false); //true는 월드기준, false는 부모기준 위치 지정
    }
}

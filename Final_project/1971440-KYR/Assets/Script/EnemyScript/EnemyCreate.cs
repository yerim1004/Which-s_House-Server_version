using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    public Transform pos;
    public GameObject enemy;

    float timer;
    int waitingTime = 8;
    void Start()
    {
        pos = gameObject.transform;
        timer = 0.0f;
        Instantiate(enemy, pos.position, pos.rotation);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > waitingTime && enemy == false)
        {
            Instantiate(enemy, pos.position, pos.rotation);
        }
    }
}

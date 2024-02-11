using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed = 200.0f;
    private Transform thisTransform;
    private float time = 0.0f;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        ShootEnergy();
    }
    private void Update()
    {
        time = time + Time.deltaTime;
        if (time > 2.0f)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void ShootEnergy()
    {
        GetComponent<Rigidbody>().AddForce(thisTransform.forward * speed);
    }
}

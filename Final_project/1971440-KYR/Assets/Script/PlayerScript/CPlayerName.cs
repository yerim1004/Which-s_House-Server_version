using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerName : MonoBehaviour
{
    public GameObject camera;
    void Start()
    {
        camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}

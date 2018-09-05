using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    GameObject[] platforms;

    void Start()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform");
    }

    void FixedUpdate()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platf in platforms)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),platf.GetComponent<Collider2D>());
        }
    }
}


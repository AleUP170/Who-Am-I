using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColl : MonoBehaviour {
    GameObject plat;

    void Start ()
    {
        plat = GameObject.Find("Platform");
    }

	void Update () {
        Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());
    }
}

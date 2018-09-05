using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{

    float speed;
    public int direction;
    int counter;
    public bool caught;
    GameObject[] ground;

    void Start()
    {
        caught = false;
        speed = 0.25f;
        counter = 8;
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector2(speed * direction * Mathf.Sign(counter), 0));
        counter--;


        if (counter < -9)
        {
            if (caught)
            {
                transform.parent.gameObject.GetComponent<PlayerMov>().caught = true;
            }
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}

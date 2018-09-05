using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public int direction;
    Rigidbody2D rigid;
    int speed;

    void Start()
    {
        speed = 12;
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0.00f;
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(speed * direction, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Block")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}

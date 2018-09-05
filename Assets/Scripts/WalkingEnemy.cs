using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour {

    float moveSpeed = 20.00f;
    float maxSpeed = 2.00f;
    public int direction;
    int jumpStored;
    public int maxJumps;
    Vector2 jump = new Vector2(0, 1);
    float jumpHeightForce = 740.00f;
    Rigidbody2D rigid;
    Vector2 movement = new Vector2(1, 0);
    bool jumping;
    public bool turns;

    void Start()
    {
        jumpStored = 0;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Camina IA
    void FixedUpdate()
    {
        rigid.AddForce(movement * moveSpeed * direction);
        rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, maxSpeed * -1, maxSpeed), rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            rigid.velocity = new Vector2();
            direction *= -1;
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            jumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "JumpTrigger")
        {
            jumpStored = maxJumps;
        }

        if (collision.gameObject.tag == "JumpActivator" && jumpStored > 0 && !jumping)
        {
            jumpStored--;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(jump * jumpHeightForce);
        }

        if (collision.gameObject.tag == "Tongue")
        {
            transform.parent = collision.gameObject.transform;
            transform.position = transform.parent.transform.position;
            transform.parent.gameObject.GetComponent<Tongue>().caught = true;
            Destroy(rigid);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this);
        }

        if (collision.gameObject.tag == "Border" && turns)
        {
            rigid.velocity = new Vector2();
            direction *= -1;
        }
    }

}

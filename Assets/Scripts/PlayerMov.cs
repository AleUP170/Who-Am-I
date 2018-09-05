using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMov : MonoBehaviour {

    float jumpHeightForce = 730.00f;
    float moveSpeed = 20.00f;
    float turnSpeed = 1.0f;
    float maxSpeed = 8.00f;
    float jumpBuffer;
    bool jumping;
    public bool caught;
    int direction;
    int cooldown;
    GameObject[] platforms;
    Vector2 jump = new Vector2(0, 1);
    Vector2 movement = new Vector2(1, 0);
    Rigidbody2D rigid;
    private Animator anim;
    public GameObject Tongue;
    public GameObject bullet;
    GameObject tongue;
    GameObject newBullet;


    void Start () {
        caught = false;
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumping = true;
        jumpBuffer = 0.00f;
        cooldown = 0;
    }

    private void Update()
    {
        
    }

    void FixedUpdate() {
        anim.SetBool("corriendo", false);
        // Buffer para el salto
        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            jumpBuffer = 2.00f;
        }
        else if (jumpBuffer > 0.00f)
        {
            jumpBuffer -= 0.25f;
        }
        // --------------------

        if ((Input.GetKeyDown(KeyCode.Z) == true || jumpBuffer > 0) && jumping == false) // Salto
        {
            jumping = true;
            jumpBuffer = 0.00f;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(jump * jumpHeightForce);
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true) // Movimiento izquierda
        {
            anim.SetBool("corriendo", true);
            turnSpeed = (rigid.velocity.x > 0) ? 2.50f : 1.00f;
            anim.SetFloat("velocidad", turnSpeed);
            rigid.AddForce(movement * moveSpeed * -1 * turnSpeed);
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, maxSpeed * -1, maxSpeed), rigid.velocity.y);
            direction = -1;
            transform.localScale = new Vector3(-1.75f, 1.75f, 1.75f);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true) // Movimiento derecha
        {
            anim.SetBool("corriendo", true);
            turnSpeed = (rigid.velocity.x < 0) ? 2.50f : 1.00f;
            anim.SetFloat("velocidad", turnSpeed);
            rigid.AddForce(movement * moveSpeed * turnSpeed);
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, maxSpeed * -1, maxSpeed), rigid.velocity.y);
            direction = 1;
            transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
        }

        // Fricción horizontal
        if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) == false && jumping == false) // En suelo
        {
            Vector2 velocity = Vector2.zero;
            rigid.velocity = Vector2.SmoothDamp(rigid.velocity, new Vector2(0, rigid.velocity.y), ref velocity, 0.03f, 200.0f, Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) == false && jumping == true) // En aire
        {
            Vector2 velocity = Vector2.zero;
            rigid.velocity = Vector2.SmoothDamp(rigid.velocity, new Vector2(0, rigid.velocity.y), ref velocity, 0.1f, 20.0f, Time.deltaTime);
        }
        // --------------------

        // Drop de plataforma
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            foreach (GameObject platf in platforms)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platf.GetComponent<Collider2D>());
            }
        } 
        else
        {
            foreach (GameObject platf in platforms)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platf.GetComponent<Collider2D>(), !jumping);
            }
        }
        // -----------------------

        // Ataque 
        if (Input.GetKeyDown(KeyCode.X) == true)
        {
            if(cooldown == 0)
            {
                tongue = Instantiate(Tongue, transform.position + new Vector3(0.20f * direction, 0, 0), transform.rotation);
                tongue.GetComponent<Transform>().parent = transform;
                tongue.GetComponent<Tongue>().direction = direction;
                cooldown = 25;
            }
            // Escupir
            if (caught)
            {
                cooldown = 10;
                Escupir();
                caught = false;
            }
        }
        // Enfriamento 
        if (cooldown > 0)
        {
            cooldown--;
        }
        
        if (caught)
        {
            cooldown = 10;
        }
        // ------------------------
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            jumping = false;
        }

        if (collision.gameObject.tag == "Ramp")
        {
            jumping = false;
            rigid.gravityScale = 0.00f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            jumping = true;
        }

        if (collision.gameObject.tag == "Ramp")
        {
            jumping = true;
            rigid.gravityScale = 1.00f;
        }
    }

    //Disparo
    int Escupir()
    {
        newBullet = Instantiate(bullet, transform.position + new Vector3(0.50f * direction, 0, 0), transform.rotation);
        newBullet.GetComponent<PlayerBullet>().direction = direction;
        return (50);
    }
}

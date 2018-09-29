using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    public int lives = 3;
    public float speed;
    public Rigidbody2D heroRigidBody;
    public float jumpForce;
    public bool onAir = false;

    // Use this for initialization
    void Start () {
        heroRigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    void Jump()
    {
        if (onAir) return;
        heroRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        Vector2 move;

        move.x = horizontal * speed;
        move.y = heroRigidBody.velocity.y;

        heroRigidBody.velocity = move;

        //anim.SetFloat("WalkSpeed", Mathf.Abs(horizontal));

        //if (Input.GetButtonDown("swordAttack"))
        //{
        //    anim.Play("Hit");
        //    lives -= 1;
        //    anim.SetInteger("Life", lives);
        //    if (lives <= 0)
        //    {
        //        anim.Play("Die");
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            print("Should jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 8) return;
        onAir = false;
        transform.SetParent(collision.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 8) return;
        onAir = true;
        transform.SetParent(null);
    }
}

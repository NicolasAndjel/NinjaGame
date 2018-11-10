using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderBody : MonoBehaviour {

    public ShredderBrain shredderBrain;
    public Animator animator;
    public int life = 1;
    public float speed;

    public Rigidbody2D shredderRigidBody;
    public BoxCollider2D shredderCollider;

    public GameObject saiPrefab;
    public Transform spawnerR;
    public Transform spawnerL;

    GameObject swordHBRight;
    GameObject swordHBLeft;
    //bool swordAttacking;
    float swordTimer;

    bool shredderStun;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        shredderRigidBody = GetComponent<Rigidbody2D>();
        shredderBrain = GetComponent<ShredderBrain>();
        shredderCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        swordHBRight = transform.Find("swordHBRight").gameObject;
        swordHBLeft = transform.Find("swordHBLeft").gameObject;
        shredderStun = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 direction)
    {
        if (!shredderStun)
        {
            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (direction != Vector3.zero)
            {
                animator.SetFloat("speed", 1f);
                direction.Normalize();
                transform.position += speed * direction * Time.deltaTime;
            }
            else
            {
                animator.SetFloat("speed", 0f);
            }
        }
    }

    public void Attack()
    {
        //swordAttacking = true;
        animator.SetTrigger("Attack");
    }

    public void ActivateSwordHB()
    {

        if (GetComponent<SpriteRenderer>().flipX == false) swordHBRight.SetActive(true);
        else swordHBLeft.SetActive(true);
    }

    public void DeactivateSwordHB()
    {
        swordHBRight.SetActive(false);
        swordHBLeft.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (life > 0)
            {
                life--;
                animator.SetTrigger("hit");
                DamageFeedback(collision.gameObject.transform.position.x);
            }
            else if (life <= 0)
            {
                animator.SetTrigger("die");
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                shredderBrain.alive = false;
                shredderBrain.Invoke("Die", 1);
            }
        }
    }

    private void DamageFeedback(float heroPosition)
    {
        shredderStun = true;
        if (transform.position.x < heroPosition)
        {
            shredderRigidBody.AddForce(new Vector2(-150, 1));
        }
        else
        {
            shredderRigidBody.AddForce(new Vector2(150, 1));
        }
        Invoke("CanMoveAgain", 0.5f);
    }

    private void CanMoveAgain()
    {
        shredderStun = false;
    }

    public void SaiThrow()
    {
        animator.SetTrigger("Attack");
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            Invoke("ThrowRight", 0.1f);
        }
        else
        {
            Invoke("ThrowLeft", 0.1f);
        }
    }

    private void ThrowRight()
    {
        Instantiate(saiPrefab, spawnerR.transform.position, Quaternion.Euler(0, 0, 270));
    }
    private void ThrowLeft()
    {
        Instantiate(saiPrefab, spawnerL.transform.position, Quaternion.Euler(0, 0, 90));
    }
}

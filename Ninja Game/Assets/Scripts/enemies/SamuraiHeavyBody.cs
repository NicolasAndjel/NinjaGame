using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiHeavyBody : MonoBehaviour {

    GameManager gameManager;
    public SamuraiHeavyBrain samuraiBrain;
    public Animator animator;
    public int life = 1;
    public float speed;
    Vector3 moveDirection;

    public Rigidbody2D samuraiRigidBody;
    public BoxCollider2D samuraiCollider;

    public GameObject shurikenPrefab;
    public Transform spawnerR;
    public Transform spawnerL;

    GameObject swordHBRight;
    GameObject swordHBLeft;
    //bool swordAttacking;
    float swordTimer;

    bool samuraiStun;
    bool stepPlaying;



    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        samuraiRigidBody = GetComponent<Rigidbody2D>();
        samuraiBrain = GetComponent<SamuraiHeavyBrain>();
        samuraiCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        swordHBRight = transform.Find("swordHBRight").gameObject;
        swordHBLeft = transform.Find("swordHBLeft").gameObject;
        samuraiStun = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stepPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
        if (!samuraiStun)
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
                if (!stepPlaying)
                {
                    gameManager.source.PlayOneShot(gameManager.heroLand, Random.Range(0.5f, 1f));
                    stepPlaying = true;
                    Invoke("StepSound", 0.4f);
                }
            }
            else
            {
                animator.SetFloat("speed", 0f);
            }
        }
    }

    void StepSound()
    {
        stepPlaying = false;
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
            if (collision.gameObject.tag == "kunai")
            {
                gameManager.source.PlayOneShot(gameManager.kunaiDenied);
            }
            else
            {
                if (life > 0)
                {
                    life--;
                    animator.SetTrigger("hit");
                    gameManager.source.PlayOneShot(gameManager.samuraiHeavyHit);
                    DamageFeedback(collision.gameObject.transform.position.x);
                }
                else if (life <= 0)
                {
                    animator.SetTrigger("die");
                    gameManager.source.PlayOneShot(gameManager.samuraiHeavyDie);
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<Rigidbody2D>().gravityScale = 0;
                    samuraiBrain.alive = false;
                    samuraiBrain.Invoke("Die", 1);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            if (collision.gameObject.transform.position.x > transform.position.x)//si choca con objeto a su derecha
            {
                if (moveDirection.x > 0)
                {
                    samuraiStun = true;
                    animator.SetFloat("speed", 0f);
                }
                else samuraiStun = false;
            }
            else //si choca con objeto a su izquierda
            {
                if (moveDirection.x < 0)
                {
                    samuraiStun = true;
                    animator.SetFloat("speed", 0f);
                }
                else samuraiStun = false;
            }
        }
    }

    private void DamageFeedback(float heroPosition)
    {
        samuraiStun = true;
        if (transform.position.x < heroPosition)//collision.gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            samuraiRigidBody.AddForce(new Vector2(-150, 1));
        }
        else
        {
            samuraiRigidBody.AddForce(new Vector2(150, 1));
        }
        Invoke("CanMoveAgain", 0.5f);
    }

    private void CanMoveAgain()
    {
        samuraiStun = false;
    }
}

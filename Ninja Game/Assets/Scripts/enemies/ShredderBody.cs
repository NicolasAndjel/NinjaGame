using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderBody : MonoBehaviour {

    GameManager gameManager;
    public ShredderBrain shredderBrain;
    public Animator animator;
    public int life = 1;
    public float speed;
    Vector3 moveDirection;
    public DataHolder dataHolder;

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
    bool stepPlaying;


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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stepPlaying = false;
        dataHolder = GameObject.Find("dataHolder").GetComponent<DataHolder>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
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
                if (!stepPlaying)
                {
                    gameManager.source.PlayOneShot(gameManager.heroLand, Random.Range(0.5f, 1f));
                    stepPlaying = true;
                    Invoke("StepSound", 0.2f);
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
            if (life > 0)
            {
                life--;
                animator.SetTrigger("hit");
                gameManager.source.PlayOneShot(gameManager.shredderHit);
                DamageFeedback(collision.gameObject.transform.position.x);
            }
            else if (life <= 0)
            {
                animator.SetTrigger("die");
                gameManager.source.PlayOneShot(gameManager.shredderDie);
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                shredderBrain.alive = false;
                shredderBrain.Invoke("Die", 1);
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
                    shredderStun = true;
                    animator.SetFloat("speed", 0f);
                }
                else shredderStun = false;
            }
            else //si choca con objeto a su izquierda
            {
                if (moveDirection.x < 0)
                {
                    shredderStun = true;
                    animator.SetFloat("speed", 0f);
                }
                else shredderStun = false;
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
        gameManager.source.PlayOneShot(gameManager.saiThrow);
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

        GameObject sai = Instantiate(saiPrefab, spawnerR.transform.position, Quaternion.Euler(0, 0, 270));
        float saiSpeed = sai.GetComponent<DarkKunai>().speed;

        if (dataHolder.difficulty == "easy")
        {
            saiSpeed -= 2;
        }
        else if (dataHolder.difficulty == "hard")
        {
            saiSpeed += 2;
        }
    }

    private void ThrowLeft()
    {
        GameObject sai = Instantiate(saiPrefab, spawnerL.transform.position, Quaternion.Euler(0, 0, 90));
        float saiSpeed = sai.GetComponent<DarkKunai>().speed;

        if (dataHolder.difficulty == "easy")
        {
            saiSpeed -= 2;
        }
        else if (dataHolder.difficulty == "hard")
        {
            saiSpeed += 2;
        }
    }
}

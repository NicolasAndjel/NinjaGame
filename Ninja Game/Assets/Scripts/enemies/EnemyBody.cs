using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour {

    GameManager gameManager;
    public EnemyBrain enemyBrain;
    public Animator animator;
    public int life = 1;
    public float speed;

    public Rigidbody2D enemyRigidBody;
    public BoxCollider2D enemyCollider;

    public GameObject shurikenPrefab;
    public Transform spawnerR;
    public Transform spawnerL;

    GameObject swordHBRight;
    GameObject swordHBLeft;
    //bool swordAttacking;
    float swordTimer;

    bool enemyStun;
    bool stepPlaying;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemyCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        swordHBRight = transform.Find("swordHBRight").gameObject;
        swordHBLeft = transform.Find("swordHBLeft").gameObject;
        enemyStun = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stepPlaying = false;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

        public void Move(Vector3 direction)
    {
        if (!enemyStun)
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
                gameManager.source.PlayOneShot(gameManager.samuraiLightHit);
                DamageFeedback(collision.gameObject.transform.position.x);
            }
            else if (life <= 0)
            {
                animator.SetTrigger("die");
                gameManager.source.PlayOneShot(gameManager.samuraiLightDie);
                print("Está muerto");
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                enemyBrain.alive = false;
                enemyBrain.Invoke("Die", 1);
            }
        }
    }

    private void DamageFeedback(float heroPosition)
    {
        enemyStun = true;
        if (transform.position.x < heroPosition)
        {
            enemyRigidBody.AddForce(new Vector2(-150, 1));
        }
        else
        {
            enemyRigidBody.AddForce(new Vector2(150, 1));
        }
        Invoke("CanMoveAgain", 0.5f);
    }

    private void CanMoveAgain()
    {
        enemyStun = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour {

    HeroBody hero;
    public EnemyBrain enemyBrain;
    public Animator animator;
    public int lives = 3;
    public float speed;

    public Rigidbody2D enemyRigidBody;
    public BoxCollider2D enemyCollider;

    public GameObject shurikenPrefab;
    public Transform spawnerR;
    public Transform spawnerL;

    GameObject swordHBRight;
    GameObject swordHBLeft;
    bool swordAttacking;
    float swordTimer;



    // Use this for initialization
    void Start () {
        hero = GameObject.Find("hero").GetComponent<HeroBody>();
        animator = GetComponent<Animator>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemyCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        swordHBRight = transform.Find("swordHBRight").gameObject;
        swordHBLeft = transform.Find("swordHBLeft").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Move(Vector3 direction)
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

    public void Attack()
    {
        swordAttacking = true;
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
            print("hit");
            animator.SetTrigger("hit");
        }
    }
}

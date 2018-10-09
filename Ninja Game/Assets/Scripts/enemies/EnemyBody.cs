﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour {

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
                //enemyCollider.enabled = !enemyCollider.enabled;
                enemyBrain.alive = false;
                enemyBrain.Invoke("Die", 1);
            }
        }
    }

    private void DamageFeedback(float heroPosition)
    {
        enemyStun = true;
        if (transform.position.x < heroPosition)//collision.gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            enemyRigidBody.AddForce(new Vector2(-150, 1));
            print("Heroe Pega desde la derecha");
        }
        else
        {
            enemyRigidBody.AddForce(new Vector2(150, 1));
            print("Heroe Pega desde la izquierda");
        }
        Invoke("CanMoveAgain", 0.5f);
    }

    private void CanMoveAgain()
    {
        enemyStun = false;
    }
}
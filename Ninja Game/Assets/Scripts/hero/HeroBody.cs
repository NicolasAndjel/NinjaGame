﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBody : MonoBehaviour {

    GameManager gameManager;
    public int life = 3;
    public float speed;
    public Rigidbody2D heroRigidBody;
    SpriteRenderer sprite;
    public float jumpForce;
    float directionOnAir;
    public float slideForce;
    bool sliding;
    float slideTimer;
    public bool onAir = false;

    public Animator animator;
    public GameObject kunaiPrefab;

    public HeroBrain heroBrain;
    public BoxCollider2D heroCollider;

    public Transform spawnerR;
    public Transform spawnerL;

    GameObject swordHBRight;
    GameObject swordHBLeft;

    bool swordAttacking;
    float swordTimer;
    bool canTakeDamage;
    float invulnerableTime;

    // Use this for initialization
    void Start () {
        heroRigidBody = GetComponent<Rigidbody2D>();
        heroBrain = GetComponent<HeroBrain>();
        heroCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        swordHBRight = transform.Find("swordHBRight").gameObject;
        swordHBLeft = transform.Find("swordHBLeft").gameObject;
        sprite = GetComponent<SpriteRenderer>();
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update () {
        if (onAir)
        {
            if (heroRigidBody.velocity.y > 0)
            {
                animator.SetInteger("SpeedY", 1);
            }
            else if (heroRigidBody.velocity.y < 0)
            {
                animator.SetInteger("SpeedY", -1);
            }
        }
        if (sliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer > 0.4f)
            {
                slideTimer = 0;
                heroCollider.size *= 1.5f;
                sliding = false;
            }
        }
        if (swordAttacking)
        {
            swordTimer += Time.deltaTime;
            if (swordTimer > 0.2f)
            {
                swordTimer = 0;
                DeactivateSwordHB();
                swordAttacking = false;
            }
        }
        if (!canTakeDamage)
        {
            invulnerableTime += Time.deltaTime;
            Color blinkAlpha = sprite.material.color;
            if ((int)invulnerableTime % 2 == 0) // esto no está funcionando!! Talvez mandando parámetro al animator??
            {
                blinkAlpha.a = 0.3f + Mathf.PingPong(Time.deltaTime, 0.7f);
            }
            //    sprite.color = Color.white;
            //    print("rojo");
            //}

            //else
            //{
            //    sprite.color = Color.red;
            //    print("blanco");
            //}
        }
    }

    public void Jump(float direction)
    {
        if (onAir) return;
        heroRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        heroRigidBody.AddForce(Vector2.right * direction, ForceMode2D.Impulse);
        directionOnAir = direction;
    }
        


    public void Move(float direction)
    {
        if (direction < 0)
        {
            sprite.flipX = true;
        }
        else if (direction > 0)
        {
            sprite.flipX = false;
        }
        animator.SetFloat("WalkSpeed", Mathf.Abs(direction));

        if (onAir)
        {
            if (direction > 0 && directionOnAir < 0)
            {
                heroRigidBody.AddForce(Vector2.right * direction / 3, ForceMode2D.Impulse);
            }
            else if (direction < 0 && directionOnAir > 0)
            {
                heroRigidBody.AddForce(Vector2.right * direction / 3, ForceMode2D.Impulse);
            }
        }
        else
        {
            Vector3 heroSpeed = new Vector3(direction * speed, heroRigidBody.velocity.y, 0);
            heroRigidBody.velocity = heroSpeed;
        }
        
    }
    
    public void Slide(float slideDir)
    {
        if (onAir) return;
        animator.SetTrigger("slide");
        heroRigidBody.AddForce(new Vector2(slideDir, 0) * slideForce, ForceMode2D.Impulse);
        heroCollider.size /= 1.5f;
        sliding = true;
      
        
    }

    public void SwordAttack()
    {
        if (onAir) animator.SetTrigger("airAttack");
        else animator.SetTrigger("groundAttack");
        swordAttacking = true;
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

    public void KunaiThrow()
    {
        animator.SetTrigger("throw");
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            Invoke("ThrowRight", 0.1f);
        }
        else
        {
            Invoke("ThrowLeft", 0.1f);
        }            
    }

    public void AirKunaiThrow()
    {
        animator.SetTrigger("airThrow");
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            Instantiate(kunaiPrefab, spawnerR.transform.position, Quaternion.Euler(0, 0, 250));
        }
        else
        {
            Instantiate(kunaiPrefab, spawnerL.transform.position, Quaternion.Euler(0, 0, 110));
        }
    }

    private void ThrowRight()
    {
        Instantiate(kunaiPrefab, spawnerR.transform.position, Quaternion.Euler(0, 0, 270));
    }
    private void ThrowLeft()
    {
        Instantiate(kunaiPrefab, spawnerL.transform.position, Quaternion.Euler(0, 0, 90));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 8)
        {
            onAir = false;
            transform.SetParent(collision.transform);
            animator.SetInteger("SpeedY", 0);
        }
        if (canTakeDamage)
        {
            if (collision.gameObject.layer == 11)
            {
                TakingDamage(collision.gameObject.transform.position.x);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            onAir = true;
            transform.SetParent(null);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (canTakeDamage)
            {
                TakingDamage(collision.gameObject.transform.position.x);
            }
        }
    }

    private void TakingDamage(float enemyPosition)
    {
        if (life > 1)
        {
            animator.SetTrigger("hit");
            animator.SetFloat("WalkSpeed", 0);
            life--;
            heroBrain.TakingDamage();
            canTakeDamage = false;
            Invoke("CanTakeDamage", 0.5f);
            if (transform.position.x < enemyPosition)
            {
                heroRigidBody.AddForce(new Vector2(-150, 1));
            }
            else
            {
                heroRigidBody.AddForce(new Vector2(150, 1));
            }


        }
        else if (life <= 1)
        {
            animator.SetTrigger("die");
            //enemyCollider.enabled = !enemyCollider.enabled;
            gameManager.Invoke("Loose", 1);
            heroBrain.Invoke("Dead", 0);
        }
    }


    private void CanTakeDamage()
    {
        canTakeDamage = true;
    }
}
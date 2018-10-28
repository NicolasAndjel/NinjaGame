using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBody : MonoBehaviour {

    GameManager gameManager;
    public int life = 3;
    public float speed;
    public Rigidbody2D bossRigidBody;
    SpriteRenderer sprite;
    public float jumpForce;
    float directionOnAir;
    public float slideForce;
    bool sliding;
    float slideTimer;
    public bool onAir = false;

    public Animator animator;
    public GameObject kunaiPrefab;

    public BossBrain bossBrain;
    public BoxCollider2D heroCollider;

    public Transform spawnerR;
    public Transform spawnerL;

    GameObject swordHBRight;
    GameObject swordHBLeft;

    bool swordAttacking;
    float swordTimer;
    bool canTakeDamage;

    // Use this for initialization
    void Start () {
        bossRigidBody = GetComponent<Rigidbody2D>();
        bossBrain = GetComponent<BossBrain>();
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
            if (bossRigidBody.velocity.y > 0)
            {
                animator.SetInteger("SpeedY", 1);
            }
            else if (bossRigidBody.velocity.y < 0)
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
    }

    public void Jump(float direction)
    {
        if (onAir) return;
        bossRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        bossRigidBody.AddForce(Vector2.right * direction, ForceMode2D.Impulse);
        directionOnAir = direction;
    }

    public void Move(float direction)
    {
        animator.SetFloat("WalkSpeed", Mathf.Abs(direction));

        if (onAir)
        {
            if (direction > 0 && directionOnAir < 0)
            {
                bossRigidBody.AddForce(Vector2.right * direction / 3, ForceMode2D.Impulse);
            }
            else if (direction < 0 && directionOnAir > 0)
            {
                bossRigidBody.AddForce(Vector2.right * direction / 3, ForceMode2D.Impulse);
            }
        }
        else
        {
            Vector3 bossSpeed;
            if (direction < 0)
            {
                sprite.flipX = true;
                bossSpeed = new Vector3(-1 * speed, bossRigidBody.velocity.y, 0);
                bossRigidBody.velocity = bossSpeed;
            }
            else if (direction > 0)
            {
                sprite.flipX = false;
                bossSpeed = new Vector3(1 * speed, bossRigidBody.velocity.y, 0);
                bossRigidBody.velocity = bossSpeed;
            }
            
            
        }

    }

    public void Slide(float slideDir)
    {
        if (onAir) return;
        animator.SetTrigger("slide");
        bossRigidBody.AddForce(new Vector2(slideDir, 0) * slideForce, ForceMode2D.Impulse);
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
            if (collision.gameObject.layer == 9)
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
        if (collision.gameObject.layer == 9)
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
            bossBrain.TakingDamage();
            canTakeDamage = false;
            Invoke("CanTakeDamage", 0.5f);
            if (transform.position.x < enemyPosition)
            {
                bossRigidBody.AddForce(new Vector2(-150, 1));
            }
            else
            {
                bossRigidBody.AddForce(new Vector2(150, 1));
            }


        }
        else if (life <= 1)
        {
            animator.SetTrigger("die");
            //enemyCollider.enabled = !enemyCollider.enabled;
            gameManager.Invoke("Win", 1);
        }
    }


    private void CanTakeDamage()
    {
        canTakeDamage = true;
    }
}

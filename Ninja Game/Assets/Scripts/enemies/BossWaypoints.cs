using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaypoints : MonoBehaviour
{
    public HeroBody hero;
    BossBrain bossBrain;
    public Transform[] waypoints;
    public enum States { MOVE, STAY };
    public States currentState;

    public float speed;

    public int currentWp;
    public int direction;
    public Animator animator;
    SpriteRenderer sprite;

    public AudioClip bossFight;

    public bool battleOn;

    // Use this for initialization
    void Start()
    {
        direction = -1;
        currentState = States.STAY;
        bossBrain = GetComponent<BossBrain>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        battleOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (battleOn)
        {
            //gameManager.BossMusic();
            Vector3 distance = waypoints[currentWp].position - transform.position;
            if (currentState == States.STAY)
            {
                animator.SetFloat("WalkSpeed", 0);
                bossBrain.ShootDarkKunai();
                Invoke("KeepMoving", 3);
            }
            else
            {
                if (distance.magnitude > speed * Time.deltaTime)
                {
                    transform.position += distance.normalized * speed * Time.deltaTime;
                    //bossBody.SwordAttack();
                    animator.SetFloat("WalkSpeed", 1);
                }
                else
                {
                    transform.position = waypoints[currentWp].position;
                    sprite.flipX = !sprite.flipX;
                    animator.SetFloat("WalkSpeed", 0);
                    direction *= -1;
                    currentWp += direction;
                    currentState = States.STAY;
                }
            }
        } 
    }

    void KeepMoving()
    {
        currentState = States.MOVE;
    }

    public void BattleOn()
    {
        battleOn = true;
    }
}

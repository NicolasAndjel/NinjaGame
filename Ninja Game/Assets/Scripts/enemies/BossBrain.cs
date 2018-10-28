using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : MonoBehaviour {

    HeroBody hero;
    BossBody bossBody;
    BossWaypoints waypoints;
    float direction;
    bool canAttack;
    bool canShoot;
    bool takingDamage;
    float stunTimer;
    public float stunTime;
    public Transform leftWP;
    public Transform rightWP;
    bool leftPosition;

    // Use this for initialization
    void Start () {
        hero = GameObject.Find("hero").GetComponent<HeroBody>();
        bossBody = GetComponent<BossBody>();
        canAttack = true;
        canShoot = true;
        waypoints = GetComponent<BossWaypoints>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!takingDamage)
        {
            CheckHero();
        }
        else
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunTime)
            {
                takingDamage = false;
                stunTimer = 0;
            }
        }

    }

    void CheckHero()
    {
        direction = hero.transform.position.x - bossBody.transform.position.x;

        float distanceToHero = Vector3.Distance(hero.transform.position, bossBody.transform.position);
        if (distanceToHero < 14)
        {
            waypoints.Invoke ("BattleOn", 1.5f); 
        }

        if (distanceToHero < 3f)
        {
            if (canAttack)
            {
                bossBody.SwordAttack();
                canAttack = false;
                Invoke("CanAttackAgain", 0.5f);
            }
            direction = 0;
        }
        else
        {
            direction = 0;
        }
    }

    public void ShootDarkKunai()
    {
        if (canShoot)
        {
            if (bossBody.onAir) bossBody.AirKunaiThrow();
            else bossBody.KunaiThrow();
            canShoot = false;
            Invoke("CanShootAgain", 1.5f);
        }
    }

    void CanShootAgain()
    {
        canShoot = true;
    }

    public void TakingDamage()
    {
        takingDamage = true;
        //falta algún timer para que se haga false
    }

    void CanAttackAgain()
    {
        canAttack = true;
    }
}

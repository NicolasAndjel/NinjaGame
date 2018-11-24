using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBrain : MonoBehaviour {

    public HeroBody heroBody;
    float kunaiTimer;
    bool canShoot;
    bool takingDamage;
    bool dead;
    bool canSlide;
    bool canGlide;
    bool canAttack;
    float stunTimer;
    public float stunTime;
    bool sliding;
    bool gliding;


    // Use this for initialization
    void Start () {
        heroBody = GetComponent<HeroBody>();
        dead = false;
        takingDamage = false;
        canShoot = true;
        canSlide = true;
        canGlide = true;
        sliding = false;
        gliding = false;
        canAttack = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead && !takingDamage && !gliding && !sliding)
        {
            CheckKeys();
        }
        if (takingDamage)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunTime)
            {
                takingDamage = false;
                stunTimer = 0;
            }
        }
    }

    void CheckKeys()
    {

        float direction = Input.GetAxis("Horizontal");
        heroBody.Move(direction);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            heroBody.Jump(direction);
        }

        if (canSlide)
        {
            if (Input.GetButtonDown("slide"))
            {
                if (direction > 0) direction = 1;
                else if (direction < 0) direction = -1;
                if (direction != 0)
                {
                    heroBody.Slide(direction);
                    canSlide = false;
                    Invoke("CanSlideAgain", 0.7f);
                }
            }
        }


        if (!heroBody.onAir)
        {
            canGlide = true;
        }
        if (heroBody.onAir && canGlide && Input.GetButtonDown("slide"))
        {
            heroBody.Glide();
            canGlide = false;
        }


        if (Input.GetButtonDown("swordAttack"))
        {
            if (canAttack)
            {
                heroBody.SwordAttack();
                canAttack = false;
                Invoke("CanAttackAgain", 0.3f);
            }
        }

        if (Input.GetButtonDown("kunaiThrow"))
        {
            if (canShoot)
            {
                if (heroBody.onAir) heroBody.AirKunaiThrow();
                else heroBody.KunaiThrow();

                canShoot = false;
                Invoke("CanShootAgain", 0.7f);
            }
            
        }
    }

    void CanAttackAgain()
    {
        canAttack = true;
    }

    void CanShootAgain()
    {
        canShoot = true;
    }

    void CanSlideAgain()
    {
        canSlide = true;
    }    

    public void Gliding()
    {
        gliding = true;
    }

    public void StopGliding()
    {
        gliding = false;
    }

    public void TakingDamage()
    {
        takingDamage = true;
        //falta algún timer para que se haga false
    }

    public void Dead()
    {
        dead = true;
    }
}

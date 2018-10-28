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
    float stunTimer;
    public float stunTime;


    // Use this for initialization
    void Start () {
        heroBody = GetComponent<HeroBody>();
        dead = false;
        takingDamage = false;
        canShoot = true;
        canSlide = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead && !takingDamage)
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
        

        if (Input.GetButtonDown("swordAttack"))
        {
            heroBody.SwordAttack();
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

    void CanShootAgain()
    {
        canShoot = true;
    }

    void CanSlideAgain()
    {
        canSlide = true;
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

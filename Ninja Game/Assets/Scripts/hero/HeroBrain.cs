using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBrain : MonoBehaviour {

    public HeroBody heroBody;
    float kunaiTimer;
    bool canShoot = true;


    // Use this for initialization
    void Start () {
        heroBody = GetComponent<HeroBody>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckKeys();
    }

    void CheckKeys()
    {
        float direction = Input.GetAxis("Horizontal");

        heroBody.Move(direction);

        //anim.SetFloat("WalkSpeed", Mathf.Abs(horizontal));

        //if (Input.GetButtonDown("swordAttack"))
        //{
        //    anim.Play("Hit");
        //    lives -= 1;
        //    anim.SetInteger("Life", lives);
        //    if (lives <= 0)
        //    {
        //        anim.Play("Die");
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            heroBody.Jump(direction);
        }

        if (Input.GetButtonDown("slide"))
        {
            if (direction > 0) direction = 1;
            else if (direction < 0) direction = -1;
            if (direction != 0)
            {
                heroBody.Slide(direction);
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
 }

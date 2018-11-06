using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderBrain : MonoBehaviour {

    HeroBody hero;
    public ShredderBody shredderBody;
    Vector3 direction;
    bool canAttack = true;
    public bool alive = true;
    bool canThrow = true;

    // Use this for initialization
    void Start()
    {
        hero = GameObject.Find("hero").GetComponent<HeroBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive) CheckHero();
    }

    void CheckHero()
    {
        float distanceToHero = Vector3.Distance(hero.transform.position, shredderBody.transform.position);

        if (distanceToHero < 14 && distanceToHero > 5f)
        {
            if (canThrow)
            {
                shredderBody.SaiThrow();
                canThrow = false;
                Invoke("CanThrowAgain", 2f);
            }
            direction = Vector3.zero;

        }

        else if (distanceToHero < 5 && distanceToHero > 1.5f)
        {
            direction = hero.transform.position - shredderBody.transform.position;
        }
        else if (distanceToHero < 2f)
        {
            if (canAttack)
            {
                shredderBody.Attack();
                canAttack = false;
                Invoke("CanAttackAgain", 0.5f);
            }
            direction = Vector3.zero;
        }
        else
        {
            direction = Vector3.zero;
        }
        shredderBody.Move(direction);
    }

    void CanAttackAgain()
    {
        canAttack = true;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    void CanThrowAgain()
    {
        canThrow = true;
    }
}

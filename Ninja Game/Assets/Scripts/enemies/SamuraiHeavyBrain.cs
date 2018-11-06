using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiHeavyBrain : MonoBehaviour {

    HeroBody hero;
    public SamuraiHeavyBody samuraiBody;
    Vector3 direction;
    bool canAttack = true;
    public bool alive = true;

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
        float distanceToHero = Vector3.Distance(hero.transform.position, samuraiBody.transform.position);

        if (distanceToHero < 9 && distanceToHero > 1.5f)
        {
            direction = hero.transform.position - samuraiBody.transform.position;
        }
        else if (distanceToHero < 2f)
        {
            if (canAttack)
            {
                samuraiBody.Attack();
                canAttack = false;
                Invoke("CanAttackAgain", 0.5f);
            }
            direction = Vector3.zero;
        }
        else
        {
            direction = Vector3.zero;
        }
        samuraiBody.Move(direction);
    }

    void CanAttackAgain()
    {
        canAttack = true;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    HeroBody hero;
    public EnemyBody enemyBody;
    Vector3 direction;
    bool canAttack = true;

    // Use this for initialization
    void Start()
    {
        hero = GameObject.Find("hero").GetComponent<HeroBody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHero();
    }

    void CheckHero()
    {
        float distanceToHero = Vector3.Distance(hero.transform.position, enemyBody.transform.position);

        if (distanceToHero < 7 && distanceToHero > 1.5f)
        {
            direction = hero.transform.position - enemyBody.transform.position;
        }
        else if (distanceToHero < 1.5f)
        {
            if (canAttack)
            {
                enemyBody.Attack();
                canAttack = false;
                Invoke("CanAttackAgain", 0.2f);
            }
            direction = Vector3.zero;
        }
        else
        {
            direction = Vector3.zero;
        }
        enemyBody.Move(direction);
        //print("distance to hero is: " + distanceToHero);
        //print("direciton is: " + direction);
    }

    void CanAttackAgain()
    {
        canAttack = true;
    }
}
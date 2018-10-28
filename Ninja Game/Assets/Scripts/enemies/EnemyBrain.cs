using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    HeroBody hero;
    public EnemyBody enemyBody;
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
        float distanceToHero = Vector3.Distance(hero.transform.position, enemyBody.transform.position);

        if (distanceToHero < 9 && distanceToHero > 1.5f)
        {
            direction = hero.transform.position - enemyBody.transform.position;
        }
        else if (distanceToHero < 2f)
        {
            if (canAttack)
            {
                enemyBody.Attack();
                canAttack = false;
                Invoke("CanAttackAgain", 0.5f);
            }
            direction = Vector3.zero;
        }
        else
        {
            direction = Vector3.zero;
        }
        enemyBody.Move(direction);
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
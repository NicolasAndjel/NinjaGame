﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {
    public int previousLife;
    public static DataHolder holder;
    public bool rememberLife = false;
    public string difficulty;

    GameManager gameManager;
    HeroBody heroBody;

    void Awake()
    {
        if (holder == null)
        {
            DontDestroyOnLoad(gameObject);
            holder = this;

        }
        else if (holder != this)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        difficulty = "normal";       
    }
    // Update is called once per frame
    void Update () {

    }

    public void RememberLife()
    {
        rememberLife = true;
        print("holder remembers");
    }

    public void ResetLife()
    {
        rememberLife = false;
    }

    public void GetLife()
    {
        heroBody = GameObject.Find("hero").GetComponent<HeroBody>();
        previousLife = heroBody.life;
        print("previous life assigned as " + heroBody.life);
    }


}

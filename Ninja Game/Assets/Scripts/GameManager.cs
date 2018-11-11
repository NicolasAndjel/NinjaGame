﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject loosePanel;
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject[] lives;

    public GameObject[] enemies;

    bool endGame;

    public AudioSource source;
    public AudioSource bossSource;
    public AudioClip level1;
    public AudioClip bossFight;
    public AudioClip heroLand;
    public AudioClip heroHit;
    public AudioClip heroDie;
    public AudioClip kunaiThrow;
    public AudioClip kunaiHit;
    public AudioClip sword;
    public AudioClip slide;
    public AudioClip jump;
    public AudioClip glide;
    public AudioClip gameOverSound;
    public AudioClip winSound;
    public AudioClip samuraiLightWalk;
    public AudioClip samuraiLightSword;
    public AudioClip samuraiLightHit;
    public AudioClip samuraiLightDie;
    public AudioClip samuraiHeavyWalk;
    public AudioClip samuraiHeavySword;
    public AudioClip samuraiHeavyHit;
    public AudioClip samuraiHeavyDie;
    public AudioClip shredderWalk;
    public AudioClip shredderSword;
    public AudioClip shredderHit;
    public AudioClip shredderDie;
    public AudioClip saiThrow;
    public AudioClip bossSound;
    public AudioClip targetHit;
    public AudioClip doorRaise;
    



    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        source.clip = level1;
        source.Play();
    }
	
	// Update is called once per frame
	void Update () {
        PauseGame();
        //CheckEnemies();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Loose()
    {
        loosePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
    }

    public void ResumeButton ()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PlayAudio(AudioClip audioName)
    {
        source.PlayOneShot(audioName);
        print("should be playing: " + audioName);
    }

    public void BossMusic()
    {
        source.Stop();
        bossSource.clip = bossFight;
        bossSource.Play();
    }

    public void ReduceLife()
    {
        endGame = true;
        for (int i = 0; i < lives.Length; i++)
        {
            GameObject life = lives[i];
            if (life.activeInHierarchy)
            {
                life.SetActive(false);
                break;
            }
        }
    }
}

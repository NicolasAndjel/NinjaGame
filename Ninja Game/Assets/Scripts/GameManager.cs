using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject loosePanel;
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject[] lives;
    public GameObject[] enemies;
    public DataHolder dataHolder;
    public GameObject normalDif;
    public GameObject easylDif;
    public GameObject hardDif;

    bool endGame;

    public AudioSource source;
    public AudioClip Tutorial;
    public AudioClip level1;
    public AudioClip levelFirst;
    public AudioClip levelSecond;
    public AudioClip bossFight;
    public AudioClip heroLand;
    public AudioClip heroHit;
    public AudioClip heroDie;
    public AudioClip kunaiThrow;
    public AudioClip kunaiHit;
    public AudioClip kunaiDenied;
    public AudioClip sword;
    public AudioClip slide;
    public AudioClip jump;
    public AudioClip glide;
    public AudioClip gameOverSound;
    public AudioClip winSound;
    public AudioClip samuraiLightSword;
    public AudioClip samuraiLightHit;
    public AudioClip samuraiLightDie;
    public AudioClip samuraiHeavySword;
    public AudioClip samuraiHeavyHit;
    public AudioClip samuraiHeavyDie;
    public AudioClip shredderSword;
    public AudioClip shredderHit;
    public AudioClip shredderDie;
    public AudioClip saiThrow;
    public AudioClip bossSound;
    public AudioClip targetHit;
    public AudioClip dummyHit;

    HeroBody heroBody;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        heroBody = GameObject.Find("hero").GetComponent<HeroBody>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        dataHolder = GameObject.Find("dataHolder").GetComponent<DataHolder>();
        if (sceneName != "Tutorial" && sceneName != "bossFight")
        {
            if (dataHolder.rememberLife)
            {
                SetLife();
            }
        }
        GetDifficulty();
        source.Stop();
        switch (sceneName)
        {
            case "Tutorial":
                source.clip = Tutorial;
                break;
            case "Level1":
                source.clip = level1;
                break;
            case "bossFight":
                source.clip = level1;
                break;
            case "LevelFirst":
                source.clip = levelFirst;

                break;
            case "LevelSecond":
                source.clip = levelSecond;
                break;
        }
        source.Play();
}
	
	// Update is called once per frame
	void Update () {
        PauseGame();
        LevelSelect();
        //CheckEnemies();
    }

    public void LevelSelect()
    {
        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("LevelFirst");
        }
        else if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("LevelSecond");
        }
        else if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("Level1");
        }
        else if (Input.GetKeyDown("4"))
        {
            SceneManager.LoadScene("bossFight");
        }
        else if (Input.GetKeyDown("0"))
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName != "Tutorial" && sceneName != "Menu" && sceneName != "bossFight" && sceneName != "LevelFirst")
        {
            dataHolder.ResetLife();
        }
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        dataHolder.GetLife();
        print("manager gets life");
        dataHolder.RememberLife();
        print("manager remembers life");
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Win()
    {
        winPanel.SetActive(true);
        source.Stop();
        source.clip = winSound;
        source.Play();
        Time.timeScale = 0;
    }

    public void Loose()
    {
        loosePanel.SetActive(true);
        source.Stop();
        source.clip = gameOverSound;
        source.Play();
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
    }

    public void BossMusic()
    {
        source.Stop();
        source.clip = bossFight;
        source.Play();
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

    public void SetLife()
    {
        heroBody.PreviousLife(dataHolder.previousLife);
        for (int i = 0; i < (4 - dataHolder.previousLife); i++)
        {
            GameObject life = lives[i];
            if (life.activeInHierarchy)
            {
                life.SetActive(false);
            }
        }
    }

    public void SetDifficulty(string dif)
    {
        dataHolder.difficulty = dif;
        GetDifficulty();
    }

    public void GetDifficulty()
    {
        switch (dataHolder.difficulty)
        {
            case "easy":
                normalDif.SetActive(false);
                easylDif.SetActive(true);
                hardDif.SetActive(false);
                break;
            case "hard":
                normalDif.SetActive(false);
                easylDif.SetActive(false);
                hardDif.SetActive(true);
                break;
            default:
                normalDif.SetActive(true);
                easylDif.SetActive(false);
                hardDif.SetActive(false);
                break;
        }
        
        for (int i = 0; i < enemies.Length; i++)
        {
            if (dataHolder.difficulty == "normal")
            {
                if (enemies[i].name == "samuraiLight")
                {
                    enemies[i].GetComponent<EnemyBody>().life = 1;
                    enemies[i].GetComponent<EnemyBody>().speed = 4;
                    print("encontré un samurai light");
                }
                else if (enemies[i].name == "samuraiHeavy")
                {
                    enemies[i].GetComponent<SamuraiHeavyBody>().life = 4;
                    enemies[i].GetComponent<SamuraiHeavyBody>().speed = 2;
                    print("encontré un samurai Heavy");
                }
                else if (enemies[i].name == "shredderNinja")
                {
                    enemies[i].GetComponent<ShredderBody>().life = 0;
                    enemies[i].GetComponent<ShredderBody>().speed = 3;
                    print("encontré un shredderNinja");
                }
            }
            else if (dataHolder.difficulty == "hard")
            {
                if (enemies[i].name == "samuraiLight")
                {
                    enemies[i].GetComponent<EnemyBody>().life = 3;
                    enemies[i].GetComponent<EnemyBody>().speed = 6;
                    print("encontré un samurai light");
                }
                else if (enemies[i].name == "samuraiHeavy")
                {
                    enemies[i].GetComponent<SamuraiHeavyBody>().life = 5;
                    enemies[i].GetComponent<SamuraiHeavyBody>().speed = 4;
                    print("encontré un samurai Heavy");
                }
                else if (enemies[i].name == "shredderNinja")
                {
                    enemies[i].GetComponent<ShredderBody>().life = 2;
                    enemies[i].GetComponent<ShredderBody>().speed = 4;
                    print("encontré un shredderNinja");
                }
            }
            else if (dataHolder.difficulty == "easy")
            {
                if (enemies[i].name == "samuraiLight")
                {
                    enemies[i].GetComponent<EnemyBody>().life = 0;
                    enemies[i].GetComponent<EnemyBody>().speed = 2;
                    print("encontré un samurai light");
                }
                else if (enemies[i].name == "samuraiHeavy")
                {
                    enemies[i].GetComponent<SamuraiHeavyBody>().life = 2;
                    enemies[i].GetComponent<SamuraiHeavyBody>().speed = 1;
                    print("encontré un samurai Heavy");
                }
                else if (enemies[i].name == "shredderNinja")
                {
                    enemies[i].GetComponent<ShredderBody>().life = 0;
                    enemies[i].GetComponent<ShredderBody>().speed = 1;
                    print("encontré un shredderNinja");
                }
            }
        }
    }
}

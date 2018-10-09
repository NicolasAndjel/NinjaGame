using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject loosePanel;
    public GameObject winPanel;
    public GameObject pausePanel;

    public GameObject[] enemies;

    bool endGame;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }
	
	// Update is called once per frame
	void Update () {
        PauseGame();
        CheckEnemies();
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

    public void CheckEnemies()
    {
        endGame = true;
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = enemies[i];
            if (enemy.activeInHierarchy)
            {
                endGame = false;
                break;
            }
        }
        if (endGame)
        {
            Invoke("Win", 1);
        }
    }

    //public void TrapActivated()
    //{
    //    print("El manager recibió la señal");
    //    GameObject trapArc = GameObject.Find("trapArc");
    //    float trapArcHeight = trapArc.transform.position.y;
    //    trapArcHeight = 3;
    //    print("El arco debería bajar");
    //}
}

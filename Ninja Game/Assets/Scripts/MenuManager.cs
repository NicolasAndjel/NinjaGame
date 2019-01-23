using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public AudioClip menuSound;
    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    public DataHolder dataHolder;

    void Start()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source = GetComponent<AudioSource>();
        source.PlayOneShot(menuSound, vol);
        dataHolder = GameObject.Find("dataHolder").GetComponent<DataHolder>();
    } 

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetDifficulty(string dif)
    {
        dataHolder.previousDifficulty = dataHolder.difficulty;
        dataHolder.difficulty = dif;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
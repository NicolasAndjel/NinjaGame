using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameManager gameManager;
    public GameObject hero;
    int challengeNumber = 0;

    public GameObject swords;
    public GameObject jumpObject;
    public GameObject lamp;
    public GameObject logg1;
    public GameObject logg2;
    public GameObject target;
    public GameObject dummy;
    public GameObject runText;
    public GameObject jumpText;
    public GameObject slideText;
    public GameObject glideText;
    public GameObject kunaiText;
    public GameObject swordText;
    public GameObject welcomeText;
    public GameObject clearPanel;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextChallenge()
    {
        challengeNumber++;
        gameManager.source.PlayOneShot(gameManager.heroLand, Random.Range(0.5f, 1f));
        switch (challengeNumber)
        {
            case 1:
                swords.SetActive(false);
                hero.SetActive(true);
                welcomeText.SetActive(false);
                runText.SetActive(false);
                jumpText.SetActive(true);
                jumpObject.SetActive(true);
                break;
            case 2:
                hero.SetActive(true);
                jumpObject.SetActive(false);
                jumpText.SetActive(false);
                slideText.SetActive(true);
                lamp.SetActive(true);
                break;
            case 3:
                hero.SetActive(true);
                lamp.SetActive(false);
                slideText.SetActive(false);
                glideText.SetActive(true);
                logg1.SetActive(true);
                logg2.SetActive(true);
                break;
            case 4:
                hero.SetActive(true);
                glideText.SetActive(false);
                kunaiText.SetActive(true);
                logg1.SetActive(false);
                logg2.SetActive(false);
                target.SetActive(true);
                target.SetActive(true);
                break;
            case 5:
                hero.SetActive(true);
                kunaiText.SetActive(false);
                target.SetActive(false);
                swordText.SetActive(true);
                dummy.SetActive(true);
                break;
            case 6:
                hero.SetActive(true);
                dummy.SetActive(true);
                Time.timeScale = 0;
                clearPanel.SetActive(true);
                break;
        }
    }
}

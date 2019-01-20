using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour {

    public GameManager gameManager;
    public TutorialManager tutorial;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tutorial = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.tag == "kunai")
            {
                gameManager.source.PlayOneShot(gameManager.kunaiDenied);
            }
            else
            {
                tutorial.NextChallenge();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour {

    public GameManager gameManager;
    public TutorialManager tutorial;
    public int dummyHealth;
    bool broken;
    public float shake_intensity;
    public Vector3 originPosition;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tutorial = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        dummyHealth = 3;
        broken = false;
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
                dummyHealth--;
                originPosition = transform.position;
                gameManager.source.PlayOneShot(gameManager.dummyHit);
                transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
                if (dummyHealth <= 0)
                {
                    gameObject.SetActive(false);   
                    tutorial.NextChallenge();
                }
            }
        }
    }
}

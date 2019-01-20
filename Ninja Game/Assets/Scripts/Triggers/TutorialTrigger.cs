using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    public TutorialManager tutorial;

	// Use this for initialization
	void Start () {
        tutorial = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            tutorial.NextChallenge();
            print("debería cargar siguiente desafío");
        }
    }
}

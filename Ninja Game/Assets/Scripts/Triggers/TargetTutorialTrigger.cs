using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTutorialTrigger : MonoBehaviour {

    public TutorialManager tutorial;

    // Use this for initialization
    void Start()
    {
        tutorial = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Kunai(Clone)")
        {
            tutorial.NextChallenge();
        }
    }
}

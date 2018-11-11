using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void RaiseDoor()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y / 2.5f, transform.position.z);
        gameManager.source.PlayOneShot(gameManager.doorRaise);
    }
}

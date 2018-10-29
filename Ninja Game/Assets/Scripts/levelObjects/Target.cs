using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject door;
    Vector3 doorPosition;
    bool doorOpened;
    float targetWidth;

    // Use this for initialization
    void Start () {
        doorPosition = transform.position;
        doorOpened = false;
        targetWidth = transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            doorPosition.y += 1;
            doorOpened = true;
            targetWidth /= 2;
            print("Target Trigger detectó al Kunai");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            doorPosition.y++;
            doorOpened = true;
            targetWidth /= 2;
            print("Target Collision detectó al Kunai");
        }
    }
}

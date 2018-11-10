using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    Vector3 targetScale;
    public Sprite hitSprite;
    float doorPosition;
    float doorMovement;
    public Door door;

    // Use this for initialization
    void Start () {
        door = transform.Find("door").gameObject.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            door.RaiseDoor();
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            GetComponent<CircleCollider2D>().enabled = false;
            print("Target Trigger detectó al Kunai");
        }
    }
}

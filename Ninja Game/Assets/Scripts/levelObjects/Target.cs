using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    GameManager gameManager;
    Vector3 targetScale;
    public Sprite hitSprite;
    float doorPosition;
    float doorMovement;
    public Door door;

    // Use this for initialization
    void Start () {
        door = transform.Find("door").gameObject.GetComponent<Door>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            gameManager.source.PlayOneShot(gameManager.targetHit);
            door.RaiseDoor();
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            GetComponent<CircleCollider2D>().enabled = false;
            print("Target Trigger detectó al Kunai");
        }
    }
}

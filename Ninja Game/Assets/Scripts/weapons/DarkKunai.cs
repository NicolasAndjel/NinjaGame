using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKunai : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start()
    {
        GameObject.Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0 || collision.gameObject.layer == 12 || collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}

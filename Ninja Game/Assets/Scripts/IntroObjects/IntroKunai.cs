using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroKunai : MonoBehaviour {

    public Transform finalPosition;
    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = finalPosition.position - transform.position;
        if (distance.magnitude > speed * Time.deltaTime)
        {
            transform.position += distance.normalized * speed * Time.deltaTime;
        }
    }
}

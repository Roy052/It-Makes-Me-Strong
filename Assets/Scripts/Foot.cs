using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public bool isFalling;
    public GameObject player;
    void Start()
    {
        isFalling = false;
    }

    void Update()
    {
        this.transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fall")
        {
            isFalling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Fall")
        {
            isFalling = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health, maxhealth;
    public int damage;
    void Start()
    {
        health = maxhealth;
    }

    void Update()
    {
        if (health == 0)
            Destroy(this.gameObject);
    }

}

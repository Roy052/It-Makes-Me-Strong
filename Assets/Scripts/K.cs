using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K : MonoBehaviour
{
    GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        this.transform.position = Player.transform.position + new Vector3(0.38f, 0.38f);
    }
}

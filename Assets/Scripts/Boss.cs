using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject bossAttack, area, missile;
    GameObject player;
    public float timedelay;

    private void Start()
    {
        bossAttack.SetActive(false);
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        bossAttack.transform.position = this.gameObject.transform.position;
        if (area.GetComponent<Area>().inArea == true)
        {
            this.transform.position -=
                (this.transform.position - player.transform.position) * 0.5f * Time.deltaTime;
            timedelay += Time.deltaTime;
            if(timedelay >= 2)
            {
                StartCoroutine(Attack());
                timedelay = 0;
            }
        }
    }

    IEnumerator Attack()
    {
        bossAttack.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        bossAttack.SetActive(false);
    }
}

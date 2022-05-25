using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSM : MonoBehaviour
{
    public Text healthText, damageText;
    GameObject playerObj;
    Player player;
    Vector3 StartPoint = new Vector3(0,0);

    int totalDeathCount;
    int[] deathCounts;
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    void Update()
    {
        healthText.text = player.health + " / " + player.maxhealth;
        damageText.text = player.damage.ToString();
    }

    public void PlayerDead(int why)
    {
        StartCoroutine(DeadToRevive());
        player.maxhealth += 100;
        player.damage += 5;
    }
    
    IEnumerator DeadToRevive()
    {
        playerObj.SetActive(false);
        //Animation
        yield return new WaitForSeconds(1f);
        player.Revive();
        player.transform.position = StartPoint;
        playerObj.SetActive(true);
    }

    
}

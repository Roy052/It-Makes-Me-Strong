using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSM : MonoBehaviour
{
    public Text healthText, damageText, angelText, charText;
    public AngelMessage am;

    GameObject playerObj;
    Player player;
    Vector3 StartPoint = new Vector3(0,0);

    int totalDeathCount;
    int[] deathCounts;
    int doubt = 0, confidence = 0;
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();

        //Text Reset
        healthText.text = "";
        damageText.text = "";
        angelText.text = "";
        charText.text = "";
    }

    void Update()
    {
        healthText.text = player.health + " / " + player.maxhealth;
        damageText.text = player.damage.ToString();
    }

    public void PlayerDead(int why)
    {
        StartCoroutine(DeadToRevive());
        StartCoroutine(AngelsMessage());
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

    public IEnumerator AngelsMessage()
    {
        am.ON();
        angelText.text = "I'll Save You";
        yield return new WaitForSeconds(2f);
        am.OFF();
        angelText.text = "";
    }

    public void CharacterMessage()
    {

    }

    void AddDoubt()
    {
        doubt++;
        if(doubt == 3)
        {
            confidence++;
            doubt = 0;
        }
    }
}

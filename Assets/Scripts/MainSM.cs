using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSM : MonoBehaviour
{
    public Text healthText, damageText, angelText, charText;
    public AngelMessage am;
    public Camera mainCamera;

    GameObject playerObj;
    Player player;
    Vector3 StartPoint = new Vector3(-3, 1);

    int totalDeathCount;
    int[] deathCounts = new int[5];
    int doubt = 0, confidence = 0;
    Messages messages = new Messages();
    Vector3 endPosition = new Vector3(84, 0, 0);
    GameManager gm;

    const float messageTime = 1.8f;
    const float waitingTime = 1f;

    int beforeDeath = -1;
    int beforeDeathCount = 0;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Text Reset
        healthText.text = "";
        damageText.text = "";
        angelText.text = "";
        charText.text = "";

        //DeathCount Reset
        totalDeathCount = 0;
        for (int i = 0; i < 5; i++)
            deathCounts[i] = 0;
    }

    void Update()
    {
        healthText.text = player.health + " / " + player.maxhealth;
        damageText.text = player.damage.ToString();
        if (gm.GameState != 3)
            mainCamera.transform.position = playerObj.transform.position - new Vector3(0, 0, 10);
    }

    public void PlayerDead(int why) // 0 : Monster, 1 : Fire, 2 : Fall, 3 : Electric, 4 : Boss
    {
        gm.GameState = 3;
        
        if (totalDeathCount == 0)
        {
            StartCoroutine(DeadToRevive((messageTime + waitingTime) * 3));
            StartCoroutine(FirstDeath());
            StartCoroutine(cameraMovement());
        }
        else
        {
            StartCoroutine(DeadToRevive(messageTime + waitingTime));
            int messageCount = deathCounts[why];
            if (messageCount > 5) messageCount = 4;

            //Angel Message
            switch (why)
            {
                case 0:
                    if(why < messages.angelMessages_monster.Length)
                    StartCoroutine(AngelsMessage(messages.angelMessages_monster[messageCount]));
                    break;
                case 1:
                    StartCoroutine(AngelsMessage(messages.angelMessages_fire[messageCount]));
                    break;
                case 2:
                    StartCoroutine(AngelsMessage(messages.angelMessages_fall[messageCount]));
                    break;
                case 3:
                    StartCoroutine(AngelsMessage(messages.angelMessages_electric[messageCount]));
                    break;
                case 4:
                    StartCoroutine(AngelsMessage(messages.angelMessages_boss[messageCount]));
                    break;
            }
            deathCounts[why]++;

            if (beforeDeath == why) beforeDeathCount++;
            else beforeDeathCount = 0;

            
            beforeDeath = why;

            //Doubt and Confidence
            if (beforeDeathCount == 3) doubt++;
            if (beforeDeathCount >= 5) doubt++;
            if (doubt == 3)
            {
                confidence++;
                doubt = 0;
            }
        }

        totalDeathCount++;

        //Character Improvement
        switch (why)
        {
            case 0:
                player.maxhealth += 100;
                player.damage += 5;
                break;
            case 1:
                player.fireResist += 10;
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
    
    IEnumerator DeadToRevive(float time)
    {
        playerObj.SetActive(false);
        //Animation
        yield return new WaitForSeconds(time);
        player.Revive();
        player.transform.position = StartPoint;
        playerObj.SetActive(true);
        gm.GameState = 1;
    }

    public IEnumerator AngelsMessage(string message)
    {
        am.ON();
        angelText.text = message;
        yield return new WaitForSeconds(messageTime);
        am.OFF();
        angelText.text = "";
        yield return new WaitForSeconds(waitingTime);
    }

    public IEnumerator FirstDeath()
    {
        for(int i = 0; i < 3; i++)
        {
            am.ON();
            angelText.text = messages.angelMessages_1st[i];
            yield return new WaitForSeconds(messageTime);
            am.OFF();
            angelText.text = "";
            yield return new WaitForSeconds(waitingTime);
        }
    }

    public IEnumerator cameraMovement()
    {
        yield return new WaitForSeconds(messageTime + waitingTime);
        while (true)
        {
            mainCamera.transform.position += endPosition / 60;
            yield return new WaitForFixedUpdate();
            if(mainCamera.transform.position.x >= endPosition.x)
                break;
        }

        yield return new WaitForSeconds(messageTime + waitingTime);
        while (true)
        {
            mainCamera.transform.position -= endPosition / 60;
            yield return new WaitForFixedUpdate();
            if (mainCamera.transform.position.x <= 0)
                break;
        }
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

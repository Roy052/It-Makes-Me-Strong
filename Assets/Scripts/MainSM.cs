using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSM : MonoBehaviour
{
    public Text healthText, damageText, angelText, charText;
    public AngelMessage am;
    public Camera mainCamera;
    public Arm arm;
    public AudioClip original, boss;

    GameObject playerObj;
    Player player;
    Vector3 StartPoint = new Vector3(-3, 1);

    int totalDeathCount;
    int[] deathCounts = new int[5];

    int doubt = 2, confidence = 0;
    Messages messages = new Messages();
    Vector3 endPosition = new Vector3(98, 0, 0);
    GameManager gm;

    int beforeDeath = -1;
    int beforeDeathCount = 0;

    int flyCount = 0;

    AudioSource audioSource;

    //otp
    bool confidenceOtp = false;

    //const
    const float messageTime = 1.8f;
    const float waitingTime = 1f;
    const int angelMessageMax = 5;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = this.GetComponent<AudioSource>();

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
        bool isMoving = player.isMoving;
        
        if (totalDeathCount == 0)
        {
            StartCoroutine(DeadToRevive((messageTime + waitingTime) * 3));
            StartCoroutine(FirstDeath());
            StartCoroutine(cameraMovement());

            beforeDeath = why;
        }
        else
        {
            StartCoroutine(DeadToRevive(messageTime + waitingTime));

            //Angel Message
            int messageCount = deathCounts[why];
            if (messageCount > 5) messageCount = angelMessageMax;

            switch (why)
            {
                case 0:
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
            else beforeDeathCount = 1;
         
            beforeDeath = why;

            //Doubt and Confidence
            if ((why == 1 || why == 3) && isMoving == false) doubt++;
            if (beforeDeathCount == 3) doubt++;
            if (beforeDeathCount >= 5) doubt++;
            if (doubt == 3)
            {
                confidence++;
                doubt = 0;
                confidenceOtp = false;
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
                player.fireResist += 20;
                break;
            case 2:
                flyCount++;
                if (flyCount == 6)
                    player.canFly = true;
                break;
            case 3:
                player.elecResist += 20;
                break;
            case 4:
                player.maxhealth += 10000;
                player.damage += 500;
                break;
        }
    }
    
    IEnumerator DeadToRevive(float time)
    {
        
        playerObj.SetActive(false);
        yield return new WaitForSeconds(time - waitingTime);
        player.transform.position = StartPoint + new Vector3(0, 3);

        playerObj.SetActive(true);
        arm.ON();
        while (true)
        {
            player.transform.position -= new Vector3(0, 3, 0) * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            if (player.transform.position.y <= 0) break;
        }
        player.Revive();
        arm.OFF();
        gm.GameState = 1;

        if (confidenceOtp == false && confidence >= 1)
        {
            int messageCount = confidence - 1;
            if (confidence > 11) messageCount = 10;
            StartCoroutine(CharMessage(messages.playerMessages_confidence[confidence - 1]));
            confidenceOtp = true;
        }
            
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

    public IEnumerator CharMessage(string msg)
    {
        for(int i = 0; i < msg.Length; i++)
        {
            charText.text += msg[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(waitingTime);
        charText.text = "";
    }

    public int GetConfidence()
    {
        return confidence;
    }

    public void BossEncounterON()
    {
        audioSource.clip = boss;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void BossEncounterOFF()
    {
        audioSource.clip = original;
        audioSource.volume = 0.2f;
        audioSource.Play();
    }
}

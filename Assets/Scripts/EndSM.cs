using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSM : MonoBehaviour
{
    public Text[] texts;
    Messages messages = new Messages();
    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(PrintText());
    }

    IEnumerator PrintText()
    {
        yield return new WaitForSeconds(1f);
        int endRoute = 0;

        if (gm.confidence >= 5) endRoute = 2;
        else if (gm.confidence >= 3) endRoute = 1;
        else endRoute = 0;

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < messages.endMessage[endRoute, i].Length; j++)
            {
                texts[i].text += messages.endMessage[endRoute, i][j];
                yield return new WaitForSeconds(0.1f);
            }
            
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(2);
        gm.EndToMenu();
    }
}

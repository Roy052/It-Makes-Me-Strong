using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (gm.GameState == 2) gm.EndToMenu();
        if (gm.GameState == 4) gm.HowToMenu();
        if (gm.GameState == 5) gm.EndingsToMenu();
    }
}

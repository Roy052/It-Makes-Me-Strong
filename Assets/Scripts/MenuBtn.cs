using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtn : MonoBehaviour
{
    public GameManager gm;
    public MenuSM menuSM;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        menuSM = GameObject.Find("MenuSM").GetComponent<MenuSM>();
    }

    private void OnMouseDown()
    {
        menuSM.ButtonSoundON();
        if (this.name == "Start")
        {
            gm.MenuToMain();
        }
        else if(this.name == "HowToPlay")
        {
            gm.MenuToHow();
        }
        else if (this.name == "Endings")
        {
            gm.MenuToEndings();
        }
        else if (this.name == "Quit")
        {
            gm.QuitGame();
        }
    }
}

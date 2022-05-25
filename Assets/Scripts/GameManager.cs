using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    int GameState = 0; // 0 : Menu, 1 : Main, 2 : End

    MenuSM menuSM;
    MainSM mainSM;
    EndSM endSM;
    bool onetime = false;
    private static GameManager gameManagerInstance;
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Scene Movement
    public void MenuToMain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MainToEnd()
    {
        SceneManager.LoadScene(2);
    }

    public void EndToMenu()
    {
        SceneManager.LoadScene(0);
    }
   
}

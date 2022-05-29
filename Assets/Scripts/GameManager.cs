using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int GameState = 0; // 0 : Menu, 1 : Main, 2 : End, 3 : Die

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
        GameState = 1;
        SceneManager.LoadScene(1);
        
    }

    public void MainToMenu()
    {
        GameState = 0;
        SceneManager.LoadScene(0);
    }

    public void MainToEnd()
    {
        GameState = 2;
        SceneManager.LoadScene(2);
    }

    public void EndToMenu()
    {
        GameState = 0;
        SceneManager.LoadScene(0);
    }
   
}

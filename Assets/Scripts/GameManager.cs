using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int GameState = 0; // 0 : Menu, 1 : Main, 2 : End, 3 : Die, 4: Howtoplay, 5 : Endings

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
        SceneManager.LoadScene("Main");
        
    }

    public void MainToMenu()
    {
        GameState = 0;
        SceneManager.LoadScene("Menu");
    }

    public void MainToEnd()
    {
        GameState = 2;
        SceneManager.LoadScene("End");
    }

    public void EndToMenu()
    {
        GameState = 0;
        SceneManager.LoadScene("Menu");
    }
   
    public void MenuToHow()
    {
        GameState = 4;
        SceneManager.LoadScene("HowToPlay", LoadSceneMode.Additive);
    }

    public void MenuToEndings()
    {
        GameState = 5;
        SceneManager.LoadScene("Endings", LoadSceneMode.Additive);
    }

    public void HowToMenu()
    {
        GameState = 0;
        SceneManager.UnloadSceneAsync("HowToPlay");
    }
    public void EndingsToMenu()
    {
        GameState = 0;
        SceneManager.UnloadSceneAsync("Endings");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

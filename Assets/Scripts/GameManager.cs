using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int GameState = 0; // 0 : Menu, 1 : Main, 2 : End, 3 : Die, 4: Howtoplay, 5 : Endings, 6 : Boss
    public bool bossTime = false;
    public int confidence = 0;

    public MenuSM menuSM;
    public MainSM mainSM;
    public EndSM endSM;
    
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
        menuSM = GameObject.Find("MenuSM").GetComponent<MenuSM>();
    }

    void Update()
    {
        if(GameState == 0 && menuSM == null)
            GameObject.Find("MenuSM").GetComponent<MenuSM>();
        else if (GameState == 1 && mainSM == null)
            mainSM = GameObject.Find("MainSM").GetComponent<MainSM>();
        else if(GameState == 2 && endSM == null)
            endSM = GameObject.Find("EndSM").GetComponent<EndSM>();
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
        confidence = mainSM.GetConfidence();
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

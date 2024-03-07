using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    GameManager gameManager;

    [SerializeField] GameObject mainMenu;
    [SerializeField] string mainMenuSceneName;
    [SerializeField] GameObject pause;
    [SerializeField] string gameplaySceneName;
    [SerializeField] GameObject options;
    [SerializeField] GameObject gameOver;
    [SerializeField] string gameOverSceneName;
    [SerializeField] GameObject win;
    [SerializeField] string winSceneName;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GetComponent<GameManager>();

        string sceneName = SceneManager.GetActiveScene().name;

        //Debug.Log(sceneName);
        switch (sceneName)
        {
            case "Menu": gameManager.state = GameState.mainMenu; break;
            case "Gameplay": gameManager.state = GameState.gameplay; Debug.LogWarning("Somehow, awoke in gameplay scene. This might be wrong."); break;
        }
    }

    private void NullCheckTempObjects()
    {
        if (options == null) { options = new(); }
        if (gameOver == null) { gameOver = new(); }
        if (win == null) { win = new(); }
    }

    

    // Update is called once per frame
    void Update()
    {
        NullCheckTempObjects();
        // Check for pause.

        try
        {
            switch (gameManager.state)
            {
                case GameState.gameplay: EnterGameplay(); break;
                case GameState.mainMenu: EnterMainMenu(); break;
                case GameState.pause: SetInactive(); EnterPause(); break;
                case GameState.options: SetInactive(); options.SetActive(true); break;
                case GameState.win: SetInactive(); win.SetActive(true); break;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(e.StackTrace);
            Debug.Log(gameManager.state);
        }
    }

    private void EnterPause()
    {
        pause.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void EnterMainMenu()
    {
        SetInactive();
        mainMenu.SetActive(true);
        if (SceneManager.GetActiveScene().name != mainMenuSceneName)
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            LevelManager lm = GetComponentInChildren<LevelManager>();
            lm.loadScene(mainMenuSceneName);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void EnterGameplay()
    {
        SetInactive();
        if (SceneManager.GetActiveScene().name != gameplaySceneName)
        {
            LevelManager lm = GetComponentInChildren<LevelManager>();
            lm.loadScene(gameplaySceneName);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void SetInactive()
    {
        mainMenu.SetActive(false);
        pause.SetActive(false);
        options.SetActive(false);
        gameOver.SetActive(false);
        win.SetActive(false);
    }

    void MainMenu()
    {
        throw new NotImplementedException();
    }
}

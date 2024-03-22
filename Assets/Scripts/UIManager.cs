using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    GameManager gameManager;
    LevelManager lm;

    [SerializeField] GameObject UIMainMenu;
    [SerializeField] string mainMenuSceneName;
    [SerializeField] GameObject UIPause;
    [SerializeField] string gameplayScene1Name;
    [SerializeField] List<string> gameplaySceneNames;
    [SerializeField] GameObject UIOptions;
    [SerializeField] GameObject UIGameOver;
    [SerializeField] string gameOverSceneName;
    [SerializeField] GameObject UIWin;
    [SerializeField] string winSceneName;

    // Start is called before the first frame update
    void Awake()
    {
        lm = GetComponentInChildren<LevelManager>();

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
        if (UIOptions == null) { UIOptions = new(); }
        if (UIGameOver == null) { UIGameOver = new(); }
        if (UIWin == null) { UIWin = new(); }
    }



    // Update is called once per frame
    void Update()
    {
        NullCheckTempObjects();
        // Check for pause.

        //try
        {
            switch (gameManager.state)
            {
                case GameState.gameplay: EnterGameplay(); break;
                case GameState.mainMenu: EnterMainMenu(); break;
                case GameState.pause: EnterPause(); break;
                case GameState.optionsGameplay:
                case GameState.optionsMenu: SetInactive(); UIOptions.SetActive(true); break;
                case GameState.win: GameOver(true); break;
                case GameState.gameOver: GameOver(false); break;
            }
        }
        //catch (Exception e)
        //{
        //    Debug.Log(e.Message);
        //    Debug.Log(e.StackTrace);
        //    Debug.Log(gameManager.state);
        //}
    }

    private void GameOver(bool win)
    {
        SetInactive();

        if (win) { UIWin.SetActive(true); }
        else { UIGameOver.SetActive(true); }
    }

    private void EnterPause()
    {
        SetInactive();

        UIPause.SetActive(true);
        gameManager.PlayerSetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }



    private void EnterMainMenu()
    {
        SetInactive();
        UIMainMenu.SetActive(true);
        if (SceneManager.GetActiveScene().name != mainMenuSceneName)
        {
            Debug.Log(SceneManager.GetActiveScene().name);

            lm.LoadScene(mainMenuSceneName);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void EnterGameplay()
    {
        SetInactive();
        if (!gameplaySceneNames.Contains(SceneManager.GetActiveScene().name))
        {
            // no, we arent saving the scene the player was on.
            gameManager.TryGameplayLoad(gameplayScene1Name);
        }

        gameManager.PlayerSetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void SetInactive()
    {
        UIMainMenu.SetActive(false);
        UIPause.SetActive(false);
        UIOptions.SetActive(false);
        UIGameOver.SetActive(false);
        UIWin.SetActive(false);
        gameManager.PlayerSetActive(false);

        Time.timeScale = 1;
    }
}

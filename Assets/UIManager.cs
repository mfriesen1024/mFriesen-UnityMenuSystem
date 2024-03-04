using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    enum GameState { mainMenu, gameplay, pause, options, gameOver, win }

    GameState state = GameState.mainMenu;

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
        if (options == null) { options = new(); }
        if (gameOver == null) { gameOver = new(); }
        if (win == null) { win = new(); }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for pause.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (state)
            {
                case GameState.pause: state = GameState.gameplay; break;
                case GameState.gameplay: state = GameState.pause; break;
            }
        }

        switch (state)
        {
            case GameState.gameplay: EnterGameplay(); break;
            case GameState.mainMenu: EnterMainMenu(); break;
            case GameState.pause: SetInactive(); pause.SetActive(true); break;
            case GameState.options: SetInactive(); options.SetActive(true); break;
            case GameState.win: SetInactive(); win.SetActive(true); break;
        }
    }

    private void EnterMainMenu()
    {
        SetInactive();
        mainMenu.SetActive(true);
        if(SceneManager.GetActiveScene().name != mainMenuSceneName) {
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

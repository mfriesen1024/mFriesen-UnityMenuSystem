using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public    enum GameState { mainMenu, gameplay, pause, options, gameOver, win }

public class UIManager : MonoBehaviour
{

    [SerializeField] GameState state;

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
        string sceneName = SceneManager.GetActiveScene().name;

        //Debug.Log(sceneName);
        switch (sceneName)
        {
            case "Menu": state = GameState.mainMenu; break;
            case "Gameplay": state = GameState.gameplay; Debug.LogWarning("Somehow, awoke in gameplay scene. This might be wrong."); break;
        }
    }

    private void NullCheckTempObjects()
    {
        if (options == null) { options = new(); }
        if (gameOver == null) { gameOver = new(); }
        if (win == null) { win = new(); }
    }

    public void SetState(int state)
    {
        this.state = (GameState)state;
    }

    // Update is called once per frame
    void Update()
    {
        NullCheckTempObjects();
        // Check for pause.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("you boring");
            switch (state)
            {
                case GameState.pause: state = GameState.gameplay; break;
                case GameState.gameplay: state = GameState.pause; break;
            }
        }

        try
        {
            switch (state)
            {
                case GameState.gameplay: EnterGameplay(); break;
                case GameState.mainMenu: EnterMainMenu(); break;
                case GameState.pause: SetInactive(); pause.SetActive(true); break;
                case GameState.options: SetInactive(); options.SetActive(true); break;
                case GameState.win: SetInactive(); win.SetActive(true); break;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(e.StackTrace);
            Debug.Log(state);
        }
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

using System;
using UnityEngine;

public enum GameState { mainMenu, gameplay, pause, optionsGameplay, optionsMenu, gameOver, angryRock, win }
public class GameManager : MonoBehaviour
{
    LevelManager levelManager;

    public GameState state;

    public GameObject player;
    [SerializeField] string playerName;

    internal string scene1Spawn;

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();

        player = GameObject.Find(playerName);
    }

    public void SetState(int state)
    {
        Debug.Log(state);
        this.state = (GameState)state;
    }

    public void LeaveOptions()
    {
        if (state == GameState.optionsGameplay) { state = GameState.gameplay; }
        else if (state == GameState.optionsMenu) { state = GameState.mainMenu; }
        else { Debug.LogError("Invalid state."); }
    }

    public void TryGameplayLoad(string sceneName, string spawnPointName)
    {
        levelManager.LoadScene(sceneName);
        if (spawnPointName != null && spawnPointName != string.Empty)
        {
            GameObject spawnPoint = GameObject.Find(spawnPointName);
            // Redundancy
            if (spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
        }
    }
    public void TryGameplayLoad(string sceneName) { TryGameplayLoad(sceneName, scene1Spawn); }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("you boring");
            switch (state)
            {
                //case GameState.pause: state = GameState.gameplay; break;
                case GameState.gameplay: state = GameState.pause; break;
            }
        }
    }
}

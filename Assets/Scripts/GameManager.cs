using System;
using UnityEngine;

public enum GameState { mainMenu, gameplay, pause, optionsGameplay, optionsMenu, gameOver, angryRock, win }
public class GameManager : MonoBehaviour
{
    LevelManager levelManager;

    public GameState state;

    public GameObject player;

    string scene1Spawn = "SpawnPoint";

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
    }

    public void SetState(int state)
    {
        Debug.Log(state);
        this.state = (GameState)state;
    }

    public void PlayerSetActive(bool enabled) { player.SetActive(enabled); }

    public void LeaveOptions()
    {
        if (state == GameState.optionsGameplay) { state = GameState.pause; }
        else if (state == GameState.optionsMenu) { state = GameState.mainMenu; }
        else { Debug.LogError("Invalid state."); }
    }

    public void TryGameplayLoad(string sceneName, string spawnPointName, bool useTag = false)
    {
        levelManager.LoadScene(sceneName);
        if (spawnPointName != null && spawnPointName != string.Empty)
        {
            GameObject spawnPoint;
            if (!useTag) { spawnPoint = GameObject.Find(spawnPointName); }
            else { spawnPoint = GameObject.FindWithTag(spawnPointName); }
            // Redundancy
            if (spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
        }
    }
    public void TryGameplayLoad(string sceneName) { TryGameplayLoad(sceneName, scene1Spawn, true); }

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

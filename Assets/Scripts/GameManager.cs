using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { mainMenu, gameplay, pause, optionsGameplay, optionsMenu, gameOver, angryRock, win }
public class GameManager : MonoBehaviour
{
    LevelManager levelManager;

    public GameState state;

    public GameObject player;
    bool rForWarp;

    string scene1Spawn = "SpawnPoint";

    private void Awake()
    {
        levelManager = GetComponentInChildren<LevelManager>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        rForWarp = true;
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
            WarpAsync(spawnPointName, useTag);
        }
    }
    public void TryGameplayLoad(string sceneName) { TryGameplayLoad(sceneName, scene1Spawn, true); }

    IEnumerator WarpAsync(string spawnPointName, bool useTag)
    {
        yield return new WaitUntil(() => rForWarp);

        GameObject spawnPoint;
        if (!useTag) { spawnPoint = GameObject.Find(spawnPointName); }
        else { spawnPoint = GameObject.FindWithTag(spawnPointName); }
        // Redundancy
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }

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

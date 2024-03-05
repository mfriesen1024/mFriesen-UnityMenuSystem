using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState state;

    public void SetState(int state)
    {
        Debug.Log(state);
        this.state = (GameState)state;
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

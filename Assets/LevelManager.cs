using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadScene(string value)
    {
        try { throw new DebugException(); }
        catch (DebugException e)
        {
            Debug.Log($"A scene change was requested, scene was {value}");
            Debug.Log(e.StackTrace);
        }
        //if(value is int) { SceneManager.LoadScene((int)value); }
        if (value is not null) { SceneManager.LoadScene(value); }
        //else { throw new NotImplementedException("Youboring"); }


    }
}

public class DebugException : Exception // this is intended to be thrown for debugging purposes in a trycatch.
{
    
}

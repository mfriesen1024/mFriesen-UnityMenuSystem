using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadScene(string value)
    {
        
        //if(value is int) { SceneManager.LoadScene((int)value); }
        if (value is not null) { SceneManager.LoadScene(value); }
        //else { throw new NotImplementedException("Youboring"); }


    }
}

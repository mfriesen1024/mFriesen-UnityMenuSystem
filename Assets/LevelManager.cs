using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void loadScene(string value)
    {
        //if(value is int) { SceneManager.LoadScene((int)value); }
        if (value is string) { SceneManager.LoadScene((string)value); }
       //else { throw new NotImplementedException("Youboring"); }
    }
}

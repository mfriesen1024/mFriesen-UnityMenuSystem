using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash : MonoBehaviour
{
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5) { Application.Quit(); }
    }
}

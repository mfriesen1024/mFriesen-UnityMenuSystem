using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    enum buttonType
    {
        loadScene
    }

    [SerializeField]buttonType type = buttonType.loadScene;
    [SerializeField]int intValue;
    [SerializeField]string stringValue;



    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        try { SceneManager.LoadScene(stringValue); }catch(NullReferenceException e) { Debug.LogException(e); Debug.Log(e.StackTrace); }
        try { SceneManager.LoadScene(intValue); }catch(NullReferenceException e) { Debug.LogException(e); Debug.Log(e.StackTrace); }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTrigger : MonoBehaviour
{
    // The reason we use a separate component here is to be able to set a spawnpoint name, and a scene name.
    // Yes, we can use various bits of string tomfoolery and other data management, but I feel like what methods I know of are hard on level design.
    // I suppose I could also handle warping logic in GameManager, but position tracking seems more complicated there.

    GameManager gm;

    [SerializeField] public string spawnPoint;
    [SerializeField] public string levelName;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { gm.TryGameplayLoad(levelName, spawnPoint); }
    }
}

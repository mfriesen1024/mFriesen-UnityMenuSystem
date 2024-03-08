using UnityEngine;

enum TriggerType { warp, win, lose }

public class WarpTrigger : MonoBehaviour
{
    // The reason we use a separate component here is to be able to set a spawnpoint name, and a scene name.
    // Yes, we can use various bits of string tomfoolery and other data management, but I feel like what methods I know of are hard on level design.
    // I suppose I could also handle warping logic in GameManager, but position tracking seems more complicated there.

    GameManager gm;

    public string spawnPoint;
    public string levelName;
    [SerializeField] TriggerType type = TriggerType.warp;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (type)
        {
            case TriggerType.warp: if (collision.CompareTag("Player")) { gm.TryGameplayLoad(levelName, spawnPoint); } break;
            case TriggerType.lose: gm.SetState(5); break;
            case TriggerType.win: gm.SetState(7); break;
        }
    }
}

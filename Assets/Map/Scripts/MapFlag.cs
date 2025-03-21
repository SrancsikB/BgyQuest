using UnityEngine;

public class MapFlag : MonoBehaviour
{
    public enum FlagType
    {
        Disabled, Enabled, Finished
    }

    public enum GameGroup
    {
        Railway, Soldier, Hero
    }

    [SerializeField] Sprite[] sprites;
    public FlagType flagType = FlagType.Disabled;
    public GameGroup gameGroup;

    void Start()
    {
        Sprite sp=GetComponent<SpriteRenderer>().sprite;
        switch (flagType)
        {
            case FlagType.Disabled:
                sp = sprites[0];
                break;
            case FlagType.Enabled:
                sp = sprites[1];
                break;
            case FlagType.Finished:
                sp = sprites[2];
                break;
            default:
                break;
        }

        GetComponent<SpriteRenderer>().sprite = sp;
    }


    private void OnMouseDown()
    {
        GameSelect gs = FindObjectsByType<GameSelect>(FindObjectsInactive.Include, FindObjectsSortMode.None)[0];
        gs.Show(gameGroup);
        MapIconClick[] icons = FindObjectsByType<MapIconClick>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (MapIconClick item in icons)
        {
            item.gameGroup = gameGroup;
        }
    }


    
}

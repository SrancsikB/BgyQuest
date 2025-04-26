using UnityEngine;

public class RwCoalHeapController : MonoBehaviour
{
    public GameObject map;
    public float coalHeapShowTime;
    float coalHeapShowActTimer;
    public float coalHeapHideTime;
    float coalHeapHideActTimer;
    public int coalHeapQuantity;

    private void Start()
    {
        //CoalHeapRandomize();
    }



    //Updated by GameController, because inactive GO update stops
    public void UpdateRemote()
    {
        //Coal heap show / hide
        coalHeapShowActTimer -= Time.deltaTime;
        if (coalHeapShowActTimer < 0)
        {
            gameObject.SetActive(true);
            coalHeapHideActTimer -= Time.deltaTime;
            if (coalHeapHideActTimer < 0)
            {
                //coalHeap.SetActive(false);
                CoalHeapRandomize();

            }
        }
    }

    public void CoalHeapRandomize()
    {
        int rndMapTile = Random.Range(0, map.transform.childCount);
        gameObject.transform.position = map.transform.GetChild(rndMapTile).position;
        gameObject.SetActive(false); //Will show after timer 
        coalHeapShowActTimer = coalHeapShowTime;
        coalHeapHideActTimer = coalHeapHideTime;
    }

}

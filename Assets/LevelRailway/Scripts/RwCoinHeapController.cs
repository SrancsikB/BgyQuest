using UnityEngine;

public class RwCoinHeapController : MonoBehaviour
{
    public GameObject map;
    public float coinHeapShowTime;
    float coinHeapShowActTimer;
    public float coinHeapHideTime;
    float coinHeapHideActTimer;
    public int coinHeapQuantity;

    private void Start()
    {
        //CoinHeapRandomize();
    }



    //Updated by GameController, because inactive GO update stops
    public void UpdateRemote()
    {
        //Coin heap show / hide
        coinHeapShowActTimer -= Time.deltaTime;
        if (coinHeapShowActTimer < 0)
        {
            gameObject.SetActive(true);
            coinHeapHideActTimer -= Time.deltaTime;
            if (coinHeapHideActTimer < 0)
            {
                //coinHeap.SetActive(false);
                CoinHeapRandomize();

            }
        }
    }

    public void CoinHeapRandomize()
    {
        int rndMapTile = Random.Range(0, map.transform.childCount);
        gameObject.transform.position = map.transform.GetChild(rndMapTile).position;
        gameObject.SetActive(false); //Will show after timer 
        coinHeapShowActTimer = coinHeapShowTime;
        coinHeapHideActTimer = coinHeapHideTime;
    }

}

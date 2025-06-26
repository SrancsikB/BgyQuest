using UnityEngine;

public class PaGameSelectController : MonoBehaviour
{
    [SerializeField] PlayerDataControl.ParkProgressData paPD;
    [SerializeField] GameObject canvasUI;

    public int coinQuantity;


    void Awake()
    {
        canvasUI.SetActive(true);

        try //try to get Player data
        {

            PlayerDataControl pDC = PlayerDataControl.Instance;
            coinQuantity = pDC.coins;
            paPD = pDC.GetParkProgressData();


        }
        catch (System.Exception)
        {
            coinQuantity = 0;
            Debug.Log("SB: Player data cannot be aquired, using public progress data");
        }
    }


    private void Start()
    {
        PaUILevelSelector[] levels = FindObjectsByType<PaUILevelSelector>(FindObjectsSortMode.None);
        foreach (PaUILevelSelector level in levels)
        {
            if (level.levelNumber > paPD.paMapLevel)
            {
                level.GetComponent<Collider2D>().enabled = false;
                level.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
            }
        }
    }


    void Update()
    {
        FindFirstObjectByType<UICoin>().coinQuantity = coinQuantity;
    }
}

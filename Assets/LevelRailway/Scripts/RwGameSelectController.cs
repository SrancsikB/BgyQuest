using UnityEngine;


public class RwGameSelectController : MonoBehaviour
{
    [SerializeField] PlayerDataControl.RailwayPogressData rwPD;
    [SerializeField] GameObject canvasUI;

    public int coinQuantity;


    void Awake()
    {
        canvasUI.SetActive(true);

        try //try to get Player data
        {

            PlayerDataControl pDC = PlayerDataControl.Instance;
            coinQuantity = pDC.coins;
            rwPD = pDC.GetRailwayProgressData();


        }
        catch (System.Exception)
        {
            coinQuantity = 0;
            Debug.Log("SB: Player data cannot be aquired, using public progress data");
        }
    }


    private void Start()
    {
        UILevelSelector[] levels = FindObjectsByType<UILevelSelector>(FindObjectsSortMode.None);
        foreach (UILevelSelector level in levels)
        {
            if (level.levelNumber > rwPD.rwMapLevel)
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

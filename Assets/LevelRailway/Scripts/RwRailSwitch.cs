using UnityEngine;

public class RwRailSwitch : MonoBehaviour
{

    public KeyCode switchButton;
    [SerializeField] Sprite alternateSprite;
    [SerializeField] RwRailController.RailType alternateRailType;

    public float switchingTime = 3;
    public float elapsedTime = 0;
    public bool triggerSwitch;

    Sprite defaultSprite;
    RwRailController.RailType defaultRailtype;
    bool switched;
    bool switchable = true;

    GameObject Canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        defaultRailtype = GetComponent<RwRailController>().railType;
        elapsedTime = 0;
        try
        {
            Canvas = GetComponentInChildren<Canvas>().gameObject; //transform.GetChild(0).gameObject;
            Canvas.GetComponentInChildren<UISwitchingNum>().switchingNum = switchButton.ToString();
            Canvas.GetComponentInChildren<UISwicthingTime>().switchingTime = switchingTime;
        }
        catch (System.Exception)
        {

            
        }

        
    }


    private void OnValidate()
    {
        
        RwRailController[] alterRail = GetComponentsInChildren<RwRailController>(true);
        alternateRailType = alterRail[1].railType;
        alternateSprite = alterRail[1].GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Canvas.GetComponentInChildren<UISwicthingTime>().elapsedTime = elapsedTime;
        }
        catch (System.Exception)
        {
                        
        }
        
        if (switchable)
        {
            if (elapsedTime == 0)
            {


                GetComponent<SpriteRenderer>().color = new Color(0.5f, 1, 0.5f, 1);
                if (Input.GetKeyDown(switchButton) || triggerSwitch)
                {
                    triggerSwitch = false;
                    elapsedTime += Time.deltaTime;
                }
            }
            else
            {
                //Switching in progress
                triggerSwitch = false; //Disable switch trigger
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1f, 1);
                elapsedTime += Time.deltaTime;
                if (elapsedTime>=switchingTime)
                {
                    //Switching ready
                    elapsedTime = 0;
                    switched = !switched;
                    if (switched == true)
                    {

                        GetComponent<SpriteRenderer>().sprite = alternateSprite;
                        GetComponent<RwRailController>().railType = alternateRailType;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = defaultSprite;
                        GetComponent<RwRailController>().railType = defaultRailtype;
                    }
                }
                
            }
            
        }
        else
        {
            
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switchable = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switchable = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switchable = true;
    }
}

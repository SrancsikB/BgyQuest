using UnityEngine;

public class RailSwitch : MonoBehaviour
{

    [SerializeField] KeyCode switchButton;
    [SerializeField] Sprite alternateSprite;
    [SerializeField] RailController.RailType alternateRailType;

    public float switchingTime = 3;
    public float elapsedTime = 0;

    Sprite defaultSprite;
    RailController.RailType defaultRailtype;
    bool switched;
    bool switchable = true;

    GameObject Canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        defaultRailtype = GetComponent<RailController>().railType;
        elapsedTime = 0;
        try
        {
            Canvas = transform.GetChild(0).gameObject;
            Canvas.GetComponentInChildren<UISwitchingNum>().switchingNum = switchButton.ToString();
            Canvas.GetComponentInChildren<UISwicthingTime>().switchingTime = switchingTime;
        }
        catch (System.Exception)
        {

            
        }

        
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
                if (Input.GetKeyDown(switchButton))
                {
                    elapsedTime += Time.deltaTime;
                }
            }
            else
            {
                //Switching in progress
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1f, 1);
                elapsedTime += Time.deltaTime;
                if (elapsedTime>=switchingTime)
                {
                    //Swtiching ready
                    elapsedTime = 0;
                    switched = !switched;
                    if (switched == true)
                    {

                        GetComponent<SpriteRenderer>().sprite = alternateSprite;
                        GetComponent<RailController>().railType = alternateRailType;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = defaultSprite;
                        GetComponent<RailController>().railType = defaultRailtype;
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

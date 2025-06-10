using TMPro;
using UnityEngine;


public class PaNpc : MonoBehaviour
{
    enum NpcType
    {
        Lion
    }

    [SerializeField] NpcType npcType;
    [SerializeField] GameObject sightGO;
    [SerializeField] GameObject msgBox;
    [SerializeField] TextMeshProUGUI text;

    Collider2D sightCollider;
    int msgIndex = 0;

    void Start()
    {
        sightCollider = sightGO.GetComponent<Collider2D>();
        msgBox.SetActive(false);
        msgIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (npcType)
        {
            case NpcType.Lion:
                switch (msgIndex)
                {
                    case 1:
                        text.text = "Hi Bobo! I'm the old Lion. My job is to guard the Park...";
                        break;
                    case 2:
                        text.text = "Unfortunately I felt asleep too early and some strange things happend in the Park...";
                        break;
                    case 3:
                        text.text = "Some letters has been stolen from the Museum's sign...";
                        break;
                    case 4:
                        text.text = "Some of the Stations has been moved...";
                        break;
                    case 5:
                        text.text = "Bobo, please help me to solve these mysteries...";
                        break;
                    case 6:
                        text.text = "Find the letters and place the Stations!";
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }


    }


    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.GetRayIntersection(ray);
        if (hit.transform.gameObject.name == this.name)
        {
            msgIndex += 1;

            switch (npcType)
            {
                case NpcType.Lion:
                    if (msgIndex > 6)
                    {
                        msgIndex = 5;
                    }
                    break;
                default:
                    break;
            }

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PaCat cat = collision.transform.GetComponent<PaCat>();
        if (cat != null)
        {
            msgBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PaCat cat = collision.transform.GetComponent<PaCat>();
        if (cat != null)
        {
            msgBox.SetActive(false);
        }
    }

}

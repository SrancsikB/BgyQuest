using TMPro;
using UnityEngine;


public class PaNpc : MonoBehaviour
{
    public enum NpcType
    {
        Lion, Crow, GangBoss, GangMember1, GangMember2, GangMember3
    }

    public NpcType npcType;
    [SerializeField] GameObject sightGO;
    [SerializeField] GameObject msgBox;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject reward;
    public Vector3 alternatePos1;
    public Vector3 alternatePos2;
    PaGameController gc;

    Collider2D sightCollider;
    int msgIndex = 0;

    public Animator animator;

    void Start()
    {

        sightCollider = sightGO.GetComponent<Collider2D>();
        msgBox.SetActive(false);
        msgIndex = 1;
        gc = FindFirstObjectByType<PaGameController>();
        animator = GetComponent<Animator>();
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
            case NpcType.Crow:
                switch (msgIndex)
                {
                    case 1:
                        text.text = "Hi Bobo! I'm the Crow. Do you want to help?";
                        break;
                    case 2:
                        text.text = "Please collect 3 packs of grass for me to finish my nest!";
                        break;
                    case 3:
                        text.text = "I will give you this puzzle piece...";
                        break;
                    case 4:
                        text.text = "Thanks Bobo! Here is the puzzle piece!";
                        break;
                    case 5:
                        text.text = "Thanks Bobo for your help!";
                        break;
                    default:
                        break;
                }
                break;

            case NpcType.GangBoss:
                switch (msgIndex)
                {
                    case 1:
                        text.text = "Hi Bobo! I'm the Boss of the biggest gang here in the park!";
                        break;
                    case 2:
                        text.text = "I know you have some issue with those dogs...";
                        break;
                    case 3:
                        text.text = "Look, find my lazy bastard gang members!";
                        break;
                    case 4:
                        text.text = "Bring them here and we will help you!";
                        break;
                    case 5:
                        text.text = "It wasn't fast, lazy bastards!";
                        break;
                    case 6:
                        text.text = "Fine, the gang is full again!";
                        break;
                    case 7:
                        text.text = "Let's go and deal with the dogs!";
                        break;
                    case 8:
                        text.text = "Bobo, climb up while we keep them busy!";
                        break;
                    default:
                        break;
                }
                break;
            case NpcType.GangMember1:
                switch (msgIndex)
                {
                    case 1:
                        text.text = "Ok, I know, the Boss is looking for me... see you there";
                        break;

                    default:
                        break;
                }
                break;
            case NpcType.GangMember2:
                switch (msgIndex)
                {
                    case 1:
                        text.text = "Oh, the Boss, I'm in trouble... going...";
                        break;

                    default:
                        break;
                }
                break;
            case NpcType.GangMember3:
                switch (msgIndex)
                {
                    case 1:
                        text.text = "I've just felt asleep... meet there...";
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
                case NpcType.Crow:
                    if (gc.CollectedItems < 3)
                    {
                        if (msgIndex > 3)
                        {
                            msgIndex = 1;
                        }
                    }
                    else
                    {
                        if (msgIndex > 5)
                        {
                            msgIndex = 5;
                        }
                    }
                    break;
                case NpcType.GangBoss:
                    if (gc.CollectedItems < 3)
                    {
                        if (msgIndex > 4)
                        {
                            msgIndex = 1;
                        }
                    }
                    else
                    {
                        if (msgIndex > 8)
                        {
                            //Attack the dogs
                            PaAnimal[] dogs = FindObjectsByType<PaAnimal>(FindObjectsSortMode.None);
                            foreach (PaAnimal dog in dogs)
                            {
                                //Flip dogs and bark
                                float direction = dog.transform.localScale.x;
                                dog.transform.localScale = new Vector3(-1 * direction, 1, 1);
                                dog.animator.SetBool("Action", true);
                                dog.timerOfAction = Mathf.NegativeInfinity;
                            }

                            PaNpc[] Gang = FindObjectsByType<PaNpc>(FindObjectsSortMode.None);

                            foreach (PaNpc cat in Gang)
                            {
                                if (cat.npcType==NpcType.GangBoss || cat.npcType == NpcType.GangMember2)
                                {
                                    cat.transform.position = cat.alternatePos2;
                                    //transform.localScale = new Vector3(-1, 1, 1);
                                    cat.sightGO.SetActive(false);
                                    cat.animator.SetBool("Attack", true);
                                }
                                else if (cat.npcType == NpcType.GangMember1 || cat.npcType == NpcType.GangMember3)
                                {
                                    cat.transform.position = cat.alternatePos2;
                                    cat.transform.localScale = new Vector3(1, 1, 1);
                                    cat.sightGO.SetActive(false);
                                    cat.animator.SetBool("Attack", true);
                                }
                            }

                            reward.GetComponent<QuizAnimReward>().StartFlipAnim();

                        }
                    }

                    break;
                case NpcType.GangMember1:

                    if (msgIndex > 1)
                    {
                        msgIndex = 1;
                        transform.position = alternatePos1;
                        transform.localScale = new Vector3(-1, 1, 1);
                        sightGO.SetActive(false);
                        gc.CollectedItems += 1;
                    }


                    break;
                case NpcType.GangMember2:

                    if (msgIndex > 1)
                    {
                        msgIndex = 1;
                        transform.position = alternatePos1;
                        //transform.localScale = new Vector3(-1, 1, 1);
                        sightGO.SetActive(false);
                        gc.CollectedItems += 1;
                    }


                    break;
                case NpcType.GangMember3:

                    if (msgIndex > 1)
                    {
                        msgIndex = 1;
                        transform.position = alternatePos1;
                        transform.localScale = new Vector3(-1, 1, 1);
                        sightGO.SetActive(false);
                        gc.CollectedItems += 1;
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
            switch (npcType)
            {
                case NpcType.Crow:
                    if (gc.CollectedItems >= 3 && msgIndex < 5)
                    {
                        msgIndex = 4;
                        reward.GetComponent<QuizAnimReward>().StartFlipAnim();
                        gc.levelFinished = true;
                    }

                    break;
                case NpcType.GangBoss:
                    animator.SetBool("Idle", true);
                   
                    break;
                case NpcType.GangMember1 or NpcType.GangMember2 or NpcType.GangMember3:
                    animator.SetBool("Idle", true);
                    break;
                default:
                    break;
            }
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

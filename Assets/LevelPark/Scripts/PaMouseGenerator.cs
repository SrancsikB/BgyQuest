using UnityEngine;

public class PaMouseGenerator : MonoBehaviour
{

    [SerializeField] PaMouse mouseV1;
    [SerializeField] PaMouse mouseV2;
    [SerializeField] PaMouse mouseV3;
    [SerializeField] float minGenTime = 2;
    [SerializeField] float maxGenTime = 10;
    [SerializeField] GameObject leftBush;
    [SerializeField] GameObject rightBush;
    [SerializeField] GameObject cat;

    float timeToGen;

    void InitGen()
    {
        timeToGen = Random.Range(minGenTime, maxGenTime);
    }

    private void Start()
    {
        InitGen();
    }

    private void Update()
    {
        float bushSize = 3;
        if (cat.transform.position.x > leftBush.transform.position.x - bushSize && cat.transform.position.x < rightBush.transform.position.x + bushSize
            && cat.transform.position.y < leftBush.transform.position.y + bushSize)
        {
            //Cat "visible"
            leftBush.transform.GetChild(0).gameObject.SetActive(false);
            rightBush.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            leftBush.transform.GetChild(0).gameObject.SetActive(true);
            rightBush.transform.GetChild(0).gameObject.SetActive(true);

            timeToGen -= Time.deltaTime;
            if (timeToGen < 0)
            {
                int dir = 0;
                while (dir == 0)
                {
                    dir = Random.Range(-1, 2);
                }

                Vector3 startPos;
                Vector3 endPos;

                if (dir == 1)
                {
                    startPos = leftBush.transform.position;
                    endPos = rightBush.transform.position;
                }
                else
                {
                    startPos = rightBush.transform.position;
                    endPos = leftBush.transform.position;
                }

                int mouseVersion = Random.Range(1, 4);
                PaMouse mouse=mouseV1;

                if (mouseVersion == 1)
                {
                    mouse = Instantiate(mouseV1, startPos, Quaternion.Euler(0, 0, 0));
                }
                else if (mouseVersion == 2)
                {
                    mouse = Instantiate(mouseV2, startPos, Quaternion.Euler(0, 0, 0));
                }
                else if (mouseVersion == 3)
                {
                    mouse = Instantiate(mouseV3, startPos, Quaternion.Euler(0, 0, 0));
                }


                mouse.direction = dir;
                mouse.endPos = endPos;

                InitGen();

            }
        }
    }

}

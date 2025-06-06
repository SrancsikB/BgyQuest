using UnityEngine;

public class PaParallax : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
   

    GameObject[] backGrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [SerializeField] float parallaxSpeed;
    [SerializeField] float yOffset;

    private void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;
        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backGrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backGrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backGrounds[i].GetComponent<Renderer>().material;

        }

        BackSpeedCalculation(backCount);
    }


    void BackSpeedCalculation(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backGrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = (backGrounds[i].transform.position.z - cam.position.z);
            }

        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backGrounds[i].transform.position.z - cam.position.z) / farthestBack;

        }
    }


    public void LateUpdate()
    {
        float distanceX = cam.position.x - camStartPos.x;
        float distanceY = 0;//cam.position.y - camStartPos.y;
        transform.position = new Vector3(cam.position.x, cam.position.y + yOffset, 0);
        for (int i = 0; i < backGrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distanceX, distanceY) * speed);
        }
    }


}

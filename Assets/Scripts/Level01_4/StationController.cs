using UnityEngine;

public class StationController : MonoBehaviour
{

    public StationNames stationName = StationNames.Bgy;
    public GameObject goStop;
    Vector3 startScale;
    float scaler=-0.1f;

    public enum StationNames
    {
        None, Bgy, Szugy, Patvarc, Mohora, Ipolyszog, Dregely, Belgrad, Zagrab

    }

    private void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scaleRate = 0.8f;
        if (goStop != null && goStop.activeSelf==true)
        {
            transform.localScale -= transform.localScale * scaler * Time.deltaTime;
            if (transform.localScale.x< scaleRate)
            {
                transform.localScale = Vector3.one * scaleRate;
                scaler *= -1;
            }
            if (transform.localScale.x > startScale.x)
            {
                transform.localScale = startScale;
                scaler *= -1;
            }
        }
    }


    private void OnValidate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10 - (int)transform.position.y;
    }
}

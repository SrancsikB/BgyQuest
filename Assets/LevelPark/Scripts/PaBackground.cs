using UnityEngine;

public class PaBackground : MonoBehaviour
{
    [SerializeField] GameObject trees;
    [SerializeField] GameObject cloudsSmall;
    [SerializeField] GameObject mountains;
    [SerializeField] GameObject cloudsBack1;
    [SerializeField] GameObject cloudsBack2;
    [SerializeField] float xOffset = 10;
    [SerializeField] float yOffset = 10;

    public Vector2 cameraPosition;
    public Vector2 referencePosition;

    private void Update()
    {
        trees.transform.position = new Vector2(cameraPosition.x - referencePosition.x / xOffset, yOffset);
        cloudsSmall.transform.position = new Vector2(cameraPosition.x - referencePosition.x / xOffset/2, yOffset);
        mountains.transform.position = new Vector2(cameraPosition.x - referencePosition.x / xOffset / 4, yOffset);
    }
}

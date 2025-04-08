using UnityEngine;

public class CoalHeapController : MonoBehaviour
{
    
    private void Start()
    {
        // This does nothing, used for collison detect compnent find, as Csaba loves :)
        Debug.Log("Started");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
    }
}

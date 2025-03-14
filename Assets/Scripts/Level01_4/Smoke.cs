using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] float duration = 3;
    float age;


    // Update is called once per frame
    void Update()
    {
        
        age += Time.deltaTime;
                
        if (age >= duration)
        {
            Destroy(gameObject);
        }
    }

}

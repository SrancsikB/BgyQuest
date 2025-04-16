using UnityEngine;

public class RwChainController : MonoBehaviour
{
 [SerializeField] Transform target;  
    void Update()
    {
        //float parentRot = transform.parent.rotation.eulerAngles.z;
        //transform.localRotation = Quaternion.Euler(0, 0, parentRot);

        Vector2 direction = target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.down, direction);
    }
}

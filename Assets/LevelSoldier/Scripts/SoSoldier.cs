using UnityEngine;

public class SoSoldier : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfProjectile;
    [SerializeField] float forceOfProjectile;
    [SerializeField] float lifetimeOfProjectile;
    float timeToProjectile;
    GameObject goSolder;

    void Start()
    {
        goSolder = transform.GetChild(0).gameObject;
        goSolder.transform.position = Vector3.up * radius;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 dir = new Vector3(ray.origin.x, ray.origin.y, 0) - transform.position;
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
        }

        timeToProjectile += Time.deltaTime;
        if (timeToProjectile >= rateOfProjectile)
        {
            timeToProjectile = 0;
            GameObject proj = Instantiate(projectile, goSolder.transform.position, transform.rotation);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.AddForce(goSolder.transform.position * forceOfProjectile, ForceMode2D.Impulse);

            Destroy(proj, lifetimeOfProjectile);
        }

    }
}

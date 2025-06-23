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

    [SerializeField] GameObject bonus;
    [SerializeField] float rateOfBonus;
    [SerializeField] float forceOfBonus;
    [SerializeField] float lifetimeOfBonus;
    float timeToBonus;

    [SerializeField] float lifetimeOfAcitveBonus;
    float timeToActiveBonus;
    bool activeBonusSpread;

    [SerializeField] float blastForce = 1;

    GameObject goSolder;

    void Start()
    {
        goSolder = transform.GetChild(0).gameObject;
        goSolder.transform.position = Vector3.up * radius;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    void Update()
    {
        //Move
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 dir = new Vector3(ray.origin.x, ray.origin.y, 0) - transform.position;
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
        }

        //Shoot
        timeToProjectile += Time.deltaTime;
        if (timeToProjectile >= rateOfProjectile)
        {
            timeToProjectile = 0;

            FireProjectile(0);
            if (activeBonusSpread)
            {
                FireProjectile(10);
                FireProjectile(-10);
                FireProjectile(20);
                FireProjectile(-20);
            }
          
        }


        //Bonus
        timeToBonus += Time.deltaTime;
        if (timeToBonus >= rateOfBonus)
        {
            timeToBonus = 0;

            GenerateBonus();
        }

        //Active bonus
        timeToActiveBonus += Time.deltaTime;
        if (timeToActiveBonus>=lifetimeOfAcitveBonus)
        {
            activeBonusSpread = false;
        }


        //Temp
        if (Input.GetMouseButtonDown(1))
        {
            //SoEnemy[] enemies = FindObjectsByType<SoEnemy>(FindObjectsSortMode.None);
            //foreach (SoEnemy enemy in enemies)
            //{
            //    enemy.ApplyBlast(blastForce);
            //}
        }

    }

    void FireProjectile(float degree)
    {
        GameObject proj = Instantiate(projectile, goSolder.transform.position, transform.rotation);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        Vector3 firingVector = goSolder.transform.position;
        proj.transform.Rotate(Vector3.forward, degree);
        firingVector = Quaternion.Euler(0, 0, degree) * firingVector;
        rb.AddForce(firingVector * forceOfProjectile, ForceMode2D.Impulse);
        Destroy(proj, lifetimeOfProjectile);
    }

    void GenerateBonus()
    {
        GameObject proj = Instantiate(bonus, transform.position, transform.rotation);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        Vector3 pos = Random.insideUnitCircle;
        pos.Normalize();
        rb.AddForce(pos * forceOfBonus, ForceMode2D.Impulse);
        Destroy(proj, lifetimeOfBonus);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoBonus bonus = collision.gameObject.GetComponent<SoBonus>();
        if (bonus != null)
        {
            Destroy(collision.gameObject);
            switch (bonus.bonusType)
            {
                case SoBonus.BonusType.Blast:
                    SoEnemy[] enemies = FindObjectsByType<SoEnemy>(FindObjectsSortMode.None);
                    foreach (SoEnemy enemy in enemies)
                    {
                        enemy.ApplyBlast(blastForce);
                    }
                    break;
                case SoBonus.BonusType.Spread:
                    activeBonusSpread = true;
                    timeToActiveBonus = 0;
                    break;
                default:
                    break;
            }
           
        }
    }
}

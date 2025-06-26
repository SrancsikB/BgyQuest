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
    bool activeBonusRate;
    bool activeBonusRing;
    int counterBonusRing;

    [SerializeField] float blastForce = 1;

    GameObject goSolder;
    Animator animator;

    void Start()
    {
        goSolder = transform.GetChild(0).gameObject;
        goSolder.transform.position = Vector3.up * radius;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        animator = goSolder.GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        //Move
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 dir = new Vector3(ray.origin.x, ray.origin.y, 0) - transform.position;
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.fixedDeltaTime * rotationSpeed);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        //Shoot
        timeToProjectile += Time.fixedDeltaTime;
        if (activeBonusRate) //Faster proj time by bonus
        {
            timeToProjectile += Time.fixedDeltaTime * 2;
        }

        if (timeToProjectile >= rateOfProjectile)
        {
            timeToProjectile = 0;

            FireProjectile(0);
            if (activeBonusSpread) //More proj by bonus
            {
                FireProjectile(10);
                FireProjectile(-10);
                FireProjectile(20);
                FireProjectile(-20);
            }

            animator.SetBool("Push", true);
        }
        else if (timeToProjectile >= rateOfProjectile / 4)
        {
            animator.SetBool("Push", false);
        }


        //Bonus
        timeToBonus += Time.fixedDeltaTime;
        if (timeToBonus >= rateOfBonus)
        {
            timeToBonus = 0;

            GenerateBonus();
        }
        

        //Active bonus
        timeToActiveBonus += Time.fixedDeltaTime;
        if (timeToActiveBonus >= lifetimeOfAcitveBonus)
        {
            activeBonusSpread = false;
            activeBonusRate = false;
        }

        //Ring bonus angle increase
        if (activeBonusRing)
        {
            FireProjectile(counterBonusRing);
            counterBonusRing += 30;
            if (counterBonusRing >= 360)
            {
                activeBonusRing = false;
                counterBonusRing = 0;
            }
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
                case SoBonus.BonusType.Rate:
                    activeBonusRate = true;
                    timeToActiveBonus = 0;
                    break;
                case SoBonus.BonusType.Ring:
                    activeBonusRing = true;
                    counterBonusRing = 0;
                    break;
                default:
                    break;
            }

        }
    }
}

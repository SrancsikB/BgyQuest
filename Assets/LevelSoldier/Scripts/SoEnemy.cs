using UnityEngine;

public class SoEnemy : MonoBehaviour
{

    SoSoldier soldier;
    [SerializeField] float force;
    [SerializeField] float forceUpdateTime=2;
    float timeToUpdateForce = 0;
    Rigidbody2D rb;

    void Start()
    {
        soldier = FindFirstObjectByType<SoSoldier>();
        rb = GetComponent<Rigidbody2D>();
        UpdateForce();
    }

   
    void Update()
    {
        timeToUpdateForce += Time.deltaTime;
        if (timeToUpdateForce>=forceUpdateTime)
        {
            timeToUpdateForce = 0;
            UpdateForce();
        }

    }

    private void UpdateForce()
    {
        rb.angularVelocity = 0;
        rb.linearVelocity = Vector2.zero;
        Vector2 forceVector = new Vector2(soldier.transform.position.x - transform.position.x, soldier.transform.position.y - transform.position.y);
        forceVector.Normalize();
        rb.AddForce(forceVector * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //timeToUpdateForce = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}

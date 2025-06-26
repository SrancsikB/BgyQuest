using UnityEngine;

public class SoEnemy : MonoBehaviour
{

    SoSoldier soldier;
    [SerializeField] float force;
    [SerializeField] float forceUpdateTime = 2;
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
        if (timeToUpdateForce >= forceUpdateTime)
        {
            timeToUpdateForce = 0;
            UpdateForce();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).localPosition = Vector2.up * (1 - (timeToUpdateForce / forceUpdateTime)) * 0.8f;
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


    public void ApplyBlast(float blastForce)
    {
        Vector2 forceVector = new Vector2(transform.position.x - soldier.transform.GetChild(0).position.x, transform.position.y - soldier.transform.GetChild(0).position.y);
        float distance = Vector3.Magnitude(forceVector);
        //forceVector.Normalize();
        rb.AddForce(forceVector * blastForce / distance, ForceMode2D.Impulse);
        timeToUpdateForce = 1;
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

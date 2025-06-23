using UnityEngine;

public class SoEnemyController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject field;
    [SerializeField] float generateTime = 10;
    float timeToGenerate;
    [SerializeField] int maxGenerationNum;
    int currentGenerationNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstantetEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        timeToGenerate += Time.deltaTime;
        if (timeToGenerate >= generateTime)
        {
            timeToGenerate = 0;
            if (currentGenerationNum < maxGenerationNum)
            {
                InstantetEnemy();
            }

        }
    }


    void InstantetEnemy()
    {
        Vector3 pos = Random.insideUnitCircle;
        pos.Normalize();
        pos *= (field.transform.localScale.x - 1) / 2;
        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.Euler(0, 0, 0));

        currentGenerationNum += 1;
    }
}

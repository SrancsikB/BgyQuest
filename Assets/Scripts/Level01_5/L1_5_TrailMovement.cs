using System.Collections.Generic;
using UnityEngine;

public class L1_5_TrailMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 1;
    [SerializeField] List<Vector3> currentTransform = new List<Vector3>();
    [SerializeField] List<Vector3> previousTransform = new List<Vector3>();
    [SerializeField] GameObject follower;
    bool movementEnable = false;
    Vector3 startPos;
    Vector3 targetPos;
    Vector3 rotPos;

    private float startTime;
    private float journeyLength;

    private void Start()
    {
        follower.transform.position = transform.position;
    }

    void FixedUpdate()
    {

        if (movementEnable == false && Input.GetKeyDown(KeyCode.Space))
        {

            targetPos = new Vector3(transform.position.x, Mathf.CeilToInt(transform.position.y), 0);
            startPos = transform.position;
            targetPos += new Vector3(0, 0.5f, 0);
            movementEnable = true;

            journeyLength = Vector3.Distance(startPos, targetPos);
            startTime = Time.time;
        }

        if (movementEnable == true)
        {
            if (previousTransform.Count>0)
            {
                follower.transform.position = previousTransform[0];
                previousTransform.RemoveAt(0);
            }

            float distCovered = (Time.time - startTime) * maxSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            if (journeyLength != 0)
                transform.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);

            if (transform.position == targetPos)
            {

                movementEnable = false;
                //previousTransform = new List<Transform>(currentTransform);
                previousTransform = currentTransform.GetRange(0,currentTransform.Count);
                currentTransform.Clear();
            }
            else
            {
                currentTransform.Add(transform.position);
            }
        }
    }

}


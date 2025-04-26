using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour


{
    [SerializeField] float Speed = 5;
    Vector3 targetLocation;
    Dictionary<string, Vector3> Targets = new Dictionary<string, Vector3>();
    public bool mapInitDone = false;


    public void AddTarget(string key, Vector3 target)
    {
        Targets.Add(key, target);
    }


    private void SetTargetLocation(Vector3 Target)
    {
        targetLocation = new Vector3(Target.x, Target.y, transform.position.z);
    }

    private bool CompareTargetLocation(Vector3 Target)
    {
        Debug.Log(transform.position.x + " " + transform.position.y);
        Debug.Log(Target.x + " " + Target.y);
        if (transform.position.x == Target.x && transform.position.y == Target.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (mapInitDone) //First run
        {
            SetTargetLocation(Targets["RightUpTurn"]);
            mapInitDone = false;
        }



        if (CompareTargetLocation(Targets["RightUpTurn"]))
        {
            SetTargetLocation(Targets["Switch1"]);
        }



        transform.position = Vector3.MoveTowards(transform.position, targetLocation, Speed * Time.deltaTime);



        //   Vector3 myLocation = transform.position;
        //   targetLocation.z = myLocation.z; // ensure there is no 3D rotation by aligning Z position

        // vector from this object towards the target location
        //    Vector3 vectorToTarget = Target.position - myLocation;
        // rotate that vector by 90 degrees around the Z axis
        //    Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
        // (resulting in the X axis facing the target)
        //    Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        // changed this from a lerp to a RotateTowards because you were supplying a "speed" not an interpolation value
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
        //GO_Train.transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.Self);

        /*Vector3 dir = Target.position - GO_Train.transform.position;

        if (dir != Vector3.zero)
        {
            GO_Train.transform.rotation = Quaternion.LookRotation(dir);
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class SideCarLDriving : MonoBehaviour
{
    public List<GameObject> wayPoints_Straight;
    public List<GameObject> wayPoints_StepIn_Accident;
    public List<GameObject> wayPoints_StepIn_Safe;

    public float speed = 80f * 1000f / 3600f;
    public int index = 0;

    [Range(1, 6)] public int accidentScenario;

    public int stepInRandomize;

    bool canDrive = false;

    bool randomizeDone = false;

    Vector3 initPos;
    Quaternion initRot;

    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            canDrive = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            canDrive = false;
            index = 0;
            transform.position = initPos;
            transform.rotation = initRot;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(accidentScenario < 6)
            {
                accidentScenario += 1;
            }
            else{
                accidentScenario = 1;
            }
        }
        if (canDrive == true)
        {
            switch (accidentScenario)
            {
                case 1: // LC sudden brake
                    MoveTo(wayPoints_Straight);
                    break;
                case 2: // LC lane change & Obstacle
                    MoveTo(wayPoints_Straight);
                    break;
                case 3: // LC straight no accident
                    MoveTo(wayPoints_Straight);
                    break;
                case 4: // LC lane change & no accident
                    MoveTo(wayPoints_Straight);
                    break;
                case 5: // SC step in & left/right random
                    // PathRandom();
                    if (stepInRandomize == 0)
                    {
                        MoveTo(wayPoints_Straight);
                    }
                    else
                    {
                        MoveTo(wayPoints_StepIn_Accident);
                    }
                    break;
                case 6: // SC step in & left/right random & no accident
                    // PathRandom(); 
                    if (stepInRandomize == 0)
                    {
                        MoveTo(wayPoints_Straight);
                    }
                    else
                    {
                        MoveTo(wayPoints_StepIn_Safe);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void MoveTo(List<GameObject> _wayPoint)
    {
        Vector3 destination = _wayPoint[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        // transform.LookAt(wayPoints_Accident1[index].transform);

        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.05f)
        {
            if (index < _wayPoint.Count - 1)
            {
                index += 1;
            }
        }
    }

    void PathRandom()
    {
        if (randomizeDone == false)
        {
            stepInRandomize = Random.Range(0, 2);
            randomizeDone = true;
        }
    }
}

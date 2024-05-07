using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class SideCarLDriving : MonoBehaviour
{
    public List<GameObject> wayPoints_Straight;
    public List<GameObject> wayPoints_StepIn_Correct;
    public List<GameObject> wayPoints_StepIn_Fake;

    public float speed = 80f * 1000f / 3600f;
    public int index = 0;

    [Range(1, 7)] public int accidentScenario;

    public int stepInRandomize;

    bool canDrive = false;

    bool randomizeDone = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            canDrive = true;
        }
        if (canDrive == true)
        {
            switch (accidentScenario)
            {
                case 1:
                    MoveTo(wayPoints_Straight);
                    break;
                case 2:
                    MoveTo(wayPoints_Straight);

                    break;
                case 3:
                    PathRandom(); 
                    if (stepInRandomize == 0)
                    {
                        MoveTo(wayPoints_Straight);
                    }
                    else
                    {
                        MoveTo(wayPoints_StepIn_Correct);
                    }
                    break;
                case 4:
                    MoveTo(wayPoints_Straight);

                    break;
                case 5:
                    MoveTo(wayPoints_Straight);

                    break;
                case 6:
                    PathRandom(); 
                    if (stepInRandomize == 0)
                    {
                        MoveTo(wayPoints_Straight);
                    }
                    else
                    {
                        MoveTo(wayPoints_StepIn_Fake);
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

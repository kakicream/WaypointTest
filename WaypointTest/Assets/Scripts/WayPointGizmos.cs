using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointGizmos : MonoBehaviour
{
    public Transform target;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));

        if (target != null)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}

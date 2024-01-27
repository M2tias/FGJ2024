using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Waypoint nextWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextWaypoint != null)
        {
            Debug.DrawLine(transform.position, nextWaypoint.transform.position, Color.blue);
        }
    }

    public Waypoint NextWaypoint()
    {
        return nextWaypoint;
    }
}

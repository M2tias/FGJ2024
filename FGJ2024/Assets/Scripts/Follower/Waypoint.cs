using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public Waypoint NextWaypoint { get { return nextWaypoint; } set { nextWaypoint = value; } }
    public Waypoint PreviousWaypoint { get { return previousWaypoint; } set { previousWaypoint = value; } }
    [SerializeField]
    private Waypoint nextWaypoint;
    [SerializeField]
    private Waypoint previousWaypoint;

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
}

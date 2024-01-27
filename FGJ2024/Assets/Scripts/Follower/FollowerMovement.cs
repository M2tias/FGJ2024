using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerMovement : MonoBehaviour
{
    public bool HasMoved { get; set; }

    [SerializeField]
    private Waypoint nextWaypoint;
    private Waypoint previousWaypoint;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(nextWaypoint.transform.position);
        agent.isStopped = false;
        HasMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"{gameObject.name}: {(HasMoved ? "Y" : "N")}");
        Vector2 waypoint = new Vector2(nextWaypoint.transform.position.x, nextWaypoint.transform.position.z);
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);

        if ((waypoint - currentPos).magnitude < 0.1f)
        {
            previousWaypoint = nextWaypoint;
            nextWaypoint = nextWaypoint.NextWaypoint();
            agent.isStopped = true;
            if (!HasMoved)
            {
                HasMoved = true;
                Debug.Log("HasMoved");
            }
        }

        if (GameManager.main.CanMove && !HasMoved)
        {
            Debug.Log("Start next waypoint");
            StartNextWaypoint();
        }
    }

    void StartNextWaypoint()
    {
        agent.SetDestination(nextWaypoint.transform.position);
        agent.isStopped = false;
       // HasMoved = false;
    }

    public Waypoint CurrentWaypoint()
    {
        return nextWaypoint;
    }

    public Waypoint PreviousWaypoint()
    {
        return previousWaypoint;
    }

    public void SetWaypoint(Waypoint waypoint)
    {
        nextWaypoint = waypoint;
    }
}

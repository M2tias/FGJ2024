using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerMovement : MonoBehaviour
{
    public bool HasMoved { get; set; }

    [SerializeField]
    private Waypoint currentWaypoint;
    private Waypoint previousWaypoint;

    private NavMeshAgent agent;

    private bool moveStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        HasMoved = false;
    }

    void Update()
    {
        if (currentWaypoint == null)
        {
            agent.SetDestination(GameManager.main.GetPlayer().position);
            currentWaypoint = previousWaypoint.NextWaypoint;
        }
        else
        {
            agent.SetDestination(currentWaypoint.transform.position);
        }

        // Vector2 waypoint = new Vector2(currentWaypoint.transform.position.x, currentWaypoint.transform.position.z);
        // Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);

        if (GameManager.main.DancePhase == DancePhase.Move)
        {
            if (!moveStarted)
            {
                Debug.Log("Start dancing");
                moveStarted = true;
                agent.isStopped = false;
                StartNextWaypoint();
            }
        }
        else if (GameManager.main.DancePhase == DancePhase.Wait)// || (waypoint - currentPos).magnitude < 0.1f)
        {
            Debug.Log("Waiting");
            // agent.isStopped = true;
            moveStarted = false;
        }
    }

    void StartNextWaypoint()
    {
        previousWaypoint = currentWaypoint;
        currentWaypoint = currentWaypoint.NextWaypoint;

        if (currentWaypoint != null)
        {
            agent.SetDestination(currentWaypoint.transform.position);
        }
        agent.isStopped = false;
        // HasMoved = false;
    }

    public Waypoint CurrentWaypoint()
    {
        return currentWaypoint;
    }

    public Waypoint PreviousWaypoint()
    {
        return previousWaypoint;
    }

    public void SetWaypoint(Waypoint waypoint)
    {
        currentWaypoint = waypoint;
    }
}

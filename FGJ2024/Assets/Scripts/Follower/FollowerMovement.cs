using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class FollowerMovement : MonoBehaviour
{
    public bool HasMoved { get; set; }
    public bool WasCrashed { get; private set; }

    private Collider collider;

    [SerializeField]
    private float normalAgentRadius;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private Waypoint currentWaypoint;
    private Waypoint previousWaypoint;

    private NavMeshAgent agent;

    private bool moveStarted = false;
    private FollowerState state = FollowerState.RunIn;
    private Transform followerInFront;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        HasMoved = false;
        moveStarted = true;
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (state != FollowerState.RunOut)
        {
            if (currentWaypoint == null)
            {
                Debug.Log($"Null waypoint");
                agent.SetDestination(GameManager.main.GetPlayer().position);
                currentWaypoint = GameManager.main.GetLastWaypoint();
            }
            else
            {
                agent.SetDestination(currentWaypoint.transform.position);
            }
        }
        else
        {
            bool hasRanAway = DestinationReached(agent, transform.position);
            if (hasRanAway)
            {
                Destroy(gameObject);
            }
        }

        if (state == FollowerState.RunIn)
        {
            collider.enabled = false;
            agent.radius = 0.15f;
            agent.speed = runSpeed;

            Vector2 dest = new(currentWaypoint.transform.position.x, currentWaypoint.transform.position.z);
            Vector2 pos = new(transform.position.x, transform.position.z);

            if ((pos - dest).magnitude <= 0.1f)
            {
                state = FollowerState.Dance;
            }

            if (GameManager.main.DancePhase == DancePhase.Move)
            {
                if (!moveStarted)
                {
                    moveStarted = true;
                    StartNextWaypoint();
                }
            }
            else if (GameManager.main.DancePhase == DancePhase.Wait)
            {
                moveStarted = false;
            }

            Transform lookAtTransform = followerInFront ?? GameManager.main.GetPlayer().transform;
            transform.LookAt(lookAtTransform, transform.up);
        }
        else if (state == FollowerState.Dance)
        {
            collider.enabled = true;
            agent.radius = normalAgentRadius;
            agent.speed = GameManager.main.MoveSpeed;

            if (GameManager.main.DancePhase == DancePhase.Move)
            {
                if (!moveStarted)
                {
                    moveStarted = true;
                    agent.isStopped = false;
                    StartNextWaypoint();
                }
            }
            else if (GameManager.main.DancePhase == DancePhase.Wait)
            {
                moveStarted = false;
            }

            Transform lookAtTransform = followerInFront ?? GameManager.main.GetPlayer().transform;
            transform.LookAt(lookAtTransform, transform.up);
        }
        else
        {
            collider.enabled = false;
            agent.radius = 0.15f;
            agent.speed = runSpeed;
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
    }

    public Waypoint CurrentWaypoint()
    {
        return currentWaypoint;
    }

    public Waypoint PreviousWaypoint()
    {
        return previousWaypoint;
    }

    public void SetPreviousWaypoint(Waypoint prev)
    {
        previousWaypoint = prev;
    }

    public void SetWaypoint(Waypoint waypoint)
    {
        currentWaypoint = waypoint;
    }

    public void RunAway()
    {
        state = FollowerState.RunOut;
        WasCrashed = true;
        Vector2 tp2 = Random.insideUnitCircle * 30f;
        Vector3 targetPos = new Vector3(tp2.x, transform.position.y, tp2.y);

        agent.SetDestination(targetPos);
        agent.isStopped = false;
    }
    public static bool DestinationReached(NavMeshAgent agent, Vector3 actualPosition)
    {
        if (agent.pathPending)
        {
            return Vector3.Distance(actualPosition, agent.pathEndPosition) <= agent.stoppingDistance;
        }
        else
        {
            return (agent.remainingDistance <= agent.stoppingDistance);
        }
    }

    public void SetFollowerInFront(Transform follower)
    {
        followerInFront = follower;
    }

    public FollowerState GetState()
    {
        return state;
    }
}

public enum FollowerState
{
    RunIn,
    Dance,
    RunOut
}

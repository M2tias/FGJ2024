using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main { get; private set; }
    public bool CanMove { get; private set; }
    public DancePhase DancePhase { get; private set; }

    [SerializeField]
    private TestSpawner spawner;
    [SerializeField]
    private float waitTime = 0.5f;
    [SerializeField]
    private float moveTime = 0.5f;
    [SerializeField]
    private GameObject waypointPrefab;
    [SerializeField]
    private GameObject followerPrefab;
    [SerializeField]
    private Transform Player;

    private List<Waypoint> waypoints = new();
    private List<FollowerMovement> followers = new();

    private float lastMove = 0f;
    private float moveStarted = 0f;
    private float waitStarted = 0f;

    void Awake()
    {
        main = this;
        DancePhase = DancePhase.Wait;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"Spawner: {spawner.GetFollowers().All(x => x.HasMoved)}, {CanMove}");
        // if (!CanMove && (Time.time - lastMove > waitTime))
        // {
        //     Debug.Log("Can move again");
        //     CanMove = true;
        // }
        // else if (CanMove)
        // {
        //     bool allMoved = spawner.GetFollowers().All(x => x.HasMoved);
        //     if (allMoved)
        //     {
        //         Debug.Log("All moved");
        //         CanMove = false;
        //         lastMove = Time.time;
        //         Debug.Log(lastMove);
        // 
        //         spawner.GetFollowers().ForEach(x => x.HasMoved = false);
        //     }
        // }

        if (DancePhase == DancePhase.Move && (Time.time - moveStarted > moveTime))
        {
            DancePhase = DancePhase.Wait;
            waitStarted = Time.time;
        }
        else if (DancePhase == DancePhase.Wait && (Time.time - waitStarted > waitTime))
        {
            DancePhase = DancePhase.Move;
            moveStarted = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFollower();
        }
    }

    public void SpawnWaypoint(Vector3 pos)
    {
        GameObject waypointObject = Instantiate(waypointPrefab);
        waypointObject.transform.parent = transform;
        waypointObject.transform.position = new Vector3(pos.x, 0, pos.z);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        if (waypoints.Count > 0)
        {
            waypoints.Last().NextWaypoint = waypoint;
            waypoint.PreviousWaypoint = waypoints.Last();
        }

        waypoints.Add(waypoint);
    }

    public void SpawnFollower()
    {
        if (waypoints.Count <= 0)
        {
            return;
        }

        GameObject followerObject = Instantiate(followerPrefab);
        followerObject.transform.parent = transform;

        FollowerMovement follower = followerObject.GetComponent<FollowerMovement>();

        if (followers.Count == 0)
        {
            follower.SetWaypoint(waypoints[waypoints.Count - 2]);
        }
        else
        {
            if (DancePhase == DancePhase.Wait)
            {
                follower.SetWaypoint(followers.Last().PreviousWaypoint());
            }
            else
            {
                follower.SetWaypoint(followers.Last().PreviousWaypoint().PreviousWaypoint);
            }
        }

        followers.Add(follower);
    }

    public Transform GetPlayer()
    {
        return Player;
    }
}

public enum DancePhase
{
    Move,
    Wait
}

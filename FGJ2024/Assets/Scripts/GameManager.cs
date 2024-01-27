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
    public float MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float waitTime = 0.2f;
    [SerializeField]
    private float moveTime = 0.2f;
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


    public int Health { get; set;}
    void Awake()
    {
        main = this;
        DancePhase = DancePhase.Wait;
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = 3;
    }

    // Update is called once per frame
    void Update()
    {
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
            follower.SetWaypoint(waypoints[waypoints.Count - 1]);
        }
        else
        {
            if (DancePhase == DancePhase.Wait)
            {
                Waypoint wp = followers.Last().PreviousWaypoint();
                int wpIndex = Mathf.Max(waypoints.IndexOf(wp), 0);
                Waypoint target = waypoints[wpIndex];

                follower.SetWaypoint(target);
                follower.SetPreviousWaypoint(waypoints[wpIndex - 1]);
            }
            else
            {
                Waypoint wp = followers.Last().PreviousWaypoint();
                int wpIndex = Mathf.Max(waypoints.IndexOf(wp), 0);
                Waypoint target = waypoints[wpIndex];

                follower.SetWaypoint(target);
                follower.SetPreviousWaypoint(waypoints[wpIndex - 1]);
            }
        }

        followers.Add(follower);
    }

    public Transform GetPlayer()
    {
        return Player;
    }

    public void DrinkBeer()
    {
        SpawnFollower();
        moveSpeed += 0.25f;
    }
    public void EatShit()
    {
        if (Health > 0)
        {
            Health -= 1;
        }
    }

    public Waypoint GetLastWaypoint()
    {
        return waypoints.Last();
    }
}

public enum DancePhase
{
    Move,
    Wait
}

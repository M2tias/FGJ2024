using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject followerPrefab;
    [SerializeField]
    private List<FollowerMovement> followers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject gameObject = Instantiate(followerPrefab);
            FollowerMovement follower = gameObject.GetComponent<FollowerMovement>();
            FollowerMovement previousFollower = followers.Last();
            followers.Add(follower);
            follower.SetWaypoint(previousFollower.CurrentWaypoint());
        }
    }

    public List<FollowerMovement> GetFollowers()
    {
        return followers;
    }
}

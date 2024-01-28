using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent navmeshagent;
    Rigidbody rb;
    private float MoveSpeed = 5.0f;
    public float RotationSpeed = 5.0f;
    [SerializeField]
    private AnimationClip anim;
    [SerializeField]
    private float animScale;
    private bool spawnWayPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpeed = GameManager.main.MoveSpeed;

        if (GameManager.main.DancePhase == DancePhase.Move)
        {
            MovePlayer();
            spawnWayPoint = true;
        }

        RotatePlayer();

        // check wall
        bool rayHit = Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, 0.5f);

        if (rayHit && hitInfo.collider.tag == "Wall")
        {
            transform.Rotate(Vector3.up * Vector3.Angle(transform.right, hitInfo.normal));
        }

        // check if we hit dancers
        bool sphereHit = Physics.SphereCast(new Ray(transform.position, transform.forward), 1f, out RaycastHit sphereHitInfo, 0.5f);

        if (sphereHit && sphereHitInfo.collider.tag == "Follower")
        {
            if (sphereHitInfo.collider.gameObject.TryGetComponent<FollowerMovement>(out FollowerMovement follower))
            {
                if (!follower.WasCrashed)
                {
                    GameManager.main.CrashedFollowers(follower);
                }
            }
        }

        if (GameManager.main.DancePhase == DancePhase.Wait && spawnWayPoint)
        {
            GameManager.main.SpawnWaypoint(transform.position);
            spawnWayPoint = false;
        }
    }
    void MovePlayer()
    {
        Vector3 move = transform.forward * MoveSpeed;
        navmeshagent.Move(move * Time.deltaTime);
    }

    void RotatePlayer()
    {
        float RotationDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * RotationDirection * RotationSpeed);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Beer")
        {
            SoundSource.main.hitBeer();
            GameManager.main.DrinkBeer();
            Destroy(other.gameObject);
        }
        if (other.tag=="Shit")
        {
            SoundSource.main.hitShit();
            GameManager.main.EatShit();
            Destroy(other.gameObject);
        }
        if(other.tag=="Fries")
        {
            SoundSource.main.hitFries();
            GameManager.main.DrinkBeer();
            Destroy(other.gameObject);
        }
    }

}

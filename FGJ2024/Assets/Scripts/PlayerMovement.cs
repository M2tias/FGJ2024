using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent navmeshagent;
    Rigidbody rb;
    private float MoveSpeed = 5.0f;

    [SerializeField]
    public float RotationSpeed;

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
        bool rayHit = Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, 1f);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);

        if (rayHit && (hitInfo.collider.tag == "Wall" || hitInfo.collider.tag == "Asset"))
        {
            Debug.Log($"{hitInfo.collider.name}");
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

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    void MovePlayer()
    {
        Vector3 move = transform.forward * MoveSpeed;
        navmeshagent.Move(move * Time.deltaTime);
    }

    void RotatePlayer()
    {
        float RotationDirection = Input.GetAxisRaw("Horizontal");
        Debug.Log($"Rotating {Time.deltaTime * RotationDirection * RotationSpeed}");
        transform.Rotate(Vector3.up, Time.deltaTime * RotationDirection * RotationSpeed);       
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

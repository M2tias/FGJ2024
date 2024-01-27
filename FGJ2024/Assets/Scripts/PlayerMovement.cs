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
        Debug.Log(navmeshagent.pathStatus);
        MoveSpeed = GameManager.main.MoveSpeed;

        if (GameManager.main.DancePhase == DancePhase.Move)
        {
            MovePlayer();
            spawnWayPoint = true;
        }

        RotatePlayer();

        // check wall
        bool rayHit = Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, 0.75f);

        if (rayHit && hitInfo.collider.tag == "Wall")
        {
            transform.Rotate(Vector3.up * Vector3.Angle(transform.right, hitInfo.normal));
        }

        // check if we hit dancers
        // bool sphereHit = Physics.SphereCast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo)

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
            GameManager.main.DrinkBeer();
            Destroy(other.gameObject);
        }
        if (other.tag=="Shit")
        {
            GameManager.main.EatShit();
            Destroy(other.gameObject);

        }
    }

}

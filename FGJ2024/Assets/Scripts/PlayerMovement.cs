using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent navmeshagent;
    Rigidbody rb;
    public float MoveSpeed = 5.0f;
    public float RotationSpeed = 5.0f;


    
    // Start is called before the first frame update
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Beer");
        {
            MoveSpeed += 1;
        }
    }
}

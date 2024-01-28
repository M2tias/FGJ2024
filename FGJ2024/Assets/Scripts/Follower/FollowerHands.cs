using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerHands : MonoBehaviour
{
    [SerializeField]
    private Transform LeftShoulder;
    [SerializeField]
    private Transform RightShoulder;

    [SerializeField]
    private Transform OwnLeftShoulder;
    [SerializeField]
    private Transform OwnRightShoulder;

    [SerializeField]
    private HingeJoint LeftFarJoint;
    [SerializeField]
    private HingeJoint RightFarJoint;

    [SerializeField]
    private GameObject tentacles;

    private FollowerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        if (tentacles != null)
        {
            tentacles.SetActive(false);
        }
    }

    public void Initialize(Transform targetRightShoulder, Transform targetLeftShoulder)
    {
        RightShoulder = targetRightShoulder;
        LeftShoulder = targetLeftShoulder;
        RightFarJoint.connectedBody = RightShoulder.GetComponent<Rigidbody>();
        LeftFarJoint.connectedBody = LeftShoulder.GetComponent<Rigidbody>();

        movement = GetComponent<FollowerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement != null)
        {
            if (movement.GetState() == FollowerState.Dance)
            {
                tentacles.SetActive(true);
            }
            else
            {
                tentacles.SetActive(false);
            }
        }
    }

    public Transform GetLeftShoulder()
    {
        return OwnLeftShoulder;
    }

    public Transform GetRightShoulder()
    {
        return OwnRightShoulder;
    }
}

using UnityEngine;

public class OscillatingDoor : MonoBehaviour
{
    public HingeJoint leftDoor;
    public HingeJoint rightDoor;
    public float force = 100f;
    public float closeDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            OpenDoor();
            Invoke("CloseDoor", closeDelay);
        }
    }

    private void OpenDoor()
    {
        JointSpring leftSpring = leftDoor.spring;
        leftSpring.targetPosition = 90f;
        leftDoor.spring = leftSpring;

        JointSpring rightSpring = rightDoor.spring;
        rightSpring.targetPosition = -90f;
        rightDoor.spring = rightSpring;

        ApplyForce(leftDoor, force);
        ApplyForce(rightDoor, force);
    }

    private void CloseDoor()
    {
        JointSpring leftSpring = leftDoor.spring;
        leftSpring.targetPosition = 0f;
        leftDoor.spring = leftSpring;

        JointSpring rightSpring = rightDoor.spring;
        rightSpring.targetPosition = 0f;
        rightDoor.spring = rightSpring;

        ApplyForce(leftDoor, -force);
        ApplyForce(rightDoor, -force);
    }

    private void ApplyForce(HingeJoint hinge, float force)
    {
        hinge.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }
}

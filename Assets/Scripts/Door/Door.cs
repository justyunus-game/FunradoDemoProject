using UnityEngine;

public class Door : MonoBehaviour
{
    public Key.KeyType requiredKeyType;

    private Animator animator;
    private Collider doorCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
        if (doorCollider != null)
        {
            doorCollider.isTrigger = true; // Make the door passable
        }
        Debug.Log("Door opened.");
    }
}

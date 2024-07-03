using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    protected bool isCollected = false;

    public abstract void Collect();
    public abstract void Use();

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Collect();
        }
    }
}

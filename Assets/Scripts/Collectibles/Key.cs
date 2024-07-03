using UnityEngine;
using UnityEngine.UI;

public class Key : Collectible
{
    public enum KeyType { Blue, Red }
    public KeyType keyType;

    public Image blueKeyUIImage;
    public Image redKeyUIImage;

    private void Start()
    {
        if (blueKeyUIImage != null)
        {
            blueKeyUIImage.enabled = false;
        }
        if (redKeyUIImage != null)
        {
            redKeyUIImage.enabled = false;
        }
    }

    public override void Collect()
    {
        if (isCollected) return;

        isCollected = true;

        if (keyType == KeyType.Blue && blueKeyUIImage != null)
        {
            blueKeyUIImage.sprite = icon;
            blueKeyUIImage.enabled = true;
        }
        else if (keyType == KeyType.Red && redKeyUIImage != null)
        {
            redKeyUIImage.sprite = icon;
            redKeyUIImage.enabled = true;
        }

        Debug.Log(itemName + " collected.");
        GameManager.Instance.KeyCollected(keyType);
        Destroy(gameObject); // Destroy the key after collection
    }

    public override void Use()
    {
        // Key usage logic if needed
    }
}

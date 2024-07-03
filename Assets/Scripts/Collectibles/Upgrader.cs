using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : Collectible
{
    public int levelIncreaseAmount;

    public override void Collect()
    {
        if (isCollected) return;

        isCollected = true;
        Debug.Log(itemName + " collected.");
    }

    public override void Use()
    {
        if (isCollected)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                GameManager.Instance.LevelIncreased(levelIncreaseAmount);
                Destroy(gameObject); // Destroy the upgrader after use
            }
        }
    }
}

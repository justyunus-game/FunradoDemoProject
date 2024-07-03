using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null) {
            GameManager.Instance.NextLevel();
        }
    }
}

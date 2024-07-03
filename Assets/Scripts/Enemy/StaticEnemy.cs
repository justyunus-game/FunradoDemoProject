using System.Collections;
using UnityEngine;

public class StaticEnemy : Enemy
{
    protected override void Update ()
    {
        LevelColorCheck();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && fieldOfView != null && fieldOfView.IsInFieldOfView(player.transform.position))
        {
            InteractWithPlayer(player);
        }
    }

    public override void InteractWithPlayer(PlayerController player)
    {
        if (player.level > level)
        {
            StartCoroutine(player.Attack());
            StartCoroutine(EnemyDie());
            Destroy(gameObject, 1.5f);
        }
        else
        {
            StartCoroutine(EnemyAttack());
            player.StartCoroutine(player.RestartAfterDelay(1.5f));
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    public List<Transform> patrolPoints;
    public float speed = 2f;
    public float waitTime = 1f;
    public float rotationSpeed = 5f; // Rotasyon hızı

    private int currentPointIndex;
    private bool waiting;
    private float waitTimer;

    protected override void Start()
    {
        base.Start();
        currentPointIndex = 0;
        waiting = false;
    }

    protected override void Update()
    {
        base.Update();
        LevelColorCheck();

        if (!waiting)
        {
            Patrol();
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waiting = false;
            }
        }
    }

    private void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Count == 0)
            return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            waiting = true;
            waitTimer = 0f;
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
        }
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

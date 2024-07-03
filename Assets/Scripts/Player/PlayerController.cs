using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : Character
{
    private bool isPlaying = true;
    private float restartDelay = 1.5f;
    private Joystick joystick;
    private FieldOfView enemyFOV;

    private List<Key.KeyType> keys;

    protected override void Start()
    {
        isPlaying = true;
        base.Start();
        joystick = FindObjectOfType<Joystick>();

        // Get FieldOfView component from Enemy
        enemyFOV = FindObjectOfType<FieldOfView>();

        keys = new List<Key.KeyType>();

        GameManager.Instance.OnKeyCollected += AddKey;
        GameManager.Instance.OnLevelIncreased += IncreaseLevel;
    }

    protected override void Update()
    {
        if (!isPlaying)
        {
            return; // Early exit if game is not playing
        }

        // Inputs from joystick
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        // Move the Character
        if (direction.magnitude >= 0.1f)
        {
            Move(direction);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        // Update level text position
        if (levelText != null)
        {
            levelText.transform.position = transform.position + Vector3.up * 3f;
            levelText.text = "Lv. " + level;
        }

        // Check if player is in enemy's field of view
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f); // Adjust the radius as needed
        foreach (Collider col in hitColliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null && enemy.fieldOfView != null && enemy.fieldOfView.IsInFieldOfView(transform.position))
            {
                InteractWithEnemy(enemy);
                //break; // Interaction should happen only once per frame if multiple enemies are in range
            }
        }
    }

    public override void Move(Vector3 direction)
    {
        // Calculate Target Rotation
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        // Softening
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 moveDirection = direction.normalized * speed * Time.deltaTime;
        transform.Translate(moveDirection, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            InteractWithEnemy(enemy);
        }

        // Key and Upgrader interactions
        Collectible collectible = other.GetComponent<Collectible>();
        if (collectible != null)
        {
            collectible.Collect();
            collectible.Use(); // Try to use the collectible immediately after collecting
        }

        // Check if it's a door and try to open it
        DoorTrigger doorTrigger = other.GetComponent<DoorTrigger>();
        if (doorTrigger != null)
        {
            TryOpenDoor(doorTrigger.door);
        }
    }

    public void TryOpenDoor(Door door)
    {
        if (HasKey(door.requiredKeyType))
        {
            door.OpenDoor();
            UseKey(door.requiredKeyType);
        }
        else 
        {
            Debug.Log("You don't have the required key!");
        }
    }

    public override void InteractWithEnemy(Enemy enemy)
    {
        if (level > enemy.level)
        {
            // Enemy Dies
            StartCoroutine(Attack());
            StartCoroutine(enemy.EnemyDie());
            Destroy(enemy.gameObject, 2.2f);
        }
        else
        {
            // Player Dies
            StartCoroutine(enemy.EnemyAttack());
            StartCoroutine(RestartAfterDelay(restartDelay));
        }
    }

    public void AddKey(Key.KeyType keyType)
    {
        if (!keys.Contains(keyType))
        {
            keys.Add(keyType);
            UpdateKeyUI(keyType); // Update UI after collecting the key
        }
    }

    public bool HasKey(Key.KeyType keyType)
    {
        return keys.Contains(keyType);
    }

    public void UseKey(Key.KeyType keyType)
    {
        keys.Remove(keyType);
    }

    public void IncreaseLevel(int amount)
    {
        level += amount;
        if (levelText != null)
        {
            levelText.text = "Lv. " + level;
        }
    }

    public IEnumerator RestartAfterDelay(float delay)
    {
        animator.SetBool("Die", true);
        isPlaying = false;
        yield return new WaitForSecondsRealtime(delay);
        animator.SetBool("Die", false);
        // Restart the Game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSecondsRealtime(1.5f);
        animator.SetBool("Attack", false);
    }

    private void UpdateKeyUI(Key.KeyType keyType)
    {
        // Update UI based on the collected key
        switch (keyType)
        {
            case Key.KeyType.Blue:
                // Update Blue Key UI
                break;
            case Key.KeyType.Red:
                // Update Red Key UI
                break;
            default:
                break;
        }
    }
}

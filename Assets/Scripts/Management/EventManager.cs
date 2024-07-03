using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public delegate void EnemyInteractionHandler(PlayerController player, Enemy enemy);
    public event EnemyInteractionHandler OnEnemyInteraction;

    public void TriggerEnemyInteraction(PlayerController player, Enemy enemy)
    {
        OnEnemyInteraction?.Invoke(player, enemy);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public int level;
    public Text levelTextPrefab;

    public FieldOfView fieldOfView;

    protected Text levelText;
    protected Canvas canvas;
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();

        // Enemy Canvas
        canvas = new GameObject("EnemyCanvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.transform.SetParent(this.transform);
        canvas.transform.localPosition = new Vector3(0, 3, 0); // Above Head

        levelText = Instantiate(levelTextPrefab, canvas.transform);
        levelText.text = "Lv. " + level;
    }

    protected virtual void Update()
    {
        canvas.transform.rotation = Quaternion.identity;
    }

    public IEnumerator EnemyAttack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSecondsRealtime(1.5f);
        animator.SetBool("Attack", false);
    }

    public IEnumerator EnemyDie() 
    {
        animator.SetBool("Die", true);
        yield return new WaitForSecondsRealtime(1.5f);
    }

    protected virtual void LevelColorCheck()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player.level >= level) {
            levelText.color = Color.green;
        }else{
            levelText.color = Color.red;
        }
    }

    public abstract void InteractWithPlayer(PlayerController player);
}

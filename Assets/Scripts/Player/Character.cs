using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour, IMovable
{
    public int level;
    protected float speed = 5f;
    protected float rotationSpeed = 10f;
    protected Animator animator;
    protected Canvas canvas;
    protected Text levelText;
    public Text levelTextPrefab;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();

        // Creating Canvas for Level Text
        canvas = new GameObject("PlayerCanvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.transform.SetParent(this.transform);
        canvas.transform.localPosition = new Vector3(0, 3, 0); // Above the characters head

        // Creating Level Text
        levelText = Instantiate(levelTextPrefab, canvas.transform);
        levelText.text = "Lv. " + level;
    }

    protected virtual void Update()
    {
        canvas.transform.rotation = Quaternion.identity;
    }

    public virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public abstract void InteractWithEnemy(Enemy enemy);
}

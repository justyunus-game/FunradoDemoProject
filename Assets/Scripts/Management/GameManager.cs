using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action OnGameFinished;
    public event Action OnGameRestart;
    public event Action OnLevelNext;
    public event Action<Key.KeyType> OnKeyCollected;
    public event Action<int> OnLevelIncreased;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true; // Müziğin tekrar etmesini sağlar
        audioSource.Play();
    }

    public void KeyCollected(Key.KeyType keyType)
    {
        OnKeyCollected?.Invoke(keyType);
    }

    public void LevelIncreased(int amount)
    {
        OnLevelIncreased?.Invoke(amount);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        OnGameRestart?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        OnLevelNext?.Invoke();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Time.timeScale = 0f;
            Debug.LogWarning("Sonraki sahne bulunamadı!");
        }
    }

    public void RestartWholeGame()
    {
        OnGameFinished?.Invoke();
        SceneManager.LoadScene(0);
        RestartMusic();
    }

    private void RestartMusic() {
        audioSource.Stop();
        audioSource.Play();
    }
}

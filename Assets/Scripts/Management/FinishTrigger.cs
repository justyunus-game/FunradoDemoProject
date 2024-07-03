using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour
{
    public GameObject finishedPanel;
    public Text countDownText;
    private int countDown = 10;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            finishedPanel.SetActive(true);
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        while (countDown > 0)
        {
            countDownText.text = countDown + " Seconds left";
            yield return new WaitForSecondsRealtime(1f);
            countDown--;
        }

        GameManager.Instance.RestartWholeGame();
    }
}

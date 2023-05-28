using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pausedText;
    [SerializeField] private TextMeshProUGUI score;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pausedText.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pausedText.SetActive(true);
            }
        }
    }
    
    public IEnumerator<WaitForSeconds> EndGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        int highScore = PlayerPrefs.GetInt("highScore");
        int actualScore = int.Parse(score.text);
        if (actualScore > highScore) {
            PlayerPrefs.SetInt("highScore", actualScore);
            gameOverUI.transform.Find("text_score").GetComponent<TextMeshProUGUI>().text = "New  High Score! " + actualScore + " points!";
        } else {
            gameOverUI.transform.Find("text_score").GetComponent<TextMeshProUGUI>().text = "Score: " + actualScore;
        }
        gameOverUI.SetActive(true);
    }
}

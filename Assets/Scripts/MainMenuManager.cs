using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    private void Awake()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        text.text = "HIGH SCORE: " + highScore;
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FlappyManager : MonoBehaviour {

    public static FlappyManager Instance = null;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeScaleText;

    private int score = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }
        Instance = this;
        timeScaleText.text = "Time scale: " + Time.timeScale;
    }

    public void SetScore(int score) {
        this.score = score;
        scoreText.text = score.ToString();
    }

    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();
    }

    public void SetTimeScale(float value) {
        Time.timeScale = value;
        timeScaleText.text = "Time scale: " + Time.timeScale;
    }
}

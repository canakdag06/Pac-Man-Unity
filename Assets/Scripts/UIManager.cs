using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] lives;
    [SerializeField] private FloatingScore scorePopup;

    private void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScoreText;
        GameEvents.OnLivesChanged += UpdateLivesUI;
        GameEvents.OnGhostScored += SpawnFloatingText;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScoreText;
        GameEvents.OnLivesChanged -= UpdateLivesUI;
        GameEvents.OnGhostScored -= SpawnFloatingText;
    }

    private void UpdateScoreText(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    private void UpdateLivesUI(int newLives)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            bool isActive = i < newLives;
            lives[i].SetActive(isActive);
        }
    }

    private void SpawnFloatingText(int points, Vector3 position)
    {
        FloatingScore scoreInstance = Instantiate(scorePopup, position, Quaternion.identity);
        scoreInstance.Initialize(points, position);
    }
}

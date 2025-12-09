using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] lives;
    [SerializeField] private FloatingScore scorePopup;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject wellDonePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI finalScore;

    public bool IsPaused { get; private set; }


    private void Awake()
    {
        Time.timeScale = 1.0f;
        IsPaused = false;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        wellDonePanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScoreText;
        GameEvents.OnLivesChanged += UpdateLivesUI;
        GameEvents.OnGhostScored += SpawnFloatingText;
        GameEvents.OnGameOver += ShowGameOverPanel;
        GameEvents.OnLevelCompleted += ShowWellDonePanel;
        inputReader.PauseEvent += TogglePause;
        inputReader.ResumeEvent += TogglePause;

        if (inputReader != null)
        {
            inputReader.SetGameplay();
        }
    }

    private void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScoreText;
        GameEvents.OnLivesChanged -= UpdateLivesUI;
        GameEvents.OnGhostScored -= SpawnFloatingText;
        GameEvents.OnGameOver -= ShowGameOverPanel;
        GameEvents.OnLevelCompleted -= ShowWellDonePanel;
        inputReader.PauseEvent -= TogglePause;
        inputReader.ResumeEvent -= TogglePause;
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

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void ShowWellDonePanel()
    {
        wellDonePanel.SetActive(true);
        finalScore.text = GameManager.Instance.Score.ToString();
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    private void TogglePause()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (IsPaused) return;

        Time.timeScale = 0f;
        IsPaused = true;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (!IsPaused) return;

        Time.timeScale = 1f;
        IsPaused = false;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void OnClick_Resume()
    {
        ResumeGame();

        inputReader.SetGameplay();
    }

}

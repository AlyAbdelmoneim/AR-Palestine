using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuizManager : MonoBehaviour
{
    public AudioClip correctAnswerSound;
    public AudioClip WrongAnswerSound;
    private AudioSource audioSource;
    public GameObject quizPanel;
    public GameObject gameEndPanel; // ✅ New field for the win panel
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI scoreText;

    private string correctAnswer;
    private int currentScore = 0;
    private ArtifactInteraction currentArtifact;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void ShowQuestion(string question, string[] answers, string correct, ArtifactInteraction artifact)
    {
        Debug.Log("ShowQuestion triggered: " + question);
        quizPanel.SetActive(true);
        questionText.text = question;
        correctAnswer = correct;
        currentArtifact = artifact;

        Debug.Log("AnswerButtons length: " + answerButtons.Length);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < answers.Length)
            {
                string answer = answers[i];
                Debug.Log("Setting text for button " + i + ": " + answer);

                var label = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (label != null)
                {
                    label.text = answer;
                }
                else
                {
                    Debug.LogWarning("No TMP label found on button " + i);
                }

                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answer));
            }
            else
            {
                Debug.LogWarning("Not enough answers provided for button " + i);
            }
        }
    }

    private void CheckAnswer(string selected)
    {
        Debug.Log("Selected: " + selected + " | Correct: " + correctAnswer);

        string selectedNormalized = selected.Trim().ToLower();
        string correctNormalized = correctAnswer.Trim().ToLower();
        Debug.Log($"Comparing: '{selectedNormalized}' vs '{correctNormalized}'");

        if (selectedNormalized == correctNormalized)
        {
            if (audioSource != null && correctAnswerSound != null)
            {
                audioSource.PlayOneShot(correctAnswerSound);
            }

            currentScore++;
            scoreText.text = "Score: " + currentScore;

            if (currentArtifact != null)
            {
                currentArtifact.HideArtifact();
                GameManager.Instance.ShowNextClue(); // 👈 Show next clue
            }

            quizPanel.SetActive(false);

            // ✅ Show win panel when score reaches 7
            if (currentScore >= 7 && gameEndPanel != null)
            {
                gameEndPanel.SetActive(true);
                Debug.Log("You found all the treasures!");
            }
        }
        else
        {
            if (audioSource != null && WrongAnswerSound != null)
            {
                audioSource.PlayOneShot(WrongAnswerSound);
            }

            Debug.Log("Wrong answer! Try again.");
            // Optionally, you can provide feedback to the player here
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}


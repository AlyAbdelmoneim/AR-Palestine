using UnityEngine;

public class ArtifactInteraction : MonoBehaviour
{
    [TextArea]
    public string question = "Default question?";
    public string[] answers = { "A", "B", "C", "D" };
    public string correctAnswer = "A";

    private void OnMouseDown()
{
    Debug.Log("Clicked: " + gameObject.name);

    QuizManager qm = FindObjectOfType<QuizManager>();
    if (qm != null)
    {
        GameManager.Instance.HideCurrentClue(); // 👈 Hide current clue
        qm.ShowQuestion(question, answers, correctAnswer, this);
    }
}


    // ✅ Add this method so the QuizManager can call it to hide the object
    public void HideArtifact()
    {
        gameObject.SetActive(false);
    }
}

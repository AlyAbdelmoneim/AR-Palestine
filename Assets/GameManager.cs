using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] clues; // Clue Texts in order
    private int currentClueIndex = 0;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowClue(0); // Start with the first clue
    }

    public void ShowClue(int index)
    {
        for (int i = 0; i < clues.Length; i++)
        {
            clues[i].SetActive(i == index);
        }
    }

    public void ShowNextClue()
    {
        currentClueIndex++;
        if (currentClueIndex < clues.Length)
        {
            ShowClue(currentClueIndex);
        }
        else
        {
            Debug.Log("All clues completed!");
        }
    }

    public void HideCurrentClue()
    {
        if (currentClueIndex < clues.Length)
            clues[currentClueIndex].SetActive(false);
    }
}

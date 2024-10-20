using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public UnityEvent<int> onPlayerWin;

    public bool isGameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        onPlayerWin = new UnityEvent<int>();
    }

    private void Start()
    {
        onPlayerWin.AddListener(HandlePlayerWin);

        isGameOver = false;
    }

    private void HandlePlayerWin(int playerIndex)
    {
        Debug.Log($"Player {playerIndex + 1} wins!");

        UIController.Instance.EndGame(playerIndex + 1);

        // Stop game runtime
        isGameOver = true;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
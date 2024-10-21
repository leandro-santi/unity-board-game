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

    // Win event to be observed and called by the players
    private void HandlePlayerWin(int playerIndex)
    {
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
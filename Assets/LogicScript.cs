using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
     public AudioSource scoreSFX;
     public Text highScoreText;
     private int highScore;
     public GameObject titleScreen;
    public BirdScript bird;
    public bool gameIsActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
        titleScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        gameIsActive = false; // Game starts inactive
    }
    
    public void StartGame()
    {
        titleScreen.SetActive(false);
        gameIsActive = true;
        bird.myRigidbody.bodyType = RigidbodyType2D.Dynamic; // Enable physics
        bird.myRigidbody.linearVelocity = Vector2.zero;
        playerScore = 0;
        scoreText.text = "0";
    }
    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd){
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
        scoreSFX.PlayOneShot(scoreSFX.clip);

        if (playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver(){
        gameOverScreen.SetActive(true);
        gameIsActive = false; // Stop the game
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text =highScore.ToString();
        }
        else
        {
            Debug.LogError("High Score Text UI is NOT assigned in Inspector!");
        }
    }
}

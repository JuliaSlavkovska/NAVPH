using UnityEngine;
using UnityEngine.SceneManagement;

//script for controling Game actions
public class MenuController : MonoBehaviour
{
    private AudioManager _audioManager;
    private ScoreController _scoreController;

    public string GameOverReason { get; private set; } //reason for gameOver printed on GameOverscreen

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _scoreController = FindObjectOfType<ScoreController>();
    }

    public void GameOver(string reason)
    {
        _scoreController.FreezeCam(true); //if game over, freeze camera view
        GameOverReason = reason;

        //deal with audio
        _audioManager.FadeIn("Background", 0.5f, 1);
        _audioManager.StopAll();
        _audioManager.Play("GameOver");
    }

    public void RestartGame()
    {
        _scoreController.RestoreScore();
        _audioManager.PlayOnStart();
        _scoreController.FreezeCam(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        _scoreController.FreezeCam(false);
        _audioManager.PlayOnGameStart();
        _scoreController.RestoreScore();
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        _audioManager.PlayOnStart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
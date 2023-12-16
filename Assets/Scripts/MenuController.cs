using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //public static MenuController instance;
    private AudioManager _audioManager;
    private ScoreController _scoreController;
    
    public string GameOverReason { get; private set; }
    private void Start()
    {

        _audioManager = FindObjectOfType<AudioManager>();
        _scoreController = FindObjectOfType<ScoreController>();
        
    }
    public void GameOver(string reason)
    {
        _scoreController.FreezeCam(true);
        GameOverReason = reason;
        StartCoroutine("Example");

    }
    
    IEnumerator Example()
    {
        yield return new WaitForSecondsRealtime(0.5f);
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
        //_audioManager.FadeOut("Background", 0.5f, 0.3f);
        _audioManager.PlayOnStart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

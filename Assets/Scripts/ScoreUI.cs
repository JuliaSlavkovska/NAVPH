using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{

    [SerializeField] private TMP_Text _scoreValue;
    [SerializeField] private TMP_Text _Delivery;
    [SerializeField] private TMP_Text _Rules;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject PlayerViewPanel;
    [SerializeField] private GameObject RestartGamePanel;
 
    
    public void UpdateScore(ScoreController scoreController)
    {
        _scoreValue.text = scoreController.Score.ToString();
    }
    
    public void UpdateHealthBar(ScoreController scoreController)
    {
        _slider.value = scoreController.Health;
    }
    
    public void GameOver(ScoreController scoreController)
    {
        int broken_rules = (int)((1 - scoreController.Health) / scoreController.damage);
        PlayerViewPanel.SetActive(false);
        RestartGamePanel.SetActive(true);
        _Delivery.text = "Sucessful delivery: " + scoreController.Score;
        _Rules.text = "Broken ruels: " + broken_rules;

    }
    

}

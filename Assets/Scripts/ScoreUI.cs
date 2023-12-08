using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{

    [SerializeField] private TMP_Text _scoreValue;
    [SerializeField] private Slider _slider;
    
    public void UpdateScore(ScoreController scoreController)
    {
        _scoreValue.text = scoreController.Score.ToString();
    }
    
    public void UpdateHealthBar(ScoreController scoreController)
    {
        _slider.value = scoreController.Health;
    }
}

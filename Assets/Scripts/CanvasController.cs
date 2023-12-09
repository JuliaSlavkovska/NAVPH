using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CanvasController : MonoBehaviour
{

    [SerializeField] private TMP_Text _DeliveryPlayer;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _DeliveryResults;
    [SerializeField] private TMP_Text _RulesResults;
    [SerializeField] private GameObject PlayerViewPanel;
    [SerializeField] private GameObject RestartGamePanel;
 
    
    public void UpdateDelivery(ScoreController scoreController)
    {
        _DeliveryPlayer.text = scoreController.Delivery.ToString();
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
        _DeliveryResults.text = "Sucessful delivery: " + scoreController.Delivery;
        _RulesResults.text = "Broken rules: " + broken_rules;

    }
    

}

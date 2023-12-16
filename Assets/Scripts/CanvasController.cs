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
    [SerializeField] private TMP_Text _GameOverReason;
    [SerializeField] private GameObject PlayerViewPanel;
    [SerializeField] private GameObject RestartGamePanel;
    
    private MenuController _menuController;


    private void Start()
    {
        _menuController = FindObjectOfType<MenuController>();
    }

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
        PlayerViewPanel.SetActive(false);
        RestartGamePanel.SetActive(true);
        _GameOverReason.text = _menuController.GameOverReason;
        _DeliveryResults.text = "Sucessful delivery: " + scoreController.Delivery;
        _RulesResults.text = "Broken rules: " + scoreController.BrokenRules;

    }
    

}

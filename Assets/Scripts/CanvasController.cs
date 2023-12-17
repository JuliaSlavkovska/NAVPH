
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//script for controlling Canvas
public class CanvasController : MonoBehaviour
{

    [SerializeField] private TMP_Text _DeliveryPlayer;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _DeliveryResults;
    [SerializeField] private TMP_Text _RulesResults;
    [SerializeField] private TMP_Text _GameOverReason;
    [SerializeField] private GameObject PlayerViewPanel;
    [SerializeField] private GameObject RestartGamePanel;
    [SerializeField] private TMP_Text _Warning;
    
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
    
    public void UpdateBrokenRule(ScoreController scoreController)
    {
        _Warning.gameObject.SetActive(true);
        _Warning.text = scoreController.BrokenRule;
        StartCoroutine(ClearWarning ()); // desapearing warning after specific amount of seconds
    }
    
    //change Panel object to Gameover from Player screen
    public void GameOver(ScoreController scoreController)
    {
        PlayerViewPanel.SetActive(false);
        RestartGamePanel.SetActive(true);
        _GameOverReason.text = _menuController.GameOverReason;
        _DeliveryResults.text = "Sucessful delivery: " + scoreController.Delivery;
        _RulesResults.text = "Broken rules: " + scoreController.BrokenRules;

    }
    
    //clear warning
    IEnumerator ClearWarning()
    {
        yield return new WaitForSeconds(3);
        _Warning.gameObject.SetActive(false);
    }
    

}

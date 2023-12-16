using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private List<GameObject> crossroads = new List<GameObject>();
    [SerializeField] private List<GameObject> carPrefabs = new List<GameObject>();
    [SerializeField] private List<WaypointEdge> spawnWaypoints = new List<WaypointEdge>();
    [SerializeField] private int carCount;
    [SerializeField] GameObject allCars;
    

    
    void Start()
    {


        for(var i = 0; i < carCount; ++i)
        {
            SpawnCar(i);
        }

        //_scoreController.FreezeCam(false);
    }

    void SpawnCar(int i)
    {
        // Get random waypoint from a random crossroad
        WaypointEdge waypoint;
        do
        {
            waypoint = spawnWaypoints[Random.Range(0, spawnWaypoints.Count)];
        } while (waypoint.spawnedCar);

        waypoint.spawnedCar = true;
        
        GameObject newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)]);
        newCar.GetComponent<CarMover>().setWaypoint(waypoint);
        newCar.transform.parent = allCars.transform;
        newCar.name = "Car " + i;

        newCar.transform.LookAt(waypoint.transform);
        newCar.transform.position = waypoint.transform.position;
        //newCar.GetComponent<IndicatorControl>().LeftBlink(true);
        //newCar.GetComponent<IndicatorControl>().RightBlink(true);
   
            

        
    }

    /*
    public void GameOver(string reason)
    {
        _scoreController.FreezeCam(true);
        GameOverReason = reason;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    */


}

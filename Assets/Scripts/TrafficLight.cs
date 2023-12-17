using UnityEngine;

public enum LightColor
{
    Red,
    YellowToRed,
    YellowToGreen,
    Green
}

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject green;
    [SerializeField] private float timer;
    [SerializeField] private float timerMain;
    [SerializeField] private float timerYellow;
    [SerializeField] private bool startGreen;
    [SerializeField] private GameObject barrier;
    private LightColor currentColor;

    public void Start()
    {
        if (startGreen)
            SetGreen();
        else
            SetRed();
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer >= 0f)
            return;
        switch (currentColor)
        {
            case LightColor.Red:
                SetYellow();
                currentColor = LightColor.YellowToGreen;
                break;
            case LightColor.Green:
                SetYellow();
                currentColor = LightColor.YellowToRed;
                break;
            case LightColor.YellowToGreen:
                SetGreen();
                break;
            case LightColor.YellowToRed:
                SetRed();
                break;
        }
    }

    private void SetRed()
    {
        barrier.transform.localPosition = new Vector3(0, 0, 0);
        timer = timerMain;
        currentColor = LightColor.Red;
        yellow.SetActive(false);
        green.SetActive(false);
        red.SetActive(true);
    }

    private void SetYellow()
    {
        timer = timerYellow;
        green.SetActive(false);
        red.SetActive(false);
        yellow.SetActive(true);
    }

    private void SetGreen()
    {
        barrier.transform.localPosition = new Vector3(0, 100, 0);
        timer = timerMain;
        currentColor = LightColor.Green;
        yellow.SetActive(false);
        red.SetActive(false);
        green.SetActive(true);
    }

}
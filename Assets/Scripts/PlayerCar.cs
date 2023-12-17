using UnityEngine;

//script for controoling player car
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject EasterEgg;

    [Header("Lights")] [SerializeField] private GameObject LeftBlink;

    [SerializeField] private GameObject RightBlink;

    [Header("Turn Timers")] [SerializeField]
    private float turnTimerLimit;

    [SerializeField] private float turnTimer;

    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float BrakeForce;

    private AudioManager _audioManager;
    private ScoreController _scoreController;
    private float anglex;
    private float anglez;
    private bool LeftTurn;


    private bool RightTurn;
    private float timer = 0.4f; //timer for blinkers


    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _scoreController = FindObjectOfType<ScoreController>();
        //_audioManager.Play("CarIdle");
    }


    private void Update()
    {
        if (!_scoreController.getCamStatus()) //if game is not frozen because of gameover
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed); //move car forward/backward based on speed value
            CarMovement();
            Blinks();
            CarFlip();
        }
    }


    //check if car dod not roll over
    private void CarFlip()
    {
        anglez = transform.localEulerAngles.z;
        anglex = transform.localEulerAngles.x;

        anglez = (anglez > 180) ? anglez - 360 : anglez;
        anglex = (anglex > 180) ? anglex - 360 : anglex;

        //GameOver
        if ((anglez > 30 || anglez < -30 ) || (anglex > 30 || anglex < -30 ) )
        {
            _scoreController.initializeGameOver("Car flip");
        }
    }


    //controlling car movement
    private void CarMovement()
    {
        //if not pressed any key, slow down to zero
        if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false)
        {
            if (speed > 0)
                speed -= 0.05f;
            else
                speed += 0.05f;
        }

        //brake
        if (Input.GetKey(KeyCode.Space))
        {
            if (speed > 0)
                speed -= BrakeForce;
            else
                speed += 0.2f;
        }

        /* These sounds were creating "cracking" sound on background, we removed them from game
         *Engine sound
         *Gass sound

        if (Input.GetKeyDown(KeyCode.W))
        {
            //_audioManager.Play("CarMovement");
            //_audioManager.FadeIn("CarMovement", 2, 1f);
            //_audioManager.FadeOut("CarIdle", 2, 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //_audioManager.Stop("CarMovement");
            //_audioManager.FadeOut("CarMovement", 0.5f, 0);
            //_audioManager.FadeIn("CarIdle", 0.5f, 0.6f);
        }
        */

        //WASD controlling
        if (Input.GetKey(KeyCode.W))
        {
            //if car was moving backward (previosly pressed "s"), and now pressed "w", slow down from moving backwards to zero
            if (speed < 0)
                speed += 0.2f;

            //move forward
            else
                speed = Mathf.Clamp(speed + 0.1f, 0, maxSpeed);
        }

        //turn left
        if (Input.GetKey(KeyCode.A))
        {
            //correction of rotation based if car is moving forward, or backward.
            //Not set >0, because the car was acting unreal with that
            if (speed > 3)
            {
                transform.Rotate(0, -rotationAngle * Time.deltaTime, 0);

                //checking, if blinker wa on/off --- same have to be done in elfe if
                if (turnTimer < turnTimerLimit)
                {
                    turnTimer = Mathf.Clamp(turnTimer + Time.deltaTime, 0, turnTimerLimit);
                    if (Mathf.Approximately(turnTimer, turnTimerLimit))
                        if (!LeftTurn)
                            // Turning without a blinker
                            _scoreController.CheckTurn();
                }
            }

            else if (speed < -3)
            {
                transform.Rotate(0, rotationAngle * Time.deltaTime, 0);
            }
        }


        //moving backward
        if (Input.GetKey(KeyCode.S))
        {
            //if car was moving forward (previosly pressed "w"), and now pressed "s", slow down from moving forward to zero
            if (speed > 0)
                speed -= BrakeForce;
            else if (speed >= -1 * maxSpeed) 
                speed -= 0.1f;
        }

        //turn right
        if (Input.GetKey(KeyCode.D))
        {
            //correction of rotation based if car is moving forward, or backward
            if (speed < -3)
            {
                transform.Rotate(0, -rotationAngle * Time.deltaTime, 0);
            }
            else if (speed > 3)
            {
                transform.Rotate(0, rotationAngle * Time.deltaTime, 0);
                if (turnTimer < turnTimerLimit)
                {
                    turnTimer = Mathf.Clamp(turnTimer + Time.deltaTime, 0, turnTimerLimit);
                    if (Mathf.Approximately(turnTimer, turnTimerLimit))
                        if (!RightTurn)
                            _scoreController.CheckTurn();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) 
            turnTimer = 0;

        if (Input.GetKeyDown(KeyCode.F)) 
            EasterEgg.SetActive(!EasterEgg.activeSelf);
    }

    //controlling blinkers
    private void Blinks()
    {
        //Right signal on/off
        if (Input.GetKeyDown(KeyCode.Mouse1))
            SignalLightControl(ref RightTurn, ref LeftTurn, RightBlink, LeftBlink);


        //Left signal on/off
        if (Input.GetKeyDown(KeyCode.Mouse0))
            SignalLightControl(ref LeftTurn, ref RightTurn, LeftBlink, RightBlink);

        //flashing when signal is turn on
        if (RightTurn)
            SignalLightFlashing(RightBlink);

        if (LeftTurn)
            SignalLightFlashing(LeftBlink);
    }

    //if left blink is on and user pressed right blink, turn off left blink and turn on right blink. Same on the other way
    private void SignalLightControl(ref bool oneSignal, ref bool secondSignal, GameObject oneblink,
        GameObject secondblink)
    {
        //signal on, turn off
        if (oneSignal)
        {
            oneblink.SetActive(false);
            _audioManager.Stop("Indicator");
            oneSignal = false;
        }

        //signal off, turn on && check if already not active the otherone signal if so, shut down
        else
        {
            if (secondSignal)
            {
                secondblink.SetActive(false);
                _audioManager.Stop("Indicator");
                secondSignal = false;
            }

            oneblink.SetActive(true);
            _audioManager.Play("Indicator");
            oneSignal = true;
            timer = 0.4f;
        }
    }

    private void SignalLightFlashing(GameObject blink)
    {
        if (timer > 0) 
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            blink.GetComponent<Renderer>().enabled = !blink.GetComponent<Renderer>().enabled;
            timer = 0.3f;
        }
    }
}
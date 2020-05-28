using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private int accelerationSpeed;
    [SerializeField] private int boostAccelerationSpeed;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 0.1f;
    [SerializeField] private float maxBoostSpeed = 0.5f;
    private bool boosting = false;
    private Vector3 currentDirection;
    private Vector3 wantedSpeedDir;
    private Vector3 wantedBoostSpeedDir;
    public Vector3 adjustDir;
    private int currentStep = 0;
    private int stepGoal;
    public float stopSpeed;
    public bool stop;
    public bool tempStop = false;
    public float stopTimer;
    public Vector3 CurrentDirection
    {
        get { return currentDirection; }
        set { currentDirection = value; }
    }
    
    public bool Boosting
    {
        get { return boosting; }
    }

    void Start()
    {
        wantedSpeedDir = transform.forward * maxMoveSpeed;
        wantedBoostSpeedDir = transform.forward * maxBoostSpeed;
        //DirectionChanged(accelerationSpeed);
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (stop)
        {
            currentDirection = Vector3.RotateTowards(currentDirection,Vector3.zero, rotationSpeed * Time.deltaTime, stopSpeed);
            transform.position += currentDirection;
        }
        else
        {
            Vector3 direction = Vector3.zero;
            if (boosting){
                //direction += transform.forward * boostAccelerationSpeed;
                //if (direction.magnitude > maxBoostSpeed) direction = direction.normalized * maxBoostSpeed;
                if (!tempStop)
                {
                    currentDirection = Vector3.RotateTowards(currentDirection,transform.forward * maxBoostSpeed, rotationSpeed * Time.deltaTime, boostAccelerationSpeed);
                    transform.position += currentDirection; 
                } else if (Time.time >= stopTimer)
                {
                    tempStop = false;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    TurnLeft();
                }
                if (Input.GetKey(KeyCode.D))
                {
                    TurnRight();
                }

                if (Input.GetKey(KeyCode.B))
                {
                    SetBoost(true);
                }
            
                //direction += transform.forward * accelerationSpeed;
                //if (direction.magnitude > maxMoveSpeed) direction = direction.normalized * maxMoveSpeed;
                /*time += Time.deltaTime * accelerationSpeed;
                if (time >= 1f)
                {
                    time = 1f;
                }
                direction = Vector3.Lerp(startDirection,wantedSpeedDir, time);*/
                currentDirection = Vector3.RotateTowards(currentDirection,transform.forward * maxMoveSpeed, rotationSpeed * Time.deltaTime, accelerationSpeed);
                /*if (!StepGoalReached())
                {
                    currentDirection += adjustDir;
                }*/
                Debug.DrawRay(transform.position, currentDirection * 50, Color.cyan);
                transform.position += currentDirection;
            }
        
        }

        //currentStep++;
    }

    private void DirectionChanged(int newStepGoal)
    {
        Vector3 adjustDirection = wantedSpeedDir - currentDirection;
        adjustDirection /= newStepGoal;
        adjustDir = adjustDirection;
        currentStep = 0;
        stepGoal = newStepGoal;
    }

    private bool StepGoalReached()
    {
        bool reached = (currentStep >= stepGoal) ? true : false;
        return reached;
    }
    
    public void TurnLeft()
    {
        transform.Rotate(Vector3.down * rotationSpeed);
    }
    
    public void TurnRight()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }

    public void SetBoost(bool activate)
    {
        boosting = activate;
    }
}

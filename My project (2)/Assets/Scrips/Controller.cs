using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;
    public float speed;
    public GameObject startPoint;

    public int maxFuel;
    public float currentFuel;
    public GameObject coinCounter;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;


    private void Start()
    {
        currentFuel = maxFuel;
    }


    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        onFall();
        Cheat();
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        if (currentFuel > 0)
            verticalInput = Input.GetAxis(VERTICAL);
        else
            SceneManager.LoadScene(1);

        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position = startPoint.transform.position;
            transform.rotation = startPoint.transform.rotation;
            currentFuel -= 10;
        }
            
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        
        currentFuel -= 0.02f;
        frontLeftWheelCollider.motorTorque = Convert.ToInt32(verticalInput * motorForce);
        frontRightWheelCollider.motorTorque = Convert.ToInt32(verticalInput * motorForce);
        currentbreakForce = isBreaking ? breakForce : 0f;

       
        if (verticalInput > 0)
        {
            speed += 1000;
        }
        else if(isBreaking && speed > 0)
        {
            speed -= 3500;
        }
        else if (verticalInput == 0 && speed > 0)
        {
            speed -= 100;
        }
        else if(verticalInput < 0 && speed > 0)
        {
            speed -= 1400;
        }
            
        
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    public float getSpeed()
    {
        return speed;
    }

    public float getFuel()
    {
        return currentFuel;
    }

    public int getMaxFuel()
    {
        return maxFuel;
    }

    public void takeFuel()
    {
        currentFuel += 30;
        if (currentFuel > maxFuel)
            currentFuel = maxFuel;
    }

    private void OnDestroy()
    {
        bool g;
        bool h;
        int money;

        g = Data.secondCarIsBlocked;
        h = Data.secondRoadIsBlocked;
        money = Data.coins;

        money += coinCounter.GetComponent<CoinsCounter>().getCoins();

        Data.secondCarIsBlocked = g;
        Data.secondRoadIsBlocked = h;
        Data.coins = money;
    }

    private void onFall()
    {
        if (transform.position.y < -5)
        {
            transform.position = startPoint.transform.position;
            transform.rotation = startPoint.transform.rotation;

            currentFuel -= 10;
        }
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.P))
            GameObject.FindGameObjectWithTag("CoinsCounter").GetComponent<CoinsCounter>().CoinAdd();
    }
}
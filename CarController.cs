using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody carRb;    
    private Transform carTf;   

    private float horizontalInput;  
    private float verticalInput;    
    private float currentSteerAngle; 
    private float currentBrakeForce;
    private bool isBraking;          
    private int speed;               

    private const int MaxForwardSpeed = 180; 
    private const int MaxBackwardSpeed = 60; 

    [Header("Car Attributes")]
    [SerializeField] private GameObject Car; 
    [SerializeField] private float motorForce; 
    [SerializeField] private float brakeForce; 
    [SerializeField] private float maxSteerAngle; 
    [SerializeField] private float decelerationRate; 

    [Header("Wheels Meshes")]
    [SerializeField] private Transform frontLeftWheelTransform;  
    [SerializeField] private Transform frontRightWheelTransform; 
    [SerializeField] private Transform rearLeftWheelTransform;  
    [SerializeField] private Transform rearRightWheelTransform; 

    [Header("Wheels Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider;  
    [SerializeField] private WheelCollider frontRightWheelCollider; 
    [SerializeField] private WheelCollider rearLeftWheelCollider;   
    [SerializeField] private WheelCollider rearRightWheelCollider;  

    // Public properties to access current speed and max speed values
    public int Speed => speed;  // Get the current speed
    public int GetMaxForwardSpeed => MaxForwardSpeed;
    public int GetMaxBackwardSpeed => MaxBackwardSpeed;

    private void Start()
    {
        carRb = Car.GetComponent<Rigidbody>();
        carTf = Car.GetComponent<Transform>();

        // Adjust the center of mass for better stability
        carRb.centerOfMass = new Vector3(0, -0.2f, 0);
    }

    private void Update()
    {
        CalculateSpeed();
    }

    private void FixedUpdate()
    {
        GetInput();            
        HandleMotor();         
        HandleSteering();      
        ApplyBrakes();         
        UpdateWheels();       
        ApplyDeceleration();   
    }

    // Capture player input for steering and throttle
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Left/right input (steering)
        verticalInput = Input.GetAxis("Vertical");     // Forward/backward input (acceleration)

        isBraking = false; 

        // Calculate if the car is moving forward or backward
        float dotP = Vector3.Dot(carTf.forward.normalized, carRb.velocity.normalized);
        bool carIsMovingForward = dotP > 0.1f;
        bool carIsMovingBackward = dotP < -0.1f;

        // Brake if reversing direction or space (handbrake) is pressed
        if ((carIsMovingForward && verticalInput < 0) || (carIsMovingBackward && verticalInput > 0) || Input.GetKey(KeyCode.Space))
        {
            isBraking = true;
        }
    }

    // Apply motor force for acceleration or braking based on player input
    private void HandleMotor()
    {
        // Calculate motor force based on throttle input
        float targetMotorForce = verticalInput * motorForce;

        // Boost the motor force when the car is on a slope (for better hill climbing)
        if (IsOnSlope())
        {
            targetMotorForce *= 3f;
        }

        // Limit forward speed to the MaxForwardSpeed
        if (targetMotorForce > 0 && speed > MaxForwardSpeed)
        {
            targetMotorForce = 0; 
        }

        // Limit reverse speed to the MaxBackwardSpeed
        if (targetMotorForce < 0 && speed > MaxBackwardSpeed)
        {
            targetMotorForce = 0; 
        }

        frontLeftWheelCollider.motorTorque = targetMotorForce;
        frontRightWheelCollider.motorTorque = targetMotorForce;
    }

    // Adjust the steering angle based on player input
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;

        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    // Apply brakes when required
    private void ApplyBrakes()
    {
        // Apply brake force if braking is enabled
        currentBrakeForce = isBraking ? brakeForce : 0f;

        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    // Update the position and rotation of the visual wheel meshes to match the wheel colliders
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    // Sync a single wheel's mesh position and rotation with its collider
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        // Get the position and rotation from the wheel collider
        wheelCollider.GetWorldPose(out pos, out rot);

        // Set the position and rotation of the visual wheel mesh
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    // Apply deceleration when the player is not pressing the throttle
    private void ApplyDeceleration()
    {
        // If no throttle input and not braking, decelerate the car
        if (Mathf.Approximately(verticalInput, 0f) && !isBraking)
        {
            float decelerationForce = -decelerationRate * carRb.velocity.magnitude;
            carRb.AddForce(decelerationForce * carRb.velocity.normalized);
        }
    }

    // Check if the car is on a slope (inclined terrain)
    private bool IsOnSlope()
    {
        // Calculate the angle between the car's up vector and the world's up vector
        float slopeAngle = Vector3.Angle(Vector3.up, carTf.up);
        return slopeAngle > 5f; // If the angle is greater than 5 degrees, consider it a slope
    }

    // Calculate the car's speed in km/h based on its Rigidbody velocity
    private void CalculateSpeed()
    {
        speed = Mathf.RoundToInt(carRb.velocity.magnitude * 3.6f);
    }
}

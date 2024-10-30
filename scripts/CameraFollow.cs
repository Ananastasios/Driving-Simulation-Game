using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Vector3 offset;

    [SerializeField] private Transform target;

    // Speed at which the camera translates (moves) to follow the target
    [SerializeField] private float translateSpeed;

    // Speed at which the camera rotates to face the target
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate()
    {
        HandleTranslation(); 
        HandleRotation();   
    }
    // Method to handle the camera's translation (movement)
    private void HandleTranslation()
    {
        // Calculate the target position based on the target's position and the camera's offset
        var targetPosition = target.TransformPoint(offset);

        // Move the camera towards the target position using linear interpolation (Lerp)
        // Lerp smoothly moves the camera between the current position and target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    // Method to handle the camera's rotation
    private void HandleRotation()
    {
        // Calculate the direction vector from the camera to the target
        var direction = target.position - transform.position;

        // Create a rotation that points the camera towards the target
        var rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Smoothly rotate the camera towards the target using spherical linear interpolation (Slerp)
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}

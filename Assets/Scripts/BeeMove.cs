using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMove : MonoBehaviour
{
    public Transform centerPoint;  // The point around which the object will buzz
    public float radius = 5f;      // Maximum distance from the center point
    public float speed = 5f;       // Speed of movement
    public float jitterStrength = 1f; // Strength of the jitter for erratic motion
    public float jitterFrequency = 2f; // How often the jitter changes

    private Vector3 currentTarget; // Current target position
    private float timeSinceLastJitter; // Time since last jitter calculation

    void Start()
    {
        GenerateNewTarget();
    }

    void Update()
    {
        // Move towards the current target
        transform.position = Vector3.Lerp(transform.position, currentTarget, Time.deltaTime * speed);

        // Update jitter periodically to make the movement erratic
        timeSinceLastJitter += Time.deltaTime;
        if (timeSinceLastJitter >= 1f / jitterFrequency)
        {
            GenerateNewTarget();
            timeSinceLastJitter = 0f;
        }
    }

    void GenerateNewTarget()
    {
        // Randomize a new position within the radius
        Vector3 randomOffset = Random.insideUnitSphere * radius;
        randomOffset.y = Random.Range(-radius * 0.5f, radius * 0.5f); // Limit vertical range

        // Add jitter to the motion
        Vector3 jitter = Random.insideUnitSphere * jitterStrength;

        // Calculate the new target around the center
        currentTarget = centerPoint.position + randomOffset + jitter;
    }
}
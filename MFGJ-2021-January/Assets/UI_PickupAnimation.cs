﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PickupAnimation : MonoBehaviour
{

    Vector3 destination;
    // Animation parameters
    private float acceleration = 40f;
    private float maxSpeed = 100f;
    private float currentSpeed = 1f;
    private float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        currentSpeed += Mathf.Min(acceleration * Time.deltaTime, maxSpeed);
        float step = currentSpeed * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        if (Vector3.Distance(transform.position, destination) < 0.001f)
        {
            Destroy(this);
        }
    }

    public void MoveItem(Vector3 iDestination)
    {
        destination = iDestination;
    }

}

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public GameObject fuel;
    bool autoPilot = false;
    void Start()
    {

    }

    void CalculateAngle()
    {
        Vector3 tF = this.transform.up;
        Vector3 fD = fuel.transform.position - this.transform.position;

        float dot = tF.x * fD.x + tF.y * fD.y;
        float angle = Mathf.Acos(dot / (tF.magnitude * fD.magnitude));

        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        Debug.Log("Unity Angle: " + Vector3.Angle(tF, fD));

        Debug.DrawRay(this.transform.position, tF * 10, Color.green, 2);
        Debug.DrawRay(this.transform.position, fD, Color.red, 2);

        int clockwise = 1;
        if (Cross(tF, fD).z < 0)
            clockwise = -1;

        this.transform.Rotate(0, 0, (angle * clockwise * Mathf.Rad2Deg) * 0.02f);
    }

    Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }
    float CalculateDistance()
    {
        Vector3 tP = this.transform.position;
        Vector3 fP = this.transform.position;

        float distance = Mathf.Sqrt(
            Mathf.Pow(tP.x - fP.x, 2) + Mathf.Pow(tP.y - fP.y, 2) + Mathf.Pow(tP.z - fP.z, 2));

        float unityDistance = Vector3.Distance(tP, fP);


        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + unityDistance);
        return (distance);
    }

    float autoSpeed = 0.1f;
    void AutoPilot()
    {
        CalculateAngle();
        this.transform.Translate(this.transform.up * autoSpeed, Space.World);
    }
    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
            CalculateAngle();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            autoPilot = !autoPilot;
        }
        if (autoPilot)
        {
            if(CalculateDistance() > 5)
            AutoPilot();
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerapan : MonoBehaviour
{
    private Vector3 startpoint;
    [SerializeField] GameObject cam;
    [SerializeField] Camera cameraWorld;
    [SerializeField] float groundZ = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startpoint = WorldPosition(groundZ);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = startpoint - WorldPosition(0);
            cam.transform.position += direction;
        }
    }
    private Vector3 WorldPosition(float z)
    {
        Ray mousePos = cameraWorld.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}
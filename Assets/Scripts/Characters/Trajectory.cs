﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 NOTA
 Todo esta comentado, por favor leer o preguntar si algo no se entiende
 Este script permite moverse en un tiro parabolico usando el mouse para determinar direccion y fuerza
 Cualquier cosa me preguntan
 ATT: Jesus Antonio Buitrago (Octovalve)
 */
//In progres do not touch
public class Trajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] [Range(3, 30)] private int _lineSegmentCount = 20;
    private List<Vector3> _linePoints = new List<Vector3>();
    [FMODUnity.EventRef]
    public string Event;

    #region Singleton
    public static Trajectory Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    //Se encarga de calcular la trayectoria  y actualizar la gráfica en un tiempo determinado 
    //para permitir la visualización del desplazamiento que seguirá el objeto
    private void Start()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
    }
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedDeltaTime;
        float FlightDuration = 1;
        float stepTime = FlightDuration / _lineSegmentCount;
        _linePoints.Clear();
        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 MovementVector = new Vector3(
                 velocity.x * stepTimePassed,
                 velocity.y * stepTimePassed + 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                 velocity.z * stepTimePassed
                );
            _linePoints.Add(item: MovementVector + startingPoint);
        }
        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }
    public void UpdateTrajectoryInpuls(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidbody.mass);
        float FlightDuration = 1;
        float stepTime = FlightDuration / _lineSegmentCount;
        _linePoints.Clear();
        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 MovementVector = new Vector3(
                 velocity.x * stepTimePassed,
                 velocity.y * stepTimePassed + 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                 velocity.z * stepTimePassed
                );
            _linePoints.Add(item: MovementVector + startingPoint);
        }
        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }
    //Desaparece la línea poniendo los puntos de proyección en 0
    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }

}

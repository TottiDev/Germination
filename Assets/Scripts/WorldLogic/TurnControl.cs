﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControl : MonoBehaviour
{
    // 4 turno asignado
    // 5 salto
    // 6 ataco
    // 7 termino
    private int estado = 0;
    CharctesSelection teamselection;
    private int contadorTurno = 1;
    public int Estado { get => estado; set => estado = value; }
    public int ContadorTurno { get => contadorTurno; set => contadorTurno = value; }

    private void Start()
    {
        teamselection = GetComponent<CharctesSelection>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(contadorTurno);
        if (estado >= 7 && teamselection.Team == 1)
        {
            teamselection.Team += 1;
            estado = 0;
            contadorTurno++;
        }
        else if (estado >= 7 && teamselection.Team == 2)
        {
            teamselection.Team -= 1;
            estado = 0;
            contadorTurno++;
        }
    }
}

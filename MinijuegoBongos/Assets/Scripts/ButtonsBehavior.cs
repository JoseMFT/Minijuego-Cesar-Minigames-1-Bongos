using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;

public class ButtonsBehavior: MonoBehaviour
{
    public enum EstadosBoton
    {
        estadoPorDefecto,
        estadoSeleccionarDificultad,
        estadoOpciones,
        estadoSalir

    }

    EstadosBoton estadoActual;

    void Awake ()
    {
        estadoActual = EstadosBoton.estadoPorDefecto;
        UnityEngine.Debug.Log(estadoActual);
    }

    public void CambiarEstados (int valor)
    {

        if (valor == 0)
        {
            estadoActual = EstadosBoton.estadoSeleccionarDificultad;
        } else if (valor == 1)
        {
            estadoActual = EstadosBoton.estadoOpciones;

        } else if (valor == 2)
        {
            estadoActual = EstadosBoton.estadoSalir;
        }

        switch (estadoActual)
        {

            case EstadosBoton.estadoPorDefecto:
                UnityEngine.Debug.Log("Por defecto");
                break;

            case EstadosBoton.estadoSeleccionarDificultad:
                UnityEngine.Debug.Log("Cambiar Dificultad");
                break;

            case EstadosBoton.estadoOpciones:
                UnityEngine.Debug.Log("Opciones");
                break;

            case EstadosBoton.estadoSalir:

                if (Application.isPlaying)
                {
                    UnityEngine.Debug.Log("Salir");
                    Application.Quit();

                    EditorApplication.isPlaying = false;
                }

                break;

        }    
    }
}

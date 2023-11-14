using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonsBehavior: MonoBehaviour
{
    public Slider sliderDificultad;
    float nuevoValorSlider = 0f;
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
        //LeanTween.value(sliderDificultad.value, 0f, 0f);
    }

    public void CambiarEstados (int valor)
    {

        if (valor == 0)
        {
            estadoActual = EstadosBoton.estadoSeleccionarDificultad;
            

        } else if (valor == 1)
        {
            VolverCambiarDificultad();
            estadoActual = EstadosBoton.estadoOpciones;

        } else if (valor == 2)
        {
            VolverCambiarDificultad();
            estadoActual = EstadosBoton.estadoSalir;
        }

        switch (estadoActual)
        {

            case EstadosBoton.estadoPorDefecto:
                AbrirCerrarDificultad();
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

    private void Update () {
        UnityEngine.Debug.Log(sliderDificultad.value);
    }

    public void VolverCambiarDificultad () {
        if (sliderDificultad.value >= 1f) {
            LeanTween.value(sliderDificultad.value, 0f, .75f).setEaseOutBounce();
        }
    }

    public void ScaleUp (GameObject Boton) {
        LeanTween.scale(Boton, Vector3.one * 1.1f * 5.29f, .15f).setEaseOutCubic();
    }

    public void ScaleDown (GameObject Boton) {
        LeanTween.scale(Boton, Vector3.one * .9f * 5.29f, .15f).setEaseOutCubic().setOnComplete( () => {
            ScaleBack(Boton);
        });        
    }

    public void ScaleBack (GameObject Boton) {
        LeanTween.scale(Boton, Vector3.one * 5.29f, .15f).setEaseOutCubic();        
    }

    public void AbrirCerrarDificultad () {
        nuevoValorSlider = 1 - nuevoValorSlider;

        while (sliderDificultad.value != nuevoValorSlider) {

            if (nuevoValorSlider > sliderDificultad.value) {
                sliderDificultad.value += Time.deltaTime * 2f;

            } else if (nuevoValorSlider < sliderDificultad.value ) {
                sliderDificultad.value -= Time.deltaTime * 2f;
            }
        }
    }
}

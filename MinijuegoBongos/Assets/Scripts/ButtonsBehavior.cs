using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonsBehavior: MonoBehaviour
{
    public GameObject sliderDificultadGameObject;
    Slider sliderDificultad;
    float nuevoValorSlider = 0f;
    public GameObject [] botones;

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
        sliderDificultad = sliderDificultadGameObject.GetComponent<Slider>();
        estadoActual = EstadosBoton.estadoPorDefecto;
        UnityEngine.Debug.Log(estadoActual);
        //LeanTween.value(sliderDificultad.value, 0f, 0f);
    }

    private void Start () {
        AparecerBoton(botones [0], 0f);
        AparecerBoton(botones [1], .125f);
        AparecerBoton(botones [2], .25f);
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
                AbrirCerrarDificultad();

                if (nuevoValorSlider == 1) {
                    DesaparecerBoton(botones [1], 0f);
                    DesaparecerBoton(botones [2], .125f);

                } else {
                    AparecerBoton(botones [1], 0f);
                    AparecerBoton(botones [2], .125f);
                }
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

    public void ScaleUp (GameObject Boton) {
        LeanTween.scale(Boton, Vector3.one * 1.05f * 5.29f, .15f).setEaseOutCubic();
    }

    public void ScaleDown (GameObject Boton) {
        LeanTween.scale(Boton, Vector3.one * .95f * 5.29f, .15f).setEaseOutCubic().setOnComplete( () => {
            ScaleBack(Boton);
        });        
    }

    public void ScaleBack (GameObject Boton) {
        LeanTween.scale(Boton, Vector3.one * 5.29f, .15f).setEaseOutCubic();        
    }

    public void AbrirCerrarDificultad () {
        nuevoValorSlider = 1 - nuevoValorSlider;

            if (nuevoValorSlider > sliderDificultad.value) {
                LeanTween.value(sliderDificultadGameObject, sliderDificultad.value, 1f, 1f).setEaseOutBounce();

            } else if (nuevoValorSlider < sliderDificultad.value ) {
                
                LeanTween.value(sliderDificultadGameObject, sliderDificultad.value, 0f, 1f).setEaseOutBounce();
            }
    }

    public void AparecerBoton (GameObject botonQueAparece, float delayAnimacion) {

        if (botonQueAparece.activeSelf == false) {
            botonQueAparece.SetActive(true);
        }
        LeanTween.scale(botonQueAparece, Vector3.zero, 0f).setOnComplete(() => {
            LeanTween.scale (botonQueAparece, Vector3.one * 5.29f, .75f).setEaseOutCubic().setDelay(delayAnimacion);
        });
    }

    public void DesaparecerBoton (GameObject botonQueDesaparece, float delayAnimacion) {
        LeanTween.scale(botonQueDesaparece, Vector3.zero, .75f).setOnComplete(() => {
            botonQueDesaparece.SetActive(false);
        }).setDelay(delayAnimacion);
        
    }
}

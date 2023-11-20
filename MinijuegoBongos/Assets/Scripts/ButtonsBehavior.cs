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
    float nuevoValorSlider = 0f, escalaBotonesMenu = 5.29f, escalaBotonesPeques = 4.23f;
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
    }

    private void Start () {
        AparecerBoton(botones [0], 0f, escalaBotonesMenu);
        AparecerBoton(botones [1], .125f, escalaBotonesMenu);
        AparecerBoton(botones [2], .25f, escalaBotonesMenu);
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
                    AparecerBoton(botones [3], .5f, escalaBotonesPeques);
                    AparecerBoton(botones [4], .625f, escalaBotonesPeques);
                    AparecerBoton(botones [5], .75f, escalaBotonesPeques);

                } else {
                    DesaparecerBoton(botones [5], 0f);
                    DesaparecerBoton(botones [4], .125f);
                    DesaparecerBoton(botones [3], .25f);
                    AparecerBoton(botones [2], .625f, escalaBotonesMenu);
                    AparecerBoton(botones [1], .75f, escalaBotonesMenu);
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

    public void ScaleUp (GameObject botonAEscalar) {
        float escalaPropia;

        if (botonAEscalar == botones [3] || botonAEscalar == botones [4] || botonAEscalar == botones [5]) {
            escalaPropia = escalaBotonesPeques;

        } else {
            escalaPropia = escalaBotonesMenu;
        }
        LeanTween.scale(botonAEscalar, Vector3.one * 1.05f * escalaPropia, .15f).setEaseOutCubic();
    }

    public void ScaleDown (GameObject botonAEscalar) {
        float escalaPropia;

        if (botonAEscalar == botones [3] || botonAEscalar == botones [4] || botonAEscalar == botones [5]) {
            escalaPropia = escalaBotonesPeques;

        } else {
            escalaPropia = escalaBotonesMenu;
        }

        LeanTween.scale(botonAEscalar, Vector3.one * .95f * escalaPropia, .15f).setEaseOutCubic().setOnComplete( () => {
            LeanTween.scale(botonAEscalar, Vector3.one * escalaPropia, .15f).setEaseOutCubic();
        });        
    }

    public void ScaleBack (GameObject botonAEscalar) {
        float escalaPropia;

        if (botonAEscalar == botones [3] || botonAEscalar == botones [4] || botonAEscalar == botones [5]) {
            escalaPropia = escalaBotonesPeques;

        } else {
            escalaPropia = escalaBotonesMenu;
        }

        LeanTween.scale(botonAEscalar, Vector3.one * escalaPropia, .15f).setEaseOutCubic();
    }

    public void AbrirCerrarDificultad () {
        float valorActualSlider = sliderDificultad.value;
        nuevoValorSlider = 1 - nuevoValorSlider;

        if (nuevoValorSlider > valorActualSlider) {
            LeanTween.value(gameObject, 0f, 1f, 1f).setEaseOutBounce().setOnUpdate((float val) => {
                valorActualSlider = val;
                sliderDificultad.value = valorActualSlider;
            }).setOnComplete(() => {
                valorActualSlider = 1f;
                sliderDificultad.value = valorActualSlider;
            });

        } else if (nuevoValorSlider < valorActualSlider) {
            LeanTween.value(gameObject, 1f, 0f, 1.25f).setEaseOutCubic().setOnUpdate((float val) => {
                valorActualSlider = val;
                sliderDificultad.value = valorActualSlider;
            }).setDelay(.4f).setOnComplete(() => {
                valorActualSlider = 0f;
                sliderDificultad.value = valorActualSlider;
            });
        }
    }

    public void AparecerBoton (GameObject botonQueAparece, float delayAnimacion, float escalaPropia) {

        if (botonQueAparece.activeSelf == false) {
            botonQueAparece.SetActive(true);
        }
        LeanTween.scale(botonQueAparece, Vector3.zero, 0f).setOnComplete(() => {
            LeanTween.scale (botonQueAparece, Vector3.one * escalaPropia, .75f).setEaseOutCubic().setDelay(delayAnimacion);
        });
    }

    public void DesaparecerBoton (GameObject botonQueDesaparece, float delayAnimacion) {
        LeanTween.scale(botonQueDesaparece, Vector3.zero, .75f).setEaseOutCubic().setOnComplete(() => {
            botonQueDesaparece.SetActive(false);
        }).setDelay(delayAnimacion);        
    }
}

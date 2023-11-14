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
    public GameObject [] botones;
    bool contarTiempo = false;
    float intervaloDeTiempo = .5f;
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

    private void Start () {
        AnimacionBotonesAparecer(0);
    }
    private void Update () {
        UnityEngine.Debug.Log(sliderDificultad.value);

        if (contarTiempo == true) {
            if (intervaloDeTiempo >= 0f) {
                intervaloDeTiempo -= Time.deltaTime;

            } else {
                intervaloDeTiempo = .5f;
                contarTiempo = false;
            }
        }
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
                UnityEngine.Debug.Log("Por defecto");
                break;

            case EstadosBoton.estadoSeleccionarDificultad:
                UnityEngine.Debug.Log("Cambiar Dificultad");
                AbrirCerrarDificultad();
                if (nuevoValorSlider == 1) {
                    AnimacionBotonesDesaparecer(1);
                } else {
                    AnimacionBotonesAparecer(1);
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


    public void VolverCambiarDificultad () {
        if (sliderDificultad.value >= 1f) {
            LeanTween.value(sliderDificultad.value, 0f, .75f).setEaseOutBounce();
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

        while (sliderDificultad.value != nuevoValorSlider) {

            if (nuevoValorSlider > sliderDificultad.value) {
                sliderDificultad.value += Time.deltaTime * 2f;

            } else if (nuevoValorSlider < sliderDificultad.value ) {
                sliderDificultad.value -= Time.deltaTime * 2f;
            }
        }
    }

    public void AparecerBoton (GameObject botonQueAparece) {
        if (botonQueAparece.activeSelf == false) {
            botonQueAparece.SetActive(true);
        }
        LeanTween.scale(botonQueAparece, Vector3.zero, 0f).setOnComplete(() => {
            LeanTween.scale (botonQueAparece, Vector3.one * 5.29f, .5f).setEaseOutBounce();
        });
    }

    public void DesaparecerBoton (GameObject botonQueDesaparece) {
        LeanTween.scale(botonQueDesaparece, Vector3.zero, .5f).setEaseOutBounce().setOnComplete ( ()=> {
            botonQueDesaparece.SetActive(false);
        });
        
    }

    public void AnimacionBotonesAparecer (int botonInicial) {
        int cuentaBotones = botonInicial;
        contarTiempo = true;

        while (cuentaBotones <= botones.Length - 1) {
            
            if (intervaloDeTiempo >= 0f) {
                UnityEngine.Debug.Log(intervaloDeTiempo);
            } else {
                AparecerBoton(botones [cuentaBotones]);
                cuentaBotones++;
                contarTiempo = true;
            }
        }
        

    }

    public void AnimacionBotonesDesaparecer (int botonInicial) {
        int cuentaBotones = botonInicial;
        contarTiempo = true;

        while (cuentaBotones <= botones.Length - 1) {

            if (intervaloDeTiempo >= 0f) {
                UnityEngine.Debug.Log(intervaloDeTiempo);
            } else {
                DesaparecerBoton(botones [cuentaBotones]);
                cuentaBotones++;
                contarTiempo = true;
            }
        }
    }
}

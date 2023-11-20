using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Specialized;

public class ButtonsBehavior: MonoBehaviour
{
    public GameObject sliderDificultadGameObject, bandejaOpciones;
    Slider sliderDificultad;
    float nuevoValorSlider = 0f, escalaBotonesMenu = 5.29f, escalaBotonesDificultad = 4.23f;
    public GameObject [] botonesMenu;
    public GameObject [] botonesDificultad;
    public GameObject[] botonesOpciones;
    public Canvas mainCanvas;

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
        AparecerBoton(botonesMenu [0], 0f, botonesMenu [0].GetComponent<ButtonScaler>().escalaPropia);
        AparecerBoton(botonesMenu [1], .125f, botonesMenu [1].GetComponent<ButtonScaler>().escalaPropia);
        AparecerBoton(botonesMenu [2], .25f, botonesMenu [2].GetComponent<ButtonScaler>().escalaPropia);
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
                    DesaparecerBoton(botonesMenu [1], 0f);
                    DesaparecerBoton(botonesMenu [2], .125f);
                    AparecerBoton(botonesDificultad [0], .5f, botonesDificultad [0].GetComponent<ButtonScaler>().escalaPropia);
                    AparecerBoton(botonesDificultad [1], .625f, botonesDificultad [1].GetComponent<ButtonScaler>().escalaPropia);
                    AparecerBoton(botonesDificultad [2], .75f, botonesDificultad [2].GetComponent<ButtonScaler>().escalaPropia);

                } else {
                    DesaparecerBoton(botonesDificultad [2], 0f);
                    DesaparecerBoton(botonesDificultad [1], .125f);
                    DesaparecerBoton(botonesDificultad [0], .25f);
                    AparecerBoton(botonesMenu [2], .625f, botonesMenu [2].GetComponent<ButtonScaler>().escalaPropia);
                    AparecerBoton(botonesMenu [1], .75f, botonesMenu [1].GetComponent<ButtonScaler>().escalaPropia);
                }
                break;



            case EstadosBoton.estadoOpciones:
                UnityEngine.Debug.Log("Opciones");
                AbrirCerrarOpciones();
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

    public void AbrirCerrarOpciones ()
    {
        if (bandejaOpciones.activeSelf == false)
        {
            bandejaOpciones.SetActive(true);
            mainCanvas.GetComponent<GraphicRaycaster>().enabled = false;
            LeanTween.moveLocal(bandejaOpciones, new Vector3(0f, 1100f, 0f), 0f);
            LeanTween.moveLocal(bandejaOpciones, Vector3.zero, 1.5f).setEaseOutBounce().setOnComplete (() =>
            {
                mainCanvas.GetComponent<GraphicRaycaster>().enabled = true;
            });
        } else
        {
            LeanTween.moveLocal(bandejaOpciones, new Vector3(0f, 1100f, 0f), 1f).setEaseOutCubic().setOnComplete(() =>
            {
                bandejaOpciones.SetActive(false);
                mainCanvas.GetComponent<GraphicRaycaster>().enabled = true;    
            });
        }
    }

    public void AparecerBoton (GameObject botonQueAparece, float delayAnimacion, float escalaBoton) {

        if (botonQueAparece.activeSelf == false) {
            botonQueAparece.SetActive(true);
        }
        LeanTween.scale(botonQueAparece, Vector3.zero, 0f).setOnComplete(() => {
            LeanTween.scale (botonQueAparece, Vector3.one * escalaBoton, .75f).setEaseOutCubic().setDelay(delayAnimacion);
        });
    }

    public void DesaparecerBoton (GameObject botonQueDesaparece, float delayAnimacion) {
        LeanTween.scale(botonQueDesaparece, Vector3.zero, .75f).setEaseOutCubic().setOnComplete(() => {
            botonQueDesaparece.SetActive(false);
        }).setDelay(delayAnimacion);        
    }
}

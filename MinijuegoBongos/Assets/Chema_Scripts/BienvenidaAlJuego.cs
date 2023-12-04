using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Collections.Specialized;

public class BienvenidaAlJuego : MonoBehaviour
{
    public Image imagenSaltar;
    public GameObject rellenoSaltar,imagenTransiciones, controlesMando, controlesPC, textoBienvenida, totalExplicacion;
    bool volverImagen = true;
    public KeyCode interactuarMouse, interactuarMando;

    public enum EstadosIntroduccion
    {
        estadoInicio,
        bienvenida,
        explicacionJuego,
        controles,
        despedida
    }

    EstadosIntroduccion estadoIntro;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alphaCanvas(imagenTransiciones.GetComponent<CanvasGroup>(), 0f, 0f);
        estadoIntro = EstadosIntroduccion.estadoInicio;
        CambiarEstados();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) || Input.GetKey(interactuarMando))
        {
            if (volverImagen != false)
            {
                volverImagen = false;
            }
            imagenSaltar.fillAmount = imagenSaltar.fillAmount += Time.deltaTime / 3f;
            
            if (LeanTween.isTweening(rellenoSaltar) == false && rellenoSaltar.transform.localScale.x == 1.45f)
            {
                ScaleUpSaltar();
            }
        
        } else
        {
            if (volverImagen != true)
            {
                volverImagen = true;
            }

            if (LeanTween.isTweening(rellenoSaltar) == false && rellenoSaltar.transform.localScale.x == 1.45f * 1.1f)
            {
                ScaleBackSaltar();
            }
        }

        if (imagenSaltar.fillAmount >= 1f && LeanTween.isTweening(imagenTransiciones) == false)
        {
            LeanTween.alphaCanvas(imagenTransiciones.GetComponent<CanvasGroup>(), 1f, 1f).setOnComplete( ()=>
            {
                SceneManager.LoadScene("Menu");
            });
        } 
        

        if (volverImagen == true && imagenSaltar.fillAmount > 0f)
        {
            imagenSaltar.fillAmount -= Time.deltaTime;
        }
    }

    public void EstaClicando ()
    {
        UnityEngine.Debug.Log("ta clicando");
        if (volverImagen != false)
        {        
        volverImagen = false; 
        }
        
        imagenSaltar.fillAmount += Time.deltaTime / 3f;
    }

    public void ScaleUpSaltar ()
    {
        LeanTween.scale(rellenoSaltar, Vector3.one * 1.45f * 1.1f, .5f).setEaseOutCubic();
    }

    public void ScaleBackSaltar ()
    {
        LeanTween.scale(rellenoSaltar, Vector3.one * 1.45f, .5f).setEaseOutCubic();        
    }

    public void CambiarEstados ()
    {
        switch (estadoIntro)
        {
            case EstadosIntroduccion.estadoInicio:
                UnityEngine.Debug.Log("Inicio del juego");
                LeanTween.alphaCanvas(imagenTransiciones.GetComponent<CanvasGroup>(), 1f, 0f).setOnComplete(() => {
                    LeanTween.alphaCanvas(imagenTransiciones.GetComponent<CanvasGroup>(), 0f, 1.5f).setOnComplete(()=> {
                        estadoIntro = EstadosIntroduccion.bienvenida;
                        CambiarEstados();
                    });                
                });
                break;

            case EstadosIntroduccion.bienvenida:
                UnityEngine.Debug.Log("Bienvenida");
                textoBienvenida.SetActive(true);
                float tiempo = 2f;
                LeanTween.value(tiempo, 0f, 2f).setOnComplete(()=>
                {
                    textoBienvenida.GetComponent<TextMeshProUGUI>().text = "Bienvenido a Bongo Mania, \n te mostraremos como funciona el juego";
                    textoBienvenida.GetComponent<AnimacionesTextoIntro>().reaparecer = true;
                    LeanTween.moveLocal(textoBienvenida, new Vector3(0f, 300f, 0f), 1.5f).setDelay(2f).setOnComplete(() => {
                        estadoIntro = EstadosIntroduccion.explicacionJuego;
                        textoBienvenida.SetActive(false);
                        CambiarEstados();
                    });
                });
                break;

            case EstadosIntroduccion.explicacionJuego:
                UnityEngine.Debug.Log("Explicación del Juego");
                totalExplicacion.SetActive(true);
                tiempo = 1.5f;
                int indiceArray = 0;
                float esperaSpawn = 1.5f;

                LeanTween.value(tiempo, 0f, 60f).setOnComplete(() =>
                {
                    textoBienvenida.SetActive(true);
                    textoBienvenida.GetComponent<TextMeshProUGUI>().text = "Ahora te mostraremos los controles del juego";
                    textoBienvenida.GetComponent<AnimacionesTextoIntro>().reaparecer = true;
                    totalExplicacion.SetActive(false);
                    estadoIntro = EstadosIntroduccion.controles;
                    CambiarEstados();
                });
                break;

            case EstadosIntroduccion.controles:
                controlesMando.SetActive(true);
                tiempo = 15f;

                LeanTween.value(tiempo, 0f, 30f).setOnComplete(() => {
                    controlesMando.SetActive(false);
                    controlesPC.SetActive(true);
                    tiempo = 15;

                    LeanTween.value(tiempo, 0f, 30f).setOnComplete (()=> {
                        controlesPC.SetActive(false);
                        estadoIntro = EstadosIntroduccion.despedida;
                        CambiarEstados ();
                    });
                }); 

                break;

            case EstadosIntroduccion.despedida:
                textoBienvenida.GetComponent<TextMeshProUGUI>().text = "Esto es todo lo que necesitas saber, \n esperamos que disfrutes del juego.";
                textoBienvenida.GetComponent<AnimacionesTextoIntro>().reaparecer = true;
                LeanTween.moveLocal(textoBienvenida, Vector3.zero, 1.5f).setOnComplete(() => { 
                    tiempo = 2f;

                    LeanTween.value(tiempo, 0f, 2f).setOnComplete(() => {

                        LeanTween.alphaCanvas(imagenTransiciones.GetComponent<CanvasGroup>(), 1f, 1f).setOnComplete(() =>
                        {
                            SceneManager.LoadScene("Menu");
                        });
                    });
                });
                break;


        }
    }
}

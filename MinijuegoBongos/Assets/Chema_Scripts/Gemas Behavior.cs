using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System.Diagnostics;
using System;
// -1490 a -1720
public class GemasBehavior : MonoBehaviour
{
    public GameObject puntosGameObject, imagenBlanca, menuOpciones, canvasOpciones, opcionesDesplegadas, camara;
    public GameObject [] mensajesMarcar;
    GameObject gameManager, cancionSonando;
    Slider sliderPuntos;
    CanvasGroup canvas, blancaCanvas;
    public KeyCode buttonCode1, buttonCode2;
    bool animando = false;
    public bool cambioPuntos = false, puedeMarcar = true, gemaParalela = false, primeraGema = false;
    float velocidad = 100f, sentidoY = 0f;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        opcionesDesplegadas = gameManager.GetComponent<GameManager>().opcionesDesplegadas;
        imagenBlanca.SetActive (false);
        velocidad = velocidad * (gameManager.GetComponent<GameManager>().velocidadJuego * 2);
        sliderPuntos = puntosGameObject.GetComponent<Slider> ();
        blancaCanvas = imagenBlanca.GetComponent<CanvasGroup> ();
        canvasOpciones = GameObject.Find("CanvasOpciones");
        menuOpciones = GameObject.Find("Fondo Opciones");
    }
    void Start()
    {
        cancionSonando = gameManager.GetComponent<GameManager>().canciones [gameManager.GetComponent<GameManager>().dificultad];
    }

    // Update is called once per frame
    void Update()
    {
        if (opcionesDesplegadas.activeSelf == false)
        {
            transform.eulerAngles = transform.eulerAngles + new Vector3 (0f, 0f, 1f) * velocidad * Time.deltaTime;
            transform.position = transform.position - new Vector3 (velocidad * 5f, -velocidad * (0f - sentidoY) , 0f) * Time.deltaTime;

            if (transform.localPosition.x < -1720) {
                if (animando == false) {
                    sentidoY = .5f; 
                    imagenBlanca.SetActive (true);
                    imagenBlanca.GetComponent<Image> ().color = Color.black;
                    imagenBlanca.SetActive (false);
                    AnimacionDesaparecer ();
                }
            } else {
                sentidoY = 0f;
            }

            if (puedeMarcar == true && puedeMarcar == CheckerPuedeMarcar()) {
                if (gemaParalela == true) {
                    MarcarPuntos (.01f);
                } else {
                    MarcarPuntos (.02f);
                }
            }

            if (animando == false && sliderPuntos.value >= 1f) {
                AnimacionDesaparecer();
            }

            if (cancionSonando != null && primeraGema == true)
            {
                primeraGema = false;
                float x = .1f;
                LeanTween.value(x, 0f, .1f).setOnComplete(() =>
                {
                    UnityEngine.Debug.Log(gameObject.transform.localPosition.x.ToString());
                    cancionSonando.SetActive(true);
                    UnityEngine.Debug.Log("Puse una canci�n");

                });
            }
        }
        //gameObject.transform.localPosition.x <= (-1520f - (1f * velocidad))  && 
    }

    
    public void MarcarPuntos(float puntosAMarcar) {

        if ((Input.GetKeyDown (buttonCode1) || Input.GetKeyDown (buttonCode2)) && sliderPuntos.value < 1f ) {
            UnityEngine.Debug.Log ("+" + puntosAMarcar.ToString() + " puntos");
            cambioPuntos = true;
            sliderPuntos.value += puntosAMarcar;
            MostrarMensajesAlMarcar();
            imagenBlanca.SetActive (true);
            imagenBlanca.GetComponent<Image> ().color = Color.white;
            imagenBlanca.SetActive (false);

            if (imagenBlanca.activeSelf == false) {
                imagenBlanca.SetActive (true);
                LeanTween.alphaCanvas (blancaCanvas, 0f, 0f).setOnComplete (() => {
                    LeanTween.alphaCanvas (blancaCanvas, 1f, .5f);
                });
            }
            AnimacionDesaparecer ();            
        }
    }

    public void AnimacionDesaparecer ()
    {
        puedeMarcar = false;

        if (animando == false)
        {
            animando = true;
            LeanTween.scale(gameObject, Vector3.one * 1.5f, 1f).setEaseOutQuad();
            LeanTween.alphaCanvas (canvas, 0f, 1f).setEaseOutQuad ().setOnComplete (() => {
                LeanTween.scale (gameObject, transform.localScale * .75f, 0f);
                gameObject.SetActive (false);

            });
        }
    }


    public void Reset()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, 0f);
        transform.localPosition = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        imagenBlanca.SetActive(false);
        cambioPuntos = false;
        animando = false;
        puedeMarcar = true;
        canvas = gameObject.GetComponent<CanvasGroup>();

        LeanTween.alphaCanvas(canvas, 0f, 0f).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(canvas, 1f, 1f).setEaseOutQuad();
        });
    }


    public bool CheckerPuedeMarcar () {

        if (transform.localPosition.x <= -1490f && transform.localPosition.x > -1720f && camara.GetComponent<ControladorTiempoInput>().puedeTocarBongos == true) {
            return true;
        } else {
            return false;
        }
    }

    public void MostrarMensajesAlMarcar ()
    {
        bool puedeGenerar = false;

        while (puedeGenerar == false)
        {
            int y = Mathf.FloorToInt(UnityEngine.Random.Range(0f, mensajesMarcar.Length - .01f));
            if (mensajesMarcar [y].activeSelf == false)
            {
                puedeGenerar = true;
                mensajesMarcar [y].SetActive(true);
                mensajesMarcar [y].GetComponent<ScriptAnimMensajesInGame>().activarAnim = true;                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System.Diagnostics;
// -1570 a -1720
public class GemasBehavior : MonoBehaviour
{
    public GameObject puntosGameObject, imagenBlanca, FXluz, FXestrellas;
    Slider sliderPuntos;
    CanvasGroup canvas, blancaCanvas;
    public KeyCode buttonCode;
    bool animando = false;
    public bool cambioPuntos = false, puedeMarcar = true, gemaParalela = false;
    float velocidad = 100f, sentidoY = 0f;


    private void Awake()
    {
        imagenBlanca.SetActive (false);
        sliderPuntos = puntosGameObject.GetComponent<Slider> ();
        blancaCanvas = imagenBlanca.GetComponent<CanvasGroup> ();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3 (0f, 0f, 1f) * velocidad * Time.deltaTime;
        transform.position = transform.position - new Vector3 (velocidad * 5f, -velocidad * (0f - sentidoY) , 0f) * Time.deltaTime;

        if (transform.localPosition.x < -1710) {
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
                MarcarPuntos (.05f);
            } else {
                MarcarPuntos (.1f);
            }
        }

        if (animando == false && sliderPuntos.value >= 1f) {
            AnimacionDesaparecer();
        }
    }

    
    public void MarcarPuntos(float puntosAMarcar) {

        if (Input.GetKeyDown (buttonCode) && CheckerPuedeMarcar () == puedeMarcar && sliderPuntos.value < 1f) {
            UnityEngine.Debug.Log ("a");
            cambioPuntos = true;
            sliderPuntos.value += puntosAMarcar;
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

        if (transform.localPosition.x <= -1570f && transform.localPosition.x > -1710f) {
            return true;
        } else {
            return false;
        }
    }
}

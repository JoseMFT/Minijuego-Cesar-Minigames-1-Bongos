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
    public GameObject puntosGameObject, imagenBlanca;
    Slider sliderPuntos;
    CanvasGroup canvas;
    public KeyCode buttonCode;
    bool animando = false, pulsando = false;
    public bool quitoPuntos = false, puedeMarcar = true, gemaParalela = false;
    float velocidad = 70f;


    private void Awake()
    {
        imagenBlanca.SetActive (false);
        sliderPuntos = puntosGameObject.GetComponent<Slider> ();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3 (0f, 0f, 1f) * velocidad * Time.deltaTime;
        transform.position = transform.position - new Vector3 (velocidad * 5f, 0f, 0f) * Time.deltaTime;

        if (transform.localPosition.x < -1710 && animando == false) {
            Animate ();
        }

        if (puedeMarcar == true && puedeMarcar == CheckerPuedeMarcar()) {
            if (gemaParalela == true) {
                MarcarPuntos (.05f);
            } else {
                MarcarPuntos (.1f);
            }
        }

        if (animando == false && sliderPuntos.value >= 1f) {
            Animate();
        }

    }

    
    public void MarcarPuntos(float puntosAMarcar) {

        if (Input.GetKeyDown (buttonCode) && CheckerPuedeMarcar () == puedeMarcar && sliderPuntos.value < 1f) {
            UnityEngine.Debug.Log ("a");
            quitoPuntos = true;
            sliderPuntos.value += puntosAMarcar;

            if (imagenBlanca.activeSelf == false) {
                imagenBlanca.SetActive (true);
            }
            Animate ();            
        }
    }
    public void Animate ()
    {
        puedeMarcar = false;

        if (animando == false)
        {
            animando = true;
            LeanTween.scale(gameObject, Vector3.one * 1.5f, 2f).setEaseOutQuad();
            LeanTween.alphaCanvas(canvas, 0f, 2f).setEaseOutQuad().setOnComplete(() =>
            {
                LeanTween.scale(gameObject, transform.localScale * .75f, 0f);
                gameObject.SetActive(false);

            }).setDelay(.35f);
        }
    }

    public void Reset()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, 0f);
        transform.localPosition = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        imagenBlanca.SetActive(false);
        quitoPuntos = false;
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

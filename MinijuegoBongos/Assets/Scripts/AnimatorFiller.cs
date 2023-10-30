using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorFiller : MonoBehaviour
{
    public GameObject particleChangeVal, estrellaFinal;
    Slider selfSlider;
    GameObject estrellaVacia;
    CanvasGroup estCanvas;
    bool estActiva = false;

    private void Awake()
    {
        selfSlider = gameObject.GetComponent<Slider>();
        estrellaFinal = GameObject.Find("Estrella llena");
        estCanvas = estrellaFinal.GetComponent<CanvasGroup> ();
        estCanvas.alpha = 0;
        estrellaFinal.SetActive(false);
        estrellaVacia = GameObject.Find ("Estrella");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (selfSlider.value >= 1f && estrellaFinal.activeSelf == false) {
            estrellaFinal.SetActive (true);
        }

        if (estrellaFinal.activeSelf == true && estActiva == false) {
            estActiva = true;
            LeanTween.alphaCanvas (estCanvas, 1f, .75f);
            Vector3 escalaEstVacia = estrellaVacia.transform.localScale;
            LeanTween.scale (estrellaVacia, estrellaVacia.transform.localScale * .65f, .25f).setEaseOutQuad().setOnComplete (() => {
                LeanTween.scale (estrellaVacia, escalaEstVacia, .5f).setEaseInOutElastic ();
            });
        }
    }
}

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
    public Slider sliderPuntos;
    Vector3 objScale;
    public GameObject white;
    CanvasGroup canvas;
    public KeyCode buttonCode;
    bool animating = false;
    public bool tookPoints = false, canScore = true;
    float speed = 70f;


    private void Awake()
    {
        
        objScale = transform.localScale;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 1f) * speed * Time.deltaTime;
        transform.position = transform.position - new Vector3(speed * 5f, 0f, 0f) * Time.deltaTime;
        if (transform.localPosition.x < -1710 && animating == false) {
            Animate ();
        }

        if (canScore == true) {
            if (Input.GetKey (buttonCode) && CanScoreChecker() == canScore) {
                UnityEngine.Debug.Log ("a");
                if (Input.GetKey (buttonCode) && sliderPuntos.value < 1f) {
                    tookPoints = true;
                    sliderPuntos.value += .1f;
                    if (white.activeSelf == false) {
                        white.SetActive (true);
                    }
                        Animate ();
                }
            }
        }

        if (animating == false && sliderPuntos.value >= 1f) {
            Animate();
        }
    }

    public void Animate ()
    {
        canScore = false;
        if (animating == false)
        {
            animating = true;
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
        white.SetActive(false);
        tookPoints = false;
        animating = false;
        canScore = true;
        canvas = gameObject.GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(canvas, 0f, 0f).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(canvas, 1f, 1f).setEaseOutQuad();
        });
    }

    public bool CanScoreChecker () {
        if (transform.localPosition.x <= -1570f && transform.localPosition.x > -1710f) {
            return true;
        } else {
            return false;
        }
    }
}

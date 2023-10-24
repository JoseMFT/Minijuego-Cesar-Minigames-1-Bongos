using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// -1570 a -1720
public class GemasBehavior : MonoBehaviour
{
    Slider sliderPuntos;
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

        if (transform.localPosition.x <= -1570 && transform.localPosition.x > -1720)
        {
            if (Input.GetKeyDown(buttonCode) && canScore ==  true)
            {
                Debug.Log("Good!");
                canScore = false;
                if (sliderPuntos.value < 1f)
                {
                    sliderPuntos.value += .1f;
                    white.SetActive(true);
                    Animate();
                }
            }

            Animate();
        }
        else if (transform.localPosition.x <= -1720 && tookPoints == false)
        {
            sliderPuntos.value -= .05f;
            tookPoints = true;
            Animate();
        }

        if (sliderPuntos.value >= 1f && white.activeSelf == false) Animate();
    }

    public void Animate ()
    {
        canScore = false;
        if (animating == false && white.activeSelf == false)
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
        sliderPuntos = FindObjectOfType<Slider>();
        canvas = gameObject.GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(canvas, 0f, 0f).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(canvas, 1f, 1f).setEaseOutQuad();
        });
    }
}

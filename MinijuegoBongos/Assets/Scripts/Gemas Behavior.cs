using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// -643 a -758
public class GemasBehavior : MonoBehaviour
{
    Slider sliderPuntos;
    public GameObject white;
    CanvasGroup canvas;
    public KeyCode buttonCode;
    bool animating = false, tookPoints = false;
    float speed = 40f;
    void Start()
    {
        
        sliderPuntos = FindObjectOfType<Slider>();
        canvas = gameObject.GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(canvas, 0f, 0f).setOnComplete(() =>
       {
           LeanTween.alphaCanvas(canvas, 1f, 1f).setEaseOutQuad();
       });
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 1f) * speed * Time.deltaTime;
        transform.position = transform.position - new Vector3(speed * 5f, 0f, 0f) * Time.deltaTime;

        if (transform.localPosition.x <= -643 && transform.localPosition.x >= -758)
        {
            if (Input.GetKeyDown(buttonCode))
            {
                Debug.Log("Good!");
                if (sliderPuntos.value < 1f)
                {
                    sliderPuntos.value += .1f;
                    white.SetActive(true);
                    Destroy(gameObject, .25f);
                }
            }

            if (animating == false && white.activeSelf == false)
            {
                animating = true;
                LeanTween.scale(gameObject, transform.localScale * 1.5f, 4f).setEaseOutQuad();
                LeanTween.alphaCanvas(canvas, 0f, 1f).setEaseOutQuad().setOnComplete(() =>
                {
                    Destroy(gameObject);
                }).setDelay(.35f);
            }
        }
        else if (transform.localPosition.x < -758 && tookPoints == false)
        {
            sliderPuntos.value -= .05f;
            tookPoints = true;
        }

        if (sliderPuntos.value >= 1f && white.activeSelf == false) Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("me choco con " + collision.name);
        LeanTween.alphaCanvas(canvas, 0f, 1f).setEaseOutQuad().setDelay(.5f);
    }

    void OnTriggerExit(Collider collision)
    {
        Destroy(gameObject);
        
    }
}

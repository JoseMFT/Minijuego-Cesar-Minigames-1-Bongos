using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ScriptMuestraGemas : MonoBehaviour
{
    float velocidad = 100f, sentidoY = 0f;
    bool animando = false;
    public GameObject imagenBlanca;
    public CanvasGroup canvas;
    public Slider puntajeMuestra;
        
    // Start is called before the first frame update
    void Start()
    {
        imagenBlanca.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = transform.localPosition - new Vector3(velocidad * 5f, -velocidad * (0f - sentidoY), 0f) * Time.deltaTime;
        transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 1f) * velocidad * Time.deltaTime;
        if (transform.localPosition.x < -1710)
        {
            if (gameObject.name.Contains("Falla") == true && animando == false)
            {
                sentidoY = .5f;
                imagenBlanca.SetActive(true);
                imagenBlanca.GetComponent<Image>().color = Color.black;
                AnimacionDesaparecer();
            }
        } else
        {
            if (animando == false && gameObject.name.Contains("Acierta") && gameObject.transform.localPosition.x <= -1570f)
            {
                sentidoY = 0f;
                imagenBlanca.SetActive(true);
                imagenBlanca.GetComponent<Image>().color = Color.white;
                puntajeMuestra.value += .1f;
                AnimacionDesaparecer();            
            }
        }
    }

    public void AnimacionDesaparecer ()
    {
        if (animando == false)
        {
            animando = true;
            LeanTween.scale(gameObject, Vector3.one * 1.5f, 1f).setEaseOutQuad();
            LeanTween.alphaCanvas(canvas, 0f, 1f).setEaseOutQuad().setOnComplete(() => {
                LeanTween.scale(gameObject, transform.localScale * .75f, 0f);
                gameObject.SetActive(false);
            });
        }
    }
}

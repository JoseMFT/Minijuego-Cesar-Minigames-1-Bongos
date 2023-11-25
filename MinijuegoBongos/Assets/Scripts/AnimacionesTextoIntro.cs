using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionesTextoIntro : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public bool reaparecer = false;
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        Aparecer();
    }

    // Update is called once per frame
    void Update()
    {
        if (reaparecer == true)
        {
            Aparecer();
            reaparecer = false;
        }
    }

    public void Aparecer ()
    {
        LeanTween.alphaCanvas(canvasGroup, 0f, 0f).setOnComplete(()=> {
            LeanTween.alphaCanvas(canvasGroup, 1f, 1f);    
        });
    }
}

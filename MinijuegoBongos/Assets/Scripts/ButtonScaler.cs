using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScaler : MonoBehaviour
{
    public float escalaPropia;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleUp ()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.05f * escalaPropia, .15f).setEaseOutCubic();
    }

    public void ScaleDown ()
    {
        LeanTween.scale(gameObject, Vector3.one * .95f * escalaPropia, .15f).setEaseOutCubic().setOnComplete(() => {
            ScaleUp();
        });
    }

    public void ScaleBack ()
    {
        LeanTween.scale(gameObject, Vector3.one * escalaPropia, .15f).setEaseOutCubic();
    }

}

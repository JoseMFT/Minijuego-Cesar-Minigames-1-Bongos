using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorFiller : MonoBehaviour
{
    public GameObject particleChangeVal;
    Slider selfSlider;
    public GameObject estrellaFinal;

    private void Awake()
    {
        selfSlider = gameObject.GetComponent<Slider>();
        estrellaFinal = GameObject.Find("Estrella llena");
        estrellaFinal.SetActive(false);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (selfSlider.value >= 1f && estrellaFinal.activeSelf == false) estrellaFinal.SetActive(true);   
    }

    public void animationLT()
    {
        GameObject.Instantiate(particleChangeVal, estrellaFinal.transform.position, Quaternion.identity);
        LeanTween.scale(gameObject, Vector3.one, .5f).setEaseInOutElastic();
    }
}

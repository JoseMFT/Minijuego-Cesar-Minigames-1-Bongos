using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorFiller : MonoBehaviour
{
    public GameObject particleChangeVal;
    Slider selfSlider;
    public GameObject finalStar;
    bool activeStar = false;

    private void Awake()
    {
        selfSlider = gameObject.GetComponent<Slider>();
        finalStar = GameObject.Find("Estrella llena");
        finalStar.SetActive(false);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (selfSlider.value >= 1f && finalStar.activeSelf == false) {
            finalStar.SetActive (true);
        }

        if (finalStar.activeSelf == true && activeStar == false) {
            activeStar = true;
        }
    }

    public void animationLT()
    {
        GameObject.Instantiate(particleChangeVal, finalStar.transform.position, Quaternion.identity);
        LeanTween.scale(gameObject, Vector3.one, .5f).setEaseInOutElastic();
    }
}

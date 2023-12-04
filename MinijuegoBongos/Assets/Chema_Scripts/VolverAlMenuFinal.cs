using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMenuFinal : MonoBehaviour
{
    float value = 1f;
    public GameObject victoria, derrota;
    GameObject gameManager, cameraPos;
    public CanvasGroup imgTransicion;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        cameraPos = GameObject.Find("GameCameraPos");

        if (gameManager.GetComponent<GameManager>().derrotaOVictoria == false) {
            derrota.SetActive(true);

        } else
        {
            victoria.SetActive(true);
        }     

        LeanTween.alphaCanvas(imgTransicion, 0f, 0f).setOnComplete(()=>
        {
            LeanTween.value(value, 0f, 3f).setOnComplete(()=> {
                LeanTween.alphaCanvas(imgTransicion, 1f, 2f).setOnComplete(() =>
                {
                    Destroy(gameManager);
                    Destroy(cameraPos);
                    SceneManager.LoadScene("Menu");
                });
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

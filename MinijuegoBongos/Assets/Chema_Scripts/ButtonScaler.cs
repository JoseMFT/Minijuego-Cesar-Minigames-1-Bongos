using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ButtonScaler : MonoBehaviour
{
    public float escalaPropia;
    GameObject gameManager, cameraPos, estadoPartida;

    void Start()
    {
        cameraPos = GameObject.Find("GameCameraPos");
        gameManager = GameObject.Find("GameManager");
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
            ScaleBack();
        });
    }

    public void ScaleBack ()
    {
        LeanTween.scale(gameObject, Vector3.one * escalaPropia, .15f).setEaseOutCubic();
    }
}

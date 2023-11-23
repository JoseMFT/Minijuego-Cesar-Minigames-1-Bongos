using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraMovStart : MonoBehaviour
{
    GameObject menuCanvas, logotipo;
    public GameObject canvasOpciones;
    float velocidad = 1.25f;
    CanvasGroup menuCanvasGroup;
    public Vector3 nuevaPos, cameraPos;
    bool animar = false, mismaPos = false, mismaRot = false;
    public Quaternion nuevaRot, cameraRot;

    private void Awake () {
        nuevaPos = GameObject.Find ("GameCameraPos").transform.localPosition;
        nuevaRot = GameObject.Find ("GameCameraPos").transform.localRotation;
        menuCanvas = GameObject.Find ("Canvas");
        logotipo = GameObject.Find("RawImage-DisplayVideo");
        menuCanvasGroup = menuCanvas.GetComponent<CanvasGroup> ();
    }
    private void Update () {

        cameraPos = transform.position;
        cameraRot = transform.rotation;

        if (animar == true) {

            if (cameraPos != nuevaPos) {
                transform.position = Vector3.MoveTowards (cameraPos, nuevaPos, velocidad * Time.deltaTime);
            } else {
                mismaPos = true;
            }

            if (cameraRot != nuevaRot) {
                transform.rotation = Quaternion.RotateTowards (cameraRot, nuevaRot, 3.575f * velocidad * Time.deltaTime);

                if (Quaternion.Angle (cameraRot, nuevaRot) < .2f)
                    transform.rotation = nuevaRot;

            } else {
                mismaRot = true;
            }

            if (mismaPos == true && mismaRot == true)
            {
                animar = false;
                float y = 0f;
                LeanTween.value(y, 1f, 1f).setOnComplete(() =>
                {
                    canvasOpciones.SetActive(true);
                    SceneManager.LoadScene("EscenaPrincipal", LoadSceneMode.Single);
                });
            }
        }
    }

    public void AnimacionQuitarCanvas () {
        animar = true;
        menuCanvas.GetComponent<GraphicRaycaster> ().enabled = false;
        canvasOpciones.SetActive(false);

        LeanTween.moveLocal(logotipo, Vector3.zero, 3f).setOnComplete(() =>
        {
            float x = 0f;
            LeanTween.value(x, 1f, .5f).setOnComplete(() => {
                x = 0f;
                logotipo.SetActive(false);
            });
        });
        LeanTween.alphaCanvas (menuCanvasGroup, 0.0001f, 3f);
    }
}

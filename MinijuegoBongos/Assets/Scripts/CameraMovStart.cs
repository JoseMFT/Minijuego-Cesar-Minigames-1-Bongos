using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovStart : MonoBehaviour
{
    GameObject menuCanvas, logotipo;
    float velocidad = 1.25f;
    CanvasGroup menuCanvasGroup;
    public Vector3 nuevaPos, cameraPos;
    bool animar = false, mismaPos = false, mismaRot = false;
    public Quaternion nuevaRot, cameraRot;

    private void Awake () {
        nuevaPos = GameObject.Find ("GameCameraPos").transform.position;
        nuevaRot = GameObject.Find ("GameCameraPos").transform.rotation;
        menuCanvas = GameObject.Find ("Canvas");
        logotipo = GameObject.Find("Quad");
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
                animar = false;
        }
    }


    public void AnimacionOpciones () {
    
    }

    public void AnimacionQuitarCanvas () {
        animar = true;
        menuCanvas.GetComponent<GraphicRaycaster> ().enabled = false;
        LeanTween.alphaCanvas (menuCanvasGroup, 0f, 2f);
        logotipo.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovStart : MonoBehaviour
{
    GameObject menuCanvas;
    float speed = 5f;
    CanvasGroup menuCanvasGroup;
    Vector3 newCameraPos, cameraPos, newCameraRot, cameraRot;
    public bool animate = false;

    private void Awake () {
        newCameraPos = GameObject.Find ("GameCameraPos").transform.position;
        newCameraRot = GameObject.Find ("GameCameraPos").transform.rotation.eulerAngles;
        menuCanvas = GameObject.Find ("Canvas");
        menuCanvasGroup = menuCanvas.GetComponent<CanvasGroup> ();
    }
    private void Update () {

        cameraPos = transform.position;
        cameraRot = transform.rotation.eulerAngles;

        if (animate == true && cameraPos != newCameraPos && cameraRot != newCameraRot) {
            transform.position = Vector3.MoveTowards(cameraPos, newCameraPos, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.MoveTowards (cameraRot, newCameraRot, speed * Time.deltaTime));
        } else {
            animate = false;
            menuCanvas.GetComponent <GraphicRaycaster>().enabled = false;
            LeanTween.alphaCanvas (menuCanvasGroup, 0f, 1f);
        }
    }


}

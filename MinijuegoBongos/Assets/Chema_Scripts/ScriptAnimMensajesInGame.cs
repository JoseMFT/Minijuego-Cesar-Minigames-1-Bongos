using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAnimMensajesInGame : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 ogPos, ogScale;
    public bool activarAnim = false;

    void Awake ()
    {
        ogPos = gameObject.transform.localPosition;
        ogScale = gameObject.transform.localScale;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (activarAnim == true)
        {
            Aparecer();
        }
    }

    void Aparecer ()
    {
        activarAnim = false;
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0f, 0f).setEaseOutQuad();
        gameObject.transform.localPosition = ogPos;
        gameObject.transform.localScale = ogScale;
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1f, .65f).setEaseOutQuad();
        LeanTween.scale(gameObject, ogScale * 1.3f, 1f).setEaseOutCubic();
        LeanTween.moveLocal(gameObject, new Vector3(ogPos.x, ogPos.y + 1f, ogPos.z), 1f).setEaseOutCubic().setOnComplete(()=> {
            gameObject.SetActive(false);
        });
    }
}

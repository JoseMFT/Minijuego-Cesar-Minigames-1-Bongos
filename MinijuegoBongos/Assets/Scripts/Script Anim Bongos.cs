using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScriptAnimBongos : MonoBehaviour
{
    public  Animator anim;
    public AudioSource sonidoBongoL, sonidoBongoR, esteSonido;
    GameObject menuOpciones, canvasOpciones, esteFX;
    public KeyCode BongoL, BongoR, otraTecla;
    public float tiempoEspera = .05f;
    const float ktiempoEsperaReferencia = .05f;
    string animBongoL = "Tocar BongoL", animBongoR = "Tocar BongoR", estaAnim;
    public GameObject ShockWaveL, shockWaveR;
    bool esperar = false;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        menuOpciones = GameObject.Find("Fondo Opciones");
        canvasOpciones = GameObject.Find("CanvasOpciones");
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasOpciones.GetComponent<GraphicRaycaster>().enabled == false && LeanTween.isTweening(menuOpciones) == false)  {
            
            if (esperar == true) {

                if (tiempoEspera > 0f) {
                    tiempoEspera -= Time.deltaTime;

                    if (Input.GetKey (otraTecla)) {
                        ShockWaveL.SetActive(true);
                        shockWaveR.SetActive(true);
                        anim.Play ("TocarBongos");
                        tiempoEspera = ktiempoEsperaReferencia;
                        sonidoBongoR.Play();
                        sonidoBongoL.Play();
                        esperar = false;
                    }

                } else {
                    esteFX.SetActive(true);
                    anim.Play (estaAnim);
                    esteSonido.Play ();
                    tiempoEspera = ktiempoEsperaReferencia;
                    esperar = false;
                }
            }

            if (Input.GetKeyDown (BongoL)) {
                esteFX = ShockWaveL;
                otraTecla = BongoR;
                estaAnim = animBongoL;
                esteSonido = sonidoBongoL;
                esperar = true;
            
            } else if (Input.GetKeyDown (BongoR)) {
                esteFX = shockWaveR;
                otraTecla = BongoL;
                estaAnim = animBongoR;
                esteSonido = sonidoBongoR;
                esperar = true;
            }        
        }
    }
}

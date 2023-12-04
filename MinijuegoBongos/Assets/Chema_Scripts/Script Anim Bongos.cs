using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ScriptAnimBongos : MonoBehaviour
{
    public  Animator anim;
    public AudioSource sonidoBongoL, sonidoBongoR, esteSonido;
    GameObject menuOpciones, canvasOpciones, esteFX;
    KeyCode BongoL1, BongoL2, BongoR1, BongoR2, otraTecla1, otraTecla2;
    public float tiempoEspera = .05f;
    const float ktiempoEsperaReferencia = .05f;
    string animBongoL = "Tocar BongoL", animBongoR = "Tocar BongoR", estaAnim;
    public GameObject ShockWaveL, shockWaveR, camara;
    bool esperar = false;
    
    // Using UnityEngine.InputSystem;
    //InputAction.CallbackContext.callback => es un booleano 
    //void PulsoXBoton (ImnputAction.CallbackContext.callback) { Hace patata; } 
    // callback.started => Cuando empeiza el callback (booleano)
    // callback.canceled => Cuando termina el callback (booleano)
    // no tenemos para detectar si el boton se mantiene pulsado, pero si que podemnos detectarlo mediante un booleano que cambie al pulsar y soltar el boton

    void Awake()
    {
        anim = GetComponent<Animator>();
        menuOpciones = GameObject.Find("Fondo Opciones");
        canvasOpciones = GameObject.Find("CanvasOpciones");
        BongoL1 = KeyCode.Mouse0;
        BongoL2 = KeyCode.JoystickButton4;
        BongoR1 = KeyCode.Mouse1;
        BongoR2 = KeyCode.JoystickButton5;
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasOpciones.GetComponent<GraphicRaycaster>().enabled == false && LeanTween.isTweening(menuOpciones) == false)  {
            
            if (esperar == true) {

                if (tiempoEspera > 0f) {
                    tiempoEspera -= Time.deltaTime;

                    if (Input.GetKey (otraTecla1) || Input.GetKey(otraTecla2)) {
                        camara.GetComponent<ControladorTiempoInput>().puedeTocarBongos = false;
                        ShockWaveL.SetActive(true);
                        shockWaveR.SetActive(true);
                        anim.Play ("TocarBongos");
                        tiempoEspera = ktiempoEsperaReferencia;
                        sonidoBongoR.Play();
                        sonidoBongoL.Play();
                        esperar = false;
                    }

                } else {                    
                    camara.GetComponent<ControladorTiempoInput>().puedeTocarBongos = false;
                    esteFX.SetActive(true);
                    anim.Play (estaAnim);
                    esteSonido.Play ();
                    tiempoEspera = ktiempoEsperaReferencia;
                    esperar = false;
                }
            }

            if (Input.GetKeyDown (BongoL1) || Input.GetKeyDown(BongoL2)) {
                esteFX = ShockWaveL;
                otraTecla1 = BongoR1;
                otraTecla2 = BongoR2;
                estaAnim = animBongoL;
                esteSonido = sonidoBongoL;
                esperar = true;
            
            } else if (Input.GetKeyDown (BongoR1) || Input.GetKeyDown(BongoR2)) {
                esteFX = shockWaveR;
                otraTecla1 = BongoL1;
                otraTecla2 = BongoL2;
                estaAnim = animBongoR;
                esteSonido = sonidoBongoR;
                esperar = true;
            }        
        }
    }
}

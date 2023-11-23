using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAnimBongos : MonoBehaviour
{
    public  Animator anim;
    public AudioSource sonidoBongoL, sonidoBongoR, esteSonido;
    public GameObject menuOpciones;
    public KeyCode BongoL, BongoR, otraTecla;
    public float tiempoEspera = .05f;
    const float ktiempoEsperaReferencia = .05f;
    string animBongoL = "Tocar BongoL", animBongoR = "Tocar BongoR", estaAnim;
    bool esperar = false;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        menuOpciones = GameObject.Find("Fondo Opciones");
    }

    // Update is called once per frame
    void Update()
    {
        if (menuOpciones.transform.localPosition.y == 1100f || LeanTween.isTweening(menuOpciones))  {
            if (esperar == true) {

                if (tiempoEspera > 0f) {
                    tiempoEspera -= Time.deltaTime;

                    if (Input.GetKey (otraTecla)) {
                        anim.Play ("TocarBongos");
                        tiempoEspera = ktiempoEsperaReferencia;
                        sonidoBongoR.Play();
                        sonidoBongoL.Play();
                        esperar = false;
                    }

                } else {
                    anim.Play (estaAnim);
                    esteSonido.Play ();
                    tiempoEspera = ktiempoEsperaReferencia;
                    esperar = false;
                }
            }

            if (Input.GetKeyDown (BongoL)) {
                otraTecla = BongoR;
                estaAnim = animBongoL;
                esteSonido = sonidoBongoL;
                esperar = true;
            
            } else if (Input.GetKeyDown (BongoR)) {
                otraTecla = BongoL;
                estaAnim = animBongoR;
                esteSonido = sonidoBongoR;
                esperar = true;
            }        
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAnimBongos : MonoBehaviour
{
    public  Animator anim;
    public KeyCode BongoL, BongoR, otraTecla;
    public float tiempoEspera = .05f;
    const float ktiempoEsperaReferencia = .05f;
    string animBongoL = "Tocar BongoL", animBongoR = "Tocar BongoR", estaAnim;
    bool esperar = false;
    void Start()
    {
        anim = GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {

        if (esperar == true) {

            if (tiempoEspera > 0f) {
                tiempoEspera -= Time.deltaTime;

                if (Input.GetKey (otraTecla)) {
                    anim.Play ("TocarBongos");
                    tiempoEspera = ktiempoEsperaReferencia;
                    esperar = false;
                }

            } else {
                anim.Play (estaAnim);
                tiempoEspera = ktiempoEsperaReferencia;
                esperar = false;
            }
        }

        if (Input.GetKeyDown (BongoL)) {
            otraTecla = BongoR;
            estaAnim = animBongoL;
            esperar = true;
            
        } else if (Input.GetKeyDown (BongoR)) {
            otraTecla = BongoL;
            estaAnim = animBongoR;
            esperar = true;
        }        
    }
}

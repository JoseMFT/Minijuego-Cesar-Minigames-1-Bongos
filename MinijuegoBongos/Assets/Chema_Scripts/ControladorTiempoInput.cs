using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTiempoInput : MonoBehaviour
{
    public bool puedeTocarBongos = true;
    float tiempoDelay, refTiempoDelay, velocidadDelJuego;


    private void Awake () {
        velocidadDelJuego = GameObject.Find("GameManager").GetComponent<GameManager>().velocidadJuego;
        refTiempoDelay = (1f / velocidadDelJuego) / (3f * velocidadDelJuego);
        UnityEngine.Debug.Log("El tiempo de espera para el input es: " + refTiempoDelay.ToString());
        tiempoDelay = refTiempoDelay;
        
    }
    void Start ()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (puedeTocarBongos != true) {
            if (tiempoDelay > 0f) {
                tiempoDelay -= Time.deltaTime;
            } else {
                puedeTocarBongos = true;
                tiempoDelay = refTiempoDelay;
            }
        }
    }
}

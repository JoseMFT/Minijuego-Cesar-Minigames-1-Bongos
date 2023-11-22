using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


public class GameManager : MonoBehaviour
{
    public float dificultadJuego = 1, volumenJuego = 100;
    public AudioMixer mezclador;
    public Slider barraVolumen;
    public TextMeshProUGUI textoVolumen;
    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start () {
        DetectarSonido();        
    }

    // Update is called once per frame
    void Update()
    {
    //El Audio Source desaparecerá al cambiar la escena, por lo que necesitamos tomar el otro
        if (barraVolumen == null) {
            DetectarSonido();
        }        
    }

    public void DetectarSonido () {
        barraVolumen = GameObject.Find("Volumen Barra").GetComponent<Slider>();
        volumenJuego = barraVolumen.value;
        //mezclador.SetFloat("Volumen", -25f - 25f * ((100f - volumenJuego) / 100f));
        textoVolumen = GameObject.Find("Volumen").GetComponent<TextMeshProUGUI>();

        /*barraVolumen.onValueChanged.AddListener(delegate {
            CambiarVolumen();
        });*/
    }

    public void CambiarVolumen () {
        volumenJuego = barraVolumen.value;
        mezclador.SetFloat("Volumen", -25f - 35f * ((100f- volumenJuego) / 100f));
        if (volumenJuego == 0f) {
            mezclador.SetFloat("Volumen", -100f);
        }
        textoVolumen.text = volumenJuego.ToString();
    }
}

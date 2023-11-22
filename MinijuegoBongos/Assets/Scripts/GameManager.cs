using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


public class GameManager : MonoBehaviour
{
    public float dificultadJuego = 1, volumenJuegoMaestro = 100f, volumenJuegoMusica = 100f, volumenJuegoFX = 100f;
    public AudioMixer mezclador;
    public Slider barraVolumenMaestro, barraVolumenMusica, barraVolumenFX;
    public TextMeshProUGUI textoVolumenMaestro, textoVolumenMusica, textoVolumenFX;
    public GameObject menuOpciones;
    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(menuOpciones);
        DetectarSonido();
    }

    private void Start () {
        //DetectarSonido();        
    }

    // Update is called once per frame
    void Update()
    {
    //El Audio Source desaparecerá al cambiar la escena, por lo que necesitamos tomar el otro
        if (barraVolumenMaestro == null) {
            DetectarSonido();
        }        
    }

    public void DetectarSonido () {
        volumenJuegoMaestro = barraVolumenMaestro.value;
        volumenJuegoMusica = barraVolumenMusica.value;
        volumenJuegoFX = barraVolumenFX.value;
        mezclador.SetFloat("VolumenMaestro", 0f - 35f * ((100f - volumenJuegoMaestro) / 100f));
        mezclador.SetFloat("VolumenMusica", 0f - 35f * ((100f - volumenJuegoMusica) / 100f));
        mezclador.SetFloat("VolumenFX", 0f - 35f * ((100f - volumenJuegoFX) / 100f));
        menuOpciones.SetActive(false);
    }

    public void CambiarVolumen () {
        volumenJuegoMaestro = barraVolumenMaestro.value;
        volumenJuegoMusica = barraVolumenMusica.value;
        volumenJuegoFX = barraVolumenFX.value;
        mezclador.SetFloat("VolumenMaestro", 0f - 35f * ((100f- volumenJuegoMaestro) / 100f));
        mezclador.SetFloat("VolumenMusica", 0f - 35f * ((100f- volumenJuegoMusica) / 100f));
        mezclador.SetFloat("VolumenFX", 0f - 35f * ((100f- volumenJuegoFX) / 100f));
        if (volumenJuegoMaestro == 0f) {
            mezclador.SetFloat("VolumenMaestro", -100f);
        } else if (volumenJuegoMusica == 0f)
        {
            mezclador.SetFloat("VolumenMusica", -100f);
        
        } else if (volumenJuegoFX == 0f)
        {
            mezclador.SetFloat("VolumenFX", -100f);
        
        }
        textoVolumenMaestro.text = volumenJuegoMaestro.ToString();
        textoVolumenMusica.text = volumenJuegoMusica.ToString();
        textoVolumenFX.text = volumenJuegoFX.ToString();
    }

    public void CambiarDificultad (float nuevaDificultad)
    {
        dificultadJuego = nuevaDificultad;
    }
}

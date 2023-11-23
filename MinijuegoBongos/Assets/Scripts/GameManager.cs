using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


public class GameManager : MonoBehaviour
{
    public float dificultadJuego = 1, volumenJuegoMaestro = 100f, volumenJuegoMusica = 100f, volumenJuegoFX = 100f;
    List <GameObject> arrayOriginales;
    public int cargasDeEscena = 0;
    public AudioMixer mezclador;
    public Slider barraVolumenMaestro, barraVolumenMusica, barraVolumenFX;
    public TextMeshProUGUI textoVolumenMaestro, textoVolumenMusica, textoVolumenFX;
    public GameObject menuOpciones, canvasOpciones, botonMenuOpciones, botonSalirOpciones, posicionDeCamara;
    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(canvasOpciones);
        DontDestroyOnLoad(posicionDeCamara);
        DetectarSonido();
    }

    private void Start () {
        arrayOriginales.Add(posicionDeCamara);
        arrayOriginales.Add(canvasOpciones);
        //DetectarSonido();        
    }

    // Update is called once per frame
    void Update()
    {
    //El Audio Source desaparecerá al cambiar la escena, por lo que necesitamos tomar el otro
        if (barraVolumenMaestro == null) {
            DetectarSonido();
        }

        if (SceneManager.GetActiveScene().name == "EscenaPrincipal" && botonMenuOpciones.activeSelf == false)
        {
            botonSalirOpciones.SetActive(true);
            botonMenuOpciones.SetActive(true);
        } else if (SceneManager.GetActiveScene().name == "Menu" && botonMenuOpciones.activeSelf == true)
        {
            botonSalirOpciones.SetActive(false);
            botonMenuOpciones.SetActive(false);
        }
        if (cargasDeEscena != 0)
        {
            BorrarDuplicados();
            cargasDeEscena = 0;
        }
    }

    public void DetectarSonido () {
        volumenJuegoMaestro = barraVolumenMaestro.value;
        volumenJuegoMusica = barraVolumenMusica.value;
        volumenJuegoFX = barraVolumenFX.value;
        mezclador.SetFloat("VolumenMaestro", 0f - 35f * ((100f - volumenJuegoMaestro) / 100f));
        mezclador.SetFloat("VolumenMusica", 0f - 35f * ((100f - volumenJuegoMusica) / 100f));
        mezclador.SetFloat("VolumenFX", 0f - 35f * ((100f - volumenJuegoFX) / 100f));
        menuOpciones.transform.localPosition = new Vector3 (0f, 1100f, 0f);
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

        } else if (volumenJuegoMusica == 0f) {
            mezclador.SetFloat("VolumenMusica", -100f);
        
        } else if (volumenJuegoFX == 0f) {
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
    public void VolverMenu ()
    {
        botonMenuOpciones.SetActive(false);
        botonSalirOpciones.SetActive(false);
        cargasDeEscena++;
        SceneManager.LoadScene("Menu");
    }

    public void BorrarDuplicados ()
    {
        GameObject [] arrayDuplicados = SceneManager.GetSceneByName("Menu").GetRootGameObjects();
        int indiceArray = 0;
        while (indiceArray < arrayOriginales.ToArray().Length) {

            foreach (GameObject objDuplicado in arrayDuplicados) {

                if (objDuplicado.name == arrayOriginales.ToArray()[indiceArray].name) {
                    Destroy(objDuplicado);
                }
            }

            indiceArray++;
        }

        indiceArray = 0;
    }
}

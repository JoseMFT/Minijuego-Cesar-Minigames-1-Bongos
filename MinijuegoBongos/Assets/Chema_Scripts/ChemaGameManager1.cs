using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ChemaGameManager1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidadJuego = 1, volumenJuegoMaestro, volumenJuegoMusica, volumenJuegoFX;
    public int creadosPorEscena = 0, dificultad = 0;
    Slider sliderPuntos;
    public AudioMixer mezclador;
    public Slider barraVolumenMaestro, barraVolumenMusica, barraVolumenFX;
    public TextMeshProUGUI textoVolumenMaestro, textoVolumenMusica, textoVolumenFX;
    public GameObject menuOpciones, canvasOpciones, botonMenuOpciones, botonSalirOpciones, posicionDeCamara, camara, opcionesDesplegadas;
    GameObject [] objNuevos;
    public GameObject [] canciones;
    bool generoFinal = false;
    public bool derrotaOVictoria = false; // false es derrota, true es victoria; 

    // Start is called before the first frame update

    void Awake () {
        //volumenJuegoMaestro = (volMast / 35f * 100f + 100);
        //UnityEngine.Debug.Log("Valor del volMast recibido: " + volMast.ToString() + ", valor del volMast del Juego: " + volumenJuegoMaestro.ToString());

        objNuevos = SceneManager.GetSceneByName("Menu").GetRootGameObjects();
        DontDestroyOnLoad(canvasOpciones);
        DontDestroyOnLoad(posicionDeCamara);
        BorrarDuplicados();
        DetectarSonido();
    }

    private void Start () {
        /*foreach (GameObject objOriginal in objNuevos)
        {
            UnityEngine.Debug.Log(objOriginal);
        }*/
    }

    // Update is called once per frame
    void Update () {
        //El Audio Source desaparecerá al cambiar la escena, por lo que necesitamos tomar el otro
        if (barraVolumenMaestro == null) {
            DetectarSonido();
        }

        if (SceneManager.GetActiveScene().name == "EscenaPrincipal") {
            if (botonMenuOpciones.activeSelf == false) {
                botonSalirOpciones.SetActive(true);
                botonMenuOpciones.SetActive(true);
            }

            if (sliderPuntos == null) {
                sliderPuntos = GameObject.Find("Puntaje").GetComponent<Slider>();
            } else {
                if (sliderPuntos.value < 1f) {
                    if (canciones [dificultad].GetComponent<AudioSource>().isPlaying == false && canciones [dificultad].activeSelf == true && (opcionesDesplegadas.activeSelf == false)) {
                        if (generoFinal == false) {
                            generoFinal = true;
                            derrotaOVictoria = false;
                            FinalDelJuego();
                        }
                    }
                } else {
                    if (generoFinal == false) {
                        generoFinal = true;
                        derrotaOVictoria = true;
                        FinalDelJuego();
                    }
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Menu" && botonMenuOpciones.activeSelf == true) {
            botonSalirOpciones.SetActive(false);
            botonMenuOpciones.SetActive(false);
        }
    }

    public void DetectarSonido () {
        mezclador.ClearFloat("VolumenMaestro");
        mezclador.GetFloat("VolumenMaestro", out volumenJuegoMaestro);
        volumenJuegoMaestro = (volumenJuegoMaestro / 35f * 100f + 100);
        UnityEngine.Debug.Log("Vol maestro: " + volumenJuegoMaestro.ToString());
        barraVolumenMaestro.value = volumenJuegoMaestro;

        mezclador.ClearFloat("VolumenMusica");
        mezclador.GetFloat("VolumenMusica", out volumenJuegoMusica);
        volumenJuegoMusica = (volumenJuegoMusica / 35f * 100f + 100);
        UnityEngine.Debug.Log("Vol musica: " + volumenJuegoMusica.ToString());
        barraVolumenMusica.value = volumenJuegoMusica;

        mezclador.ClearFloat("VolumenFX");
        mezclador.GetFloat("VolumenFX", out volumenJuegoFX);
        volumenJuegoFX = (volumenJuegoFX / 35f * 100f + 100);
        UnityEngine.Debug.Log("Vol FX: " + volumenJuegoFX.ToString());
        barraVolumenFX.value = volumenJuegoFX;
    }

    public void CambiarVolumen () {
        volumenJuegoMaestro = barraVolumenMaestro.value;
        volumenJuegoMusica = barraVolumenMusica.value;
        volumenJuegoFX = barraVolumenFX.value;
        mezclador.SetFloat("VolumenMaestro", 0f - 35f * ((100f - volumenJuegoMaestro) / 100f));
        mezclador.SetFloat("VolumenMusica", 0f - 35f * ((100f - volumenJuegoMusica) / 100f));
        mezclador.SetFloat("VolumenFX", 0f - 35f * ((100f - volumenJuegoFX) / 100f));

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

    public void CambiarDificultad (int nuevaDificultad) {
        dificultad = nuevaDificultad;
        if (dificultad == 0) {
            velocidadJuego = 1f;

        } else if (dificultad == 1) {
            velocidadJuego = 1.25f;

        } else if (dificultad == 2) {
            velocidadJuego = 1.5f;
        }
    }
    public void VolverMenu () {
        botonMenuOpciones.SetActive(false);
        botonSalirOpciones.SetActive(false);
        menuOpciones.transform.localPosition = new Vector3(0f, 1100f, 0f);
        SceneManager.LoadScene("Menu");
        mezclador.SetFloat("VelocidadMusica", 1f);
        Destroy(posicionDeCamara);
        Destroy(canvasOpciones);
    }

    public void BorrarDuplicados () {
        GameObject objOriginales;
        foreach (GameObject objCreados in objNuevos) {
            objOriginales = GameObject.Find(objCreados.name);

            if (objOriginales != objCreados && objOriginales.name == objCreados.name && objOriginales != null) {
                Destroy(objOriginales);
            }
        }
    }

    public void FinalDelJuego () {
        if (SceneManager.GetActiveScene().name != "Final") {
            foreach (GameObject cancionSonando in canciones) {
                if (cancionSonando.activeSelf == true) {
                    Destroy(cancionSonando);

                }
            }

            float x = 1f;
            LeanTween.value(x, 0f, 1.5f).setOnComplete(() => {
                SceneManager.LoadScene("Final");
                canvasOpciones.SetActive(false);
            });
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public int spawnID = 0;
    public Slider puntos;
    public float tempo = 1f, defaultTempo = 1f;
    public string gemasTagUp, gemasTagDown;
    public GameObject[] poolUp, poolDown;
    GameObject gameManager, opcionesDesplegadas;
    bool creado = false;

    void Awake ()
    {
        gameManager = GameObject.Find("GameManager");
        opcionesDesplegadas = gameManager.GetComponent<GameManager>().opcionesDesplegadas;
        defaultTempo = 1f / (gameManager.GetComponent<GameManager>().velocidadJuego * 2);
        tempo = defaultTempo;
    }

    void Start()
    {
        poolUp = GameObject.FindGameObjectsWithTag(gemasTagUp);
        poolDown = GameObject.FindGameObjectsWithTag(gemasTagDown);
        foreach (GameObject desactivar in poolUp)
        {
            desactivar.SetActive(false);
        }
        foreach (GameObject desactivar in poolDown)
        {
            desactivar.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (opcionesDesplegadas.activeSelf == false)
        {
            if (puntos.value < 1f) {
                if (tempo >= 0) {
                    tempo -= Time.deltaTime;
                } else {
                    int row = (Mathf.FloorToInt(Random.Range(1.01f, 5.99f)));
                    if (row == 1 || row == 2)
                    {
                        Creator(poolUp, false);
                    }
                    else if (row == 3 || row == 4)
                    {
                        Creator(poolDown, false);

                    } else if (row >= 5)
                    {
                        Creator(poolUp, true);
                        creado = false;
                        Creator(poolDown, true);
                    }

                    ResetTimer();
                    creado = false;
                }
            } else {
                gameObject.GetComponent<Spawner> ().enabled = false;
            }
        }
    }

    public void ResetTimer ()
    {
        tempo = defaultTempo;
    }

    public void Creator (GameObject[] myObj, bool tieneParalela)
    {
        foreach (GameObject gema in myObj)
        {
            if (creado == false && gema.activeSelf == false)
            {
                gema.SetActive(true);
                gema.GetComponent<GemasBehavior>().Reset();
                gema.GetComponent<GemasBehavior> ().gemaParalela = tieneParalela;
                creado = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Slider puntos;
    public float tempo = .75f, defaultTempo = .75f;
    public string gemasTagUp, gemasTagDown;
    public GameObject[] poolUp, poolDown;
    bool creado = false;

    void Awake ()
    {       
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
        if (puntos.value < 1f) {
            if (tempo >= 0) {
                tempo -= Time.deltaTime;
            } else {
                int row = (Mathf.FloorToInt(Random.Range(1.01f, 5.01f)));
                if (row == 1 || row == 2)
                {
                    Creator(poolUp);
                }
                else if (row == 3 || row == 4)
                {
                    Creator(poolDown);

                } else if (row == 5)
                {
                    Creator(poolUp);
                    creado = false;
                    Creator(poolDown);
                }

                ResetTimer();
                creado = false;
            }
        }
    }

    public void ResetTimer ()
    {
        tempo = defaultTempo;
    }

    public void Creator (GameObject[] myObj)
    {
        foreach (GameObject gema in myObj)
        {
            if (creado == false && gema.activeSelf == false)
            {
                Debug.Log("Acabo de crear una gema de color " + gema.tag);
                gema.SetActive(true);
                gema.GetComponent<GemasBehavior>().Reset();
                creado = true;
            }
        }
    }
}

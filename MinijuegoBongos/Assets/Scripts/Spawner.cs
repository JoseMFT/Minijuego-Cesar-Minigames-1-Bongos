using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public int spawnID = 0;
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
                int row = (Mathf.FloorToInt(Random.Range(1.01f, 5.99f)));
                if (row == 1 || row == 2)
                {
                    spawnID++;
                    Creator(poolUp, spawnID);
                }
                else if (row == 3 || row == 4)
                {
                    spawnID++;
                    Creator(poolDown, spawnID);

                } else if (row >= 5)
                {
                    spawnID++;
                    Creator(poolUp, spawnID);
                    creado = false;
                    Creator(poolDown, spawnID);
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

    public void Creator (GameObject[] myObj, int identificacion)
    {
        foreach (GameObject gema in myObj)
        {
            if (creado == false && gema.activeSelf == false)
            {
                gema.SetActive(true);
                gema.GetComponent<GemasBehavior>().Reset();
                gema.GetComponent<GemasBehavior> ().gemaID = identificacion;
                creado = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Slider puntos;
    public string gemasTag;
    public GameObject[] pool;
    float timer;
    bool creado = false;

    void Awake ()
    {
        timer = Random.Range(.5f, 1.5f);        
    }
    void Start()
    {
        pool = GameObject.FindGameObjectsWithTag(gemasTag);
        foreach(GameObject desactivar in pool)
        {
            desactivar.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (puntos.value < 1f) {
            if (timer >= 0) {
                timer -= Time.deltaTime;
            } else {
                foreach (GameObject gema in pool) {
                    if (creado == false && gema.activeSelf == false) {
                        gema.SetActive(true);
                        gema.GetComponent<GemasBehavior>().Reset();
                        creado = true;
                    }
                }

                ResetTimer();
                creado = false;
            }
        }
    }

    public void ResetTimer ()
    {
        timer = Random.Range(0.5f, 5f);
    }
}

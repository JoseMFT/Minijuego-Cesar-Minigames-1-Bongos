using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject gema;
    public Slider puntos;
    GameObject creado;
    float timer;

    void Awake ()
    {
        timer = Random.Range(.5f, 1.5f);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puntos.value < 1f)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                creado = Instantiate(gema, gameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 359.99f)));
                creado.transform.SetParent(gameObject.transform);
                ResetTimer();
            }
        }
    }

    public void ResetTimer ()
    {
        timer = Random.Range(0.5f, 5f);
    }
}

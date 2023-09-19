using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject gema;
    GameObject creado;
    float timer;

    void Awake ()
    {
        ResetTimer();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            creado = Instantiate(gema, gameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 359.99f)));
            creado.transform.parent = this.transform;
            ResetTimer();
        }
    }

    public void ResetTimer ()
    {
        timer = Random.Range(0.5f, 5f);
    }
}

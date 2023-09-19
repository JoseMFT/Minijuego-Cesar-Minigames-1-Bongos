using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemasBehavior : MonoBehaviour
{
    float speed = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 1f) * speed * Time.deltaTime;
        transform.position = transform.position - new Vector3(speed * 5f, 0f, 0f) * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeanTween.alpha(gameObject, .25f, 1f).setEaseOutQuad();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAnimBongos : MonoBehaviour
{
    Animator anim;
    public KeyCode BongoL, BongoR;
    void Start()
    {
        anim = GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(BongoL) || Input.GetKeyDown(BongoR))
        {
            anim.Play ("Tocar Bongos Ambos");

        }
    }
}

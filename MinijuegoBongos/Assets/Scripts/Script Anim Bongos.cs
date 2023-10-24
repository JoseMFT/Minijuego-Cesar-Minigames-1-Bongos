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
        if (Input.GetKey(BongoL))
        {
            anim.Play("Tocar Bongos L");
        } else if (Input.GetKey(BongoR))
        {
            anim.Play("Tocar Bongos R");
        } else if (Input.GetKey(BongoL) && Input.GetKey(BongoR))
        {
            anim.Play("Tocar Ambos Bongos");
        }
    }
}

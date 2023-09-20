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
        anim.Stop();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(BongoL))
        {
            anim.Stop();
            anim.Play("Tocar Bongos L");
        } else if (Input.GetKey(BongoR))
        {
            anim.Stop();
            anim.Play("Tocar Bongos R");
        }
    }
}

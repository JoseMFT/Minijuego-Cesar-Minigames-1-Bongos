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
        if (Input.GetKeyDown(BongoL))
        {
            anim.SetTrigger("BongoL");


        } else if (Input.GetKeyDown(BongoR))
        {
            anim.SetTrigger("BongoR");
        }
    }
}

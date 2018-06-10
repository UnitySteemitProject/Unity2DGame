using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetInteger("State", 0);

    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.W))
            anim.SetInteger("State", 1);
        else if(Input.GetKeyUp(KeyCode.W))
            anim.SetInteger("State", 0);

        if (Input.GetKey(KeyCode.I))
            anim.SetInteger("State", 0);
        else if (Input.GetKey(KeyCode.R))
            anim.SetInteger("State", 2);
    }
}

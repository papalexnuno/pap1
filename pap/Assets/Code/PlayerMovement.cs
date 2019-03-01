using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //associa ao ficheiro CharacterController2D 
    public CharacterController2D controller;

    //valor da velocidade do player
    public float RunSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool run = false;

	void Update () {
        //cria o movimento na horizontal
        horizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;
        //se a tecla associada ao salto for premida o player salta
        if (Input.GetButtonDown("Jump")){
            jump = true;
        }
	}
    //volta a desativar o salto até a tecla ser premida novamente
    void FixedUpdate (){
        controller.Move(horizontalMove * Time.fixedDeltaTime, run, jump);
        jump = false;
    }
}

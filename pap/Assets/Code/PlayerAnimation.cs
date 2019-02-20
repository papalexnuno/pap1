using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour{

    //cria variavel animator
    private Animator animator;

    //liga o componente Animator ao ficheiro de codigo
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //se premir nas Teclas A ou D executa a animaçao de correr ou para a esquerda ou para a direita
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsRunning", true);
        }
        
        //se nao estiver a premir as teclas A ou D a animaçao para
        else
        {
            animator.SetBool("IsRunning", false);
        }

        //se clicar na tecla W inicia a animaçao de saltar
        if (Input.GetKeyDown(KeyCode.W)){
            animator.SetTrigger("IsJumping");
        }
    }
}
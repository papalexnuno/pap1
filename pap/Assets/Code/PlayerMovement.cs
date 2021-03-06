﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    public int health = 100;

    public GameObject deathEffect;

    public Image HealthBar;

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

    //vida do player
    [System.Serializable]
    public class PlayerStats
    {
        public int Health = 100;
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;

        HealthBar.fillAmount = health / 100f;


        if (health <= 0)
        {
            Die();
            SceneManager.LoadScene(3);
        }
        
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

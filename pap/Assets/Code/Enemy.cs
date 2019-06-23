using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private float Distance;
    public float velocidade;
    public bool facingRight = true;
    public float stoppingDistance;
    private float AttackTime;
    private Transform target;
    public int health = 100;
    public GameObject deathEffect;
    public int damage = 30;
    public int attackdelay = 2;
    public float attackrange = 0.9f;
    public int scoreValue = 13;

    void Start()
    {
        AttackTime = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void Update()
    {
        Distance = Vector2.Distance(target.position, transform.position);
        if (Distance < attackrange)
        {
            attack();
        }   

        //Faz o inimigo parar quando esta a determinada distancia do player
        if (Vector2.Distance(transform.position, target.position) > 0.8)
        {
            //Move o inimigo da sua posiçao para a posiçao do player a uma determinada velocidade
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocidade * Time.deltaTime);
            if (target.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
                Flip();
            if (target.position.x < transform.position.x && facingRight)
                Flip();
        }

    }

    private void attack()
    {
        if (Time.time > AttackTime)
        {
            target.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Debug.Log("Enemy is attacking");
            AttackTime = Time.time + attackdelay;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
        ScoreManager.score += scoreValue;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private float Distance;
    public float velocidade;
    public float stoppingDistance;
    private float AttackTime;
    private Transform target;
    public int health = 100;
    public GameObject deathEffect;
    public int damage = 30;
    public int attackdelay = 2;
    public float attackrange = 0.9f;

    void Start()
    {
        AttackTime = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
    }
}
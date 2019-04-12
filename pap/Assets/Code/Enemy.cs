using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidade;
    public float stoppingDistance;
    private Transform target;

    public int health = 100;
    public GameObject deathEffect;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //Faz o inimigo parar quando esta a determinada distancia do player
        if (Vector2.Distance(transform.position, target.position) > 0.8)
        {
            //Move o inimigo da sua posiçao para a posiçao do player a uma determinada velocidade
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocidade * Time.deltaTime);
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
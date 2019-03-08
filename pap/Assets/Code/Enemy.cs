using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidade;
    public float stoppingDistance;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //Faz o inimigo parar quando esta a determinada distancia do player
        if (Vector2.Distance(transform.position, target.position) >0.8 ){
            //Move o inimigo da sua posiçao para a posiçao do player a uma determinada velocidade
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocidade * Time.deltaTime);
        }
    }
}

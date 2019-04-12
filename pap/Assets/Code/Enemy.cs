using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats {
        public int maxHealth = 100;
        private int _curHealth;
        public int curHealth
        {
            get { return curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth; }
        }

        public int damage = 50;

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public EnemyStats stats = new EnemyStats();

    [Header("Optional: ")]
    [SerializeField]

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

    private void OnCollisionEnter2D(Collision2D_ColInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();
        if (player != null)
        {
            PlayerAnimation.DamagePlayer(stats.damage);
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
        Destroy(gameObject);
    }
}
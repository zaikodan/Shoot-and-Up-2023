using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HUD))]

public class Asteroid : Projectile, IDamageable
{
    [SerializeField] float maxHealth;
    [SerializeField] int score;
    float health;


    HUD hud;
    Animator animator;

    protected override void Start()
    {
        base.Start();

        health = maxHealth;
        hud = GetComponent<HUD>();
        animator = GetComponent<Animator>();

        hud.UpdateHealthBar(health, maxHealth);

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hud.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            GameManager.instance.AddScore(score);
            StartCoroutine(DestroyCoroutine());
        }
    }

    protected override void Destroy()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        GetComponent<CircleCollider2D>().enabled = false; //Desativa o collider
        transform.GetChild(0).gameObject.SetActive(false); //Desativa o barra de vida
        animator.SetTrigger("Explosion");
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Explosion")) 
        {
            yield return null; 
        }
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }


}

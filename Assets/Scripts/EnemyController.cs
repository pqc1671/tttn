using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject damPopUp;

    Health health;
    ColoredFlash flash;
    EnemyAI enemyAI;
    [SerializeField] private GameObject coin;

    private void Start()
    {
        health = GetComponent<Health>();
        flash = GetComponent<ColoredFlash>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void SpawnCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
        Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
        Vector2 randomVector = new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
        rb.AddForce(randomVector,ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (health.isDead)
        {
            SpawnCoin();
        }
    }
    public void TakeDamEffect(int damage)
    {
        if (damPopUp != null)
        {
            GameObject instance = Instantiate(damPopUp, transform.position
                    + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
            Animator animator = instance.GetComponentInChildren<Animator>();
            if (damage <= 10) animator.Play("normal");
            else animator.Play("critical");
        }

        // Flash
        if (flash != null)
        {
            flash.Flash(Color.white);
        }
        // Freeze
        if (enemyAI != null)
        {
            enemyAI.FreezeEnemy();
        }
    }
}

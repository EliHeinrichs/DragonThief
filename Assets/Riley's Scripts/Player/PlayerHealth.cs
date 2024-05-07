using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    public float maxHealth = 5;
    private float currentHealth = 1;
    private float damageCooldownDefault = 0.5f;
    private float damageCooldown = 0f;

    public Image[] hearts;

    void Awake() {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

    }

 

    void Start() {
           
        if ( GameManager . Instance . currentPlayerHealth < currentHealth )
        {
            currentHealth = GameManager . Instance . currentPlayerHealth;
        }

        if(currentHealth > maxHealth )
        {

            currentHealth = maxHealth;
        }
    }

    void Update() {
        UpdateHearts();

        if(damageCooldown > 0) 
        {
            StartDamageCooldown();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(currentHealth <= 0) {
            Die();
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                // Display a full heart for each unit of health
                hearts[i].color = Color.white;
            }
            else
            {
                // Hide the remaining hearts
                hearts[i].color = Color.black;
            }
        }
    }
    public void TakeDamage(float damage) 
    {
        if(damageCooldown > 0) {
            return;
        }

        damageCooldown = damageCooldownDefault;
        animator.SetTrigger("Hurt");

        currentHealth -= damage;

        //Save the current health in game manager
        GameManager.Instance.currentPlayerHealth = currentHealth;
    }

    private void Die() {
        animator.SetTrigger("Dead");
        GameManager . Instance . GoldPileNum = 0;
        GameManager . Instance . Coins = 0;
        GameManager . Instance .level = 1;
        GameManager.Instance.itemList.Clear();
        GameManager . Instance . weaponData = GameManager . Instance . copperSword;
        GameManager.Instance.currentPlayerHealth = maxHealth;
        GameManager.Instance.playerIsDead = true;
    }

    private void StartDamageCooldown() {
        if(damageCooldown <= 0) {
            return;
        }

        damageCooldown -= Time.deltaTime;
    }

}

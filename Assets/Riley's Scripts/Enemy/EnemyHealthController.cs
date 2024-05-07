using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    private Slider healthSlider;
    private Slider easeHealthSlider;
    private Canvas sliderCanvas;

    private SFXAudioManager sfx;

    private Animator animator;
    public float maxHealth = 10;
    private float currentHealth;
    private bool isDead = false;

    private float lerpSpeed = 0.02f;

    void Awake() 
    {
        healthSlider = transform.Find("HealthCanvas/Healthbar").GetComponent<Slider>();
        easeHealthSlider = transform.Find("HealthCanvas/Ease Healthbar").GetComponent<Slider>();
        sliderCanvas = transform.Find("HealthCanvas").GetComponent<Canvas>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        sfx = animator.GetComponent<SFXAudioManager>();
    }

    void Update() 
    {
        if(healthSlider.value != currentHealth) 
        {
            healthSlider.value = currentHealth;
        }

        if(healthSlider.value != easeHealthSlider.value) 
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed);
        }
    }

    public void TakeDamage(float damage) 
    {
        if(isDead) {
            return;
        }

        sfx.PlaySound("DragonDamage");
        
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if(currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        isDead = true;
        GameManager . Instance . Coins += 1;
        animator .SetTrigger("Dead");
        sliderCanvas.enabled = false;
        Destroy(gameObject, 5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private EnemyHealthController enemyHealth;
   

    private SpriteRenderer spriteRenderer;

    public Data_WeaponData Data;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Data.weaponSprite;
        Data = GameManager . Instance . weaponData;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Enemies")) 
        {
           enemyHealth = col.GetComponent<EnemyHealthController>();

           enemyHealth.TakeDamage(Data.attackDamage);
        }
        if(col.CompareTag("Object"))
        {
            GameManager . Instance . Coins += 1;
            Destroy (col . gameObject);
        }
    }
}

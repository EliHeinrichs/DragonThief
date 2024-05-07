using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public Data_EnemyData Data;

    private PlayerHealth player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        if(player == null) {
            Debug.Log("Player Health not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            Debug.Log("Player Hit");
            player.TakeDamage(Data.attackDamage);
        }
    }
}

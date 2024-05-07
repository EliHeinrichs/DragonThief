using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
	public Transform player;
    public Data_EnemyData Data;
	public bool isFlipped = false;
    private SpriteRenderer sprite;
    private Transform fire;
    private Animator animator;

    void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        fire = transform.Find("FireHolder");

    }
    private void Start ( )
    {
        player = GameObject . FindGameObjectWithTag ("Player") . transform;
    }

    public void ActiveCoroutine() {
        StartCoroutine("AttackCooldown");
    }
    
    public IEnumerator AttackCooldown() 
    {
        yield return new WaitForSeconds(Data.attackSpeed);
        animator.SetBool("canAttack", true);
    }

    public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
            fire.Rotate(0f, 180f, 0f);
            fire.Translate(-8f, 0f, 0f);
			sprite.flipX = false;
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
            fire.Rotate(0f, -180f, 0f);
            fire.Translate(-8f, 0f, 0f);
			sprite.flipX = true;
			isFlipped = true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Transform aimTransform;
    private Vector2 inputDirection;

    private bool playerAttacked = false;
    private bool canAttack = true;

    public Data_WeaponData Data;

    private Animator weaponAnimator;

    void Start()
    {
        aimTransform = transform.Find("Aim");
        weaponAnimator = aimTransform.GetComponentInChildren<Animator>(); //works only if there is one animator in children of player.
    }

    void Update()
    {
        if(GameManager.Instance.playerIsDead) {
            return;
        }

        HandleWeaponRotation();
        HandleAttack();
    }

    private void HandleWeaponRotation() 
    {
        Vector2 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 direction = inputDirection - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        aimTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void HandleAttack() {
        if(playerAttacked) 
        {
            playerAttacked = false;
            canAttack = false;

            //activate the weapon animation
            weaponAnimator.SetTrigger("Attacked");

            //Start attack cooldown
            StartCoroutine("AttackCooldown");
        }
    }

    private IEnumerator AttackCooldown() 
    {
        yield return new WaitForSeconds(Data.attackSpeed);
        canAttack = true;
    }

    public void GetInputDirection(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void GetAttackInput(InputAction.CallbackContext context) 
    {
        if(context.performed && canAttack) {
            playerAttacked = true;
        }
    }
}
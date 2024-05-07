using UnityEngine;

[CreateAssetMenu(menuName = "Data/WeaponData")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class Data_WeaponData : ScriptableObject
{
    public float attackSpeed;
    public float attackDamage;

    public Sprite weaponSprite;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class Data_EnemyData : ScriptableObject
{
    public float movementSpeed;
    public float detectRange;
    public float attackSpeed;
    public float attackDamage;
    public float attackRange = 2;
}

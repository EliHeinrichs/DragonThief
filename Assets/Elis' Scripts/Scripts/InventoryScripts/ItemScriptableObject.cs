using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
   
    public Sprite itemSprite;
    public bool isStackable;
    public int sellAmount;

}

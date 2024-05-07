using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;
public class GameManager : MonoBehaviour
{

    public int level;
    public int Coins;
    public int GoldPileNum;

    public float currentPlayerHealth = 5f;
    public bool playerIsDead = false;
    public Data_WeaponData weaponData;
    public Data_WeaponData copperSword;
    public List<Item> itemList;

    public static GameManager Instance { get; private set; }

    // Other variables and functions can be added as needed

    private void Awake ( )
    {
        // Set up the singleton pattern
        if ( Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
    }

   



   
   
}

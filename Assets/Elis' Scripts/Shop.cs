using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;

public class Shop : MonoBehaviour
{

    public Data_WeaponData silverSword;
    public Data_WeaponData GoldSword;
    private void Start ( )
    {
       foreach(Item item in GameManager.Instance.itemList)
       {
            GameManager.Instance.Coins += item . itemScriptableObject . sellAmount;
           
       }
       GameManager.Instance.itemList.Clear();
    }


    public void PurchaseHeal()
    {
        if (GameManager.Instance.Coins >= 100)
        {
            GameManager . Instance . Coins -= 100;
            GameManager . Instance . currentPlayerHealth += 1;
        }

    }


    public void BuySilverDagger()
    {
        if ( GameManager . Instance . Coins >= 800 )
        {
            GameManager . Instance . Coins -= 800;
            GameManager . Instance . weaponData = silverSword;
        }
    }

    public void BuyGoldDagger()
    {
        if ( GameManager . Instance . Coins >= 2000 )
        {
            GameManager . Instance . Coins -= 2000;
            GameManager . Instance . weaponData = GoldSword;
        }
    }

    public void Return()
    {
        SceneManager . LoadScene ("Eli's Scene");
    }

    public void GiveGold( )
    {
       
       GameManager . Instance . GoldPileNum += GameManager . Instance . Coins;
       GameManager . Instance . Coins -= GameManager . Instance . Coins;
        

    }


}

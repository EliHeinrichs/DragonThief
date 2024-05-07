using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . InputSystem;
using UnityEngine . Windows;

public class PlayerInventory : MonoBehaviour
{

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    private PlayerMovement player;
    private int currentIndex =0;
    private float scrollingSpeed = 0.01f;

  

    private void Start ( )
    {
    
        inventory = new Inventory (UseItem);
        inventory . itemList = GameManager . Instance . itemList;


        uiInventory . SetInventory (inventory);
    }



    //Drop the current item referenced, removing it from the inventory, spawning it in the world, and updating the UI
    private void UseItem ( Item item )
    {

        Item duplicateItem = new Item { itemScriptableObject = item.itemScriptableObject, amount = item.amount };
        inventory . RemoveItem (item);
        GameManager . Instance . itemList. Remove(item);
        ItemWorld . DropItem (GetPlayerPosition () , duplicateItem);
        uiInventory . SetSelectedInventoryItem (uiInventory . GetInventorySize ());
    

     } 

    // When walked on, pickup the item if there is space, adding it  to the inventory and updating the UI inventory that shows items
    private void OnTriggerEnter2D ( Collider2D collider )
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if ( itemWorld != null && inventory . itemList . Count < 4 )
        {
            GameManager . Instance . itemList . Add(itemWorld . GetItem ());
            itemWorld . DestroySelf ();

            uiInventory . SetSelectedInventoryItem (uiInventory . GetInventorySize ());
        }
    }

    // when the drop input is pressed and there is a selected item use the current selected item
    public void GetDropInput ( InputAction . CallbackContext context )
    {
        if ( context . performed && uiInventory . GetSelectedItem ().amount >=1)
        {
            UseItem (uiInventory . GetSelectedItem ());

        }
    }

    //When the scroll wheels value changes(is used), depending on the direction move an index up or down
    public void OnScroll ( InputAction . CallbackContext context )
    {
        Vector2 scrollDelta = context.ReadValue<Vector2>();
        float scrollValue = scrollDelta.y;

        // Scroll up action
        if ( scrollValue > 0.1f )
        {
            currentIndex = Mathf . Min (currentIndex + 1 , 3);
            SelectInventoryItemByIndex (currentIndex);
        }
        // Scroll down action
        else if ( scrollValue < -0.1f )
        {
            currentIndex = Mathf . Max (currentIndex - 1 , 0);
            SelectInventoryItemByIndex(currentIndex);
        }
    }



    //if there is items in the inventory, change the SelectedItem to the current index
    private void SelectInventoryItemByIndex ( int index )
    {
        if ( uiInventory != null && index >= 0 && index < uiInventory . GetInventorySize () )
        {
            uiInventory . SetSelectedInventoryItem (index);
        }
    }

    public Vector3 GetPlayerPosition ( )
    {
        return transform . position;
    }

}

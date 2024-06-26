using System . Collections;
using System . Collections . Generic;
using UnityEngine;
using System;
using Unity . VisualScripting;

public class Inventory
{
    public List<Item> itemList;
    public event EventHandler OnItemListChanged;
    private Action<Item> useItemAction;

    public Inventory ( Action<Item> useItemAction )
    {
        this . useItemAction = useItemAction;

        itemList = new List<Item> ();

        //AddItem(new Item {itemType = Item.ItemType.Sword, amount = 1});
    }

    public void AddItem ( Item item )
    {


         itemList . Add (item);


        

      
        OnItemListChanged?.Invoke (this , EventArgs . Empty);



    }


    public void RemoveItem ( Item item )
    {


        itemList . Remove (item);

        
     
        OnItemListChanged?.Invoke (this , EventArgs . Empty);
    }



    public List<Item> GetItemList ( )
    {
        return itemList;
    }
    public void SetUseItemCallback ( Action<Item> useItemCallback )
    {
        useItemAction = useItemCallback;
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System . Reflection;

public class UI_Inventory : MonoBehaviour
{
     private Inventory inventory;
     public  Transform itemSlotContainer;
     public Transform itemSlotTemplate;

    public int selectedIndex = 1;
    private float selectedScaleFactor = 1.2f;
    private Item selectedItem = null;
    private PlayerInventory player;
    public TextMeshProUGUI sellAmtText;
   


    public void SetPlayer ( PlayerInventory player )
    {
        this . player = player;
    }

    
    public void SetInventory(Inventory inventory) {


        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
     }


   private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
   {
      RefreshInventoryItems();
   }

    public int GetInventorySize ( )
    {
        return inventory . GetItemList () . Count;
        
    }
    //Sets the current ite mthe player has selected, and shows its sell value
    public void SetSelectedInventoryItem ( int index )
    {
        selectedIndex = Mathf . Clamp (index , 0 , GetInventorySize () - 1);
        selectedItem = inventory . GetItemList () [selectedIndex];
        RefreshInventoryItems (); // Refresh UI to highlight the selected item
       sellAmtText .text = "Sell Value: " +GetSelectedItem ().itemScriptableObject.sellAmount.ToString() + "$";
     
    }
   
    private void RefreshInventoryItems()
    {

        // Destroy slots 
      foreach(Transform child in itemSlotContainer){
         if(child == itemSlotTemplate) continue;
         Destroy(child.gameObject);
     

      }

      int x = 0;
      int y = 0;
      float itemSlotCellSize = 130f;
        int index = 0;
        // spawn new slots for each item in inventory, adding to the index each time so items are spaced
      foreach(Item item in inventory.GetItemList()){
         
         RectTransform itemSlotRectTransform = Instantiate (itemSlotTemplate,itemSlotContainer).GetComponent<RectTransform>();
         itemSlotRectTransform.gameObject.SetActive(true);
  
      
         itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
         Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
         image.sprite = item.itemScriptableObject.itemSprite;


            Vector3 scale = (index == selectedIndex) ? new Vector3(selectedScaleFactor, selectedScaleFactor, 1f) : Vector3.one;
            itemSlotRectTransform . localScale = scale;
   


         x++;
         index++;
         if(x>4){

            x=0;
            y++;
         }

           GetInventorySize ();
      }
    }

 
 
    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
      public Item GetSelectedItem()
    {
        return selectedItem;
    }

}

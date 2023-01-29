using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Text itemName;
    public Text itemDescription;
    public Text buyCost;
    public Item item;
    public ItemSlot otherItem;
    public Text amount; 

    void Update(){
        if(amount != null){
            amount.text = item.uses.ToString();
        }
    }

    public void ShowItemDescription()
    {
        itemName.text = item.name;
        itemDescription.text = item.description;
        buyCost.text = item.purchaseCost.ToString();
        otherItem.item = item;
    }
}

using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int potionPrice = 10; // Цена зелья
    
    public void SellHealthPotion()
    {
        PlayerInventory inventory = FindAnyObjectByType<PlayerInventory>();
        if (inventory != null)
        {
            inventory.SellPotion("Health Potion", potionPrice);
        }
    }
    
    public void SellManaPotion()
    {
        PlayerInventory inventory = FindAnyObjectByType<PlayerInventory>();
        if (inventory != null)
        {
            inventory.SellPotion("Mana Potion", potionPrice);
        }
    }
}
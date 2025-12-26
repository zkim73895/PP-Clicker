using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<string, int> ingredients = new Dictionary<string, int>();
    public Dictionary<string, int> potions = new Dictionary<string, int>();
    public int gold = 0;
    
    public TextMeshProUGUI inventoryText;
    public TextMeshProUGUI goldText;

    // Метод для добавления ингредиента
    public void AddIngredient(string name, int amount)
    {
        if (ingredients.ContainsKey(name))
        {
            ingredients[name] += amount;
        }
        else
        {
            ingredients.Add(name, amount);
        }
        
        Debug.Log("Собрано: " + name + ". Теперь есть: " + ingredients[name]);
        UpdateInventoryUI();
    }

    // Метод для добавления зелья
    public void AddPotion(string name, int amount)
    {
        if (potions.ContainsKey(name))
        {
            potions[name] += amount;
        }
        else
        {
            potions.Add(name, amount);
        }
        
        Debug.Log("Приготовлено: " + name + ". Теперь есть: " + potions[name]);
        UpdateInventoryUI();
    }

    // Метод для продажи зелья
    public void SellPotion(string potionName, int price)
    {
        if (potions.ContainsKey(potionName) && potions[potionName] > 0)
        {
            potions[potionName]--;
            gold += price;
            
            // Если зелья закончились, удаляем из словаря
            if (potions[potionName] <= 0)
            {
                potions.Remove(potionName);
            }
            
            UpdateInventoryUI();
            UpdateGoldUI();
            Debug.Log($"Продано {potionName} за {price} золота. Всего золота: {gold}");
        }
        else
        {
            Debug.Log($"Нет зелий {potionName} для продажи");
        }
    }

    // Метод для обновления текста инвентаря
    public void UpdateInventoryUI()
    {
        if (inventoryText == null) return;
        
        string inventoryString = "Инвентарь:\n";
        
        // Ингредиенты
        inventoryString += "--- Ингредиенты ---\n";
        foreach (var item in ingredients)
        {
            inventoryString += $"{item.Key}: {item.Value}\n";
        }
        
        // Зелья
        inventoryString += "\n--- Зелья ---\n";
        foreach (var item in potions)
        {
            inventoryString += $"{item.Key}: {item.Value}\n";
        }
        
        inventoryText.text = inventoryString;
    }

    // Метод для обновления текста золота
    void UpdateGoldUI()
    {
        if (goldText != null)
            goldText.text = $"Золото: {gold}";
    }
    
    // Стартовый метод для инициализации UI
    void Start()
    {
        UpdateInventoryUI();
        UpdateGoldUI();
    }
}
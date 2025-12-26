using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class PotionRecipe
{
    public string potionName;
    public List<IngredientRequirement> requirements;
}

[System.Serializable]
public class IngredientRequirement
{
    public string ingredientName;
    public int amount;
}

public class CauldronBrewing : MonoBehaviour
{
    [Header("Настройки варки")]
    public int clicksToBrew = 20;
    public float clickPower = 5f;
    
    [Header("Ссылки на UI")]
    public Slider brewingSlider;
    public TextMeshProUGUI recipeText;
    
    [Header("Рецепты")]
    public List<PotionRecipe> recipes = new List<PotionRecipe>();
    private int currentRecipeIndex = 0;
    
    private float currentProgress = 0;
    private bool isBrewing = false;
    private PlayerInventory playerInventory;

    void Start()
    {
        // Находим инвентарь один раз при старте
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        
        // Инициализируем UI рецепта
        UpdateRecipeText();
        
        // Скрываем слайдер в начале
        if (brewingSlider != null)
            brewingSlider.gameObject.SetActive(false);
    }

    // Метод для обновления текста рецепта
    void UpdateRecipeText()
    {
        if (recipeText == null || recipes.Count == 0) return;
        
        var recipe = recipes[currentRecipeIndex];
        string recipeString = $"Рецепт: {recipe.potionName}\n";
        
        foreach (var req in recipe.requirements)
        {
            recipeString += $"{req.ingredientName}: {req.amount}\n";
        }
        
        recipeText.text = recipeString;
    }

    // Метод для смены рецепта (вызывается из UI кнопок)
    public void SelectRecipe(int index)
    {
        if (index >= 0 && index < recipes.Count)
        {
            currentRecipeIndex = index;
            UpdateRecipeText();
            Debug.Log($"Выбран рецепт: {recipes[currentRecipeIndex].potionName}");
        }
    }

    // Обработка клика по горшку
    void OnMouseDown()
    {
        if (!isBrewing)
        {
            // Проверяем, можем ли начать варку
            if (CanStartBrewing())
            {
                StartBrewing();
            }
            else
            {
                Debug.Log("Не хватает ингредиентов!");
            }
        }
        else
        {
            // Увеличиваем прогресс варки
            currentProgress += clickPower;
            brewingSlider.value = currentProgress;
            
            // Проверяем, завершена ли варка
            if (currentProgress >= clicksToBrew)
            {
                FinishBrewing();
            }
        }
    }

    // Проверка наличия ингредиентов для текущего рецепта
    bool CanStartBrewing()
    {
        if (playerInventory == null || recipes.Count == 0) return false;
        
        var recipe = recipes[currentRecipeIndex];
        
        foreach (var req in recipe.requirements)
        {
            if (!playerInventory.ingredients.ContainsKey(req.ingredientName) || 
                playerInventory.ingredients[req.ingredientName] < req.amount)
            {
                return false;
            }
        }
        return true;
    }

    // Начало варки зелья
    void StartBrewing()
    {
        if (recipes.Count == 0) return;
        
        var recipe = recipes[currentRecipeIndex];
        
        Debug.Log($"Начинаем варку {recipe.potionName}!");
        isBrewing = true;
        currentProgress = 0;
        
        // Показываем слайдер прогресса
        if (brewingSlider != null)
        {
            brewingSlider.gameObject.SetActive(true);
            brewingSlider.maxValue = clicksToBrew;
            brewingSlider.value = 0;
        }
        
        // Тратим ингредиенты
        foreach (var req in recipe.requirements)
        {
            playerInventory.ingredients[req.ingredientName] -= req.amount;
        }
        
        // Обновляем UI инвентаря
        playerInventory.UpdateInventoryUI();
    }

    // Завершение варки зелья
    void FinishBrewing()
    {
        if (recipes.Count == 0) return;
        
        var recipe = recipes[currentRecipeIndex];
        string potionName = recipe.potionName;
        
        Debug.Log($"Зелье {potionName} сварено!");
        
        // Добавляем зелье в инвентарь игрока
        if (playerInventory.potions.ContainsKey(potionName))
        {
            playerInventory.potions[potionName]++;
        }
        else
        {
            playerInventory.potions.Add(potionName, 1);
        }
        
        // Обновляем UI
        playerInventory.UpdateInventoryUI();
        
        // Сбрасываем состояние варки
        isBrewing = false;
        currentProgress = 0;
        
        // Скрываем слайдер
        if (brewingSlider != null)
            brewingSlider.gameObject.SetActive(false);
    }

    // Метод для сброса варки (на случай, если нужно прервать)
    void ResetBrewing()
    {
        isBrewing = false;
        currentProgress = 0;
        
        if (brewingSlider != null)
        {
            brewingSlider.value = 0;
            brewingSlider.gameObject.SetActive(false);
        }
    }
}
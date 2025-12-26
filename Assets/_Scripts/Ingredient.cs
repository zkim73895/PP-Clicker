using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // Это публичная переменная. Её можно будет менять для каждого ингредиента в Инспекторе.
    public string ingredientName = "Mashrooms"; // Название ингредиента
    public int amount = 1; // Количество, которое даётся за сбор

    // Этот метод вызывается автоматически, когда другой объект с коллайдером входит в триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что в триггер вошел именно игрок (по тегу)
        if (other.CompareTag("Player"))
        {
            // Вызываем метод сбора у игрока
            other.GetComponent<PlayerInventory>().AddIngredient(ingredientName, amount);
            
            // Уничтожаем объект ингредиента на сцене
            Destroy(gameObject);
        }
    }
}
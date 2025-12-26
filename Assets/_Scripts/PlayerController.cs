using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 300f; // Скорость движения
    private Vector2 targetPosition; // Целевая позиция
    private bool isMoving = false; // Движется ли игрок
    private Rigidbody2D rb; // Ссылка на Rigidbody2D

    void Start()
    {
        // Получаем компонент Rigidbody2D при старте
        rb = GetComponent<Rigidbody2D>();
        // Начальная целевая позиция - текущее положение
        targetPosition = rb.position;
    }

    void Update()
    {
        // 1. Обработка клика мыши
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            // Получаем позицию клика в мировых координатах
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Устанавливаем новую цель
            targetPosition = clickPosition;
            isMoving = true;
        }

        // 2. Движение к цели
        if (isMoving)
        {
            // Вычисляем направление и расстояние
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
            // Применяем движение через Rigidbody2D
            rb.MovePosition(newPosition);

            // Если почти дошли, останавливаемся
            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
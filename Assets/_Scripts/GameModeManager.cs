using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public GameObject forestCamera;   // Камера леса (Main Camera)
    public GameObject labCamera;      // Камера лаборатории (Lab_Camera)
    
    public GameObject forestBackground; // Фон леса (Ground)
    public GameObject labBackground;    // Фон лаборатории (Lab_Background)
    
    public PlayerController playerController; // Скрипт движения игрока
    
    // Текущий режим: true = Лес, false = Лаборатория
    private bool isInForest = true;

    void Start()
    {
        // Убедимся, что начальное состояние правильное
        SwitchToForest();
    }

    // Включить режим леса
    public void SwitchToForest()
    {
        isInForest = true;
        forestCamera.SetActive(true);
        labCamera.SetActive(false);
        
        forestBackground.SetActive(true);
        labBackground.SetActive(false);
        
        // Включаем движение игрока по клику только в лесу
        if (playerController != null)
            playerController.enabled = true;
    }

    // Включить режим лаборатории
    public void SwitchToLab()
    {
        isInForest = false;
        forestCamera.SetActive(false);
        labCamera.SetActive(true);
        
        forestBackground.SetActive(false);
        labBackground.SetActive(true);
        
        // Отключаем движение игрока по клику в лаборатории
        if (playerController != null)
            playerController.enabled = false;
    }
    
    // Метод для переключения по кнопке
    public void ToggleMode()
    {
        if (isInForest)
            SwitchToLab();
        else
            SwitchToForest();
    }
}
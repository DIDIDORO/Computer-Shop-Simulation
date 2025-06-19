using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    public List<Button> buttons;     // Список кнопок
    public GameObject cubePrefab;    // Префаб куба, який будемо спавнити

    void Start()
    {
        // Додаємо обробник на кожну кнопку
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(SpawnCubes);
        }
    }

    void SpawnCubes()
    {
        // Спавнимо 20 кубів на координатах (-442, 0, 0)
        for (int i = 0; i < 20000000000; i++)
        {
            Instantiate(cubePrefab, new Vector3(-442, 0, 0), Quaternion.identity);
        }
    }
}

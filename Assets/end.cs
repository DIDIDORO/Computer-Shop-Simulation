using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Гра закривається...");

        // Для білду (реальної гри)
        Application.Quit();

        // Для редактора Unity (щоб бачити, що кнопка працює)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

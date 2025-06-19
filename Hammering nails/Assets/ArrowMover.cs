using UnityEngine;

public class ArrowSmoothController : MonoBehaviour
{
    private RectTransform rectTransform;
    private float currentSpeed;
    private int direction = 1; // 1 = вправо, -1 = вліво

    private bool slowingDown = false;
    private float slowDownProgress = 0f;

    [Header("Швидкості налаштувань")]
    public float startSpeed = 100f;              // Початкова швидкість
    public float boostedSpeedMultiplier = 5f;    // Множник прискорення на -90 і 90
    public float minSpeed = 100f;                 // Мінімальна швидкість (після спаду)
    public float slowDownTime = 1.5f;             // Час плавного спаду швидкості (секунди)

    [Header("Позиції подій")]
    public float leftEdge = -215f;
    public float rightEdge = 215f;
    public float leftBoostPoint = -90f;
    public float rightBoostPoint = 90f;
    public float slowDownStartPoint = 0f;
    public float slowDownThreshold = 5f; // Допуск ±5 пікселів для старту спаду

    private float boostedSpeed; // Сюди зберігається підсилена швидкість

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(leftEdge, rectTransform.anchoredPosition.y);
        currentSpeed = startSpeed;
    }

    void Update()
    {
        Vector2 pos = rectTransform.anchoredPosition;
        pos.x += currentSpeed * direction * Time.deltaTime;
        rectTransform.anchoredPosition = pos;

        if (direction == 1) // рух вправо
        {
            if (IsAround(pos.x, leftBoostPoint))
            {
                BoostSpeed();
            }
            else if (IsAround(pos.x, slowDownStartPoint) && !slowingDown)
            {
                StartSlowDown();
            }
            else if (IsAround(pos.x, rightBoostPoint))
            {
                ResetSpeed();
            }
            else if (pos.x >= rightEdge)
            {
                ReverseDirection();
            }
        }
        else if (direction == -1) // рух вліво
        {
            if (IsAround(pos.x, rightBoostPoint))
            {
                BoostSpeed();
            }
            else if (IsAround(pos.x, slowDownStartPoint) && !slowingDown)
            {
                StartSlowDown();
            }
            else if (IsAround(pos.x, leftBoostPoint))
            {
                ResetSpeed();
            }
            else if (pos.x <= leftEdge)
            {
                ReverseDirection();
            }
        }

        if (slowingDown)
        {
            SmoothSlowDown();
        }
    }

    private void BoostSpeed()
    {
        currentSpeed *= boostedSpeedMultiplier;
        boostedSpeed = currentSpeed;
    }

    private void StartSlowDown()
    {
        slowingDown = true;
        slowDownProgress = 0f;
    }

    private void ResetSpeed()
    {
        currentSpeed = startSpeed;
        slowingDown = false;
    }

    private void ReverseDirection()
    {
        direction *= -1;
        currentSpeed = startSpeed;
        slowingDown = false;
    }

    private void SmoothSlowDown()
    {
        slowDownProgress += Time.deltaTime / slowDownTime;
        currentSpeed = Mathf.Lerp(boostedSpeed, minSpeed, slowDownProgress);
        if (currentSpeed <= minSpeed + 1f)
        {
            currentSpeed = minSpeed;
            slowingDown = false;
        }
    }

    private bool IsAround(float value, float target)
    {
        return Mathf.Abs(value - target) <= slowDownThreshold;
    }
}
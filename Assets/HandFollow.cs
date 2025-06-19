using UnityEngine;

public class HandsFollower : MonoBehaviour
{
    public Transform player;                      // Гравець
    public Vector3 offset = new Vector3(0, -0.5f, 1.5f); // Зміщення рук
    public float followSpeed = 10f;               // Швидкість слідування
    public float transparencyDistance = 1f;       // Відстань до камери для прозорості
    public float fadeSpeed = 5f;                  // Швидкість зміни прозорості
    public Material handsMaterial;                // Матеріал рук

    public AudioSource movementAudioSource;       // Джерело звуку для руху
    public AudioClip movementClip;                // Аудіокліп для руху

    private Animator animator;
    private Camera mainCamera;
    private Color originalColor;
    private float currentAlpha = 1f;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        // Зберегти оригінальний колір
        if (handsMaterial != null)
            originalColor = handsMaterial.color;

        // Перевірка наявності AudioSource
        if (movementAudioSource == null)
            movementAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Плавне переміщення рук у позицію перед гравцем
        Vector3 targetPosition = player.position + player.forward * offset.z + player.up * offset.y + player.right * offset.x;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Поворот за напрямком гравця
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.deltaTime * followSpeed);

        // Перевірка руху гравця
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        // Програвання звуку під час руху
        if (isMoving)
        {
            if (!movementAudioSource.isPlaying)
            {
                movementAudioSource.clip = movementClip;
                movementAudioSource.loop = true;
                movementAudioSource.Play();
            }
        }
        else
        {
            if (movementAudioSource.isPlaying)
            {
                movementAudioSource.Stop();
            }
        }

        // Змінення прозорості залежно від відстані до камери
        float distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);

        float targetAlpha = distanceToCamera < transparencyDistance ? 0 : 1;
        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);

        if (handsMaterial != null)
        {
            Color newColor = originalColor;
            newColor.a = currentAlpha;
            handsMaterial.color = newColor;
        }

        // Миттєве переключення анімацій
        if (isMoving)
        {
            if (!animator.GetBool("isMoving"))
            {
                animator.SetBool("isMoving", true);
                animator.Play("MoveAnimation", 0, 0f); // Програвання анімації руху без затримки
            }
        }
        else
        {
            if (animator.GetBool("isMoving"))
            {
                animator.SetBool("isMoving", false);
                animator.Play("IdleAnimation", 0, 0f); // Повертає на idle без затримки
            }
        }
    }
}

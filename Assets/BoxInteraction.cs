using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 3f;

    public GameObject UI1;
    public GameObject UI2;
    public GameObject UI3;

    public GameObject boxObject;
    public GameObject itemObject;
    public Animator boxAnimator;

    [Header("Позиція коробки в руках")]
    public Vector3 offsetFromPlayer = new Vector3(0f, 1.5f, 1f);

    private bool isNearBox = false;
    private bool hasPickedBox = false;
    private bool isBoxOpened = false;

    private Rigidbody boxRb;

    void Start()
    {
        boxRb = boxObject.GetComponent<Rigidbody>();
        if (boxRb != null)
            boxRb.isKinematic = true;

        if (itemObject != null)
            itemObject.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        isNearBox = distance <= interactionDistance;

        // Показати UI1 коли біля коробки
        if (isNearBox && !hasPickedBox)
        {
            UI1.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                UI1.SetActive(false);
                UI2.SetActive(true);
                hasPickedBox = true;

                if (boxRb != null)
                    boxRb.isKinematic = true;
            }
        }
        else if (!isNearBox && !hasPickedBox)
        {
            UI1.SetActive(false);
        }

        // Коли коробка в руках
        if (hasPickedBox)
        {
            // Кинути коробку (X)
            if (Input.GetKeyDown(KeyCode.X))
            {
                UI2.SetActive(false);
                hasPickedBox = false;

                if (boxRb != null)
                {
                    boxRb.isKinematic = false;
                    Vector3 throwDirection = player.forward + Vector3.up * 0.3f;
                    boxRb.AddForce(throwDirection.normalized * 5f, ForceMode.Impulse);
                }
            }

            // Відкрити коробку (C)
            if (Input.GetKeyDown(KeyCode.C) && UI2.activeSelf && !isBoxOpened)
            {
                boxAnimator.SetTrigger("opening");
                UI2.SetActive(false);
                UI3.SetActive(true);
                isBoxOpened = true;
            }

            // Закрити коробку (C)
            else if (Input.GetKeyDown(KeyCode.C) && UI3.activeSelf && isBoxOpened)
            {
                boxAnimator.SetTrigger("Closing");
                UI3.SetActive(false);
                UI2.SetActive(true);
                isBoxOpened = false;
            }

            // Поставити коробку на землю (Enter)
            if (Input.GetKeyDown(KeyCode.Return) && UI3.activeSelf)
            {
                UI3.SetActive(false);
                hasPickedBox = false;
                isBoxOpened = false;

                boxObject.transform.SetParent(null);
                if (boxRb != null)
                    boxRb.isKinematic = false;
            }
        }
    }

    void LateUpdate()
    {
        // Позиціонування коробки в руках
        if (hasPickedBox)
        {
            Vector3 targetPosition = player.position + player.forward * offsetFromPlayer.z + player.right * offsetFromPlayer.x;
            targetPosition.y = player.position.y + offsetFromPlayer.y;

            boxObject.transform.position = targetPosition;
            boxObject.transform.rotation = Quaternion.Euler(-90f, player.eulerAngles.y, 0f);
        }
    }

    // Метод для події в анімації opening
    public void ReplaceBoxWithItem()
    {
        Debug.Log("Замінюємо коробку на предмет");

        if (boxObject != null)
            boxObject.SetActive(false);

        if (itemObject != null)
            itemObject.SetActive(true);
    }
}

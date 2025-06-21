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

    [Header("Налаштування положення коробки в руках")]
    public Vector3 holdPosition = new Vector3(0f, 0.5f, 1.5f);
    public Vector3 holdRotation = Vector3.zero;

    private Transform holdPoint;

    private bool isNearBox = false;
    private bool hasPickedBox = false;
    private bool isBoxOpen = false;

    private Rigidbody itemRb;
    private Rigidbody boxRb;

    void Start()
    {
        // Створюємо holdPoint автоматично
        holdPoint = new GameObject("HoldPoint").transform;
        holdPoint.SetParent(player);
        holdPoint.localPosition = holdPosition;
        holdPoint.localRotation = Quaternion.Euler(holdRotation);

        itemRb = itemObject.GetComponent<Rigidbody>();
        if (itemRb != null)
            itemRb.isKinematic = true;

        boxRb = boxObject.GetComponent<Rigidbody>();
        if (boxRb != null)
            boxRb.isKinematic = true;
    }

    void Update()
    {
        // Оновлюємо позицію holdPoint на випадок, якщо ти міняєш значення в інспекторі під час гри
        holdPoint.localPosition = holdPosition;
        holdPoint.localRotation = Quaternion.Euler(holdRotation);

        float distance = Vector3.Distance(player.position, transform.position);
        isNearBox = distance <= interactionDistance;

        // Підходиш до коробки — UI1
        if (isNearBox && !hasPickedBox)
        {
            UI1.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                UI1.SetActive(false);
                UI2.SetActive(true);

                hasPickedBox = true;

                // Помістити коробку в руки
                boxObject.transform.SetParent(holdPoint);
                boxObject.transform.localPosition = Vector3.zero;
                boxObject.transform.localRotation = Quaternion.identity;

                if (boxRb != null)
                    boxRb.isKinematic = true;
            }
        }
        else if (!isNearBox && !hasPickedBox)
        {
            UI1.SetActive(false);
        }

        if (hasPickedBox && !isBoxOpen)
        {
            // Відліт коробки (X)
            if (Input.GetKeyDown(KeyCode.X))
            {
                UI2.SetActive(false);
                hasPickedBox = false;

                // Від'єднати коробку
                boxObject.transform.SetParent(null);

                if (boxRb != null)
                {
                    boxRb.isKinematic = false;
                    Vector3 throwDirection = player.forward + Vector3.up * 0.3f;
                    boxRb.AddForce(throwDirection.normalized * 5f, ForceMode.Impulse);
                }
            }

            // Відкрити коробку (C)
            if (Input.GetKeyDown(KeyCode.C))
            {
                boxAnimator.SetTrigger("opening");
                UI2.SetActive(false);
                UI3.SetActive(true);

                boxObject.SetActive(false);

                itemObject.SetActive(true);
                itemObject.transform.position = holdPoint.position;
                itemObject.transform.rotation = holdPoint.rotation;

                if (itemRb != null)
                    itemRb.isKinematic = true;

                isBoxOpen = true;
            }
        }

        if (isBoxOpen)
        {
            // Кинути предмет вниз (Enter)
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (itemRb != null)
                {
                    itemRb.isKinematic = false;
                    itemRb.AddForce(Vector3.down * 2f, ForceMode.Impulse);
                }
            }

            // Закрити коробку (C)
            if (Input.GetKeyDown(KeyCode.C))
            {
                boxAnimator.SetTrigger("closing");
                UI3.SetActive(false);
                UI2.SetActive(true);

                boxObject.SetActive(true);
                boxObject.transform.SetParent(holdPoint);
                boxObject.transform.localPosition = Vector3.zero;
                boxObject.transform.localRotation = Quaternion.identity;

                if (boxRb != null)
                    boxRb.isKinematic = true;

                itemObject.SetActive(false);

                isBoxOpen = false;
                hasPickedBox = true;
            }
        }
    }
}

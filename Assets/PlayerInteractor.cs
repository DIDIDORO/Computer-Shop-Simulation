using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Посилання")]
    public Transform playerTransform;               // Сам гравець
    public Camera playerCamera;                     // Камера гравця
    public PickupableBox targetBox;                 // Коробка, з якою взаємодіяти

    [Header("UI Об'єкти")]
    public GameObject uiHover;                      // UI: "Press E to interact"
    public GameObject uiHold;                       // UI: "Click X to throw", "Click C to unpack"
    public GameObject uiUnpacked;                   // UI: "Click ENTER to put", "Click C to pack"

    [Header("Параметри")]
    public float interactDistance = 3f;

    private enum State { Idle, Holding, Unpacked }
    private State currentState = State.Idle;

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, targetBox.transform.position);
        Vector3 dirToBox = (targetBox.transform.position - playerCamera.transform.position).normalized;
        float dot = Vector3.Dot(playerCamera.transform.forward, dirToBox);

        if (currentState == State.Idle)
        {
            bool inRange = distance <= interactDistance && dot > 0.9f;

            // Включити/вимкнути обводку і UI при наведенні
            targetBox.SetOutline(inRange);
            uiHover.SetActive(inRange);

            if (inRange && Input.GetKeyDown(KeyCode.E))
            {
                currentState = State.Holding;
                uiHover.SetActive(false);
                uiHold.SetActive(true);
                targetBox.PickUp(playerTransform);
            }
        }
        else if (currentState == State.Holding)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                currentState = State.Idle;
                uiHold.SetActive(false);
                targetBox.Throw(playerCamera.transform.forward);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                currentState = State.Unpacked;
                uiHold.SetActive(false);
                uiUnpacked.SetActive(true);
            }
        }
        else if (currentState == State.Unpacked)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // ENTER
            {
                // Тут буде логіка "поставити"
                Debug.Log("Поки нічого не робить — ENTER");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                currentState = State.Holding;
                uiUnpacked.SetActive(false);
                uiHold.SetActive(true);
            }
        }
    }
}

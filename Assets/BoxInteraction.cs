using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 3f;

    public GameObject UI1;
    public GameObject UI2;
    public GameObject UI3;

    public GameObject boxObject;
    public Animator boxAnimator;

    [Header("Позиція та обертання коробки в руках")]
    public Vector3 holdLocalPosition = new Vector3(0f, 1.5f, 1f);
    public Vector3 holdLocalRotation = new Vector3(-90f, 0f, 0f);

    private Transform holdPoint;
    private bool isNearBox = false;
    private bool hasPickedBox = false;

    private Rigidbody boxRb;

    void Start()
    {
        // Створюємо HoldPoint прив'язаний до гравця
        holdPoint = new GameObject("HoldPoint").transform;
        holdPoint.SetParent(player);
        holdPoint.localPosition = holdLocalPosition;
        holdPoint.localRotation = Quaternion.Euler(holdLocalRotation);

        boxRb = boxObject.GetComponent<Rigidbody>();
        if (boxRb != null)
            boxRb.isKinematic = true;
    }

    void Update()
    {
        // Підтримуємо позицію HoldPoint відносно гравця
        holdPoint.localPosition = holdLocalPosition;
        holdPoint.localRotation = Quaternion.Euler(holdLocalRotation);

        float distance = Vector3.Distance(player.position, transform.position);
        isNearBox = distance <= interactionDistance;

        if (isNearBox && !hasPickedBox)
        {
            UI1.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                UI1.SetActive(false);
                UI2.SetActive(true);
                hasPickedBox = true;

                // Поміщаємо коробку в руки
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

        if (hasPickedBox)
        {
            // Відкидання коробки на X
            if (Input.GetKeyDown(KeyCode.X))
            {
                UI2.SetActive(false);
                hasPickedBox = false;

                boxObject.transform.SetParent(null);

                if (boxRb != null)
                {
                    boxRb.isKinematic = false;
                    Vector3 throwDirection = player.forward + Vector3.up * 0.3f;
                    boxRb.AddForce(throwDirection.normalized * 5f, ForceMode.Impulse);
                }
            }
        }
    }
}

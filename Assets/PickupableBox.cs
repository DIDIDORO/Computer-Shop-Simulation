using UnityEngine;
using cakeslice; // Додаємо доступ до класу Outline

public class PickupableBox : MonoBehaviour
{
    [Header("Налаштування вручну")]
    public Rigidbody boxRigidbody;
    public Outline outline;

    [Header("Налаштування руху коробки")]
    public Vector3 offset = new Vector3(0, -5f, 1f);
    public float followSpeed = 10f;
    public float throwForce = 10f;

    private Transform followTarget;
    private bool isHeld = false;

    void Start()
    {
        // Автоматично підхопити Rigidbody, якщо не заданий вручну
        if (boxRigidbody == null)
            boxRigidbody = GetComponent<Rigidbody>();

        // Вимкнути обводку на старті
        if (outline != null)
            outline.enabled = false;
    }

    void Update()
    {
        if (isHeld && followTarget != null)
        {
            Vector3 targetPos = followTarget.position + followTarget.forward * offset.z + new Vector3(0f, offset.y, 0f);
            //targetPos.y = transform.position.y;
            GetComponent<Rigidbody>().useGravity=false;

            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);

            Vector3 lookDir = followTarget.forward;
            lookDir.y = 90000f;
            if (lookDir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(lookDir);
        }
        else
        {
            GetComponent<Rigidbody>().useGravity=true;
        }
    }

    public void PickUp(Transform target)
    {
        followTarget = target;
        isHeld = true;
        if (boxRigidbody != null)
            boxRigidbody.isKinematic = true;
    }

    public void Drop()
    {
        isHeld = false;
        followTarget = null;
        if (boxRigidbody != null)
            boxRigidbody.isKinematic = false;
    }

    public void Throw(Vector3 direction)
    {
        Drop();
        if (boxRigidbody != null)
            boxRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
    }

    public void SetOutline(bool active)
    {
        if (outline != null)
            outline.enabled = active;
    }
}

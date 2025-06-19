using UnityEngine;

public class PickupableBox : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject itemPrefab; // єдиний предмет в коробці
    public Transform spawnPoint;

    private GameObject spawnedItem;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void PickUp(Transform player)
    {
        rb.isKinematic = true;
        transform.SetParent(player);
        transform.localPosition = new Vector3(0, 0, 2);
        transform.localRotation = Quaternion.identity;
    }

    public void Throw(Vector3 direction)
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(direction * 500f, ForceMode.Impulse);
    }

    public void OpenBox()
    {
        if (animator != null)
            animator.SetTrigger("opening");

        if (itemPrefab != null && spawnPoint != null)
        {
            spawnedItem = Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedItem.SetActive(false);
        }
    }

    public void CloseBox()
    {
        if (animator != null)
            animator.SetTrigger("closing");

        if (spawnedItem != null)
        {
            Destroy(spawnedItem);
            spawnedItem = null;
        }
    }

    public void DropItem()
    {
        if (spawnedItem != null)
        {
            spawnedItem.SetActive(true);
            spawnedItem.transform.SetParent(null);
            Rigidbody itemRb = spawnedItem.GetComponent<Rigidbody>();
            if (itemRb != null)
                itemRb.isKinematic = false;

            spawnedItem = null;
        }
    }
}

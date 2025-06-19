using UnityEngine;
using System.Collections.Generic;

public class PlayerInteractor : MonoBehaviour
{
    public Transform playerTransform;
    public Camera playerCamera;

    public List<PickupableBox> allBoxes = new List<PickupableBox>(); // список усіх коробок
    public PickupableBox targetBox;

    public GameObject uiHover;
    public GameObject uiHold;
    public GameObject uiUnpacked;

    public float interactDistance = 3f;

    private enum State { Idle, Holding, Unpacked }
    private State currentState = State.Idle;

    void Update()
    {
        if (allBoxes.Count == 0) return;

        // Перевірка на найближчу коробку
        foreach (var box in allBoxes)
        {
            float distance = Vector3.Distance(playerTransform.position, box.transform.position);
            Vector3 dirToBox = (box.transform.position - playerCamera.transform.position).normalized;
            float dot = Vector3.Dot(playerCamera.transform.forward, dirToBox);

            if (distance <= interactDistance && dot > 0.9f)
            {
                targetBox = box;
                uiHover.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentState = State.Holding;
                    uiHover.SetActive(false);
                    uiHold.SetActive(true);
                    targetBox.PickUp(playerTransform);
                    break;
                }
            }
        }

        if (currentState == State.Holding)
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
                targetBox.OpenBox();
            }
        }
        else if (currentState == State.Unpacked)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                targetBox.DropItem();
                uiUnpacked.SetActive(false);
                currentState = State.Idle;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                currentState = State.Holding;
                uiUnpacked.SetActive(false);
                uiHold.SetActive(true);
                targetBox.CloseBox();
            }
        }
    }
}

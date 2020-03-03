using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageText;

    private void Start()
    {
        _messageText = GameObject.FindWithTag("MessageText").gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void ItemPickUp(GameObject player)
    {
        if (player.GetComponent<ThirdPersonController>().PickUpSlot.transform.childCount == 0)
        {
            transform.SetParent(player.GetComponent<ThirdPersonController>().PickUpSlot);
            transform.localPosition = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<GravityBody>().enabled = false;
        }
    }

    public void ItemDrop()
    {
        RemoveMessage();
        GetComponent<GravityBody>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DisplayMessage();
            if (Input.GetMouseButton(0))
            {
                ItemPickUp(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RemoveMessage();
        }
    }

    private void DisplayMessage()
    {
        _messageText.text = "Hold LEFT MOUSE BUTTON to carry item";
    }

    private void RemoveMessage()
    {
        _messageText.text = "";
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private TextMeshProUGUI _messageText;
    private Transform _parent;

    private void Start()
    {
        _parent = transform.parent;
        _messageText = GameObject.FindWithTag("MessageText").gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void ItemPickUp(GameObject player)
    {
        if (player.GetComponent<ThirdPersonController>().PickUpSlot.transform.childCount == 0)
        {
           // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;;
            transform.SetParent(player.GetComponent<ThirdPersonController>().PickUpSlot);
            transform.localPosition = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<GravityBody>().enabled = false;
        }
    }

    public void ItemDrop(bool value)
    {
        RemoveMessage();
        GetComponent<GravityBody>().enabled = value;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(_parent);
        if (!value)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DisplayMessage();
            if (Input.GetMouseButtonDown(0))
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
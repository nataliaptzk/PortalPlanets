using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private TextMeshProUGUI _messageText;
    private MessageBoxAnimation _messageBoxAnimation;
    private Transform _parent;

    private void Start()
    {
        _parent = transform.parent;
        _messageText = GameObject.FindWithTag("MessageText").gameObject.GetComponent<TextMeshProUGUI>();
        _messageBoxAnimation = _messageText.transform.parent.gameObject.GetComponent<MessageBoxAnimation>();
    }

    private void ItemPickUp(GameObject player)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        transform.SetParent(player.GetComponent<ThirdPersonController>().PickUpSlot);
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<GravityBody>().enabled = false;
        DisplayMessage("Press SPACE again to drop the item or find the puzzle area");
        player.GetComponent<ThirdPersonController>().holdingItem = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DisplayMessage("Press SPACE to pick up");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Jump") && !other.GetComponent<ThirdPersonController>().holdingItem)
            {
                RemoveMessage();
                ItemPickUp(other.gameObject);
            }
            else if (Input.GetButtonDown("Jump") && other.GetComponent<ThirdPersonController>().holdingItem)
            {
                ItemDrop(true);
                other.GetComponent<ThirdPersonController>().holdingItem = false;
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

    private void DisplayMessage(string message)
    {
        _messageText.text = message;
        _messageBoxAnimation.Appear();
    }

    private void RemoveMessage()
    {
        _messageBoxAnimation.Disappear();
    }
}
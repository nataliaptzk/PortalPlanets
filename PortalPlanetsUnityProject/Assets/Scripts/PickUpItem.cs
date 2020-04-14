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
    private ThirdPersonController _player;
    private ParticleSystem _ps;

    [SerializeField] private AudioClip _pickUp;
    [SerializeField] private AudioClip _drop;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _ps = gameObject.GetComponentInChildren<ParticleSystem>();
        _player = FindObjectOfType<ThirdPersonController>();
        _parent = transform.parent;
        _messageText = GameObject.FindWithTag("MessageText").gameObject.GetComponent<TextMeshProUGUI>();
        _messageBoxAnimation = _messageText.transform.parent.gameObject.GetComponent<MessageBoxAnimation>();
    }

    private void ItemPickUp(GameObject player)
    {
        _audioSource.clip = _pickUp;
        _audioSource.Play();
        _ps.Stop();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        transform.SetParent(player.GetComponent<ThirdPersonController>().PickUpSlot);
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<GravityBody>().enabled = false;
        DisplayMessage("Press SPACE again to drop the item or find the puzzle area");
        _player.holdingItem = true;
    }

    public void ItemDrop(bool value)
    {
        _audioSource.clip = _drop;
        _audioSource.Play();
        RemoveMessage();
        GetComponent<GravityBody>().enabled = value;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(_parent);
        if (!value)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        _player.holdingItem = false;
        _ps.Play();

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
            if (Input.GetButtonDown("Jump") && !_player.holdingItem)
            {
                RemoveMessage();
                ItemPickUp(other.gameObject);
            }
            else if (Input.GetButtonDown("Jump") && _player.holdingItem)
            {
                ItemDrop(true);
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
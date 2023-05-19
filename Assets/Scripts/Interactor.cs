using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public GameObject highlight;
    public UnityEvent onInteract;

    private bool _readyToInteract;

    public void Start()
    {
        _readyToInteract = false;
    }

    public void Update()
    {
        highlight.SetActive(_readyToInteract);
        if (!_readyToInteract) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            onInteract.Invoke();
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _readyToInteract = true;
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (!other.gameObject.CompareTag("Player")) return;
        _readyToInteract = false;
    }
}

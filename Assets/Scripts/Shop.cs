using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shelf;
    public GameObject cart;
    
    private Canvas _canvas;

    public void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public void Open()
    {
        _canvas.enabled = true;
        MovementController.CanMove.Add("Shop");
    }

    public void Close()
    {
        _canvas.enabled = false;
        MovementController.CanMove.Remove("Shop");
    }
}

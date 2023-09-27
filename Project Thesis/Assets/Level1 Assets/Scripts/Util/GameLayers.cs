using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask scaleLayer;
    [SerializeField] LayerMask background;

    [SerializeField] LayerMask solidObjects;

    public static GameLayers i { get; set; }

    private void Awake()
    {
        i = this;
    }

    public LayerMask ScaleLayer
    {
        get => scaleLayer;
    }

    public LayerMask Background
    {
        get => background;
    }

    public LayerMask SolidObjects
    {
        get => solidObjects;
    }


}

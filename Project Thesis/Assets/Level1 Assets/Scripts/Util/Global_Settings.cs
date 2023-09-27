using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Settings : MonoBehaviour
{
    [SerializeField] Color  highlightedColor;

    public Color HighlightedColor => highlightedColor;

    public  static Global_Settings i { get; private set; }
    private void Awake()
    {
        i = this;
    }
}

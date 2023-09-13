using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Child", menuName= "Child/Create new child")]
public class Child_Stats : ScriptableObject
{
   [SerializeField] string name;

   [TextArea]
   [SerializeField] string note;

   [SerializeField] Sprite frontSprite;
   [SerializeField] Sprite leftSprite;

   [SerializeField] Child_Type type;

   [SerializeField] int Age;
   [SerializeField] int Height;
   [SerializeField] int Weight;
   [SerializeField] int BMI;
}

public enum Child_Type
{
    None,
    Healthy,
    Obese,
    Stunted,
    Wasted
}

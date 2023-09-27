using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Child", menuName= "Child/Create new child")]
public class Child_Stats : ScriptableObject
{
   [SerializeField] string name;

   [TextArea]
   [SerializeField] string note;

   [SerializeField] List <Sprite> frontSprite;
   [SerializeField] List <Sprite> leftSprite;

   [SerializeField] Child_Type type;

   [SerializeField] int height;
   [SerializeField] int weight;
   [SerializeField] int bmi;

   public string Name
   {
    get {return name; }
   }

   public string Note
   {
    get {return note; }
   }

   public List<Sprite> FrontSprite 
   {
    get {return frontSprite; }
   }

   public List<Sprite> LeftSprite 
   {
    get {return leftSprite; }
   }

   public Child_Type Type 
   {
    get {return type; }
   }

   public int Height 
   {
    get {return height; }
   }

   public int Weight 
   {
    get {return weight; }
   }

   public int BMI 
   {
    get {return bmi; }
   }
}

public enum Child_Type
{
    None,
    Healthy,
    Obese,
    Stunted,
    Wasted
}

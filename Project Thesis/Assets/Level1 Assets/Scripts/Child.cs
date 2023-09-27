using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Child 
{
    [SerializeField] Child_Stats _stats; 
    [SerializeField] int age;

    public Child_Stats Stat  { 
        get {return _stats; }
    }
    int Age { 
        get {return age; }
    }

    public void Init()
    {
        
    }
    
    public List<Sprite> LeftSprite
    {
        get { return Stat.LeftSprite;}
    }

    public List<Sprite> IdleSprite
    {
        get { return Stat.FrontSprite;}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoActor 
{
    public string name;
    public string className;

    public int level;
    public int xp;

    public int hp;
    public int mp;

    public DemoActor(ActorData data, ActorClassData classData)
    {
        name = data.name;
        level = data.initialLevel;
        className = classData.name;
    }

    public void SetAttributes(int hp, int mp)
    {
        this.hp = hp;
        this.mp = mp;
    }
    
}

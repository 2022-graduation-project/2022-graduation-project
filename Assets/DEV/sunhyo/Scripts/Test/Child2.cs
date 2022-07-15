using System.Collections;
using UnityEngine;


public class Child2 : Parent
{
    new private string name = "child2";
    public override void Print()
    {
        print("my name is " + name);
    }
}
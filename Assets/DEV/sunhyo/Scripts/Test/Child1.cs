using System.Collections;
using UnityEngine;


public class Child1 : Parent
{
    new private string name = "child1";
    public override void Print()
    {
        print("my name is " + name);
    }
}
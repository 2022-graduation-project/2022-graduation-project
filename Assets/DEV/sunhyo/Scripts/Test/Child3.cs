using System.Collections;
using UnityEngine;


public class Child3 : Parent
{
    new private string name = "child3";
    public override void Print()
    {
        print("my name is " + name);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wearable : MonoBehaviour, IItem
{
    public abstract void Use();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    protected PlayerController player;

    protected float healAmount;
    public abstract void Use();
}
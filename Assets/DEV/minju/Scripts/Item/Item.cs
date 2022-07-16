using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    protected PlayerController player;

    protected float healAmount;
    protected float manaAmount;
    protected float shieldDuration;
    public abstract void Use();
}
using System.Collections;
using UnityEngine;

public class ItemDummy : MonoBehaviour
{
    public void Use()
    {
        print($"{this.name} 아이템 사용");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot
{
    public void SetSlot(string _item_code, int _item_count);
    public void ResetSlot();
    public void SetColorA(float _delta);
}

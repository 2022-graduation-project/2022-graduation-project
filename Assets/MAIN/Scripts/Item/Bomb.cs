using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Consumable
{
    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    public void FireBomb(float firePower)
    {
        Rigidbody rigOfBomb = GetComponent<Rigidbody>();
        rigOfBomb.AddForce((GameObject.Find("Main Camera").transform.forward + new Vector3(0, 0.2f, 0))
                            * firePower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {
        }

        Destroy(gameObject, 0.1f);
    }
}

using System.Collections;
using UnityEngine;


public class RollingStone : MonoBehaviour
{
    private float damage = 12f;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Attack(other.GetComponent<ICombat>()));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Attack(ICombat _pc)
    {
        float curTime = 0;

        while(true)
        {
            curTime += 1f;
            _pc.Damaged(damage);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
using System.Collections;
using UnityEngine;

public class StoneStorm : MonoBehaviour
{
    private float damage = 17f;
    public void StartAttack(Vector3 pos)
    {
        StartCoroutine(Attack(pos));
    }

    IEnumerator Attack(Vector3 pos)
    {
        int layerMask = 1 << LayerMask.GetMask("Player");
        RaycastHit[] hits;

        while (true)
        {
            hits = Physics.SphereCastAll(pos, 5f, Vector3.forward, 5f, ~layerMask - 1);
            foreach(RaycastHit hit in hits)
            {
                if(hit.transform.gameObject.tag == "Player")
                {
                    hit.transform.GetComponent<PlayerCombat>().Damaged(damage);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
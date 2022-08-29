using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItemPool : MonoBehaviour
{
    /*----------------------------------------------------------------
     *              [Normal Monster] 사망 후 아이템 떨구기
     * --------------------------------------------------------------*/


    public GameObject item;
    public int itemIdx = 0;



    public void DropItem(NormalMonster _monster)
    {
        item = transform.GetChild(itemIdx).gameObject;
        item.transform.position = _monster.transform.position + new Vector3(0, 1.1f, 0);  // 아이템 떨굴 위치 (죽은 자리 + 1) 설정

        if (!(item.activeSelf))
        {
            item.SetActive(true);
            StartCoroutine(DestroyItem(itemIdx));
        }

        if (itemIdx < item.transform.parent.childCount - 1)
            itemIdx++;
        else
            itemIdx = 0;
    }

    public IEnumerator DestroyItem(int index)
    {
        yield return new WaitForSeconds(15);

        if (transform.GetChild(index).gameObject.activeSelf == true)
        {
            Debug.Log("안 먹었지? ㅇㅇ 없앨게~");
            transform.GetChild(index).gameObject.SetActive(false);
        }
    }
}

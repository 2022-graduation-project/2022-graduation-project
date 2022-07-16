using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ItemManager : MonoBehaviour
{
    private float hpCool = 2f;
    private float mpCool = 2f;
    private float spCool = 10f;
    private float bmCool = 5f;
    private Dictionary<string, List<float>> coolTimes;
    /*****************************************
     * (InventorySlot에서 아이템 사용 물어볼 시)
     * 해당 아이템 사용 가능 여부 반환
     * 
     * @ param - string item_name (인수: itemData.image_name)
     * @ return - bool
     * @ exception - X
    ******************************************/
    public bool ReturnUseItem(string item_name)
    {
        switch(item_name)
        {
            case "000_hpPotion":
            case "001_mpPotion":
            case "005_sheildPotion":
            case "006_bomb":
                if(coolTimes[item_name][0] < coolTimes[item_name][1])
                {
                    return false;
                }
                // 해당 아이템의 사용 쿨타임 재기
                StartCoroutine(CountCool(item_name));
                return true;
            // 주요 4가지 아이템 외에는 쿨타임 없음
            default:
                return true;
        }
    }

    private void Awake() {
        coolTimes = new Dictionary<string, List<float>>();
        List<float> temp = new List<float>();
        temp.Add(0f);
        temp.Add(hpCool);
        coolTimes.Add("000_hpPotion", temp.ToList());
        temp.Clear();
        temp.Add(0);
        temp.Add(mpCool);
        coolTimes.Add("001_mpPotion", temp.ToList());
        temp.Clear();
        temp.Add(0);
        temp.Add(spCool);
        coolTimes.Add("005_sheildPotion", temp.ToList());
        temp.Clear();
        temp.Add(0);
        temp.Add(bmCool);
        coolTimes.Add("006_bomb", temp.ToList());
    }


    /*****************************************
     * (ReturnUseItem 해당 아이템 쿨타임 다됐을 때)
     * 해당 아이템의 사용 쿨타임 재기
     * 
     * @ param - string item_name (인수: itemData.image_name)
     * @ return - X
     * @ exception - X
    ******************************************/
    private IEnumerator CountCool(string item_name)
    {
        while(coolTimes[item_name][0] < coolTimes[item_name][1])
        {
            yield return new WaitForSeconds(1f);
            coolTimes[item_name][0]++;
            if (coolTimes[item_name][0] >= coolTimes[item_name][1])
            {
                coolTimes[item_name][0] = coolTimes[item_name][1];
                yield break;
            }
        }
    }
}

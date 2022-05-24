using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    private Camera uiCamera;                // UI Camera 변수
    private Canvas canvas;                  // canvas 변수
    private RectTransform rectParent;       // RectTransform from parent
    private RectTransform rectHp;           // RectTransform from itself
    [HideInInspector]
    public Vector3 offset = Vector3.zero;   // for location of HpBar
    [HideInInspector]
    public Transform targetTr;               // Position of monster

    private Camera playerMainCam;            // PlayerMainCamera

    // Start is called before the first frame update
    void Start()
    {
        // Get canvas from parent
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
        playerMainCam = GameObject.Find("Player").transform.Find("Main Camera").GetComponent<Camera>();
    }

    // LateUpdate is called once per frame after calling Update()
    void LateUpdate()
    {
        // from World(3D) to Screen(2D). Offset locate UI to monster's head
        var screenPos = playerMainCam.WorldToScreenPoint(targetTr.position + offset);

        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
            // distance from MainCamera to XY-plane 
        }   // FOR BACK-VIEW

        var localPos = Vector2.zero;
        // from Screen(2D) to Canvas point.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        // Save above as localPos and print hpBar
        rectHp.localPosition = localPos;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableCook : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
	private Image image;
	private RectTransform rect;

	private int numSlot = -1;
	private Image icon;
	private Image[] icons;
	private string icon_name;
	public Cooking cook;

	private void Awake()
	{
		image = GetComponent<Image>();
		rect = GetComponent<RectTransform>();
	}

	/// <summary>
	/// ���콺 ����Ʈ�� ���� ������ ���� ���� ���η� �� �� 1ȸ ȣ��
	/// </summary>
	public void OnPointerEnter(PointerEventData eventData)
	{
		// ������ ������ ������ ��������� ����
		image.color = Color.yellow;
	}

	/// <summary>
	/// ���콺 ����Ʈ�� ���� ������ ���� ������ �������� �� 1ȸ ȣ��
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		// ������ ������ ������ �Ͼ������ ����
		image.color = Color.white;
	}

	/// <summary>
	/// ���� ������ ���� ���� ���ο��� ����� ���� �� 1ȸ ȣ��
	/// </summary>
	public void OnDrop(PointerEventData eventData)
	{
		// pointerDrag�� ���� �巡���ϰ� �ִ� ���(=������)
		if (eventData.pointerDrag != null)
		{
			// �巡���ϰ� �ִ� ����� �θ� ���� ������Ʈ�� �����ϰ�, ��ġ�� ���� ������Ʈ ��ġ�� �����ϰ� ����
			eventData.pointerDrag.transform.SetParent(transform);
			eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
		}

		switch (gameObject.name)
		{
			case "ItemSlot":
				numSlot = 1;
				break;
			case "ItemSlot (1)":
				numSlot = 2;
				break;
		}

        // �巡�� �� ������ �̹���
        icons = gameObject.GetComponentsInChildren<Image>();
        icon = icons[icons.Length - 1];
		icon_name = icon.gameObject.GetComponent<DraggableItem>().slot.itemData.image_name;

		sendItem();
	}

	public void sendItem()
    {
        switch (numSlot)
        {
			case 1:
				cook.setFirstItem(icon_name);
				break;
			case 2:
				cook.setSecondItem(icon_name);
				break;
        }
		cook.ItemOn();
	}
}


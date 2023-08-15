using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemIcon; // 아이템 아이콘

    // 필요한 컴포넌트
    [SerializeField]
    private TMP_Text text_Count;
    [SerializeField]
    private GameObject go_Count_Image;

    // 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemIcon.color;
        color.a = _alpha;
        itemIcon.color = color; // 0이면 투명, 1이면 보이도록
    }

    // 아이템 획득
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemIcon.sprite = item.itemImage;

        Debug.Log("아이템 획득");

        // 장비가 아닐 경우
        if (item.itemType != Item.ItemType.Equipment)
        {
            text_Count.text = itemCount.ToString();
            go_Count_Image.SetActive(true);
        }
        // 장비일 경우
        else
        {
            text_Count.text = "0";
            go_Count_Image.SetActive(false);
        }
        SetColor(1);
    }

    // 아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemIcon.sprite = null;
        SetColor(0);

        go_Count_Image.SetActive(false);
        text_Count.text = "0";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    // 필요한 컴포넌트
    [SerializeField] private GameObject go_InventoryBase;
    [SerializeField] private GameObject go_SlotsParent;

    // 슬롯
    private InventorySlot[] slots;
    public Item item; // 획득한 아이템

    public int stoneCount = 0;
    public int boneCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        go_InventoryBase.SetActive(inventoryActivated);
        slots = go_SlotsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    // I키로 인벤토리 활성화
    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (_item.itemType != Item.ItemType.Equipment) // 장비 아이템이 아닌 경우
        {
            for (int i = 0; i < slots.Length; i++) // 아이템이 이미 있으면 개수 증가
            {
                if (slots[i].item != null) // 비어있지 않을 때만 비교
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++) // 아이템이 없으면 빈 자리에 넣기
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject // MonoBehaviour와 달리 게임 오브젝트에 붙일 필요X
{
    public string itemName; // 아이템의 이름
    [TextArea]
    public string itemDesc; // 아이템 설명
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템 스프라이트
    public GameObject itemPrefab; // 아이템 프리팹
    public int ResourceCnt; // 구매 시 필요한 자원 개수

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }
}

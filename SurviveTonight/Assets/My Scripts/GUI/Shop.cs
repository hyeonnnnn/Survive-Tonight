using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public static bool shopActivated = false;

    public Item[] weaponObj; // 무기 아이템 목록
    public int[] acquiredStone; // 필요한 돌맹이 개수

    public Item[] towerObj; // 타워 아이템 목록
    public int[] acquiredBone; // 필요한 뼈다귀 개수

    [SerializeField] InventorySlot theInventorySlot;
    [SerializeField] Inventory theInventory;
    [SerializeField] WeaponManager theWeaponManager;
    [SerializeField] Tower theTower;

    public List<Tower> towers = new List<Tower>();

    public TextMeshProUGUI TowerDmg;

    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(shopActivated);
    }

    // u키 눌러서 상점 활성화
    void Update()
    {
        TryOpenShop();
    }

    void TryOpenShop()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            shopActivated = !shopActivated;
            shopPanel.SetActive(shopActivated);
        }
    }

    // 무기 구매 (돌맹이)
    public void Buy(int index)
    {
        theWeaponManager.hasWeapons[index + 1] = true;
        theWeaponManager.equipWeapon = theWeaponManager.weapons[index + 1];
        theWeaponManager.weaponBg[index + 1].GetComponent<Image>().color = Color.white;
    }

    // 타워 업그레이드 (뼈다귀)
    public void Upgrade(int index)
    {
        foreach (Tower tower in towers)
        {
            tower.dmg += 3;
            TowerDmg.text = "Tower Damage : " + tower.dmg.ToString();
        }
    }
}
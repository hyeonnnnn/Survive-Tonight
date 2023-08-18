using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public static bool shopActivated = false;

    public Item[] weaponObj; // ���� ������ ���
    public int[] acquiredStone; // �ʿ��� ������ ����

    public Item[] towerObj; // Ÿ�� ������ ���
    public int[] acquiredBone; // �ʿ��� ���ٱ� ����

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

    // uŰ ������ ���� Ȱ��ȭ
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

    // ���� ���� (������)
    public void Buy(int index)
    {
        theWeaponManager.hasWeapons[index + 1] = true;
        theWeaponManager.equipWeapon = theWeaponManager.weapons[index + 1];
        theWeaponManager.weaponBg[index + 1].GetComponent<Image>().color = Color.white;
    }

    // Ÿ�� ���׷��̵� (���ٱ�)
    public void Upgrade(int index)
    {
        foreach (Tower tower in towers)
        {
            tower.dmg += 3;
            TowerDmg.text = "Tower Damage : " + tower.dmg.ToString();
        }
    }
}
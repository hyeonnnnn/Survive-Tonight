using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    // 무기 관리
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public GameObject equipWeapon;

    public Image[] weaponBg;

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        // 무기 교체: 1, 2, 3
        Swap();
    }

    void Swap()
    {
        // 무기가 없으면 교체X
        for (int i = 0; i < 3; i++)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1 + i)) && !hasWeapons[i])
                return;
        }

        int weaponIndex = -1;

        // 번호키에 따른 인덱스 변경
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                weaponIndex = i;
                break;
            }
        }

        // 인덱스에 따른 무기 교체
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 현재 무기 비활성화
            if (equipWeapon != null)
                equipWeapon.SetActive(false);

            // 바꿀 무기 활성화
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
    }

    public void ActivateHammer()
    {
        HammerController.isActivate = true;
        hasWeapons[0] = true;
        equipWeapon = weapons[0];
        equipWeapon.SetActive(true);
        weaponBg[0].GetComponent<Image>().color = Color.white;
    }
}

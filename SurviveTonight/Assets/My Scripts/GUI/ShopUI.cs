using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject shopPanel;

    public static bool activeShop = false;

    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(activeShop);
    }

    // u키 눌러서 상점 활성화
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            activeShop = !activeShop;
            shopPanel.SetActive(activeShop);
        }
    }
}

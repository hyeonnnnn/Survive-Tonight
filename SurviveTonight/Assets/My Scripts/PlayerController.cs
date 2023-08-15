using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // 체력
    [SerializeField] int maxHp;
    int currentHp;
    [SerializeField] TextMeshProUGUI hpText;

    // 스피드 조정 변수
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    float applySpeed;

    [SerializeField] float jumpForce;

    // 상태 변수
    bool isGround = true;
    bool isDamage = false;
    bool isDead = false;

    // 카메라 민감도
    [SerializeField] float lookSensitivity;
    
    // 카메라 한계
    [SerializeField] float cameraRotationLimit;
    float currentCameraRotation_X = 0f;

    [SerializeField] Camera theCamera;
    [SerializeField] Rigidbody myRigid;
    [SerializeField] CapsuleCollider capsuleCollider;

    [SerializeField] Inventory theInventory;
    [SerializeField] ShopUI theShopUI;
    [SerializeField] WeaponManager theWeaponManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        applySpeed = walkSpeed;
        hpText.text = "<color=yellow>" + currentHp + "</color>/" + maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        IsGround();
        Move();

        // 인벤토리가 활성화되면 카메라, 캐릭터를 가만히
        if (Inventory.inventoryActivated == false)
        {
            CameraRotation();
            CharacterRotation();
        }
    }

    void GetInput()
    {
        // 점프: Space
        if (Input.GetKeyDown(KeyCode.Space) && isGround) Jump();

        // 달리기: Left Shift
        if (Input.GetKey(KeyCode.LeftShift)) Running();
        if (Input.GetKeyUp(KeyCode.LeftShift)) RunningCancle();
    }

    void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
    }

    void Running()
    {
        applySpeed = runSpeed;
    }

    void RunningCancle()
    {
        applySpeed = walkSpeed;
    }

    // 캐릭터 이동
    void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 hMove = transform.right * hAxis;
        Vector3 vMove = transform.forward * vAxis;

        Vector3 _velocity = (hMove + vMove).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    // 카메라 회전 (상하)
    void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotation_X -= _cameraRotationX;
        currentCameraRotation_X = Mathf.Clamp(currentCameraRotation_X, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotation_X, 0f, 0f);
    }

    // 캐릭터 회전 (좌우)
    void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (isDead == false && !isDamage)
        {
            currentHp -= _dmg;
            hpText.text = "<color=yellow>" + currentHp + "</color>/" + maxHp;
            StartCoroutine(OnDamage());

            if (currentHp <= 0)
            {
                Dead();
                return;
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(2.5f); // 무적 타임
        isDamage = false; // 무적 타임 종료
    }

    void Dead()
    {
        isDead = true;
        Debug.Log("게임 오버");
    }

    // 필드아이템 접촉
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            Item item = collision.GetComponent<FieldItems>().item;

            // 망치 접촉
            if (item.itemName == "Hammer")
            {
                theWeaponManager.ActivateHammer();
            }

            theInventory.AcquireItem(item);
            Destroy(collision.gameObject);
        }
    }
}

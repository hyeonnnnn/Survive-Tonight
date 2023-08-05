using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 무기 관리
    [SerializeField] GameObject[] weapons;
    public bool[] hasWeapons;

    GameObject equipWeapon;

    // 체력
    [SerializeField] int hp;

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

    // Start is called before the first frame update
    void Start()
    {
        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        IsGround();
        Move();
        CameraRotation();
        CharacterRotation();
    }

    void GetInput()
    {
        // 점프: Space
        if (Input.GetKeyDown(KeyCode.Space) && isGround) Jump();

        // 달리기: Left Shift
        if (Input.GetKey(KeyCode.LeftShift)) Running();
        if (Input.GetKeyUp(KeyCode.LeftShift)) RunningCancle();

        // 무기 교체: 1, 2, 3
        Swap();
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

    void Swap()
    {
        // 무기가 없으면 교체X
        if (Input.GetKeyDown(KeyCode.Alpha1) && !hasWeapons[0])
            return;
        if (Input.GetKeyDown(KeyCode.Alpha2) && !hasWeapons[1])
            return;
        if (Input.GetKeyDown(KeyCode.Alpha3) && !hasWeapons[2])
            return;
        
        int weaponIndex = -1;

        // 번호키에 따른 인덱스 변경
        if (Input.GetKeyDown(KeyCode.Alpha1)) weaponIndex = 0; // 망치
        if (Input.GetKeyDown(KeyCode.Alpha2)) weaponIndex = 1; // 총1
        if (Input.GetKeyDown(KeyCode.Alpha3)) weaponIndex = 2; // 총2

        // 인덱스에 따른 무기 교체
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 현재 무기 비활성화
            if(equipWeapon != null)
                equipWeapon.SetActive(false);

            // 바꿀 무기 활성화
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
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

    void OnTriggerEnter(Collider other)
    {   // 자원 습득
        if (other.CompareTag("Resource"))
        {
            Resource resource = other.GetComponent<Resource>();
            switch (resource.type)
            {
                case Resource.Type.InitHammer:
                    hasWeapons[0] = true;
                    equipWeapon = weapons[0];
                    equipWeapon.SetActive(true);
                    break;
                case Resource.Type.StoneSculpture:
                    resource.stoneSculpture += resource.value;
                    break;
                case Resource.Type.Bone:
                    resource.bone += resource.value;
                    break;
                case Resource.Type.Bullet:
                    resource.bullet += resource.value;
                    break;
                case Resource.Type.Torax:
                    resource.torax += resource.value;
                    break;
                case Resource.Type.Key:
                    resource.key += resource.value;
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (isDead == false && isDamage == false)
        {
            hp -= _dmg;
            StartCoroutine(OnDamage());

            if (hp <= 0)
            {
                Dead();
                return;
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(1); // 무적 타임
        isDamage = false;
    }

    void Dead()
    {
        isDead = true;
        Debug.Log("게임 오버");
    }
}

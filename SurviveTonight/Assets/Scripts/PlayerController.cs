using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���� ����
    [SerializeField] GameObject[] weapons;
    public bool[] hasWeapons;

    GameObject equipWeapon;

    // ü��
    [SerializeField] int hp;

    // ���ǵ� ���� ����
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    float applySpeed;

    [SerializeField] float jumpForce;

    // ���� ����
    bool isGround = true;
    bool isDamage = false;
    bool isDead = false;

    // ī�޶� �ΰ���
    [SerializeField] float lookSensitivity;
    
    // ī�޶� �Ѱ�
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
        // ����: Space
        if (Input.GetKeyDown(KeyCode.Space) && isGround) Jump();

        // �޸���: Left Shift
        if (Input.GetKey(KeyCode.LeftShift)) Running();
        if (Input.GetKeyUp(KeyCode.LeftShift)) RunningCancle();

        // ���� ��ü: 1, 2, 3
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
        // ���Ⱑ ������ ��üX
        if (Input.GetKeyDown(KeyCode.Alpha1) && !hasWeapons[0])
            return;
        if (Input.GetKeyDown(KeyCode.Alpha2) && !hasWeapons[1])
            return;
        if (Input.GetKeyDown(KeyCode.Alpha3) && !hasWeapons[2])
            return;
        
        int weaponIndex = -1;

        // ��ȣŰ�� ���� �ε��� ����
        if (Input.GetKeyDown(KeyCode.Alpha1)) weaponIndex = 0; // ��ġ
        if (Input.GetKeyDown(KeyCode.Alpha2)) weaponIndex = 1; // ��1
        if (Input.GetKeyDown(KeyCode.Alpha3)) weaponIndex = 2; // ��2

        // �ε����� ���� ���� ��ü
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ���� ���� ��Ȱ��ȭ
            if(equipWeapon != null)
                equipWeapon.SetActive(false);

            // �ٲ� ���� Ȱ��ȭ
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
    }

    // ĳ���� �̵�
    void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 hMove = transform.right * hAxis;
        Vector3 vMove = transform.forward * vAxis;

        Vector3 _velocity = (hMove + vMove).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    // ī�޶� ȸ�� (����)
    void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotation_X -= _cameraRotationX;
        currentCameraRotation_X = Mathf.Clamp(currentCameraRotation_X, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotation_X, 0f, 0f);
    }

    // ĳ���� ȸ�� (�¿�)
    void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    void OnTriggerEnter(Collider other)
    {   // �ڿ� ����
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
        yield return new WaitForSeconds(1); // ���� Ÿ��
        isDamage = false;
    }

    void Dead()
    {
        isDead = true;
        Debug.Log("���� ����");
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EngineTank : MonoBehaviour
{

    //скорость движения танка
    float MoveSpeed = 2;
    //скорость поворота танка
    float RotateSpeed = 60;
    //получаем компонент движения танка 
    Rigidbody TankEngine;
    public GameObject Tower;
    
    public Transform[] shootElement;    
    public GameObject bullet; 
    public ParticleSystem[] ShootFX;
    public GameObject Towerbug;
    public Camera cam;
    public Vector3 impactNormal_2;    
    public GameObject[] Meshes;
    private bool Check;
    private int n;

    void Start()
    {
        n = 0;     
        Check = false;

        TankEngine = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if ((Check == false))
        {
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i].SetActive(false);
            }
            Check = true;
        }
    }

    void Move()
    {
        Vector3 Move = transform.forward * Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        Vector3 Poze = TankEngine.position + Move;
        TankEngine.MovePosition(Poze);
    }

    void Rotates()
    {
        float R = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;

        Quaternion RotateAngle = Quaternion.Euler(0, R, 0);

        Quaternion CurrentUgol = TankEngine.rotation * RotateAngle;

        TankEngine.MoveRotation(CurrentUgol);
    }   

        void RotatesTower()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Tower.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    void Fire()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            for (int i = 0; i < ShootFX.Length; i++)
            {
                ShootFX[i].Play();
            }

            if ((n >= 0) && (n <= shootElement.Length+1))
            {
                Quaternion SpawnRoot = Tower.transform.rotation;

                GameObject bullet_tank = GameObject.Instantiate(bullet, shootElement[n].position, SpawnRoot) as GameObject;
                bullet_tank.GetComponent<BulletTank>().ShootElement = shootElement[n];

                Rigidbody bullet_rigidbody = bullet_tank.GetComponent<Rigidbody>();

                bullet_rigidbody.AddForce(bullet_tank.transform.forward * 20, ForceMode.Impulse);

                Destroy(bullet_tank, 5);
                n = n + 1;
                if (n >= shootElement.Length) { n = 0; }
            }
        }
    }

    void FixedUpdate()
    {
        Fire();
        Move();
        Rotates();
        RotatesTower();
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private GameObject barrel;
    private float timer;


    [SerializeField] private float bulletDespawnTime;
    [SerializeField] private float projectileSpeed;

    [SerializeField] private float HPDamage;
    public float HPDAMAGE { get { return HPDamage; } set { HPDamage = value; } }

    [SerializeField] private float shieldDamage;
    public float SHIELDDAMAGE { get { return shieldDamage;  } set { shieldDamage = value; } }

    private void Awake()
    {
        barrel = GameObject.Find("Barrel").gameObject;
    }

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = barrel.transform.position - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(-direction.x, -direction.y).normalized * projectileSpeed;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > bulletDespawnTime)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Building" && collision.gameObject.tag != "PatrolPoint" && collision.gameObject.tag != "PatrolPointStart" 
            && collision.gameObject.tag != "Shield" && collision.gameObject.tag != "Planet" && collision.gameObject.tag != "EnemyBullet" && collision.gameObject.tag != "Barrel"
            && collision.gameObject.tag != "InsideShip")
        {
            Destroy(gameObject);
        } 
    }
}


using UnityEngine;

public class GameStart : MonoBehaviour
{
    Controls controls;

    private Rigidbody2D rb;
    [SerializeField] private float startForce;
    [SerializeField] private float timeToPopup;
    [SerializeField] private GameObject startPopup;
    [SerializeField] private AudioSource audioSource1;

    private float timer;
    private bool isPaused;
    private bool hasPaused;
    public bool IsPaused { get { return isPaused; }set { isPaused = value; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = GetComponent<Controls>();
        
    }

    private void Start()
    {
        rb.AddForce(transform.right * startForce, ForceMode2D.Impulse);
        rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
        rb.mass = 0.01f;
        rb.angularDrag = 0.1f;
        controls.enabled = false;
        timer = timeToPopup;
        hasPaused = false;
    }


    void Update()
    {
        if (hasPaused == false) timer -= Time.deltaTime; else timer = 1000;
        if (timer <= 0)
        {
            hasPaused = true;
            IsPaused = true;
            startPopup.SetActive(true);
            rb.mass = 1f;
            rb.angularDrag = 1f;
            controls.enabled = true;
            Time.timeScale = 0f;
        }
        CloseStartPopupWithKey();
    }
    public void CloseStartPopup()
    {
        audioSource1.Play();
        Time.timeScale = 1f;
        IsPaused = false;
        startPopup.gameObject.SetActive(false);
    }
    private void CloseStartPopupWithKey()
    {
        if (controls.IsThrusting && startPopup.activeInHierarchy == true)
        {
            audioSource1.Play();
            Time.timeScale = 1f;
            IsPaused = false;
            startPopup.gameObject.SetActive(false);
        }
    }
}

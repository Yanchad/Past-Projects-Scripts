
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private float timer2;
    [SerializeField] private float timeCap = 60f; 
    [SerializeField] private int timeValue;
    [SerializeField] private bool isReducingStars;
    private float intervalTimer;
    [SerializeField] private float intervalTime = 10;
    [SerializeField] private int stars = 5;
    [SerializeField] private int maxStars = 5;

    public event System.Action<float> StarsChanged;
    public float IntervalTimer => intervalTimer;
    public int Stars { get { return stars; } set { stars = value; } }
    public int MaxStars { get { return maxStars; } set { maxStars = value; } }
    

    void Start()
    {
        if (PlayerPrefs.HasKey("stars"))
        {
            stars = PlayerPrefs.GetInt("stars");
        }
        else stars = maxStars;
        intervalTimer = 0;
        timer2 = 0;
        StarsChanged?.Invoke(stars);

        if (PlayerPrefs.HasKey("timeValue"))
        {
            timeValue = PlayerPrefs.GetInt("timeValue");
        }
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "SkeletonEndScene" || SceneManager.GetActiveScene().name == "SkeletonShop")
        {
            
        }
        else
        {
            timer2 += Time.deltaTime;
            if (timer2 >= 1)
            {
                timer2 = 0;
                timeValue++;
                PlayerPrefs.SetInt("timeValue", timeValue);
            }
            if (PlayerPrefs.GetInt("timeValue") >= timeCap)
            {
                isReducingStars = true;
            }
        }
        if (SceneManager.GetActiveScene().name != "SkeletonEndScene" && isReducingStars == true)
        {
            intervalTimer += Time.deltaTime;
        }
        if(intervalTimer >= intervalTime)
        {
            intervalTimer = 0;
            stars--;
            PlayerPrefs.SetInt("stars", stars);
            StarsChanged?.Invoke(stars);
        }
        if (Input.GetKeyDown(KeyCode.F1)) // Resets the totalStars gained.
        {
            ResetPlayerPrefsValues();
        }
        if (Input.GetKeyDown(KeyCode.F2)) // Adds 100 stars
        {
            AddTotalStars();
        }
    }


    private void ResetPlayerPrefsValues()
    {
        PlayerPrefs.DeleteAll();
    }
    private void AddTotalStars()
    {
        PlayerPrefs.SetInt("totalStars", 100);
    }
}

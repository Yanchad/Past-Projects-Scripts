using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    [SerializeField] Image image0;
    [SerializeField] Image image1;
    [SerializeField] Image image2;
    [SerializeField] Image image3;
    [SerializeField] Image image4;
    [SerializeField] Image image5;
    ScoreManager scoreManager;


    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
        scoreManager.StarsChanged += UpdateImageFill;
    }

    private void UpdateImageFill(float stars)
    {
        if (stars == 0)
        {
            image0.gameObject.SetActive(true);
        }
        else image0.gameObject.SetActive(false);

        if (stars == 1)
        {
            image1.gameObject.SetActive(true);
        }
        else image1.gameObject.SetActive(false);

        if (stars == 2)
        {
            image2.gameObject.SetActive(true);
        }
        else image2.gameObject.SetActive(false);
        if (stars == 3)
        {
            image3.gameObject.SetActive(true);
        }
        else image3.gameObject.SetActive(false);
        if (stars == 4)
        {
            image4.gameObject.SetActive(true);
        }
        else image4.gameObject.SetActive(false);
        if (stars == 5)
        {
            image5.gameObject.SetActive(true);
        }
        else image5.gameObject.SetActive(false);
    }
}

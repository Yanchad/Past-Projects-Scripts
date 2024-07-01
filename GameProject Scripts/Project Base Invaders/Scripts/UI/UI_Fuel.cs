using UnityEngine;
using UnityEngine.UI;

public class UI_Fuel : MonoBehaviour, IOnFuelChanged
{

    [SerializeField] private Fuel fuel;
    [SerializeField] Image fuelImage;


    private void OnEnable()
    {
        fuel.RegisterListener(this);
    }
    private void OnDisable()
    {
        fuel.RemoveListener(this);
    }

    public void OnFuelChanged(float fuel)
    {
        fuelImage.fillAmount = fuel * 0.5f;
    }
}

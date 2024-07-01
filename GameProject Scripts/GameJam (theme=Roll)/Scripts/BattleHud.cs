using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dmgText;
    public Slider hpSlider;
    public TextMeshProUGUI currentHpText;
    public TextMeshProUGUI maxHpText;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        dmgText.text = unit.damage.ToString();
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        currentHpText.text = unit.currentHP.ToString();
        maxHpText.text = unit.maxHP.ToString();
    }

    public void SetHP(int hp, int maxHP)
    {
        hpSlider.value = hp;
        currentHpText.text = hp.ToString();
        maxHpText.text = maxHP.ToString();
    }
    public void SetDMG(int dmg)
    {
        dmgText.text = dmg.ToString();
    }
}

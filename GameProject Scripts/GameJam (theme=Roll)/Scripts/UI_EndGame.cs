using UnityEngine;

public class UI_EndGame : MonoBehaviour
{
    [SerializeField] private GameObject youWinImage;
    [SerializeField] private GameObject gameOverImage;

    BattleSystem battleSystem;

    private void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    void Update()
    {
        if(battleSystem.state == BattleState.WON)
        {
            youWinImage.SetActive(true);
        }
        if(battleSystem.state == BattleState.LOST)
        {
            gameOverImage.SetActive(true);
        }
    }
}

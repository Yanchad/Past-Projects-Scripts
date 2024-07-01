using UnityEngine;

public class DiceEnemy : MonoBehaviour
{
    static Rigidbody rbEnemy;

    bool hasLanded;
    bool thrown;
    public bool Thrown { get { return thrown; }set { thrown = value; } }

    public int diceValueEnemy;
    public DiceSideEnemy[] diceSidesEnemy;

    public GameObject enemyGO;

    Unit enemyUnit;
    BattleSystem battleSystem;


    void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();

        battleSystem = FindObjectOfType<BattleSystem>();
        enemyUnit = enemyGO.GetComponent<Unit>();
    }
    void Update()
    {
        if (battleSystem.state == BattleState.ENEMYDICETHROW)
        {
            RollDice();
        }
        else diceValueEnemy = 0;


        if (rbEnemy.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rbEnemy.isKinematic = true;
            SideValueCheck();
            enemyUnit.damage += diceValueEnemy;
        }
    }



    private void RollDice()
    {
        if (!thrown && !hasLanded)
        {

        }
         if (thrown && hasLanded)
        {
            Reset();
        }
    }
    private void Reset()
    {
        thrown = false;
        hasLanded = false;
        rbEnemy.isKinematic = false;
    }
    void SideValueCheck()
    {
        diceValueEnemy = 0;
        foreach (DiceSideEnemy side in diceSidesEnemy)
        {
            if (side.OnGroundEnemy())
            {
                diceValueEnemy = side.sideValueEnemy;
                Debug.Log(diceValueEnemy + " has been rolled!");
            }
        }
    }
}

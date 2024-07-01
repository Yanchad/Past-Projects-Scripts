using UnityEngine;

public class Dice : MonoBehaviour
{
    static Rigidbody rb;

    private bool hasLanded;
    private bool thrown;
    public bool Thrown { get { return thrown; }set { thrown = value; } }
    public bool HasLanded { get { return hasLanded; } set { hasLanded = value; } }

    Vector3 initPosition;

    public int diceValue;

    public DiceSide[] diceSides;

    Unit playerUnit;
    public GameObject playerGO;

    BattleSystem battleSystem;

    public bool hasRolled;
    private bool hasJumped;
    public bool HasJumped { get { return hasJumped; } set { hasJumped = value; } }
    private bool isPressed;
    public bool IsPressed => isPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        battleSystem = FindObjectOfType<BattleSystem>();
        playerUnit = playerGO.GetComponent<Unit>();
        thrown = true;
    }

    
    void Update()
    {
        if (battleSystem.state == BattleState.PLAYERDICETHROW)
        {
            if (Input.GetKeyDown(KeyCode.Space) && hasJumped == false)
            {
                RollDice();
                isPressed = false;
            }
            if (Input.GetKeyUp(KeyCode.Space) && hasJumped == false)
            {
                RollDice();
                isPressed = true;
                hasJumped = true;
            }
            else isPressed = false;
        }
        else diceValue = 0;



        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;


            SideValueCheck();
            playerUnit.damage += diceValue;
        }

        if (diceValue > 0)
        {
            hasRolled = true;
            Debug.Log("hasRolled");
        }
        else hasRolled = false;

    }

    private void RollDice()
    {
        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddForce(Vector3.up * 25, ForceMode.Impulse);
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if (thrown && hasLanded)
        {
            Reset();
        }
    }
    private void Reset()
    {
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    void SideValueCheck()
    {
        diceValue = 0;
        foreach(DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                Debug.Log(diceValue + " has been rolled!");

            }
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERDICETHROW, PLAYERTURN, ENEMYDICETHROW, EVENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject playerGO;
    public GameObject enemyGO;
    private Rigidbody enemyRb;
    [SerializeField] private GameObject enemyDome;
    [SerializeField] private GameObject enemyDomeBroken;
    [SerializeField] private GameObject playerDome;
    [SerializeField] private GameObject playerDomeBroken;
    
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button healButton;


    public BattleHud playerHUD;
    public BattleHud enemyHUD;
    DiceEnemy diceEnemy;
    Dice dice;

    public TrackSwitcher playerSwitch;
    public GameObject playerCam;

    private float timer;
    public float Timer => timer;
    private bool isTiming;
    //int seconds;

    [SerializeField] private Animator hammerAnimator;
    [SerializeField] private Animator domeAnimator;
    [SerializeField] private Animator domeAnimatorBroken;
    [SerializeField] private Animator domeAnimatorEnemy;
    [SerializeField] private Animator domeAnimatorEnemyBroken;

    [SerializeField] private GameObject playerParticle;
    [SerializeField] private GameObject enemyParticle;

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        enemyRb = enemyGO.GetComponent<Rigidbody>();
        dice = FindObjectOfType<Dice>();
    }

    private void Update()
    {

        if (isTiming == true)
        {
            timer -= Time.deltaTime;
            int seconds = ((int)timer % 60);
            dialogueText.text = timer.ToString(string.Format("0", seconds)) + " Seconds to Roll yourself!";
        }
        else timer = 10;
        if (dice.IsPressed == true && playerDome == enabled)
        {
            domeAnimator.SetTrigger("isPressed");
        }
        else if(dice.IsPressed == true && playerDomeBroken == enabled)
        {
            domeAnimatorBroken.SetTrigger("isPressed");
        }
    }
    IEnumerator SetupBattle()
    {
        playerSwitch.ResetCameraToSpectate();
        playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Welcome to Highroller!";

        yield return new WaitForSeconds(2);

        dialogueText.text = "May the best roll win!";

        yield return new WaitForSeconds(5f);
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        state = BattleState.PLAYERDICETHROW;
        StartCoroutine(PlayerDiceThrow());
    }

    IEnumerator PlayerDiceThrow() 
    {
        dice.HasJumped = false;
        playerSwitch.ResetCameraToPlayer();
        diceEnemy = FindObjectOfType<DiceEnemy>();

        diceEnemy.Thrown = false;
        isTiming = true;
        playerUnit.damage = playerUnit.minDamage;

        yield return new WaitForSeconds(10f);

        isTiming = false;
        dialogueText.text = "Your Power has increased by " + dice.diceValue.ToString() + "!";

        yield return new WaitForSeconds(2f);

        playerHUD.SetDMG(playerUnit.damage);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
        attackButton.gameObject.SetActive(true);
        healButton.gameObject.SetActive(true);
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        if (enemyUnit.currentHP <= 0)
        {
            enemyParticle.gameObject.SetActive(true);
        }
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);

        if(enemyUnit.currentHP <= 5)
        {
            enemyDomeBroken.gameObject.SetActive(true);
            enemyDome.GetComponent<MeshRenderer>().enabled = false;
        }
        else 
        {
            enemyDomeBroken.gameObject.SetActive(false);
            enemyDome.GetComponent<MeshRenderer>().enabled = true;
        }
        
        yield return new WaitForSeconds(2f);

        if (isDead)
        { 
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYDICETHROW;
            StartCoroutine(EnemyDiceThrow());
        }

        playerUnit.damage = playerUnit.minDamage;
        playerHUD.SetDMG(playerUnit.damage);
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(1 + playerUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        playerUnit.damage = playerUnit.minDamage;
        playerHUD.SetDMG(playerUnit.damage);

        state = BattleState.ENEMYDICETHROW;
        StartCoroutine (EnemyDiceThrow());
    }
    IEnumerator EnemyDiceThrow()
    {
        playerSwitch.ResetCameraToEnemy();
        dialogueText.text = "Opponent is rolling!";

        yield return new WaitForSeconds(2f);
        if(enemyDome == enabled)
        {
            domeAnimatorEnemy.SetTrigger("isPressed");
        }
        else if (enemyDomeBroken == enabled)
        {
            domeAnimatorEnemyBroken.SetTrigger("isPressed");
        }
        enemyRb.AddForce(Vector3.up * 20, ForceMode.Impulse);
        enemyRb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        diceEnemy.Thrown = true;

        yield return new WaitForSeconds(5f);

        enemyHUD.SetDMG(enemyUnit.damage);
        dialogueText.text = "Power increased by " + diceEnemy.diceValueEnemy.ToString() + "!";

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {

        dialogueText.text = "Opponent attacks!";
        
        yield return new WaitForSeconds(1f);
        hammerAnimator.SetTrigger("isHittingPlayer");
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);

        if (playerUnit.currentHP <= 5)
        {
            playerDomeBroken.gameObject.SetActive(true);
            playerDome.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            playerDomeBroken.gameObject.SetActive(false);
            playerDome.GetComponent<MeshRenderer>().enabled = true;
        }
        if (playerUnit.currentHP <= 0)
        {
            playerParticle.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }else
        {
            state = BattleState.PLAYERDICETHROW;
            StartCoroutine(PlayerDiceThrow());
        }

        enemyUnit.damage = enemyUnit.minDamage;
        enemyHUD.SetDMG(enemyUnit.damage);
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }


    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        hammerAnimator.SetTrigger("isHittingEnemy");
        StartCoroutine(PlayerAttack());
        attackButton.gameObject.SetActive(false);
        healButton.gameObject.SetActive(false);
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerHeal());
        attackButton.gameObject.SetActive(false);
        healButton.gameObject.SetActive(false);
    }
}

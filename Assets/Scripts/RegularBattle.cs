using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class RegularBattle : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public GameObject attackMenu;
    public GameObject targetMenu;
    public GameObject itemMenu;

    public GameManager gm;

    PlayerBattle playerUnit;

    MoldController moldUnitOne;
    MoldController moldUnitTwo;
    MoldController moldUnitThree;
    MoldController moldUnitFour;

    //more controllers go here

    public Transform playerPos;
    public Transform firstEnemyPos;
    public Transform secondEnemyPos;
    public Transform thirdEnemyPos;
    public Transform forthEnemyPos;

    private Button[] targets;
    private Button[] itemButtons;

    public TextMeshProUGUI dialogueText;

    private bool firstFight = true;

    private int target;
    private int numEnemies;
    private int currEnemies;

    private float experienceGained;

    private List<string> loot = new List<string>();
    private List<string> usedItems = new List<string>();

    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
    public BattleState state;

    public void Begin()
    {
        targets = targetMenu.GetComponentsInChildren<Button>();
        itemButtons = itemMenu.GetComponentsInChildren<Button>();

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    { 
        GameObject playerGO = Instantiate(player, playerPos);
        playerUnit = playerGO.GetComponent<PlayerBattle>();

        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        forthEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        if (gm.GetLevel() == 1)
        {
            if (firstFight)
            {
                GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                moldUnitOne = enemy1.GetComponent<MoldController>();
                moldUnitOne.SetLevel(1);
                firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                numEnemies = 1;
                firstFight = false;
                AdjustAttacks();
                PopulateItems();
            }

            else
            {
                int moldSpawnChance = Random.Range(1, 100);

                if(gm.GetExperience() >= 3.0f)
                {
                    if(moldSpawnChance >= 75)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        moldUnitOne.SetLevel(3);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        numEnemies = 1;
                    }

                    else if(moldSpawnChance >= 50)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(3);
                        moldUnitTwo.SetLevel(3);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        numEnemies = 2;
                    }

                    else if(moldSpawnChance >= 25)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();
                        GameObject enemy3 = Instantiate(enemy, thirdEnemyPos);
                        moldUnitThree = enemy3.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(3);
                        moldUnitTwo.SetLevel(3);
                        moldUnitThree.SetLevel(3);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        numEnemies = 3;
                    }

                    else
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();
                        GameObject enemy3 = Instantiate(enemy, thirdEnemyPos);
                        moldUnitThree = enemy3.GetComponent<MoldController>();
                        GameObject enemy4 = Instantiate(enemy, forthEnemyPos);
                        moldUnitFour = enemy4.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(3);
                        moldUnitTwo.SetLevel(3);
                        moldUnitThree.SetLevel(3);
                        moldUnitFour.SetLevel(3);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        forthEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 3";
                        numEnemies = 4;
                    }
                }
                
                else if(gm.GetExperience() >= 2.0f)
                {
                    if (moldSpawnChance >= 67)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        moldUnitOne.SetLevel(2);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        numEnemies = 1;
                    }

                    else if (moldSpawnChance >= 37)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(2);
                        moldUnitTwo.SetLevel(2);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        numEnemies = 2;
                    }

                    else if (moldSpawnChance >= 17)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();
                        GameObject enemy3 = Instantiate(enemy, thirdEnemyPos);
                        moldUnitThree = enemy3.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(2);
                        moldUnitTwo.SetLevel(2);
                        moldUnitThree.SetLevel(2);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        numEnemies = 3;
                    }

                    else
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();
                        GameObject enemy3 = Instantiate(enemy, thirdEnemyPos);
                        moldUnitThree = enemy3.GetComponent<MoldController>();
                        GameObject enemy4 = Instantiate(enemy, forthEnemyPos);
                        moldUnitFour = enemy4.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(2);
                        moldUnitTwo.SetLevel(2);
                        moldUnitThree.SetLevel(2);
                        moldUnitFour.SetLevel(2);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        forthEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 2";
                        numEnemies = 4;
                    }
                }

                else if(gm.GetExperience() >= 1.0f)
                {
                    if (moldSpawnChance >= 50)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        moldUnitOne.SetLevel(1);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        numEnemies = 1;
                    }

                    else if (moldSpawnChance >= 25)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(1);
                        moldUnitTwo.SetLevel(1);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        numEnemies = 2;
                    }

                    else if (moldSpawnChance >= 5)
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();
                        GameObject enemy3 = Instantiate(enemy, thirdEnemyPos);
                        moldUnitThree = enemy3.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(1);
                        moldUnitTwo.SetLevel(1);
                        moldUnitThree.SetLevel(1);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        numEnemies = 3;
                    }

                    else
                    {
                        GameObject enemy1 = Instantiate(enemy, firstEnemyPos);
                        moldUnitOne = enemy1.GetComponent<MoldController>();
                        GameObject enemy2 = Instantiate(enemy, secondEnemyPos);
                        moldUnitTwo = enemy2.GetComponent<MoldController>();
                        GameObject enemy3 = Instantiate(enemy, thirdEnemyPos);
                        moldUnitThree = enemy3.GetComponent<MoldController>();
                        GameObject enemy4 = Instantiate(enemy, forthEnemyPos);
                        moldUnitFour = enemy4.GetComponent<MoldController>();

                        moldUnitOne.SetLevel(1);
                        moldUnitTwo.SetLevel(1);
                        moldUnitThree.SetLevel(1);
                        moldUnitFour.SetLevel(1);
                        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        forthEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LVL 1";
                        numEnemies = 4;
                    }
                }
            }

            currEnemies = numEnemies;
            StartCoroutine(PrintText("MOLD has sensed your presence..."));
            AdjustTargets();
        }

        yield return new WaitForSeconds(4f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void AdjustTargets()
    {
        for (int i = 0; i < 4; i++)
        {
            targets[i].interactable = true;
        }

        for (int i = numEnemies; i < 4; i++)
        {
            targets[i].interactable = false;
        }
    }

    void AdjustAttacks()
    {
        if (gm.GetLevel() == 1)
        {
            RectTransform[] attacks = attackMenu.GetComponentsInChildren<RectTransform>();

            foreach (RectTransform rt in attacks)
            {
                rt.gameObject.SetActive(true);
            }

            for (int i = 3; i < 9; i++)
            {
                attacks[i].gameObject.SetActive(false);
            }

            attacks[0].gameObject.SetActive(false);
        }
    }

    void PopulateItems()
    {
        if(gm.GetInventory().Count == 0)
        {
            for(int i = 0; i < 5; i++)
            {
                itemButtons[i].interactable = false;
            }

            itemButtons[6].interactable = true;
        }

        else
        {
            for(int i = 0; i < gm.GetInventory().Count; i++)
            {
                itemButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = gm.GetInventory()[i].ToUpper(); 
            }

            for(int i = gm.GetInventory().Count; i < itemButtons.Length; i++)
            {
                itemButtons[i].interactable = false;
            }

            itemButtons[6].interactable = true;
        }

    }

    void PlayerTurn()
    {
        StartCoroutine(PrintText("What will you do?"));
    }

    void EnemyTurn()
    {
        if(state != BattleState.ENEMYTURN)
        {
            return;
        }

        if(moldUnitOne.AttemptAttack(playerUnit.GetSpeed(), playerUnit.GetIntimidation(), playerUnit.GetDefense(false)))
        {
            StartCoroutine(PrintText("MOLD uses SPORES"));

            StartCoroutine(EnemyAttack(moldUnitOne.ReturnDamage()));
        }

        else
        {
            StartCoroutine(PrintText("MOLD misses attack"));

            if(numEnemies > 1)
            {
                StartCoroutine(EnemyTwoTurn()); 
            }

            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
    }

    IEnumerator EnemyTwoTurn()
    {
        if (moldUnitTwo.AttemptAttack(playerUnit.GetSpeed(), playerUnit.GetIntimidation(), playerUnit.GetDefense(false)))
        {
            StartCoroutine(PrintText("MOLD 2 uses SPORES"));
            playerUnit.TakeDamage(moldUnitTwo.ReturnDamage());
        }

        else
        {
            StartCoroutine(PrintText("MOLD 2 misses its attack"));
        }

        yield return new WaitForSeconds(2f);

        if (playerUnit.currHealth <= 0.0f)
        {
            //YOU LOSE SIR, GOOD DAY
        }

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EnemyThreeTurn()
    {
        if (moldUnitThree.AttemptAttack(playerUnit.GetSpeed(), playerUnit.GetIntimidation(), playerUnit.GetDefense(false)))
        {
            StartCoroutine(PrintText("MOLD 3 uses SPORES"));
            playerUnit.TakeDamage(moldUnitThree.ReturnDamage());
        }

        else
        {
            StartCoroutine(PrintText("MOLD 3 misses its attack"));
        }

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTwoTurn());
    }

    IEnumerator EnemyFourTurn()
    {
        if (moldUnitFour.AttemptAttack(playerUnit.GetSpeed(), playerUnit.GetIntimidation(), playerUnit.GetDefense(false)))
        {
            StartCoroutine(PrintText("MOLD 4 uses SPORES"));
            playerUnit.TakeDamage(moldUnitFour.ReturnDamage());
        }

        else
        {
            StartCoroutine(PrintText("MOLD 4 misses its attack"));
        }

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyThreeTurn());
    }

    IEnumerator EnemyAttack(float damage)
    {
        if(currEnemies == 1)
        {
            playerUnit.TakeDamage(damage);
            yield return new WaitForSeconds(2f);

            if(playerUnit.currHealth <= 0.0f)
            {
                Debug.Log("YOU LOSE");
            }

            state = BattleState.PLAYERTURN;
            PlayerTurn(); 
        }

        else if(currEnemies == 2)
        {
            playerUnit.TakeDamage(damage);
            yield return new WaitForSeconds(2f);

            StartCoroutine(EnemyTwoTurn());
        }

        else if(currEnemies == 3)
        {
            playerUnit.TakeDamage(damage);
            yield return new WaitForSeconds(2f);

            StartCoroutine(EnemyThreeTurn());
        }

        else if(currEnemies == 4)
        {
            playerUnit.TakeDamage(damage);
            yield return new WaitForSeconds(2f);

            StartCoroutine(EnemyFourTurn());
        }
    }

    public void OnAttackButton()
    {
        //...
    }

    public void OnAttackMoveButton(int moveNum)
    {
        //later
    }

    public void OnTargetButton(int tarNum)
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }

        target = tarNum;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        if(target == 1)
        {
            if (playerUnit.AttemptAttack())
            {
                moldUnitOne.TakeDamage(playerUnit.GetAttackDamage());

                if (moldUnitOne.currHealth <= 0.0f)
                {
                    currEnemies--;
                    experienceGained += moldUnitOne.GetLevel() * 0.2f;
                    StartCoroutine(RemoveEnemy(moldUnitOne, 2f, 1));
                }
            }
        }

        else if(target == 2)
        {
            if (playerUnit.AttemptAttack())
            {
                moldUnitTwo.TakeDamage(playerUnit.GetAttackDamage());

                if (moldUnitTwo.currHealth <= 0.0f)
                {
                    currEnemies--;
                    experienceGained += moldUnitTwo.GetLevel() * 0.2f;
                    StartCoroutine(RemoveEnemy(moldUnitTwo, 2f, 2));
                }
            }
        }

        else if(target == 3)
        {
            if (playerUnit.AttemptAttack())
            {
                moldUnitThree.TakeDamage(gm.playerBattle.GetComponent<PlayerBattle>().GetAttackDamage());

                if (moldUnitThree.currHealth <= 0.0f)
                {
                    currEnemies--;
                    experienceGained += moldUnitThree.GetLevel() * 0.2f;
                    StartCoroutine(RemoveEnemy(moldUnitThree, 2f, 3));
                }
            }
        }

        else if(target == 4)
        {
            if (playerUnit.AttemptAttack())
            {
                moldUnitFour.TakeDamage(gm.playerBattle.GetComponent<PlayerBattle>().GetAttackDamage());

                if (moldUnitFour.currHealth <= 0.0f)
                {
                    currEnemies--;
                    experienceGained += moldUnitFour.GetLevel() * 0.2f;
                    StartCoroutine(RemoveEnemy(moldUnitFour, 2f, 4));
                }
            }
        }

        if (currEnemies == 0)
        {
            string endBattle = "You recieved " + experienceGained * 10 + " XP";
            float delay = 5f;

            if (gm.GetLevel() == 1)
            {
                int chanceGlue = Random.Range(1, 100);

                if(chanceGlue <= 5)
                {
                    loot.Add("Glue");
                    endBattle += "\nMOLD dropped some glue. Strange. How did it get a chunk of glue?";
                    delay += 15f;
                }

                int chanceSoap = Random.Range(1, 100);

                if (chanceSoap >= 80)
                {
                    loot.Add("Soap");
                    endBattle += "\nMOLD dropped a bit of soap. Could be useful.";
                    delay += 10f;
                }
            }

            StartCoroutine(PrintText(endBattle));

            yield return new WaitForSeconds(delay);

            gm.AddLoot(loot);
            gm.SetExperiencePoints(experienceGained); 
            state = BattleState.WON;
            FreeRoam();
        }

        else
        {
            yield return new WaitForSeconds(3f);

            state = BattleState.ENEMYTURN;
            EnemyTurn();
        }
    }

    IEnumerator RemoveEnemy(MoldController defeated, float wait, int target)
    {
        yield return new WaitForSeconds(wait);

        defeated.gameObject.SetActive(false);

        if(target == 1)
        {
            firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }

        if (target == 2)
        {
            secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }

        if (target == 3)
        {
            thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }

        if (target == 4)
        {
            forthEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }

        if (currEnemies != 0)
        {
            targets[target - 1].interactable = false;
        }
    }

    public void OnItemButton(int item)
    {
        if(gm.GetInventory()[item - 1] == "Glue")
        {
            StartCoroutine(PrintText("You should hold on to this..."));
        }

        else if(gm.GetInventory()[item - 1] == "Soap")
        {
            StartCoroutine(PrintText("You used " + gm.GetInventory()[item - 1]) + " and regained 2 health");
            usedItems.Add(gm.GetInventory()[item - 1]);
            itemButtons[item - 1].interactable = false;

            if(playerUnit.currHealth + 2.0f > playerUnit.GetMaxHealth())
            {
                playerUnit.currHealth = playerUnit.GetMaxHealth();
            }

            else
            {
                playerUnit.currHealth += 2.0f;
            }
        }

        Invoke("PlayerActionDelay", 2f);
    }

    void PlayerActionDelay()
    {
        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    public void OnDefendButton()
    {
        StartCoroutine(PrintText("Coward"));

        Invoke("PlayerActionDelay", 2f);
    }

    public void OnFleeButton()
    {
        int chanceFlee = Random.Range(1, 100);

        if(gm.GetLevel() == 1)
        {
            if(chanceFlee >= 80)
            {
                StartCoroutine(PrintText("Suffer"));
                Invoke("PlayerActionDelay", 2f);
            }

            else
            {
                FreeRoam();
            }
            
        }

        if(gm.GetLevel() == 2)
        {
            if (chanceFlee >= 60)
            {
                StartCoroutine(PrintText("Suffer"));
                Invoke("PlayerActionDelay", 2f);
            }

            else
            {
                FreeRoam();
            }
        }

        if(gm.GetLevel() == 3)
        {
            if (chanceFlee >= 40)
            {
                StartCoroutine(PrintText("Suffer"));
                Invoke("PlayerActionDelay", 2f);
            }

            else
            {
                FreeRoam();
            }
        }
    }

    void FreeRoam()
    {
        firstEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        secondEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        thirdEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        forthEnemyPos.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        Destroy(playerUnit.gameObject);

        if (numEnemies == 1)
        {
            Destroy(moldUnitOne.gameObject);
        }

        else if (numEnemies == 2)
        {
            Destroy(moldUnitOne.gameObject);
            Destroy(moldUnitTwo.gameObject);
        }

        else if (numEnemies == 3)
        {
            Destroy(moldUnitOne.gameObject);
            Destroy(moldUnitTwo.gameObject);
            Destroy(moldUnitThree.gameObject);
        }

        else if (numEnemies == 4)
        {
            Destroy(moldUnitOne.gameObject);
            Destroy(moldUnitTwo.gameObject);
            Destroy(moldUnitThree.gameObject);
            Destroy(moldUnitFour.gameObject);
        }

        gm.EndRegularBattle();
    }

    IEnumerator PrintText(string t)
    {
        dialogueText.text = "";
        for(int i = 0; i < t.Length; i++)
        {
            dialogueText.text += t[i];
            yield return new WaitForSeconds(.1f);
        }
    }
}

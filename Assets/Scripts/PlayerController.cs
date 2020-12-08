using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public static int level = 1;
    //private static float XP = 1.0f;

    public bool inBattle;
    private bool isMoving;

    public float moveSpeed;

    public LayerMask EnemyTile;
    public LayerMask BossTile;

    private Vector2 input;

    private Animator animator;

    public GameManager gm;

    public TextMeshProUGUI dialogueText;

    private bool printing; 

    private void Awake()
    {
        inBattle = false;
        printing = false;
        animator = GetComponent<Animator>();
        dialogueText.text = "";
    }

    private void Update()
    {
        inBattle = gm.battling;

        if (!inBattle)
        {
            if (!isMoving)
            {
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");

                if (input.x != 0)
                {
                    input.y = 0;
                }

                if (input != Vector2.zero)
                {
                    animator.SetFloat("moveX", input.x);
                    animator.SetFloat("moveY", input.y);

                    var targetPos = transform.position;
                    targetPos.x += input.x;
                    targetPos.y += input.y;

                    StartCoroutine(Move(targetPos));
                }
            }

            animator.SetBool("isMoving", isMoving);
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null; 
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounter();
    }

    void CheckForEncounter()
    {
        if(Physics2D.OverlapBox(transform.position, new Vector2(0.3f, 0.2f), 0.0f, EnemyTile) != null)
        {
            int chanceBattle = Random.Range(1, 101);
            Debug.Log(chanceBattle);
            if(chanceBattle <= 10)
            {
                gm.StartRegularBattle();
            }
        }

        else if(Physics2D.OverlapBox(transform.position, new Vector2(0.3f, 0.2f), 0.0f, BossTile) != null)
        {
            if(gm.GetExperience() >= 3.0f)
            {
                gm.StartFilterBattle();
            }
            
            else if(!printing)
            {
                StartCoroutine(PrintText("SOMETHING EVIL LURKS HERE... I SHOULD AVOID IT FOR NOW"));
                printing = true;
            }
        }
    }

    IEnumerator PrintText(string t)
    {
        dialogueText.text = "";
        float wait = 0.0f;
        for (int i = 0; i < t.Length; i++)
        {
            dialogueText.text += t[i];
            yield return new WaitForSeconds(.1f);
            wait += 0.1f;
        }

        yield return new WaitForSeconds(wait);

        dialogueText.text = "";
        printing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerRoam;
    public GameObject playerBattle;
    public GameObject hairTiesBattle;

    public static int level = 1;
    public static float XP = 1.0f;

    public Camera mc;
    public Camera bc;

    public Canvas battleCanvas;

    public GameObject RegularMenu;
    public GameObject BossMenu;

    public bool battling;

    private static List<string> inventory = new List<string>();

    public void Awake()
    {
        //inventory.Add("Soap");
        //inventory.Add("Glue"); 

        //FOR TESTING

        inventory.Add("Soap");
        inventory.Add("Soap");
        inventory.Add("Soap");

        //END
        battling = false;
    }

    public void StartRegularBattle()
    {
        battling = true;
        mc.gameObject.SetActive(false);
        bc.gameObject.SetActive(true);

        RegularMenu.gameObject.SetActive(true);
        BossMenu.gameObject.SetActive(false);

        battleCanvas.GetComponent<RegularBattle>().Begin();
    }

    public void EndRegularBattle()
    {
        battling = false;
        mc.gameObject.SetActive(true);
        bc.gameObject.SetActive(false);

        RegularMenu.gameObject.SetActive(false);
        BossMenu.gameObject.SetActive(false);
    }

    public void StartFilterBattle()
    {
        battling = true;
        mc.gameObject.SetActive(false);
        bc.gameObject.SetActive(true);

        RegularMenu.gameObject.SetActive(false);
        BossMenu.gameObject.SetActive(true);

        battleCanvas.GetComponent<FitlerBattle>().Begin(); 
    }

    public void EndFitlerBattle()
    {
        //to be continued scene
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetExperiencePoints(float change)
    {
        XP += change; 
    }

    public float GetExperience()
    {
        return XP;
    }

    public List<string> GetInventory()
    {
        return inventory;
    }

    public void AddLoot(List<string> loot)
    {
        foreach(string item in loot)
        {
            inventory.Add(item); 
        }
    }

    public void UseItem(string item)
    {
        inventory.Remove(item);
    }
}

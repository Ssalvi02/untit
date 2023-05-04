using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillboxScript : MonoBehaviour
{
    private BattleController bc;

    private void Awake()
    {
        bc = GameObject.Find("GameControl").GetComponent<BattleController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bc.PlayerDamage();    
    }
}

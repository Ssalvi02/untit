using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [Header("Enemy Attribs")]
    private int e_dmg;
    private int e_life;
    private EnemyBattleScript es;

    [Header("Player Attribs")]
    private int p_dmg;
    private int p_life;
    private PlayerBattleScript ps;

    void Start()
    {
        es = GameObject.Find("Enemy").GetComponent<EnemyBattleScript>();
        ps = GameObject.Find("Player").GetComponent<PlayerBattleScript>();

        e_dmg = es.enemy_dmg;
        e_life = es.current_enemy_health;

        p_dmg = ps.player_dmg;
        p_life = ps.current_player_health;
    }
    void Update()
    {
        
    }
}

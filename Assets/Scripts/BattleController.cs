using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BattleController : MonoBehaviour
{
    [Header("Enemy Attribs")]
    public EnemyBattleScript es;

    [Header("Player Attribs")]
    public GameObject p_life_hud;
    public GameObject player;
    public PlayerBattleScript ps;

    [Header("Misc")]
    public Animator trans;
    public Animator hud_anim;

    private void Awake()
    {
        es = GameObject.Find("Enemy").GetComponent<EnemyBattleScript>();
        ps = GameObject.Find("Player").GetComponent<PlayerBattleScript>();
        trans = GameObject.Find("/Canvas/BattleTrans").GetComponent<Animator>();
        hud_anim = p_life_hud.GetComponent<Animator>();
        player = GameObject.Find("Player").gameObject;
    }

    void Start()
    {
        p_life_hud.SetActive(false);
    }
    void Update()
    {
        
    }

    public void PlayerDamage()
    {
        //Tira vida;
        ps.current_player_health -= es.enemy_dmg;
        //Fade out + reseta o player;
        StartCoroutine(PlayerReset());
    }

    IEnumerator PlayerReset()
    {
        trans.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(0.5f);
        player.transform.position = new Vector3(-7.25f, -3.75f, 0f);
        Time.timeScale = 0f;
        trans.SetTrigger("Reset");
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
        //Instancia texto em cima do player;
        StartCoroutine(PlayerLifeText());
    }

    IEnumerator PlayerLifeText()
    {
        p_life_hud.GetComponentInChildren<TextMeshPro>().text = ps.current_player_health.ToString() + "/" + "10";
        p_life_hud.SetActive(true);
        yield return new WaitForSeconds(1f);
        hud_anim.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        p_life_hud.SetActive(false);
    }
}

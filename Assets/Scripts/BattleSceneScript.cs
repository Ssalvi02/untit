using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneScript : MonoBehaviour
{
    [SerializeField] string scene_name;
    [SerializeField] Animator anim;
    [SerializeField] GameObject trans;
    void Start()
    {
        trans.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadBattleScene());
    }

    IEnumerator LoadBattleScene()
    {
        trans.SetActive(true);
        Time.timeScale = 0;
        anim.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(scene_name);
        Time.timeScale = 1;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleScript : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int total_enemy_health;
    public int current_enemy_health;
    public int enemy_dmg;
    [Header("Misc")]
    [SerializeField] PlayerBattleScript player;
    [SerializeField] Transform player_loc;
    [SerializeField] Camera cam;
    [SerializeField] float zoom_amount = 2f;
    [SerializeField] float zoom_time = 0.2f;
    private float vel_damp;
    private bool not_on_trigger;
    private Vector3 vel_damp_v3;
    private Vector3 original_cam_pos = new Vector3(0f, 0f, -10f);
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerBattleScript>();
        player_loc = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(not_on_trigger && (cam.orthographicSize != 5f || cam.transform.position != original_cam_pos))
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 5f, ref vel_damp, zoom_time);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, original_cam_pos, ref vel_damp_v3, zoom_time);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.is_dashing)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom_amount, ref vel_damp, zoom_time);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, player_loc.position, ref vel_damp_v3, zoom_time);
        }
        else
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 5f, ref vel_damp, zoom_time);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, original_cam_pos, ref vel_damp_v3, zoom_time);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        not_on_trigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        not_on_trigger = false;
    }
}

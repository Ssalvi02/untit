using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleScript : MonoBehaviour
{
    public CapsuleCollider2D cc;
    public LayerMask GroundLayer;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    private Vector3 velsize;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float horiz_move;
    [SerializeField] private float vert_move;
    public bool can_control;

    [Header("Jump")]
    [SerializeField] private float jump_force;
    [SerializeField] private float jump_time;
    [SerializeField] private float jump_time_count;
    [SerializeField] private float cj_time;
    [SerializeField] private float cj_time_count;
    [SerializeField] private float bj_time;
    [SerializeField] private float bj_time_count;
    [SerializeField] private bool is_jumping;
    [SerializeField] private bool is_grounded;

    [Header("Dash")]
    [SerializeField] private float dash_vel;
    [SerializeField] private float dash_time;
    [SerializeField] private Vector2 dash_dir;
    [SerializeField] private bool is_dashing;
    [SerializeField] private bool dash_unlocked = false;
    [SerializeField] private bool can_dash = true;
    public TrailRenderer tr;



    [Header("Double Jump")]
    [SerializeField] private float jump_time_count_dj;
    [SerializeField] private bool dj_unlocked = false;
    [SerializeField] private bool can_dj;
    [SerializeField] private bool just_dj;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
        can_control = true;
    }
    // Update is called once per frame
    void Update()
    {

        //Controla o dash
        if (dash_unlocked)
        {
            bool dash_input = Input.GetButtonDown("Dash");

            if (dash_input && can_dash)
            {
                tr.emitting = true;
                is_dashing = true;
                can_dash = false;
                can_control = false;
                dash_dir = new Vector2(horiz_move, vert_move);

                if (dash_dir == Vector2.zero)
                {
                    dash_dir = Vector2.up;
                }
                StartCoroutine(StopDash());
            }

            if (is_dashing)
            {
                rb.velocity = dash_dir.normalized * dash_vel;
                return;
            }
        }

        //Check se ta no chao
        is_grounded = Physics2D.Raycast(cc.bounds.center, Vector2.down, 0.3f, GroundLayer);


        //Flipa sprite
        Flip();


        //Limitar velocidade de queda
        if (rb.velocity.y < -10f)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -10f, jump_force));
        }

        //Coyote time (Pulo fora do chao)
        if (is_grounded)
        {
            can_dash = true;
            can_control = true;
            just_dj = false;
            can_dj = false;
            cj_time_count = cj_time;
        }
        else
        {
            cj_time_count -= Time.deltaTime;
        }

        //Controla o buffer do pulo
        if (Input.GetButtonDown("Jump"))
        {
            bj_time_count = bj_time;
        }
        else
        {
            bj_time_count -= Time.deltaTime;
        }

        //Controle de pulo
        if (bj_time_count > 0f && cj_time_count > 0f)
        {
            is_jumping = true;
            jump_time_count_dj = jump_time;
            jump_time_count = jump_time;
            Jump();
            bj_time_count = 0f;
        }

        if (Input.GetButton("Jump") && is_jumping)
        {
            if (jump_time_count > 0)
            {
                Jump();
                jump_time_count -= Time.deltaTime;
            }
            else
            {
                is_jumping = false;
            }
        }


        if (Input.GetButton("Jump") && can_dj && dj_unlocked)
        {
            just_dj = true;
            if (jump_time_count_dj > 0)
            {
                Jump();
                jump_time_count_dj -= Time.deltaTime;
            }
            else
            {
                can_dj = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (just_dj == false)
            {
                if (can_dj == true)
                {
                    can_dj = false;
                }
                else
                {
                    can_dj = true;
                }
            }
            else
            {
                can_dj = false;
            }
            is_jumping = false;
            cj_time_count = 0f;
        }

    }
    void FixedUpdate()
    {
        if (can_control)
        {
            horiz_move = Input.GetAxisRaw("Horizontal");
            vert_move = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(horiz_move * speed, rb.velocity.y);
        }

    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dash_time);
        can_control = true;
        tr.emitting = false;
        is_dashing = false;
    }


    private void Flip()
    {
        if (horiz_move > 0.01f)
        {
            sr.flipX = false;
        }
        else if (horiz_move < -0.01f)
        {
            sr.flipX = true;
        }
    }

    private void Jump()
    {
        if (can_control)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump_force);
        }
    }
}

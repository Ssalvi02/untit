using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DScript : MonoBehaviour
{
    private float horiz_move;
    private float vert_move;
    [SerializeField] private float move_speed = 10f;
    private Rigidbody2D rb;
    private Vector2 velref;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        horiz_move = Input.GetAxisRaw("Horizontal");
        vert_move = Input.GetAxisRaw("Vertical");
        rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(horiz_move, vert_move).normalized * move_speed, ref velref, 0.05f);
    }

}

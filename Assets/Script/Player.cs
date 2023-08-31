using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputDir;
    public Rigidbody2D rigid;
    public SpriteRenderer spriter;
    public Animator anim;
    public float speed;

    
    private void FixedUpdate()
    {
        Vector2 nextVec = inputDir * speed * Time.fixedDeltaTime;
        rigid.MovePosition(nextVec + rigid.position);
    }

    void OnMove(InputValue val)
    {
        inputDir = val.Get<Vector2>();
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputDir.magnitude);

        if(inputDir.x != 0)
            spriter.flipX = inputDir.x < 0;
        


    }
}

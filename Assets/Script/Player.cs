using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    Vector2 inputDir;
    public Rigidbody2D rigid;
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
}

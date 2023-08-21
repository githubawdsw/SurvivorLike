using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 inputDir;
    public Rigidbody2D rigid;
    public float speed;

    void Start()
    {
        
    }

    
    void Update()
    {
        // πÊ«‚≈∞
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
    }
    
    private void FixedUpdate()
    {
        Vector2 nextVec = inputDir.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(nextVec + rigid.position);
    }
}

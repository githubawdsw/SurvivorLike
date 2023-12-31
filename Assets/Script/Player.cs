using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputDir;
    public float speed;

    public Rigidbody2D rigid;
    public SpriteRenderer spriter;
    public Animator anim;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    private void Awake()
    {
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.Instance.playerId];
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputDir * speed * Time.fixedDeltaTime;
        rigid.MovePosition(nextVec + rigid.position);
    }

    void OnMove(InputValue val)
    {
        inputDir = val.Get<Vector2>();
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        anim.SetFloat("Speed", inputDir.magnitude);

        if(inputDir.x != 0)
            spriter.flipX = inputDir.x < 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive) 
            return;

        GameManager.Instance.health -= Time.deltaTime * 10;
        if(GameManager.Instance.health < 0f)
        {
            for (int i = 2; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
            anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
        
    }
}

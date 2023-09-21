using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                float disX = playerPos.x - myPos.x;
                float disY = playerPos.y - myPos.y;

                float dirX = disX < 0 ? -1 : 1;
                float dirY = disY < 0 ? -1 : 1;

                disX = Mathf.Abs(disX);
                disY = Mathf.Abs(disY);

                if (disX > disY)
                    transform.Translate(Vector3.right * dirX * 40);
                else if (disX < disY)
                    transform.Translate(Vector3.up * dirY * 40);
                break;

            case "Enermy":
                if(coll.enabled)
                {
                    Vector3 distance = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + distance * 2);
                }
                break;
        }
    }
}

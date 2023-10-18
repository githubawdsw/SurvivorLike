using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class BulletFixedupdate : MonoBehaviour
{
    Vector3 targetDir;
    Vector3 targetPos;
    Vector3 startPos;
    Vector3 reverseDir;
    Vector3 reversePos;

    float timer;
    bool check;
    int id;

    public void Init( Vector3 dir, int id)
    {
        targetDir = dir;
        this.id = id;
        check = false;
        timer = 0.35f;

        targetPos = transform.position + (targetDir * 6);
        startPos = transform.position;

        reverseDir = startPos - targetPos;
        reverseDir.Normalize();
        reversePos = startPos + (reverseDir * 100);
    }

    private void FixedUpdate()
    {
        if (id == 6)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * 120f);
            if (!check)
            {
                timer -= Time.deltaTime * 0.4f;
                transform.position = Vector3.MoveTowards(transform.position , targetPos , timer);

                if (timer < 0.15f)
                {
                    timer = 0f;
                    check = true;
                }
            }
            if (check)
            {
                timer += Time.deltaTime * 0.3f;
                transform.position = Vector3.MoveTowards(transform.position, reversePos * 10000, timer);
            }
        }
    }
}

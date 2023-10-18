using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int id;      // Scriptable Obj Id
    public int prefabsId;
    public float damage;
    public int count;
    public float speed;
    Player player;

    float timer;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire1();
                }
                break;
            case 6:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire2();
                }
                break;
            default:
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (id == 0 || (id == 5 && count == 1))
            Setin();
        if (id == 6)
            transform.localScale *= count + 1;
        player.BroadcastMessage("ApplyGear" , SendMessageOptions.DontRequireReceiver);
    }

    public void Init(Itemdata data)
    {
        // Basic Seting
        name = "Weapon" + (data.itemId);
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Seting
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        for (int index = 0; index < GameManager.Instance.pool.prefabs.Length; index++)
        {
            if(data.projectile == GameManager.Instance.pool.prefabs[index])
            {
                prefabsId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150f * Character.WeaponSpeed;
                Setin();
                break;
            case 1:
                speed = 0.5f * Character.WeaponRate; 
                break;
            case 5:
                Setin();
                break;
            case 6:
                speed = 5f;
                Fire2();
                break;

            default:
                break;
        }

        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear" , SendMessageOptions.DontRequireReceiver);
    }

    void Setin() 
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
                bullet = transform.GetChild(i);
            else
            {
                bullet = GameManager.Instance.pool.Get(prefabsId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero , id);     // -100 : Infinity Per.
            if(id == 0)
            {
                Vector3 rotVec = Vector3.forward * 360 * i / count;
                bullet.Rotate(rotVec);
                bullet.Translate(bullet.up * 1.5f, Space.World);
            }
            if(id == 5)
            {
                bullet.localScale += new Vector3(0.03f * i, 0.03f * i, 0.03f * i);
            }
        }
    }

    void Fire1() // 엽총 공격
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.Get(prefabsId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, 1, dir , id);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);
    }

    void Fire2() // 커지는 원거리 무기
    {
       
        if (!player.scanner.nearestTarget)
            return;

        Transform bullet;
        bullet = GameManager.Instance.pool.Get(prefabsId).transform;
        bullet.parent = transform;
            

        bullet.localPosition = Vector3.zero;
        bullet.localRotation = Quaternion.identity;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;

        dir = dir.normalized;

        bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero, id);
        bullet.GetComponent<BulletFixedupdate>().Init(dir, id);


        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);
    }
}

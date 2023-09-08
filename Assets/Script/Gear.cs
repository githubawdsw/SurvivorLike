using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public Itemdata.ItemType type;
    public float rate;

    public void Init(Itemdata data)
    {
        // Basic Seting
        name = "Gear" + data.itemId;
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Seting
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUP(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case Itemdata.ItemType.Glove:
                RateUp();
                break;
            case Itemdata.ItemType.Shoe:
                SpeedUp();
                break;

        }
    }

    void RateUp()
    {
        Weapons[] weapons = transform.parent.GetComponentsInChildren<Weapons>();
        foreach (var weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3; //default speed
        GameManager.Instance.player.speed = speed + speed * rate;
    }
}

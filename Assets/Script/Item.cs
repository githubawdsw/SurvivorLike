using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Itemdata data;
    public int level;
    public Weapons weapon;
    public Gear gear;

    Image Icon;
    Text textLevel;
    private void Awake()
    {
        Icon = GetComponentsInChildren<Image>()[1];
        Icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = "LV." + (level + 1);
    }

    public void onCLick()
    {
        switch (data.itemType)
        {
            case Itemdata.ItemType.Melee:
            case Itemdata.ItemType.Range:
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon =  newWeapon.AddComponent<Weapons>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            case Itemdata.ItemType.Glove:
            case Itemdata.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUP(nextRate);
                }
                    break;
            case Itemdata.ItemType.Heal:
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                break;
        }
        level++;
        
        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }

    }
}

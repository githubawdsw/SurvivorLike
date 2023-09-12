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
    Text textName;
    Text textDesc;
    private void Awake()
    {
        Icon = GetComponentsInChildren<Image>()[1];
        Icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = "LV." + (level + 1);

        switch (data.itemType)
        {
            case Itemdata.ItemType.Melee:
            case Itemdata.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            case Itemdata.ItemType.Glove:
            case Itemdata.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }

    public void OnCLick()
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

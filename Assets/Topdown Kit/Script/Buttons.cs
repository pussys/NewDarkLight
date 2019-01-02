using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    public GameObject Bag, Skill, Equip, Status,Shop;
    bool isBag=false, isSkill=false, isEquip=false, isStatus=false,isShop=false;
    public static Buttons instance;
    public Buttons()
    {
        instance = this;
    }

    private void Start()
    {
        Bag.SetActive(false);
        Skill.SetActive(false);
        Equip.SetActive(false);
        Status.SetActive(false);
        Shop.SetActive(false);
    }
    public void BagClick()
    {
        isBag = !isBag;
        if (isBag)
        {
            Bag.SetActive(true);
            Bag.transform.SetAsLastSibling();
        }
        else
        {
            Bag.SetActive(false);
        }
    }
    public void SkillClick()
    {
        isSkill = !isSkill;
        if (isSkill)
        {
            Skill.SetActive(true);
            Skill.transform.SetAsLastSibling();
        }
        else
        {
            Skill.SetActive(false);

        }
    }
    public void EquipClick()
    {
        isEquip = !isEquip;
        if (isEquip)
        {
            Equip.SetActive(true);
            Equip.transform.SetAsLastSibling();
        }
        else
        {
            Equip.SetActive(false);

        }
    }
    public void StatusClick()
    {
        isStatus = !isStatus;
        if (isStatus)
        {
            Status.SetActive(true);
            Status.transform.SetAsLastSibling();
        }
        else
        {
            Status.SetActive(false);

        }
    }
    public void ShopClick()
    {
        isShop = !isShop;
        if (isShop)
        {
            Shop.SetActive(true);
            Shop.transform.SetAsLastSibling();
        }
        else
        {
            Shop.SetActive(false);
        }
    }
}

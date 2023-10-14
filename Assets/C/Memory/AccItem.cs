using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Image
using UnityEngine.SceneManagement;
using TMPro;

public class AccItem : MonoBehaviour
{
    public Image icon;
    public TMP_Text amount;

    public Acc originAcc;
    public Vector3 originPos;
    public AccItem parent;

    public int real_amount;
    Scene scene;

    void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void SetUp(Acc acc, int num = 0)
    {
        originAcc = acc;

        icon.sprite = acc.icon;

        if (num != 0)
        {
            real_amount = num;
            string dummy = "x" + num.ToString();
            amount.text = dummy;
        }
        else
            amount.text = "";
    }

    public void Plus_amount(int num = 1)
    {
        real_amount += num;
        string dummy = "x" + real_amount.ToString();
        amount.text = dummy;
    }

    public void OnMouseOver()
    {
        //if(AccManager.Inst.select == null)
            Tooltip.Inst.SetUp(originAcc);
    }

    public void OnMouseExit()
    {
        Tooltip.Inst.CloseSet();
    }

    public void OnMouseDown()
    {
        if (scene.name != "Memory")
            return;

        if (amount.text == "x0" || AccManager.Inst.isDrag)
            return;

        if(icon.color == new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f))
            return;

        originPos = gameObject.transform.position;
        Tooltip.Inst.CloseSet();
        AccManager.Inst.AccMouseDown(this);
    }

    public void OnMouseUp()
    {
        if (scene.name != "Memory")
            return;

        if (!AccManager.Inst.isDrag)return;

        AccManager.Inst.AccMouseUp(this);
        Ray_Put();
    }

    void Ray_Put()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.tag == "Equip")
            {
                real_amount -= 1;
                hit.transform.gameObject.GetComponent<Equip>().Equip_item(this);
            }
            else if (hit.transform.gameObject.tag == "Combine")
            {
                hit.transform.gameObject.GetComponent<Equip>().Equip_item(this);
            }
            else
            {
                Plus_amount(1);
            }
        }
        else
            Plus_amount(1);
    }

    #region ¼ö·®
    public void AmountCheck(int num)
    {
        string text = "";
        TMP_Text TMP_amount = null;
        Image Image_icon = null;

        
        if (amount.text == "")
        {
            text = parent.amount.text.Substring(1);
            TMP_amount = parent.amount;
            Image_icon = parent.icon;
        }
        else
        {
            text = amount.text.Substring(1);
            TMP_amount = amount;
            Image_icon = icon;
        }

        int amount_num = int.Parse(text);
        int addrass = Player.Inst.playerdata.ItemCollect.FindIndex(x => x.name == originAcc.name);
        if (addrass != -1)
        {
            Player.Inst.playerdata.ItemCollect[addrass].amount = amount_num + num;

            TMP_amount.text = "x" + (amount_num + num).ToString();
        }
        Player.Inst.Save();
    }
    #endregion

    public void DestroyItem()
    {
        if (amount.text == "x0")
        {
            int dummy = 0;
            for (int i = 0; i < AccManager.Inst.CombineItem.Count; i++)
            {
                if (AccManager.Inst.CombineItem[i].name == originAcc.name)
                    dummy++;
            }
            if (dummy == 0)
            {
                int destroy_addrass = Player.Inst.playerdata.ItemCollect.FindIndex(x => x.name == originAcc.name);
                Player.Inst.playerdata.ItemCollect.RemoveAt(destroy_addrass);

                destroy_addrass = AccManager.Inst.isMine.FindIndex(x => x.originAcc.name == originAcc.name);
                AccManager.Inst.isMine.RemoveAt(destroy_addrass);

                Destroy(gameObject);
                Player.Inst.Save();
            }
        }
    }
}

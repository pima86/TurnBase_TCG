using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public SpriteRenderer icon;
    [SerializeField] SpriteRenderer rank_icon;
    [SerializeField] Light light;

    [SerializeField] bool thisCombine;
    [SerializeField] bool thisResult;

    bool isSelect = false;
    public AccItem acc = null;
    public Acc originAcc = null;
    public void Equip_item(AccItem item)
    {
        if (thisCombine) //���� ������
        {
            if (acc != null)
                MouseUp();

            AccManager.Inst.CombineItem.Add(item.originAcc);
            acc = item;
            originAcc = item.originAcc;
            icon.sprite = item.originAcc.icon;
            Item_light(item.originAcc.rank);
            Debug.Log("�������� " + item.name + "�� �߰��Ǿ����ϴ�.");

            AccManager.Inst.Result(AccManager.Inst.ItemCombine());

            return;
        }
        else if (thisResult) //�ϼ� ������
        {
            item.AmountCheck(1);
            return;        
        }
        else //������ ����
        {
            int addrass = AccManager.Inst.UseItem.FindIndex(x => x == item.originAcc);

            if (addrass == -1)
            {
                if (acc != null)
                    MouseUp();

                AccManager.Inst.UseItem.Add(item.originAcc);
                acc = item;
                originAcc = item.originAcc;
                icon.sprite = item.originAcc.icon;
                Item_light(item.originAcc.rank);
                Debug.Log("���������� UseItem���� �ش� �������� �߰��Ͽ����ϴ�.");

                item.DestroyItem();
                item.AmountCheck(0);

                AccManager.Inst.Player_Save_Equip(Player.Inst.playerdata.ThisItem);
                CharPower.Inst.Invoke_Start();
            }
            else
            {
                item.AmountCheck(1);
                Debug.Log("�������� �ߺ����� ������ �� �����ϴ�..");
            }
        }
    }

    public void Result_Item(Acc item)
    {
        if (item == null)
        {
            acc = null;
            originAcc = null;
            icon.sprite = null;
            Item_light("null");
        }
        else
        {
            int addrass = AccManager.Inst.isMine.FindIndex(x => x.originAcc == item);
            if (addrass == -1) //�ش� �������� �κ��丮�� �ִ��� ������
            {
                acc = null;
            }
            else
                acc = AccManager.Inst.isMine[addrass];

            //AccManager.Inst.UseItem.Add(item);
            originAcc = item;
            icon.sprite = item.icon;
            Item_light(item.rank);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1) && isSelect)
            MouseUp();
    }

    void Item_light(string rank)
    {
        if(rank == "null")
            rank_icon.color = new Color(105/255f, 111/255f, 106/255f, 1f);
        else
            rank_icon.color = new Color(1f, 1f, 1f, 1f);

        if (rank == "�Ϲ�")
            light.intensity = 0.01f;
        else
            light.intensity = 0.2f;

        if(rank == "null")
            light.color = new Color(0f, 0f, 0f, 0f);
        else if (rank == "�Ϲ�")
            light.color = new Color(1f, 1f, 1f, 1f);
        else if (rank == "���")
            light.color = new Color(80/255f, 1f, 0f, 1f);
        else if (rank == "���")
            light.color = new Color(0f, 67 / 255f, 1f, 1f);
        else if (rank == "����")
            light.color = new Color(100 / 255f, 0f, 1f, 1f);
        else if (rank == "����")
            light.color = new Color(1f, 0f, 0f, 1f);
        else if (rank == "��ȭ")
            light.color = new Color(1f, 80/255f, 0f, 1f);
    }

    public void OnMouseOver()
    {
        if (AccManager.Inst.select == null && icon.sprite != null)
        {
            isSelect = true;
            Tooltip.Inst.SetUp(originAcc);
        }
    }

    public void OnMouseExit()
    {
        isSelect = false;
        Tooltip.Inst.CloseSet();
    }

    public void OnMouseDown()
    {
        
    }

    public void ClearThis()
    {
        acc = null;
        originAcc = null;
        icon.sprite = null;
        Item_light("null");

    }

    public void MouseUp()
    {
        if (thisCombine) //���� ������
        {
            int addrass = AccManager.Inst.CombineItem.FindIndex(x => x == originAcc);
            if (addrass != -1)
            {
                AccManager.Inst.CombineItem.RemoveAt(addrass);
                Debug.Log("���������� UseItem���� �ش� �������� �����Ͽ����ϴ�.");

                int play_addrass = Player.Inst.playerdata.ItemCollect.FindIndex(x => x.name == originAcc.name);
                if (play_addrass != -1)
                    acc.AmountCheck(1);
                else
                    AccManager.Inst.Plus_AddCard(originAcc);

                ClearThis();
                AccManager.Inst.Result(AccManager.Inst.ItemCombine());
            }
            else
                Debug.Log("������ �������� UseItem���� ã�� ���߽��ϴ�.");
        }
        else if (thisResult) //�ϼ� ������
        {
            AccManager.Inst.all_Color();

            if (acc == null)
                AccManager.Inst.Plus_AddCard(originAcc);
            else
                acc.AmountCheck(1);

            AccManager.Inst.Combine_Clear();
            Player.Inst.Save();
        }
        else
        {
            int addrass = AccManager.Inst.UseItem.FindIndex(x => x == originAcc);
            if (addrass != -1)
            {
                AccManager.Inst.UseItem.RemoveAt(addrass);
                Debug.Log("���������� UseItem���� �ش� �������� �����Ͽ����ϴ�.");

                int play_addrass = Player.Inst.playerdata.ItemCollect.FindIndex(x => x.name == originAcc.name);
                if (play_addrass != -1)
                {
                    int acc_addrass = AccManager.Inst.isMine.FindIndex(x => x.name == originAcc.name);
                    acc = AccManager.Inst.isMine[addrass];
                    acc.AmountCheck(1);
                }
                else
                    AccManager.Inst.Plus_AddCard(originAcc);

                ClearThis();
                AccManager.Inst.Player_Save_Equip(Player.Inst.playerdata.ThisItem);
                CharPower.Inst.Invoke_Start();
            }
            else
                Debug.Log("������ �������� UseItem���� ã�� ���߽��ϴ�.");
        }
        OnMouseExit();
    }
}

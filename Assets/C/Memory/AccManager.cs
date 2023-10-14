using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AccManager : MonoBehaviour
{
    public static AccManager Inst { get; private set; }
    void Awake() => Inst = this;

    #region ����
    [SerializeField] AccCombineSO acccombineSO;
    [SerializeField] AccSO accSO;
    //�ش� �����ۿ� ���� �� ������ �����ϰ� ����.
    //Player�� ����� �������� name�� amount�� �̿��Ͽ� SO���� ������ �ҷ���.

    List<Acc> accSO_toPlay;
    //playerdata���� ������ AccSO ����
    //�׳� �Ź� �ҷ����� �����Ƽ�.

    [SerializeField] Equip ResultEquip;
    [SerializeField] Equip[] CombineEquip;
    [SerializeField] Equip[] NormalEquip;
    //���� �������� ������� ǥ���� ĭ

    [SerializeField] GameObject Prefab;
    //������ ������Ʈ ������
    //���ϸ� : AccItem

    public List<AccItem> isMine;
    //���� �����ϰ� �ִ� �����۵�
    //���� �ν��Ͻ��� �����Ǿ� �ִ� �����յ��� ����

    public List<Acc> CombineItem;
    //�����Ϸ��� ������

    public List<Acc> UseItem;
    //�������� ������

    public AccItem select;
    public GameObject select_Obj;
    //�� ������ ȭ��� ǥ���� ������ ���庯��

    int addrass = 0;
    //PopItem���� �̾Ƴ� �ּҰ����� ���.
    //Debug.Log :: addrass��° �������� ������ �����ɴϴ�.
    #endregion

    void Start()
    {
        Invoke("Start_Invoke", 0.01f);
    }

    public bool isDrag = false;
    void Update()
    {
        if (isDrag && select_Obj != null)
            select_Obj.transform.position = Input.mousePosition;
    }

    void Start_Invoke()
    {
        for (int i = 0; i < NormalEquip.Length; i++)
        {
            if (Player.Inst.playerdata.box_1.Count <= i)
                continue;

            if (Player.Inst.playerdata.box_1[i] != "")
            {
                Acc item = accSO.acc.Find(x => x.name == Player.Inst.playerdata.box_1[i]);
                NormalEquip[i].GetComponent<Equip>().Result_Item(item);
            }
        }

        accSO_toPlay = Player.Inst.playerdata.ItemCollect;
        for (int i = 0; i < accSO_toPlay.Count; i++)
            AddCard();

        isMineCheck();
    }

    void isMineCheck()
    {
        for (int i = 0; i < NormalEquip.Length; i++)
        {
            if (NormalEquip[i].originAcc.name != "")
            {
                UseItem.Add(NormalEquip[i].originAcc);
            }
        }
    }

    #region ������ ���뿡 ���� ����
    public void Player_Save_Equip(int num)
    {
        List<string> equipname = new List<string>();

        for (int i = 0; i < NormalEquip.Length; i++)
        {
            if (NormalEquip[i].icon.sprite == null)
                equipname.Add("");
            else if (NormalEquip[i].icon.sprite != null)
                equipname.Add(NormalEquip[i].originAcc.name);
        }

        if (num == 0)
            Player.Inst.playerdata.box_1 = equipname;

        Player.Inst.Save();
    }
    #endregion
    #region ����
    public Acc ItemCombine()
    {
        if (CombineItem.Count == 0)
        {
            Recipe(false);
            return null;
        }
        Recipe(true);

        for (int i = 0; i < acccombineSO.combineSO.Count; i++)
        {
            List<int> check = new List<int>();
            int x = 0;
            for (int j = 0; j < CombineItem.Count;)
            {
                if (check.FindIndex(p => p == j) != -1 || acccombineSO.combineSO[i].name.Length < CombineItem.Count)
                {
                    j++;
                    continue;
                }
                if (acccombineSO.combineSO[i].name[x] == CombineItem[j].name)
                {
                    check.Add(j);
                    x++;
                    j = 0;
                    if (x == acccombineSO.combineSO[i].name.Length && x == CombineItem.Count)
                    {
                        int result_add = accSO.acc.FindIndex(p => p.name == acccombineSO.combineSO[i].name_result);
                        return accSO.acc[result_add];
                    }
                    continue;
                }
                else
                {
                    j++;
                }
            }
        }
        return null;
    }

    public List<string> result = new List<string>();
    public void Recipe(bool Noreset)
    {
        result.Clear();
        int ecol = 0;
        string ecol_str = "";

        if (Noreset)
        {
            for (int j = 0; j < CombineItem.Count; j++)
            {
                if (ecol <= 1)
                {
                    ecol = 0;
                    if (CombineItem.Count >= 1)
                    {
                        if (CombineItem[0].name == CombineItem[j].name)
                            ecol += 1;
                    }
                    if (CombineItem.Count >= 2)
                    {
                        if (CombineItem[1].name == CombineItem[j].name)
                            ecol += 1;
                    }
                    if (CombineItem.Count >= 3)
                    {
                        if (CombineItem[2].name == CombineItem[j].name)
                            ecol += 1;
                    }
                    if (ecol >= 2)
                        ecol_str = CombineItem[j].name;
                }
            }

            for (int i = 0; i < acccombineSO.combineSO.Count; i++)
            {
                int num1 = 0;
                int num2 = 0;
                int num3 = 0;
                if (acccombineSO.combineSO[i].name.Length >= 1 && CombineItem.Count >= 1)
                    num1 = Array.IndexOf(acccombineSO.combineSO[i].name, CombineItem[0].name);
                if (acccombineSO.combineSO[i].name.Length >= 2 && CombineItem.Count >= 2)
                    num2 = Array.IndexOf(acccombineSO.combineSO[i].name, CombineItem[1].name);
                if (acccombineSO.combineSO[i].name.Length >= 3 && CombineItem.Count >= 3)
                    num3 = Array.IndexOf(acccombineSO.combineSO[i].name, CombineItem[2].name);
                if (num1 != -1 && num2 != -1 && num3 != -1)
                {
                    for (int j = 0; j < acccombineSO.combineSO[i].name.Length; j++)
                    {
                        if (result.FindIndex(x => x == acccombineSO.combineSO[i].name[j]) == -1)
                        {
                            if (CombineItem.FindIndex(x => x.name == acccombineSO.combineSO[i].name[j]) != -1)
                                continue;
                            if (ecol != IntGroup(acccombineSO.combineSO[i].name))
                                continue;
                            if(ecol_str != StringGroup(acccombineSO.combineSO[i].name))
                                continue;

                            result.Add(acccombineSO.combineSO[i].name[j]);
                        }
                    }
                }

                for (int x = 0; x < isMine.Count; x++)
                    isMine[x].icon.color = new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f);

                for (int y = 0; y < result.Count; y++)
                {
                    int num = (isMine.FindIndex(x => x.originAcc.name == result[y]));
                    if(num != -1)
                        isMine[num].icon.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
        else
        {
            if (CombineItem.Count == 3)
            {
                for (int x = 0; x < isMine.Count; x++)
                    isMine[x].icon.color = new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f);
            }
            else
            {
                for (int x = 0; x < isMine.Count; x++)
                    isMine[x].icon.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public void all_Color()
    {
        for (int x = 0; x < isMine.Count; x++)
            isMine[x].icon.color = new Color(1f, 1f, 1f, 1f);
    }

    int IntGroup(string[] G1)
    {
        int ecol = 0;
        for (int j = 0; j < G1.Length; j++)
        {
            if (ecol <= 1)
            {
                ecol = 0;
                if (G1.Length >= 1)
                {
                    if (G1[0] == G1[j])
                        ecol += 1;
                }
                if (G1.Length >= 2)
                {
                    if (G1[1] == G1[j])
                        ecol += 1;
                }
                if (G1.Length >= 3)
                {
                    if (G1[2] == G1[j])
                        ecol += 1;
                }
            }
        }
        return ecol;
    }

    string StringGroup(string[] G1)
    {
        string ecol_str = "";
        int ecol = 0;
        for (int j = 0; j < G1.Length; j++)
        {
            if (ecol <= 1)
            {
                ecol = 0;
                if (G1.Length >= 1)
                {
                    if (G1[0] == G1[j])
                        ecol += 1;
                }
                if (G1.Length >= 2)
                {
                    if (G1[1] == G1[j])
                        ecol += 1;
                }
                if (G1.Length >= 3)
                {
                    if (G1[2] == G1[j])
                        ecol += 1;
                }
                if (ecol >= 2)
                    ecol_str = G1[j];
            }
        }
        return ecol_str;
    }

    public void Result(Acc acc)
    {
        ResultEquip.Result_Item(acc);
    }
    #endregion
    #region ������ �߰�
    Acc PopItem() //SO���� ������ �������� ����
    {
        Acc item = accSO.acc.Find(x => x.name == accSO_toPlay[addrass].name);
        addrass++;

        if(item != null)
            return item; //Acc�� ����
        return null; //������ ����~
    }

    public void AddCard() //�������� �ν��Ͻ�ȭ��Ű�� ����
    {
        Acc acc = PopItem();

        if (accSO_toPlay[addrass - 1].amount == 0)
            return;

        var accObject = Instantiate(Prefab, new Vector3(0, 0, 0), Utils.QI, GameObject.Find("�����۽�ũ�Ѻ�").transform);
        var item = accObject.GetComponent<AccItem>();

        item.SetUp(acc, accSO_toPlay[addrass-1].amount);//SO���� �ش� ������ �����ɴϴ�.
        isMine.Add(item);//���� �ν��Ͻ�ȭ�� ������ ������Ʈ���� ����
    }

    public void Plus_AddCard(Acc acc) //�������� �ν��Ͻ�ȭ��Ű�� ����
    {
        var accObject = Instantiate(Prefab, new Vector3(0, 0, 0), Utils.QI, GameObject.Find("�����۽�ũ�Ѻ�").transform);
        var item = accObject.GetComponent<AccItem>();


        accSO_toPlay.Add(acc);
        accSO_toPlay[accSO_toPlay.Count - 1].amount = new int();
        accSO_toPlay[accSO_toPlay.Count - 1].amount = 1;
        item.SetUp(acc, accSO_toPlay[accSO_toPlay.Count - 1].amount);//SO���� �ش� ������ �����ɴϴ�.
        isMine.Add(item);//���� �ν��Ͻ�ȭ�� ������ ������Ʈ���� ����
    }

    public void Combine_Clear()
    {
        for (int i = 0; i < CombineItem.Count; i++)
        {
            int addrass = isMine.FindIndex(x => x.originAcc.name == CombineItem[i].name);
            isMine[addrass].real_amount -= 1;
            isMine[addrass].AmountCheck(0);
        }

        ResultEquip.ClearThis();
        for (int i = 0; i < CombineEquip.Length; i++)
        {
            int addrass = CombineItem.FindIndex(x => x == CombineEquip[i].originAcc);
            CombineEquip[i].ClearThis();
            if (addrass != -1)
            {
                CombineItem.RemoveAt(addrass);
            }
        }

        for (int i = 0; i < isMine.Count; i++)
        {
            isMine[i].DestroyItem();
        }
    }
    #endregion
    #region �巡�׿� ������ �߰�
    public void DragItem(AccItem item) //�������� �ν��Ͻ�ȭ��Ű�� ����
    {
        Acc acc = item.originAcc;
        var accObject = Instantiate(Prefab, new Vector3(0, 0, 0), Utils.QI, GameObject.Find("Canvas").transform);
        var accitem = accObject.GetComponent<AccItem>();
        accitem.SetUp(acc);
        accitem.parent = item;
        accObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        select_Obj = accObject;
    }
    #endregion
    #region ���콺 �Է�
    public void AccMouseDown(AccItem item)
    {
        select = item;
        //�ش� �������� ����

        DragItem(item);
        //�巡�׿� ������ �߰�

        isDrag = true;

        if (item.amount.text != "")
            item.Plus_amount(-1);
        //item.AmountCheck(-1);

    }

    public void AccMouseUp(AccItem item)
    {
        if (!isDrag)
            return;

        Destroy(select_Obj);
        select_Obj = null;
        select = null;
        isDrag = false;
        //item.AmountCheck(1);
        item.transform.position = item.originPos;
    }
    #endregion
}

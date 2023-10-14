using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryEvent : MonoBehaviour
{
    public Dictionary<int, string[]> talkData;
    public Dictionary<int, string[]> choiData;

    public string GetTalk(int id, int addrass)
    {
        return talkData[id][addrass];
    }

    public string GetChoice(int id, int addrass)
    {
        return choiData[id][addrass];
    }

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        choiData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void Update()
    {
    }

    void GenerateData()
    {
        talkData.Add(0, new string[] { " " }); //��� ��� �ʱ�ȭ��
        choiData.Add(0, new string[] { "", "", "" });

        #region �帴�� ���
        //ó�� ���� �߸� �䳢�� ����
        talkData.Add(1, new string[] { "���� ��ħ�̿���. <color=orange>A</color>",
            "A?" });
        choiData.Add(1, new string[] { "���� �����δ�.", "�ǽɽ����� ���ʸ��� ������.", "" });

        //ó���� �ƹ� ���� ���� �ʾ��� ��
        talkData.Add(-1, new string[] { "�����Ͻ� ���̽ó׿�.",
            "�����ƿ�. ��Ȳ�� ���۽������ ����.",
            "���� ó�� ���� ���� �� ��Ű� �������ϱ��."});
        choiData.Add(-1, new string[] { "", "", "" });

        //�׽�Ʈ ������ �亯
        talkData.Add(2, new string[] { "�ϴ� �ڱ�Ұ����� �ҰԿ�. <color=orange>A</color>",
            "�� �ü��� ���������μ� ����� å���ڷ� �߷ɹ��� \"����\"����.",
            "����� \"����\"�� ���������� ������ �������� �̸��̿���.",
            "�׷��� ������ ���� ���ؼ��� \"����\"��� �ҷ��ּ���.",
            "�̾߱⸦ �����ϱ� ���� ����� ���� ���� ���� �־��. <color=orange>A</color>" });
        choiData.Add(2, new string[] { "", "", "" });

        talkData.Add(8, new string[] { "��� ��Ȳ������ ���� �ൿ�� �ﰡ���ּ���." });
        choiData.Add(8, new string[] { "", "", "" });

        talkData.Add(3, new string[] { "<color=red>�׵��� �װ��� �뼭���� �ʽ��ϴ�.</color>", "�����ο�." });
        choiData.Add(3, new string[] { "���ɽ��� �����δ�.", "", "" });

        talkData.Add(4, new string[] { "���ƿ�!" });
        choiData.Add(4, new string[] { "", "", "" });

        talkData.Add(5, new string[] { "(�ٸ� �Ͽ� �����ϰ� �ִ� ������ ���δ�...)" });
        choiData.Add(5, new string[] { "������ �ִ´�.", "�̰��� ���� ���´�.", "" });

        talkData.Add(6, new string[] { "���ó�� �̰����� ���� �� ������� �����ϰ� ����ϴ� ���̿���.",
            "<color=red>�׵��� �۾�</color>�� ����� �̹����� ó���ϴ� �� �� ������ �ϰ� �־��.",
            "���� �ϴ� ���̴� <color=orange>A</color>���Ե� �׸� ����� �����ſ���.",
            "��? �׵鿡 ���ؼ�..?",
            "<color=purple>��� ���� �� �����.</color>",
            "�׳� �翬�ϰ� �׷� �ɷ� �Ǿ��־��." });
        choiData.Add(6, new string[] { "", "", "" });

        talkData.Add(7, new string[] { "���� ���� ���Ƽ� �ٷ� �ȳ� �����ؾ��� �� ���ƿ�.", "�ε� �� ��� ���� ���ñ�" });
        choiData.Add(7, new string[] { "", "", "" });
        #endregion
        //�ѤѤѤѤѤѤѤѤѤѤѤѤ�Ǯ���� ���� �ǹ�
        #region Ǯ���� ���� �ǹ�
        talkData.Add(9, new string[] { "�����ϼ̾��. <color=orange>A</color>" });
        choiData.Add(9, new string[] { "", "", "" });

        talkData.Add(10, new string[] { "�۾��� ��̳���. �Ҹ��ϼ̳���?", "<color=orange>A</color> ���� ��ġ�̳���?" });
        choiData.Add(10, new string[] { "�׷�����", "", "" });
        #region �׷�����
        talkData.Add(11, new string[] { "�Ҹ��ϼ̴ٴ� �����̿���.",
            "��� ��밡 ���� ���̱�� ������...",
            "������ �� �ƴ� �� ��Ⱑ ����ϴٰ� �����ؿ�." });
        choiData.Add(11, new string[] { "", "", "" });

        talkData.Add(12, new string[] { "(����� ���� ��޸��� ���ϴ�.)" });
        choiData.Add(12, new string[] { "�̰��� ���� �ٽ� �ѹ� ���´�.", "���� ��ǥ�� �޶�� �����Ѵ�.", "" });
        #region �̰��� ���� �ٽ� �ѹ� ���´�.
        talkData.Add(13, new string[] { "...",
            "���� ��������߰���?",
            "�״� ���� �̾߱�� �ƴ϶� �׷����." });
        choiData.Add(13, new string[] { "", "", "" });

        talkData.Add(14, new string[] { "����� ���⿡ ���� �ΰ��ΰ���..?", "����." });
        choiData.Add(14, new string[] { "�����Ѵ�.", "�����Ѵ�.", "" });
        #region �����Ѵ�.
        talkData.Add(15, new string[] { "�ƽ��Ե� �׷��� �ʾƿ�.",
            "�ΰ��� ����� �䳻���� ���𰡿� �������. ���� ��ŵ� �׷���. <color=orange>A</color>" });
        choiData.Add(15, new string[] { "", "", "" });

        talkData.Add(16, new string[] { "�̰��� ���� ���� ����Ͻó���?", "�и� ����Ͻǰſ���." });
        choiData.Add(16, new string[] { "���� �Ŵ���..", "���ĸ��� ���Ҵ�.", "" });

        talkData.Add(17, new string[] { "�� ���ĸ��� �ΰ��� �̰����� �������� �־��." });
        choiData.Add(17, new string[] { "", "", "" });

        talkData.Add(18, new string[] { "�Ƹ��� <color=red>�׵��� ����</color>�� �ǵ��� ��Ȳ�̰���.", "�Ƹ���.." });
        choiData.Add(18, new string[] { "�Ƹ���?", "", "" });

        talkData.Add(19, new string[] { "���� �������� ���� ��µǴ� �ӹ�����.", 
            "�������� �װ� �Ǹ� ���� ��� ��� �� ����ڰ� �������� ����̿���.",
            "������ �װ� ���������� ���޹��� ����� �� �� ����� �����.",
            "<color=red>�׵鿡 ���� ��</color>ó�� �翬�ϰ� �׷��ٰ� �����ϰ� �ִ� �ڽ��� �־��." });
        choiData.Add(19, new string[] { "", "", "" });

        talkData.Add(20, new string[] { "���ĸ��� ���� �� �� �翬�� ���Ŀ��� �����ε��� �����س� �̾߱⿡��.", "�ϱ� �����..?" });
        choiData.Add(20, new string[] { "�ϱ� ����� �̾߱��.", "", "" });

        talkData.Add(21, new string[] { "�׷��⿡ �������� �ٱ��� �����ֱ� ������ ������ �ʰ� �ƾ��.",
            "�׷��� ������ ������ �������� �����ϴ� ��쵵 �ִ�����." });
        choiData.Add(21, new string[] { "", "", "" });

        talkData.Add(22, new string[] { "�� ���� ����...", "..." });
        choiData.Add(22, new string[] { "�� ��쿡�� ��� �Ǵ��� ���´�.", "", "" });

        talkData.Add(23, new string[] { "...",
            "�̰��� ġ���� ��Ű�� ���ؼ� �������� �����̵� �Ѵٰ� �����帱�Կ�.",
            "����� ���� ������׿�.",
            "������ �����ϼ̾��. ���� �ٽ� ���ڽ��ϴ�." });
        choiData.Add(23, new string[] { "", "", "" });
        #endregion
        #region �����Ѵ�.
        talkData.Add(24, new string[] { "����ΰ���?",
            "�����̽ö�� �ű��ϳ׿�.",
            "�˾������� ����� �� �����ŵ��." });
        choiData.Add(24, new string[] { "", "", "" });

        talkData.Add(25, new string[] { "�׷��ôٸ� �̰��� ���� ���� ����� �����Ű���?", "�и� ����Ͻǰſ���." });
        choiData.Add(25, new string[] { "�Ŵ��� ���ĸ��� ���Ҵ�.", "", "" });
        #endregion
        #endregion
        #region ���� ��ǥ�� �޶�� �����Ѵ�.
        talkData.Add(26, new string[] { "����̶�� ����� ���� �ñ������׿�.",
            "����ϴ� �� ������ ������ ���� �����ּ���.",
            "�� ����� �״� �� ���� ���� �ʾƿ�." });
        choiData.Add(26, new string[] { "", "", "" }); //���� ����
        #endregion
        #region Ÿ�Ӿƿ�
        talkData.Add(27, new string[] { "���� �̰��� ���� �˾ư��� �ϰ� �־��.",
            "������ �װ� ���� ���������� �ʾƿ�.",
            "�߿��� �� ����� �Ʊ��̶�� �ſ���.",
            "�ε� ���� ��Ƴ����ּ���. ����� �ش� �� �ʹ����� ����ŵ��." });
        choiData.Add(27, new string[] { "", "", "" }); //���� ����
        talkData.Add(28, new string[] { "Ȥ�� ����� �ȳ��ó���..?", 
            "�������ſ���. �ݹ� �ٽ� ����������." });
        choiData.Add(28, new string[] { "", "", "" }); //���� ����
        #endregion
        #endregion
        #endregion



        talkData.Add(600, new string[] { "" });
        choiData.Add(600, new string[] { "", "", "" });
    }
}

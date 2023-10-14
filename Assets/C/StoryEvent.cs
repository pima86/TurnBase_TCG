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
        talkData.Add(0, new string[] { " " }); //상대 대사 초기화용
        choiData.Add(0, new string[] { "", "", "" });

        #region 흐릿한 기억
        //처음 눈을 뜨며 토끼와 만남
        talkData.Add(1, new string[] { "좋은 아침이에요. <color=orange>A</color>",
            "A?" });
        choiData.Add(1, new string[] { "고개를 끄덕인다.", "의심스러운 눈초리를 보낸다.", "" });

        //처음에 아무 말도 하지 않았을 때
        talkData.Add(-1, new string[] { "신중하신 분이시네요.",
            "괜찮아요. 상황이 갑작스럽기는 하죠.",
            "저도 처음 눈을 떴을 때 당신과 같았으니까요."});
        choiData.Add(-1, new string[] { "", "", "" });

        //테스트 시작의 답변
        talkData.Add(2, new string[] { "일단 자기소개부터 할게요. <color=orange>A</color>",
            "현 시설의 관리인으로서 당신의 책임자로 발령받은 \"감마\"에요.",
            "참고로 \"감마\"는 관리인으로 부임한 순서이자 이름이에요.",
            "그러니 앞으로 저에 대해서는 \"감마\"라고 불러주세요.",
            "이야기를 시작하기 전에 당신을 위해 해줄 말이 있어요. <color=orange>A</color>" });
        choiData.Add(2, new string[] { "", "", "" });

        talkData.Add(8, new string[] { "어떠한 상황에서도 돌발 행동은 삼가해주세요." });
        choiData.Add(8, new string[] { "", "", "" });

        talkData.Add(3, new string[] { "<color=red>그들은 그것을 용서하지 않습니다.</color>", "정말로요." });
        choiData.Add(3, new string[] { "조심스레 끄덕인다.", "", "" });

        talkData.Add(4, new string[] { "좋아요!" });
        choiData.Add(4, new string[] { "", "", "" });

        talkData.Add(5, new string[] { "(다른 일에 집중하고 있는 것으로 보인다...)" });
        choiData.Add(5, new string[] { "가만히 있는다.", "이곳에 대해 묻는다.", "" });

        talkData.Add(6, new string[] { "당신처럼 이곳에서 눈을 뜬 사람들을 관리하고 고용하는 곳이에요.",
            "<color=red>그들의 작업</color>에 생기는 이물질을 처리하는 걸 주 업무로 하고 있어요.",
            "저도 하는 일이니 <color=orange>A</color>에게도 그리 어렵지 않을거에요.",
            "네? 그들에 대해서..?",
            "<color=purple>사실 저도 잘 몰라요.</color>",
            "그냥 당연하게 그런 걸로 되어있어요." });
        choiData.Add(6, new string[] { "", "", "" });

        talkData.Add(7, new string[] { "일이 조금 많아서 바로 안내 시작해야할 거 같아요.", "부디 제 경고를 잊지 마시길" });
        choiData.Add(7, new string[] { "", "", "" });
        #endregion
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ풀리지 않은 의문
        #region 풀리지 않은 의문
        talkData.Add(9, new string[] { "수고하셨어요. <color=orange>A</color>" });
        choiData.Add(9, new string[] { "", "", "" });

        talkData.Add(10, new string[] { "작업은 어떠셨나요. 할만하셨나요?", "<color=orange>A</color> 많이 지치셨나요?" });
        choiData.Add(10, new string[] { "그럭저럭", "", "" });
        #region 그럭저럭
        talkData.Add(11, new string[] { "할만하셨다니 다행이에요.",
            "비록 상대가 약한 편이기는 했지만...",
            "마주할 줄 아는 그 용기가 대단하다고 생각해요." });
        choiData.Add(11, new string[] { "", "", "" });

        talkData.Add(12, new string[] { "(당신의 말을 기달리는 듯하다.)" });
        choiData.Add(12, new string[] { "이곳에 대해 다시 한번 묻는다.", "다음 목표를 달라고 재촉한다.", "" });
        #region 이곳에 대해 다시 한번 묻는다.
        talkData.Add(13, new string[] { "...",
            "역시 말씀드려야겠죠?",
            "그닥 좋은 이야기는 아니라서 그랬어요." });
        choiData.Add(13, new string[] { "", "", "" });

        talkData.Add(14, new string[] { "당신이 보기에 저는 인간인가요..?", "어떨까요." });
        choiData.Add(14, new string[] { "긍정한다.", "부정한다.", "" });
        #region 긍정한다.
        talkData.Add(15, new string[] { "아쉽게도 그렇지 않아요.",
            "인간의 모습을 흉내내는 무언가에 가까워요. 물론 당신도 그래요. <color=orange>A</color>" });
        choiData.Add(15, new string[] { "", "", "" });

        talkData.Add(16, new string[] { "이곳에 오기 전을 기억하시나요?", "분명 기억하실거에요." });
        choiData.Add(16, new string[] { "뭔가 거대한..", "해파리를 보았다.", "" });

        talkData.Add(17, new string[] { "그 해파리가 인간을 이곳으로 보내오고 있어요." });
        choiData.Add(17, new string[] { "", "", "" });

        talkData.Add(18, new string[] { "아마도 <color=red>그들의 업무</color>로 의도된 상황이겠죠.", "아마도.." });
        choiData.Add(18, new string[] { "아마도?", "", "" });

        talkData.Add(19, new string[] { "저희 관리직은 대대로 계승되는 임무에요.", 
            "관리인이 죽게 되면 그의 담당 사원 중 계승자가 정해지는 방식이에요.",
            "하지만 그걸 누군가에게 전달받은 사람은 단 한 사람도 없어요.",
            "<color=red>그들에 대한 것</color>처럼 당연하게 그렇다고 생각하고 있는 자신이 있어요." });
        choiData.Add(19, new string[] { "", "", "" });

        talkData.Add(20, new string[] { "해파리에 대한 건 그 당연한 지식에서 관리인들이 추측해낸 이야기에요.", "믿기 어렵죠..?" });
        choiData.Add(20, new string[] { "믿기 어려운 이야기다.", "", "" });

        talkData.Add(21, new string[] { "그렇기에 관리인은 바깥을 보여주기 전에는 말하지 않게 됐어요.",
            "그러지 않으면 오히려 관리인을 공격하는 경우도 있더라고요." });
        choiData.Add(21, new string[] { "", "", "" });

        talkData.Add(22, new string[] { "그 때는 정말...", "..." });
        choiData.Add(22, new string[] { "그 경우에는 어떻게 되는지 묻는다.", "", "" });

        talkData.Add(23, new string[] { "...",
            "이곳의 치안을 지키기 위해서 관리인은 무엇이든 한다고만 말씀드릴게요.",
            "충분히 말이 길어졌네요.",
            "오늘은 수고하셨어요. 내일 다시 오겠습니다." });
        choiData.Add(23, new string[] { "", "", "" });
        #endregion
        #region 부정한다.
        talkData.Add(24, new string[] { "농담인가요?",
            "정말이시라면 신기하네요.",
            "알아차리는 사람은 몇 없었거든요." });
        choiData.Add(24, new string[] { "", "", "" });

        talkData.Add(25, new string[] { "그러시다면 이곳에 오기 전에 기억이 있으신가요?", "분명 기억하실거에요." });
        choiData.Add(25, new string[] { "거대한 해파리를 보았다.", "", "" });
        #endregion
        #endregion
        #region 다음 목표를 달라고 재촉한다.
        talkData.Add(26, new string[] { "당신이라는 사람에 대해 궁금해지네요.",
            "노력하는 건 좋지만 무리는 하지 말아주세요.",
            "제 사원이 죽는 건 보고 싶지 않아요." });
        choiData.Add(26, new string[] { "", "", "" }); //게임 시작
        #endregion
        #region 타임아웃
        talkData.Add(27, new string[] { "저희도 이곳에 대해 알아가려 하고 있어요.",
            "하지만 그게 마냥 간단하지는 않아요.",
            "중요한 건 저희는 아군이라는 거에요.",
            "부디 오래 살아남아주세요. 사람을 잊는 건 너무나도 힘들거든요." });
        choiData.Add(27, new string[] { "", "", "" }); //게임 시작
        talkData.Add(28, new string[] { "혹시 기억이 안나시나요..?", 
            "괜찮을거에요. 금방 다시 떠오르겠죠." });
        choiData.Add(28, new string[] { "", "", "" }); //게임 시작
        #endregion
        #endregion
        #endregion



        talkData.Add(600, new string[] { "" });
        choiData.Add(600, new string[] { "", "", "" });
    }
}

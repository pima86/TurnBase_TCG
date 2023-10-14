using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MouseEvent : MonoBehaviour
{
    public static MouseEvent Inst { get; private set; }
    void Awake() => Inst = this;

    public GameObject RotateScript;
    public bool Rotate;

    public GameObject Typing;
    public bool Type;

    public GameObject TextBox;
    public bool UpBg;
    public bool DownBg;
    public bool SelOn;
    public bool SelOff;

    public GameObject Card;

    void Start()
    {
        Chapter(Player.Inst.playerdata.story);
        //LoadPanel.Inst.LoadEnd = true; //로딩 완료
    }

    #region 주석
    private void OnMouseUp()
    {
        /*
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            
        }
        
        if (RotateScript.gameObject != null)
        {
            if (Rotate)
                RotateScript.GetComponent<RotateScript>().RotateChar();
        }
        if (Typing.gameObject != null)
        {
            if (Type)
                Typing.GetComponent<TypeEffect>().Type(true);
        }
        if (TextBox.gameObject != null)
        {
            if (UpBg) //검정배경 위로
                TextBox.GetComponent<TextBox>().UpBg();
            if (DownBg) //검정배경 밑으로
                TextBox.GetComponent<TextBox>().DownBg();
            if (SelOn) //
                TextBox.GetComponent<TextBox>().SelOn();
            if (SelOff) //
                TextBox.GetComponent<TextBox>().SelOff();
        }
        if (LightCon.gameObject != null)
        {
            if (LightOn) //테이블 살펴보기
                LightCon.GetComponent<LightControl>().LightOn();
            if (LightOff)
                LightCon.GetComponent<LightControl>().LightOff();
        }
        if (Material.gameObject != null)
        {
            if (Light) //밝게
                Material.GetComponent<managerMaterial>().Light();
            if (Dark) //밝게
                Material.GetComponent<managerMaterial>().Dark();
            if (Deep_Dark) //밝게
                Material.GetComponent<managerMaterial>().Deep_Dark();

            //1번 스탠드
            //Material.GetComponent<managerMaterial>().N1_E_3();
        }
        */
    }

    /*
    private void OnMouseEnter()
    {
        gameObject.GetComponent<Outline>().enabled = true;
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }
    */
    #endregion

    #region 챕터별 스크립트
    private void TextFine(int ID)
    {
        GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID = ID;
        GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkAD = 0;
        GameObject.Find("managerGame").GetComponent<TypeEffect>().ChoiAD = 0;
        GameObject.Find("managerGame").GetComponent<TypeEffect>().m_TypingText.text = "";
        GameObject.Find("managerGame").GetComponent<TypeEffect>().talkEnd.SetActive(false);
    }

    private void ChapterName(string TT, string tt)
    {
        string TTtt;
        TTtt = TT + tt;
        Invoke(TTtt, 0);
    }

    private string TextI;
    private string TextT;
    public void Chapter(string tt)
    {
        TextBox.GetComponent<TextBox>().SelOff();

        #region 흐릿한 기억
        if (tt == "흐릿한 기억")
            StartCoroutine(흐릿한기억());
        else if (tt == "고개를 끄덕인다.")
        {
            TextT = "집착_자기소개_";
            집착_자기소개();
        }
        else if (tt == "집착_자기소개_")
        {
            ChapterName(TextT, TextI);
        }

        else if (tt == "의심스러운 눈초리를 보낸다.")
        {
            시작무시();
        }
        else if (tt == "시작무시_")
        {
            ChapterName(TextT, TextI);
        }

        else if (tt == "집착_경고")
        {
            TextT = "집착_경고_";
            집착_경고();
        }
        else if (tt == "집착_경고_")
        {
            ChapterName(TextT, TextI);
        }

        else if (tt == "조심스레 끄덕인다.")
        {
            TextT = "조심_끄덕_";
            조심_끄덕();
        }
        else if (tt == "조심_끄덕_")
        {
            ChapterName(TextT, TextI);
        }

        else if (tt == "안내_시작" || tt == "가만히 있는다.")
        {
            TextT = "안내_시작_";
            안내_시작();
        }
        else if (tt == "안내_시작_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "이곳에 대해 묻는다.")
        {
            TextT = "이곳_궁금_";
            이곳_궁금();
        }
        else if (tt == "이곳_궁금_")
        {
            ChapterName(TextT, TextI);
        }
        #endregion
        #region 풀리지 않은 의문
        if (tt == "풀리지 않은 의문")
        {
            StartCoroutine(풀리지않은의문());
            TextT = "풀리지않은의문_";
        }
        else if (tt == "풀리지않은의문_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "그럭저럭")
        {
            그럭저럭();
            TextT = "그럭저럭_";
        }
        else if (tt == "그럭저럭_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "이곳에 대해 다시 한번 묻는다.")
        {
            이곳에대해();
            TextT = "이곳에대해_";
        }
        else if (tt == "이곳에대해_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "긍정한다.")
        {
            인간긍정();
            TextT = "인간긍정_";
        }
        else if (tt == "인간긍정_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "뭔가 거대한.." || tt == "해파리를 보았다." || tt == "거대한 해파리를 보았다.")
        {
            해파리();
            TextT = "해파리_";
        }
        else if (tt == "해파리_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "아마도?")
        {
            아마도();
            TextT = "아마도_";
        }
        else if (tt == "아마도_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "믿기 어려운 이야기다.")
        {
            믿기어려운();
            TextT = "믿기어려운_";
        }
        else if (tt == "믿기어려운_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "그 경우에는 어떻게 되는지 묻는다.")
        {
            그경우어떻게();
            TextT = "그경우어떻게_";
        }
        else if (tt == "그경우어떻게_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "부정한다.")
        {
            인간부정();
            TextT = "인간부정_";
        }
        else if (tt == "인간부정_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "다음 목표를 달라고 재촉한다.")
        {
            다음목표재촉();
        }
        else if (tt == "다음목표재촉_")
        {
            ChapterName(TextT, TextI);
        }
        else if (tt == "의문타임아웃_")
        {
            ChapterName(TextT, TextI);
        }
        #endregion
        

        else if (tt == "인게임시작")
            인게임시작();
    }
    #endregion

    #region 시간에 관련된 대사
    public void StayChapter()
    {
        if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 1)
            Typing.GetComponent<TypeEffect>().SubType(true); //A?
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 3)
            Typing.GetComponent<TypeEffect>().SubType(true); //정말로요.
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 10)
            Typing.GetComponent<TypeEffect>().SubType(true); //많이 지치셨나요?
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 12)
            SpriteChange.Inst.obsession_Eyes_4();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 14)
            Typing.GetComponent<TypeEffect>().SubType(true); //어떨까요.
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 16)
            Typing.GetComponent<TypeEffect>().SubType(true); //분명 기억하실거에요.
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 18)
            Typing.GetComponent<TypeEffect>().SubType(true); //아마도..
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 20)
            Typing.GetComponent<TypeEffect>().SubType(true); //믿기 어렵죠..?
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 22)
            Typing.GetComponent<TypeEffect>().SubType(false); //...
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 25)
            Typing.GetComponent<TypeEffect>().SubType(false); //분명 기억하실거에요.
    }

    public void SkipChapter()
    {
        TextBox.GetComponent<TextBox>().SelOff();

        if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 1)
            시작무시();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 3)
            조심_끄덕_1();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 5)
            안내_시작();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 10)
            그럭저럭_3();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 12)
            인간긍정_2();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 16)
            의문해파리타임아웃();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 18)
            다음목표재촉();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 20)
            의문타임아웃();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 22)
            의문타임아웃();
        else if (GameObject.Find("managerGame").GetComponent<TypeEffect>().TalkID == 25)
            의문해파리타임아웃();
    }
    #endregion

    #region Update, Auto, TalkCam
    void Update()
    {
        if (!GameObject.Find("managerGame").GetComponent<TypeEffect>().IsAnim
            && GameObject.Find("managerGame").GetComponent<TypeEffect>().CT_1.text == ""
            && GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point == -1)
        {
            if (Input.GetKeyDown(KeyCode.Return) && GameObject.Find("managerGame").GetComponent<TypeEffect>().AutoMode == false)
                Chapter(TextT);
            else if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && GameObject.Find("managerGame").GetComponent<TypeEffect>().AutoMode == false)
                Chapter(TextT);
        }
    }

    public void Auto()
    {
        Chapter(TextT);
    }

    void TalkCamear()
    {
        CameraMove.Inst.TableOut();
        CameraMove.Inst.FaceIn(0.02f);
    }
    #endregion

    #region 흐릿한기억
    //연출이 필요없는 부분은 void, 연출이 필요한 부분은 IEnumerator
    IEnumerator 흐릿한기억()
    {
        TextFine(1);
        TalkCamear();

        //문 열리는 연출
        SCI_Gate.Inst.OpenGate();
        ClipManager.Inst.GateOpenSound();
        yield return new WaitForSeconds(0.3f);

        //캐릭터 세팅
        CameraMove.Inst.FaceOut();
        CameraMove.Inst.FaceLowIn(0.02f);

        SpriteChange.Inst.obsession_Default();
        yield return new WaitForSeconds(1f);

        SpriteChange.Inst.obsession_Brows_1();
        SpriteChange.Inst.obsession_Eyes_3();

        //문닫기
        SCI_Gate.Inst.CloseGate();
        ClipManager.Inst.GateCloseSound();
        yield return new WaitForSeconds(0.2f);

        TextBox.GetComponent<TextBox>().UpBg();
        yield return new WaitForSeconds(1f);

        SpriteChange.Inst.obsession_Brows_3();
        SpriteChange.Inst.obsession_Eyes_1();
        //테스트 중입니다.
        Typing.GetComponent<TypeEffect>().Type(true, true);

        yield return new WaitForSeconds(1f);

        Tutorial_page.Inst.Tutorial_1(0);
    }

    void 시작무시()
    {
        TextFine(-1);

        SpriteChange.Inst.obsession_Brows_3();

        //묵묵하신 분이시네요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextT = "시작무시_";
        TextI = "1";
    }

    void 시작무시_1()
    {
        SpriteChange.Inst.obsession_Eyes_3();
        SpriteChange.Inst.obsession_Brows_2();

        //괜찮아요. 상황이 갑작스럽기는 하죠.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }

    void 시작무시_2()
    {
        SpriteChange.Inst.obsession_Eyes_1();

        //저도 처음 눈을 떴을 때 당신과 같았으니까요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextT = "고개를 끄덕인다.";
    }
    
    void 집착_자기소개()
    {
        TextFine(2);

        SpriteChange.Inst.obsession_Eyes_3();
        SpriteChange.Inst.obsession_Brows_1();

        //일단 자기소개부터 할게요.
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "1";
    }
    void 집착_자기소개_1()
    {
        SpriteChange.Inst.obsession_Eyes_1();

        //현 시설의 관리인으로서 당신의 책임자로 발령받은 \"세번째\"에요.
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "2";
    }
    void 집착_자기소개_2()
    {
        //참고로 \"세번째\"는 관리인으로 부임한 순서이자 이름이에요.
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "3";
    }
    void 집착_자기소개_3()
    {
        //그러니 앞으로 저에 대해서는 \"세번째\"라고 불러주시면 됩니다.
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "4";
    }
    void 집착_자기소개_4()
    {
        //이야기를 시작하기 전에 당신을 위해 해줄 말이 있어요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextFine(8);
        TextT = "집착_경고";
    }
    void 집착_경고()
    {
        SpriteChange.Inst.obsession_Eyes_3();
        SpriteChange.Inst.obsession_Brows_2();

        //어떠한 상황에서도 돌발 행동을 하지 마세요. 
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextFine(3);
        TextI = "1";
    }
    void 집착_경고_1()
    {
        SpriteChange.Inst.obsession_Eyes_1();
        SpriteChange.Inst.obsession_Brows_1();
        //어떠한 상황에서도 돌발 행동을 하지 마세요. 
        Typing.GetComponent<TypeEffect>().Type(true, true);
    }
    void 조심_끄덕()
    {
        SpriteChange.Inst.obsession_Eyes_4();
        SpriteChange.Inst.obsession_Brows_3();

        //좋아요!
        TextFine(4);
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 조심_끄덕_1()
    {
        //서류를 보는 이미지
        SpriteChange.Inst.obsession_Set_1();

        TextFine(5);
        Typing.GetComponent<TypeEffect>().Type(false, true);
    }
    void 이곳_궁금()
    {
        //당신처럼 이곳에서 눈을 뜬 사람들을 관리하고 고용하는 곳이에요.
        TextFine(6);
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "1";
    }
    void 이곳_궁금_1()
    {
        //주 업무로 하고 있어요.
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "2";
    }
    void 이곳_궁금_2()
    {
        SpriteChange.Inst.obsession_Default();
        //그리 어렵지 않을거에요.
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "3";
    }
    void 이곳_궁금_3()
    {
        SpriteChange.Inst.obsession_Brows_1();

        //네? 그들에 대해서..?
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "4";
    }
    void 이곳_궁금_4()
    {
        SpriteChange.Inst.obsession_Eyes_4();
        SpriteChange.Inst.obsession_Brows_2();

        //사실 저도 잘 몰라요.
        Typing.GetComponent<TypeEffect>().Type(true, false);
        TextI = "5";
    }
    void 이곳_궁금_5()
    {
        SpriteChange.Inst.obsession_Eyes_4();
        SpriteChange.Inst.obsession_Brows_1();

        // 당연하게 그런 걸로 되어있어요.
        Typing.GetComponent<TypeEffect>().Type(true, false);
        
        TextT = "안내_시작";
    }
    void 안내_시작()
    {
        TextFine(7);
        SpriteChange.Inst.obsession_Default();

        //일이 조금 많아서 
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextT = "안내_시작_";
        TextI = "1";
    }
    void 안내_시작_1()
    {
        //부디 제 경고를 잊지 마시길
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 안내_시작_2()
    {
        인게임시작();
    }
    #endregion

    #region 풀리지 않은 의문
    IEnumerator 풀리지않은의문()
    {
        TextFine(9);
        TalkCamear();

        //문 열리는 연출
        SCI_Gate.Inst.OpenGate();
        ClipManager.Inst.GateOpenSound();
        yield return new WaitForSeconds(0.3f);

        //캐릭터 세팅
        CameraMove.Inst.FaceOut();
        CameraMove.Inst.FaceLowIn(0.02f);

        SpriteChange.Inst.obsession_Default();
        yield return new WaitForSeconds(1f);

        SpriteChange.Inst.obsession_Brows_1();
        SpriteChange.Inst.obsession_Eyes_3();

        //문닫기
        SCI_Gate.Inst.CloseGate();
        ClipManager.Inst.GateCloseSound();
        yield return new WaitForSeconds(0.2f);

        TextBox.GetComponent<TextBox>().UpBg();
        yield return new WaitForSeconds(1f);

        SpriteChange.Inst.obsession_Brows_3();
        SpriteChange.Inst.obsession_Eyes_1();

        //수고하셨어요
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 풀리지않은의문_1()
    {
        TextFine(10);
        SpriteChange.Inst.obsession_Brows_1();

        //작업은 어떠셨나요. 할만하셨나요?
        Typing.GetComponent<TypeEffect>().Type(true, true);
    }
    void 그럭저럭()
    {
        TextFine(11);

        SpriteChange.Inst.obsession_Brows_3();
        SpriteChange.Inst.obsession_Eyes_2();

        //할만하셨다니 다행이에요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 그럭저럭_1()
    {
        SpriteChange.Inst.obsession_Eyes_1();

        //상대가 약한 편
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 그럭저럭_2()
    {
        SpriteChange.Inst.obsession_Brows_1();
        SpriteChange.Inst.obsession_Eyes_3();

        //마주할 줄 아는 그 용기
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "3";
    }
    void 그럭저럭_3()
    {
        TextFine(12);

        SpriteChange.Inst.obsession_Eyes_1();

        //(당신의 말을 기달리는 듯하다.)
        Typing.GetComponent<TypeEffect>().Type(false, true);
    }
    void 이곳에대해()
    {
        TextFine(13);

        SpriteChange.Inst.obsession_Eyes_1();

        //... (어두운 표정)
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 이곳에대해_1()
    {
        //역시 말씀드려야겠죠? (어두운 표정)
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 이곳에대해_2()
    {
        SpriteChange.Inst.obsession_Brows_2();
        SpriteChange.Inst.obsession_Eyes_3();

        //그닥 좋은 이야기는 아니라서
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "3";
    }
    void 이곳에대해_3()
    {
        TextFine(14);

        SpriteChange.Inst.obsession_Eyes_1();

        //당신이 보기에 저는 인간인가요..?
        Typing.GetComponent<TypeEffect>().Type(true, true);
    }
    void 인간긍정()
    {
        TextFine(15);

        //아쉽게도 그렇지 않아요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 인간긍정_1()
    {
        //인간의 모습을 흉내내는 무언가
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 인간긍정_2()
    {
        TextFine(16);

        //당신이 이곳에 오기 전에 무엇
        Typing.GetComponent<TypeEffect>().Type(true, true);
    }
    void 해파리()
    {
        TextFine(17);

        //그 해파리가 인간을 이곳
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 해파리_1()
    {
        TextFine(18);

        //아마도 그들의 업무로
        Typing.GetComponent<TypeEffect>().Type(true, true);
    }
    void 아마도()
    {
        TextFine(19);

        //저희 관리직은 대대로 계승
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 아마도_1()
    {
        //관리인이 죽게 되면 그의 담당
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 아마도_2()
    {
        //하지만 그걸 누군가에게 전달받
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "3";
    }
    void 아마도_3()
    {
        //그들에 대한 것처럼 당연하게 그렇다고 생각하
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "4";
    }
    void 아마도_4()
    {
        TextFine(20);

        //해파리에 대한 건 그 당연한 지식
        Typing.GetComponent<TypeEffect>().Type(true, true);
    }
    void 믿기어려운()
    {
        TextFine(21);

        //그렇기에 관리인은 바깥을 보여주기
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 믿기어려운_1()
    {
        //오히려 관리인을 공격하
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 믿기어려운_2()
    {
        TextFine(22);

        //그 때는 정말
        Typing.GetComponent<TypeEffect>().Type(true, true);

        TextI = "2";
    }
    void 그경우어떻게()
    {
        TextFine(23);

        //이곳의 치안을 지키기 위해서
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 그경우어떻게_1()
    {
        //충분히 말이 길어졌네요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 그경우어떻게_2()
    {
        //내일 다시 오겠습니다.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "3";
    }
    void 그경우어떻게_3()
    {
        인게임시작();
    }
    void 인간부정()
    {
        TextFine(24);

        //농담인가요?
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "1";
    }
    void 인간부정_1()
    {
        //정말이시라면 신기하네요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 인간부정_2()
    {
        //알아차리는 사람은 몇 없었거든요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "3";
    }
    void 인간부정_3()
    {
        TextFine(25);

        //알아차리는 사람은 몇 없었거든요.
        Typing.GetComponent<TypeEffect>().Type(true, true);
    }
    void 다음목표재촉()
    {
        TextFine(26);

        SpriteChange.Inst.obsession_Eyes_1();

        //당신이라는 사람에 대해 궁금
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextT = "다음목표재촉_";
        TextI = "1";
    }
    void 다음목표재촉_1()
    {
        //노력하는 건 좋지만 무리는 하지 말아
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 다음목표재촉_2()
    {
        //제 사원이 죽는 건 보고 싶
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "3";
    }
    void 다음목표재촉_3()
    {
        인게임시작();
    }
    void 의문타임아웃()
    {
        TextFine(27);

        //저희도 이곳에 대해 알아가려 하고
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextT = "의문타임아웃_";
        TextI = "1";
    }
    void 의문타임아웃_1()
    {
        //하지만 그게 마냥 간단하지는 않아요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "2";
    }
    void 의문타임아웃_2()
    {
        //중요한 건 저희는 아군이라는 거에요
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "3";
    }
    void 의문타임아웃_3()
    {
        //부디 오래 살아남아주세요. 사람을 잊는 건 너무나도 힘들거든요.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextI = "4";
    }
    void 의문타임아웃_5()
    {
        인게임시작();
    }
    void 의문해파리타임아웃()
    {
        TextFine(28);

        //혹시 기억이 안나시나요..?
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextT = "의문해파리타임아웃_";
        TextI = "1";
    }
    void 의문해파리타임아웃_1()
    {
        //괜찮을거에요. 금방 다시 떠오르겠죠.
        Typing.GetComponent<TypeEffect>().Type(true, false);

        TextT = "의문타임아웃_";
        TextI = "2";
    }
    #endregion

    #region 인게임시작
    void 인게임시작()
    {
        StoryOpen.Inst.Openning(Player.Inst.playerdata.story);

        Onclick.Inst.OnClickStory_main();
    }
    #endregion
}
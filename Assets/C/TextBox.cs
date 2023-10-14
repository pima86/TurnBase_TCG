using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextBox : MonoBehaviour
{
    public static TextBox Inst { get; private set; }
    void Awake() => Inst = this;

    private RectTransform Rt;
    public RectTransform TimerRt;

    public Slider slTimer;
    public Image Timer;
    public Image Timer_;

    public RectTransform Sel_1;
    public Button bt_1;
    public RectTransform Sel_2;
    public Button bt_2;
    public RectTransform Sel_3;
    public Button bt_3;

    public TextMeshProUGUI Tt_1;
    public TextMeshProUGUI Tt_2;
    public TextMeshProUGUI Tt_3;

    public int Bracket_point = 0;
    public bool Bracket_bool = false;
    public GameObject Bracket_1;
    public GameObject Bracket_2;
    public GameObject Bracket_3;

    public Vector3 UpPoint = new Vector3(0, -375, 0);
    private Vector3 DownPoint = new Vector3(0, -850, 0);

    private Vector3 RightPoint;
    private Vector3 LeftPoint;

    public float Speed;

    private bool RtUpDown = false;
    private bool SelSide = false;

    public int Timer_Control = 0;


    public GameObject MouseEvent;
    private bool StayBool = true;

    public GameObject Auto;

    void Start()
    {
        Rt = this.GetComponent<RectTransform>();
    }

    void Update()
    {

        if (Timer_Control > 0)
        {
            if (Timer_Control == 1 && slTimer.value < 10f)
            {
                slTimer.value += Time.deltaTime;
                if (slTimer.value > 5f && StayBool)
                {
                    StayBool = false;
                    MouseEvent.GetComponent<MouseEvent>().StayChapter();
                }
            }
            else if (Timer_Control == 2 && slTimer.value < 5f)
            {
                slTimer.value += Time.deltaTime;
            }
            else if (Timer_Control == 1 && slTimer.value >= 10f)
            {
                Timer_Control = 0;
                slTimer.value = 0;
                MouseEvent.GetComponent<MouseEvent>().SkipChapter();
            }
            else if (Timer_Control == 2 && slTimer.value >= 5f)
            {
                Timer_Control = 0;
                slTimer.value = 0;
                MouseEvent.GetComponent<MouseEvent>().SkipChapter();
            }
        }

        if (RtUpDown)
            Rt.anchoredPosition = Vector3.Lerp(Rt.anchoredPosition, UpPoint, Speed);
        else if (!RtUpDown)
            Rt.anchoredPosition = Vector3.Lerp(Rt.anchoredPosition, DownPoint, Speed);

        if (SelSide)
        {
            TimerRt.anchoredPosition = Vector3.Lerp(TimerRt.anchoredPosition, new Vector3(0, TimerRt.anchoredPosition.y, 0), Speed);
            Sel_1.anchoredPosition = Vector3.Lerp(Sel_1.anchoredPosition, new Vector3(0, Sel_1.anchoredPosition.y, 0), Speed);
            if (Sel_1.anchoredPosition.x > -250)
                Sel_2.anchoredPosition = Vector3.Lerp(Sel_2.anchoredPosition, new Vector3(0, Sel_2.anchoredPosition.y, 0), Speed);
            if (Sel_2.anchoredPosition.x > -250)
                Sel_3.anchoredPosition = Vector3.Lerp(Sel_3.anchoredPosition, new Vector3(0, Sel_3.anchoredPosition.y, 0), Speed);
        }
        else if (!SelSide)
        {
            TimerRt.anchoredPosition = Vector3.Lerp(TimerRt.anchoredPosition, new Vector3(-300, TimerRt.anchoredPosition.y, 0), Speed);
            Sel_1.anchoredPosition = Vector3.Lerp(Sel_1.anchoredPosition, new Vector3(-300, Sel_1.anchoredPosition.y, 0), Speed);
            Sel_2.anchoredPosition = Vector3.Lerp(Sel_2.anchoredPosition, new Vector3(-300, Sel_2.anchoredPosition.y, 0), Speed);
            Sel_3.anchoredPosition = Vector3.Lerp(Sel_3.anchoredPosition, new Vector3(-300, Sel_3.anchoredPosition.y, 0), Speed);
        }

        //괄호 표시하기
        if (Bracket_point == 1)
        {
            Bracket_1.SetActive(true);
            Bracket_2.SetActive(false);
            Bracket_3.SetActive(false);
            if (Input.GetKey(KeyCode.Return))
            {
                bt_1.onClick.Invoke();
            }
        }
        else if (Bracket_point == 2)
        {
            Bracket_1.SetActive(false);
            Bracket_2.SetActive(true);
            Bracket_3.SetActive(false);
            if (Input.GetKey(KeyCode.Return))
            {
                bt_2.onClick.Invoke();
            }
        }
        else if (Bracket_point == 3)
        {
            Bracket_1.SetActive(false);
            Bracket_2.SetActive(false);
            Bracket_3.SetActive(true);
            if (Input.GetKey(KeyCode.Return))
            {
                bt_3.onClick.Invoke();
            }
        }
        else
        {
            Bracket_1.SetActive(false);
            Bracket_2.SetActive(false);
            Bracket_3.SetActive(false);
        }

        //괄호 위치 방향키로 조정하기
        if (Input.GetKey(KeyCode.UpArrow) && Bracket_bool)
            StartCoroutine(BracketUp());
        else if (Input.GetKey(KeyCode.DownArrow) && Bracket_bool)
            StartCoroutine(BracketDown());
    }

    public void UpBg() //지연시간
    {
        RtUpDown = true;
    }

    public void TimeControl_1() // 10초
    {
        slTimer.maxValue = 10;
        Timer_Control = 1;
        slTimer.value = 0;
        StayBool = true;
    }

    public void TimeControl_2() // 5초
    {
        slTimer.maxValue = 10;
        slTimer.value = 0;
        Timer_Control = 2;
    }

    public void SelOn()
    {
        if(SelOn_bool)
            StartCoroutine(FadeInCoroutine());
    }

    public void SelOff()
    {
        if (SelOff_bool)
            StartCoroutine(SelOffCoroutine());
    }

    public void DownBg()
    {
        RtUpDown = false;
    }

    private bool SelOn_bool = true;
    IEnumerator FadeInCoroutine()
    {
        
        SelOn_bool = false;
        Bracket_point = 0;
        float fadeCount = 0;
        SelSide = true;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.001f);
            Timer.color = new Color(1f, 1f, 1f, fadeCount);
            Timer_.color = new Color(1, 1, 1, fadeCount);
            Tt_1.color = new Color(1, 1, 1, fadeCount);
            Tt_2.color = new Color(1, 1, 1, fadeCount);
            Tt_3.color = new Color(1, 1, 1, fadeCount);
        }
        Bracket_bool = true;

        yield return new WaitForSeconds(1);
        SelOn_bool = true;
    }

    private bool SelOff_bool = true;
    IEnumerator SelOffCoroutine()
    {
        SelOff_bool = false;
        
        Bracket_bool = false;
        Timer.color = new Color(0.3f, 0.3f, 0.3f, 0);
        Timer_.color = new Color(1, 1, 1, 0);
        Tt_1.color = new Color(1, 1, 1, 0);
        Tt_2.color = new Color(1, 1, 1, 0);
        Tt_3.color = new Color(1, 1, 1, 0);
        SelSide = false;
        yield return new WaitForSeconds(0.1f);
        Bracket_point = -1;
        SelOff_bool = true;
    }

    IEnumerator BracketUp()
    {
        Bracket_bool = false;
        if (Tt_2.text != "")
        {
            if (Bracket_point == 0)
                Bracket_point = 1;
            else
                Bracket_point -= 1;

            if (Bracket_point <= 0)
            {
                if (Tt_3.text == "")
                {
                    Bracket_point = 2;
                }
                else
                {
                    Bracket_point = 3;
                }
            }
        }
        else
        {
            Bracket_point = 1;
        }
        yield return new WaitForSeconds(0.2f);
        Bracket_bool = true;
    }
    IEnumerator BracketDown()
    {
        Bracket_bool = false;

        if (Bracket_point == 0)
        {
            if (Tt_3.text == "")
                if (Tt_2.text == "")
                    Bracket_point = 1;
                else
                    Bracket_point = 2;
            else
                Bracket_point = 3;
        }
        else
        {
            if (Tt_3.text != "" && Bracket_point == 2)
                Bracket_point = 3;
            if (Tt_2.text != "")
                Bracket_point = 2;
            else
                Bracket_point = 1;
        }

        if (Bracket_point >= 4)
            Bracket_point = 1;
        yield return new WaitForSeconds(0.2f);

        Bracket_bool = true;
    }
}

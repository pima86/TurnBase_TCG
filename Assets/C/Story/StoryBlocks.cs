using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StoryBlocks : MonoBehaviour
{
    [SerializeField] StorySO storySO;
    [SerializeField] TMP_Text name;

    [SerializeField] SpriteRenderer illust_in;
    [SerializeField] public SpriteRenderer icon;

    [SerializeField] GameObject illust_line;
    [SerializeField] GameObject Clear;

    public Story story;
    public PRS originPRS;
    public int originAdd;
    public string kind; //전투인지 대화인지 보스인지
    public string map;

    public string content;

    float time = 0;
    void Update()
    {
        time += Time.deltaTime;

        if (time > 1f)
        {
            Clickdo = false;
            time = 0;
        }

        ThisStroySelect();
    }

    void ThisStroySelect()
    {
        if (Player.Inst.playerdata.storyList[originAdd].clear)
            Clear.SetActive(true);
        if (name.text == Player.Inst.playerdata.story)
            illust_line.SetActive(true);
        else
            illust_line.SetActive(false);
    }

    public void SetUp(Story story)
    {
        this.story = story;

        name.text = this.story.name;
        kind = this.story.kind;
        map = this.story.map;
        content = this.story.content;
        IconSprite.Inst.ChangeIcon(this);
        for (int i = 0; i < storySO.stories.Length; i++)
        {
            if (name.text == storySO.stories[i].name)
            {
                originAdd = i;
            }
        }

        if (Player.Inst.playerdata.storyList[originAdd].content != "1")
            ColorSetUp();
    }

    Color color;
    void ColorSetUp()
    {
        ClickDo = false;

        ColorUtility.TryParseHtmlString("#343941", out color);
        illust_in.color = color;
        ColorUtility.TryParseHtmlString("#9F9F9F", out color);
        name.color = color;
    }

    float duration = 0.1f;
    float smoothness = 0.01f;
    public IEnumerator LerpColor()
    {
        StoryCam.Inst.CamMove = false;
        ClipManager.Inst.GameStart();
        yield return new WaitForSeconds(0.5f);

        float progress = 0;
        float increment = smoothness / duration;
        while (progress < 1)
        {
            Color a;
            Color b;
            Color c;

            ColorUtility.TryParseHtmlString("#343941", out a);
            ColorUtility.TryParseHtmlString("#FFFFFF", out b);
            c = Color.Lerp(a, b, progress);
            illust_in.color = c;

            ColorUtility.TryParseHtmlString("#FFFFFF", out a);
            ColorUtility.TryParseHtmlString("#1D1D1D", out b);
            c = Color.Lerp(a, b, progress);
            name.color = c;

            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

        ClickDo = true;
        StoryCam.Inst.pos_addrass += 1;
        StoryCam.Inst.CamMove = true;

        Player.Inst.playerdata.storyList[originAdd].content = "1";
        Player.Inst.Save();
    }

    public bool SetA = true;
    public void SetAct()
    {
        SetA = false;
        gameObject.SetActive(false);
    }

    void Start()
    {
        if (Player.Inst.playerdata.story == name.text)
        {
            BlockSearch.Inst.NameSetting(name.text, kind, icon.sprite, content);
        }
    }

    bool ClickDo = true; //흑백 블럭의 클릭 방지용
    bool Clickdo = false; //더블클릭
    void OnMouseDown()
    {
        if (ClickDo && !StoryCam.Inst.CamMove_sub)
        {
            if (Player.Inst.playerdata.story != name.text)
            {
                Player.Inst.playerdata.story = name.text;
                ClipManager.Inst.GateLight();
                Player.Inst.Save();
            }

            BlockSearch.Inst.NameSetting(name.text, kind, icon.sprite, content);

            if (map == "시설" && Clickdo)
                Onclick.Inst.OnClickMain();
            else if (map == "N1" && Clickdo)
                Onclick.Inst.OnClickN1_Battle();
        }
        Clickdo = true;
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}

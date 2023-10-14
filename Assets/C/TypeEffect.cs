using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public AudioClip clip;

    public GameObject Story; //대화 로그 불러오기 
    public GameObject TextBox;
    public GameObject talkEnd;

    public TextMeshProUGUI m_TypingText; //타이핑 될 text

    #region stay
    public TextMeshProUGUI CT_1;
    public TextMeshProUGUI CT_2;
    public TextMeshProUGUI CT_3;
    public float m_Speed = 0.05f; //타이핑 속도

    public int ChoiAD = 0;

    public int TalkID = 1; //대화 로그 아이디
    public int TalkAD = 0; //대화 로그 주소

    public string tt; //텍스트 저장 변수

    public void Type(bool talk, bool sel)
    {
        tt = Story.GetComponent<StoryEvent>().GetTalk(TalkID, TalkAD);

        coroutine = Typing(m_TypingText, tt, m_Speed, talk, sel);
        StartCoroutine(coroutine);
        TalkAD += 1;
    }

    public void SubType(bool talk)
    {
        tt = Story.GetComponent<StoryEvent>().GetTalk(TalkID, TalkAD);

        Sub_coroutine = SubTyping(m_TypingText, tt, m_Speed, talk);
        StartCoroutine(Sub_coroutine);
        TalkAD += 1;
    }

    private IEnumerator coroutine;
    private IEnumerator Sub_coroutine;
    public bool IsAnim = false;
    public bool AutoMode = false;

    public int startPoint = 0;
    public int endPoint = 0;
    public bool WaveFont = false;
    public bool WiggleFont = false;


    Mesh mesh;

    Vector3[] vertices;

    public Gradient rainbow;

    void Start()
    {
    }
    #endregion

    void Update()
    {
        m_TypingText.ForceMeshUpdate();
        mesh = m_TypingText.mesh;
        vertices = mesh.vertices;

        Color[] colors = mesh.colors;
        var textInfo = m_TypingText.textInfo;


        if (endPoint != 0)
        {
            //위아래로 울렁
            if (WaveFont)
            {
                for (int i = startPoint; i < endPoint + startPoint; ++i)
                {
                    var charInfo = textInfo.characterInfo[i];
                    if (!charInfo.isVisible)
                        continue;
                    TMP_CharacterInfo c = m_TypingText.textInfo.characterInfo[i];

                    int index = c.vertexIndex;

                    colors[index] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.001f, 1f));
                    colors[index + 1] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.001f, 1f));
                    colors[index + 2] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.001f, 1f));
                    colors[index + 3] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.001f, 1f));

                    var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                    for (int j = 0; j < 4; ++j)
                    {
                        var orig = verts[charInfo.vertexIndex + j];
                        verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0);
                    }
                }

                for (int i = 0; i < textInfo.meshInfo.Length; ++i)
                {
                    var meshInfo = textInfo.meshInfo[i];
                    meshInfo.mesh.vertices = meshInfo.vertices;
                    m_TypingText.UpdateGeometry(meshInfo.mesh, i);
                }

                mesh.colors = colors;
                m_TypingText.canvasRenderer.SetMesh(mesh);
            }

            if (WiggleFont)
            {
                m_TypingText.ForceMeshUpdate();
                mesh = m_TypingText.mesh;
                vertices = mesh.vertices;

                for (int i = startPoint; i < endPoint; ++i)
                {
                    TMP_CharacterInfo c = m_TypingText.textInfo.characterInfo[i];
                    int index = c.vertexIndex;
                    Vector3 offset = Wobble(Time.time + i);

                    vertices[index] += offset;
                    vertices[index + 3] += offset;
                    vertices[index + 1] += offset;
                    vertices[index + 2] += offset;
                }

                mesh.vertices = vertices;
                m_TypingText.canvasRenderer.SetMesh(mesh);
            }
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 20), Mathf.Cos(time * 20));
    }

    IEnumerator Auto()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Find("Card").GetComponent<MouseEvent>().Auto();
    }

    IEnumerator SubTyping(TextMeshProUGUI typingText, string message, float speed, bool talk)
    {
        IsAnim = true;
        talkEnd.SetActive(false);
        WaveFont = false;
        WiggleFont = false;
        startPoint = 0;
        endPoint = 0;

        int j = 0; // <> 확인 용
        int font_Length = message.Length;
        float Temp_speed = speed;
        bool fontstyle = false;

        for (int i = 0; j + i < font_Length; i++)
        {
            if (message.Substring(j, i + 1).Contains("<"))
            {
                if (j != 0)
                {
                    if (endPoint == 0)
                        endPoint = i;
                    if (message.Substring(j, i + 1).Contains(">"))
                    {
                        j = j + i + 1;
                        i = 0;
                        font_Length -= i;
                        speed = Temp_speed;
                    }
                    else
                    {
                        speed = 0.001f;
                    }



                    SoundPlayer.Inst.SFXPlay("Talk", clip);
                    typingText.text = message.Substring(0, j);

                    SpriteChange.Inst.Talk_ing();
                }
                else if (message.Substring(j, i + 1).Contains(">"))
                {
                    if (font_Length >= j + i + 11 && i >= 10)
                    {
                        if (message.Substring(i - 10, 11) == "<color=red>")
                        {
                            WiggleFont = true;
                            startPoint = i - 10;
                        }
                    }

                    if (font_Length >= j + i + 14 && i >= 13)
                    {
                        if (message.Substring(i - 13, 14) == "<color=purple>")
                        {
                            WaveFont = true;
                            startPoint = i - 13;
                        }
                    }

                    j = j + i + 1;
                    i = 0;
                    font_Length -= i;
                    speed = Temp_speed;
                }
                else
                {
                    speed = 0.001f;
                }
            }
            else if (!message.Substring(j, i + 1).Contains("<") && !message.Substring(j, i + 1).Contains(">"))
            {
                speed = Temp_speed;

                SoundPlayer.Inst.SFXPlay("Talk", clip);
                typingText.text = message.Substring(0, j + i + 1);
                SpriteChange.Inst.Talk_ing();
            }
            yield return new WaitForSeconds(speed);
        }
        SpriteChange.Inst.Talk_end();

        talkEnd.SetActive(true);
        IsAnim = false;
    }

    public bool Sel;
    IEnumerator Typing(TextMeshProUGUI typingText, string message, float speed, bool talk, bool sel)
    {
        if(Sub_coroutine != null)
            StopCoroutine(Sub_coroutine);
        StopCoroutine("Auto");

        IsAnim = true;
        Sel = sel;
        talkEnd.SetActive(false);
        WaveFont = false;
        WiggleFont = false;
        startPoint = 0;
        endPoint = 0;

        if (Story.GetComponent<StoryEvent>().GetChoice(TalkID, ChoiAD) != "/")
        {
            CT_1.text = Story.GetComponent<StoryEvent>().GetChoice(TalkID, ChoiAD);
            CT_2.text = Story.GetComponent<StoryEvent>().GetChoice(TalkID, ChoiAD + 1);
            CT_3.text = Story.GetComponent<StoryEvent>().GetChoice(TalkID, ChoiAD + 2);
        }
        else
        {
            CT_1.text = "";
            CT_2.text = "";
            CT_3.text = "";
        }

        int j = 0; // <> 확인 용
        int font_Length = message.Length;
        float Temp_speed = speed;

        if (talk)
        {
            for (int i = 0; j + i < font_Length; i++)
            {
                if (message.Substring(j, i + 1).Contains("<"))
                {
                    if (j != 0)
                    {
                        if (endPoint == 0)
                            endPoint = i;
                        if (message.Substring(j, i + 1).Contains(">"))
                        {
                            j = j + i + 1;
                            i = 0;
                            font_Length -= i;
                            speed = Temp_speed;

                            SoundPlayer.Inst.SFXPlay("Talk", clip);
                            typingText.text = message.Substring(0, j);
                        }
                        else
                            speed = 0.001f;

                        SpriteChange.Inst.Talk_ing();
                    }
                    else if (message.Substring(j, i + 1).Contains(">"))
                    {
                        if (font_Length >= j + i + 11 && i >= 10)
                        {
                            if (message.Substring(i - 10, 11) == "<color=red>")
                            {
                                WiggleFont = true;
                                startPoint = i - 10;
                            }
                        }

                        if (font_Length >= j + i + 14 && i >= 13)
                        {
                            if (message.Substring(i - 13, 14) == "<color=purple>")
                            {
                                WaveFont = true;
                                startPoint = i - 13;
                            }
                        }

                        j = j + i + 1;
                        i = 0;
                        font_Length -= i;
                        speed = Temp_speed;
                    }
                    else
                    {
                        speed = 0.001f;
                    }
                }
                else if (!message.Substring(j, i + 1).Contains("<") && !message.Substring(j, i + 1).Contains(">"))
                {
                    speed = Temp_speed;

                    SoundPlayer.Inst.SFXPlay("Talk", clip);
                    typingText.text = message.Substring(0, j + i + 1);
                    SpriteChange.Inst.Talk_ing();
                }
                yield return new WaitForSeconds(speed);
                SpriteChange.Inst.Talk_end();
            }
        }
        else
            typingText.text = message;

        if (sel)
        {
            TextBox.GetComponent<TextBox>().TimeControl_1();
            TextBox.GetComponent<TextBox>().SelOn();
        }
        else
        {
            if (AutoMode)
                StartCoroutine("Auto");
        }


        talkEnd.SetActive(true);
        IsAnim = false;
    }
}
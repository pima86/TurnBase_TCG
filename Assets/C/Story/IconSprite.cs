using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSprite : MonoBehaviour
{
    public static IconSprite Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Sprite[] Icons;

    public void ChangeIcon(StoryBlocks sb)
    {
        if (sb.kind == "Ω∫≈‰∏Æ")
            sb.icon.sprite = Icons[0];
        else if (sb.kind == "F")
            sb.icon.sprite = Icons[1];
        else if (sb.kind == "E")
            sb.icon.sprite = Icons[2];
        else if (sb.kind == "D")
            sb.icon.sprite = Icons[3];
        else if (sb.kind == "C")
            sb.icon.sprite = Icons[4];
        else if (sb.kind == "B")
            sb.icon.sprite = Icons[5];
    }
}

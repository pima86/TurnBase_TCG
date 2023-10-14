using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCam : MonoBehaviour
{
    public static StoryCam Inst { get; private set; }
    void Awake() => Inst = this;

    void Start()
    {
        Invoke("Content", 0.01f);
    }

    [SerializeField] List<Vector3> pos;
    public int pos_addrass = 0;
    public bool CamMove = false;
    public bool CamMove_sub = false;
    public List<StoryBlocks> sb;
    void Update()
    {
        if (sb.Count > pos_addrass)
        {
            if (CamMove) //&& LoadPanel.Inst.LoadCompletion)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, pos[pos_addrass], Time.deltaTime * 2);

                if (gameObject.transform.position.x < (pos[pos_addrass].x + 0.5f) && gameObject.transform.position.x > (pos[pos_addrass].x - 0.5f) &&
                    gameObject.transform.position.y < (pos[pos_addrass].y + 0.5f) && gameObject.transform.position.y > (pos[pos_addrass].y - 0.5f))
                {
                    StartCoroutine(sb[pos_addrass].LerpColor());
                }
            }
        }
        else
        {
            CamMove = false;
            CamMove_sub = false;
        }
    }

    void Content()
    {
        for (int i = 0; i < StoryManager.Inst.StoryLists.Count; i++)
        {
            if (Player.Inst.playerdata.storyList[i].name == Player.Inst.playerdata.story)
                gameObject.transform.position = StoryManager.Inst.StoryLists[i].transform.position + new Vector3(0, 0, -100);
            if (Player.Inst.playerdata.storyList[i].content == "0")
            {
                sb.Add(StoryManager.Inst.StoryLists[i]);
                pos.Add(StoryManager.Inst.StoryLists[i].transform.position + new Vector3(0, 0, -100));

                CamMove_sub = true;
                CamMove = true;
            }
        }
    }
}

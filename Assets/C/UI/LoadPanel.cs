using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadPanel : MonoBehaviour
{
    public static LoadPanel Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] List<GameObject> Load_Bar;
    [SerializeField] GameObject Prefab;
    [SerializeField] GameObject isLoading;
    [SerializeField] GameObject isStickLoading;

    [SerializeField] Image LoadIcon;

    [SerializeField] List<int> addrass;
    [SerializeField] List<float> speed;

    [SerializeField] float SpawnPoint_x;

    string SceneName;

    void Start()
    {
        float obj_height = Prefab.GetComponent< RectTransform > ().rect.height;
        
        for (int i = 0; Load_Bar.Count * obj_height <= 1080; i++)
        {
            Vector3 spawnpoint = new Vector3(SpawnPoint_x, obj_height * i, 0);
            var temp = Instantiate(Prefab, spawnpoint, Utils.QI);
            temp.transform.SetParent(this.transform, true);
            Load_Bar.Add(temp);
        }
    }

    bool LoadStart = false; //막대기들 들어올지
    public bool LoadEnd; //막대기들 나갈지
    public bool LoadBlink;
    bool LoadBlink_sub = false;
    float time = 0;
    float timeColor = 0;

    void Update()
    {
        time += Time.deltaTime;

        if (LoadStart)
            LoadStart_Script();

        if (LoadEnd)
            LoadEnd_Script();

        if (LoadBlink)
            LoadBlink_Script();
    }
    public void SceneMove(string name)
    {
        time = 0;
        LoadStart = true;
        LoadEnd = false;
        SceneName = name;

        ClipManager.Inst.GameStart();
    }

    public void Load_ing(bool temp)
    {
        if(temp == false)
            LoadIcon.color = new Color(1, 1, 1, 0);
        LoadBlink = temp;
    }

    #region Update 내의 스크립트
    void LoadBlink_Script()
    {
        if (LoadBlink_sub)
        {
            timeColor += Time.deltaTime;
            LoadIcon.color = new Color(1, 1, 1, timeColor);
            if (timeColor >= 1)
                LoadBlink_sub = false;
        }
        else if (!LoadBlink_sub)
        {
            timeColor -= Time.deltaTime;
            LoadIcon.color = new Color(1, 1, 1, timeColor);
            if (timeColor <= 0)
                LoadBlink_sub = true;
        }
    }

    public bool LoadCompletion = false;
    void LoadStart_Script()
    {
        RandomAddrass();
        ScreenClose(900);

        if (time >= 1)
            isLoading.transform.position = Vector2.Lerp(isLoading.transform.position, new Vector2(960, 540), 2f * Time.deltaTime);

        if (isLoading.transform.position.x <= 1340)
        {
            Load_ing(true); //로딩 아이콘 생성
            SoundPlayer.Inst.Clear_BgSound();
            LoadScene(); //해당 씬으로 전환
        }
    }

    void LoadEnd_Script()
    {
        RandomAddrass();
        ScreenClose(4000);
        Load_ing(false); //로딩 아이콘 삭제

        if (time >= 1)
            transform.position = Vector2.Lerp(transform.position, new Vector2(4000, 540), 2f * Time.deltaTime);

        if (isStickLoading.transform.position.x >= 3500) //씬이 전환되고 로딩을 마친 후 작업을 시행하는 시간
            LoadCompletion = true;
        else
            LoadCompletion = false;
    }

    void LoadScene()
    {
        if (SceneName == "Main")
            SceneManager.LoadScene("Main");
        else if (SceneName == "Story")
            SceneManager.LoadScene("Story");
        else if (SceneName == "N1_Battle")
            SceneManager.LoadScene("N1_Battle");
    }

    void RandomAddrass()
    {
        int num = Random.Range(0, Load_Bar.Count);
        if (!addrass.Contains(num))
        {
            speed.Add(Random.Range(2.0f, 3.0f));
            addrass.Add(num);
        }
    }

    void ScreenClose(int num)
    {
        for (int i = 0; i < addrass.Count; i++)
        {
            Load_Bar[addrass[i]].transform.position
                = Vector2.Lerp(Load_Bar[addrass[i]].transform.position, new Vector2(num, Load_Bar[addrass[i]].transform.position.y), speed[i] * Time.deltaTime);
        }
    }
    #endregion
}

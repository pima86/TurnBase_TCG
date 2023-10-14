using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField] [Tooltip("시작 턴 모드를 정합니다")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("시작 카드 개수를 정합니다")] int startCardCount;

    [SerializeField] Light background;

    [Header("Properties")]
    public bool myTurn;
    public bool isLoading;

    enum ETurnMode { My} //드랍다운
    WaitForSeconds delay02 = new WaitForSeconds(0.2f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action OnAddCard;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Input.GetMouseButton(0) && !BattleCam.Inst.isPlay)
        {
            EndTurnKey();
        }
    }

    #region 톱니바퀴 회저어언
    float duration = 1f;
    float smoothness = 0.02f;
    Color currentColor = Color.white;

    IEnumerator ColorChange(string name)
    {
        float progress = 0;
        float increment = smoothness / duration;

        while (progress < 1 && name == "blue")
        {
            currentColor = Color.Lerp(Color.red, Color.blue, progress);
            background.color = currentColor;
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        while (progress < 1 && name == "red")
        {
            currentColor = Color.Lerp(Color.blue, Color.red, progress);
            background.color = currentColor;
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
    }

    public bool Circleing = false;
    IEnumerator CircleMyTrun()
    {
        PlayerCharacter.Inst.ZeroShield();
        StageBlock.Inst.Move_Possible();

        Circleing = true;

        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, 3.5f, gameObject.transform.position.z), 0.5f);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ColorChange("blue"));
        transform.DORotate(new Vector3(0, 0, 180), 1f);
        yield return new WaitForSeconds(0.8f);

        transform.DORotate(new Vector3(0, 0, 185), 0.2f);
        yield return new WaitForSeconds(0.1f);

        transform.DORotate(new Vector3(0, 0, 180), 0.2f);
        yield return new WaitForSeconds(0.1f);

        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, 4.5f, gameObject.transform.position.z), 0.5f);
        yield return new WaitForSeconds(0.5f);

        Circleing = false;
        MobManager.Inst.monsterStay();
    }

    IEnumerator CircleMobTrun()
    {
        MobManager.Inst.ZeroShield();

        Circleing = true;

        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, 3.5f, gameObject.transform.position.z), 0.5f);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ColorChange("red"));
        transform.DORotate(new Vector3(0, 0, -1), 1f);
        yield return new WaitForSeconds(0.8f);

        transform.DORotate(new Vector3(0, 0, 5), 0.2f);
        yield return new WaitForSeconds(0.1f);

        transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        yield return new WaitForSeconds(0.1f);

        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, 4.5f, gameObject.transform.position.z), 0.5f);
        yield return new WaitForSeconds(0.5f);

        Circleing = false;
        StartCoroutine(MobManager.Inst.monsterGo());
    }
    #endregion

    void OnMouseDown()
    {
        if(Time.timeScale == 1)
            EndTurnKey();
    }

    void EndTurnKey()
    {
        if (myTurn && !Circleing && !isLoading && !BattleCam.Inst.isPlay)
        {
            StopCoroutine("CircleMyTrun");
            EndTurn();
        }
    }

    #region 턴 관련
    void GameSetup()
    {
        switch (eTurnMode)
        {
            case ETurnMode.My:
                myTurn = true;
                break;
        }
    }

    public IEnumerator StartGameCo()
    {
        GameSetup();
        isLoading = true;

            for (int i = 0; i < startCardCount; i++)
        {
            yield return delay02;
            OnAddCard.Invoke();
        }
        
        StartCoroutine(StartTurnCo());
    }

    private bool drow = true;
    IEnumerator StartTurnCo()
    {
        isLoading = true;
        BattleCam.Inst.Stop();
        if (myTurn)
        {
            for (int i = 0; i < MobManager.Inst.InField.Count; i++)
                MobManager.Inst.InField[i].Mob_Turn_buf(); 

            StartCoroutine("CircleMyTrun");
            //GameManager.Inst.Notification("나의 턴");
        }
        else
        {
            PlayerCharacter.Inst.Player_Turn_buf();

            StartCoroutine("CircleMobTrun");
        }

        yield return delay07;
        if (myTurn && drow)
        {
            OnAddCard.Invoke();
            drow = false;
        }
        isLoading = false;
    }

    public void EndTurn()
    {
        myTurn = !myTurn;
        drow = true;
        StartCoroutine(StartTurnCo());
    }
    #endregion
}

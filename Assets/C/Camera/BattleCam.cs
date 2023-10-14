using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using TMPro;

public class BattleCam : MonoBehaviour
{
    public static BattleCam Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject Prefab;

    [SerializeField] SpriteRenderer character;
    [SerializeField] GameObject playerchar;
    [SerializeField] GameObject playerotate;
    [SerializeField] GameObject ItemTarget;
    [SerializeField] Vector3[] ItemPoint;
    [SerializeField] Sprite[] motion;
    [SerializeField] List<Mob> mob;

    [SerializeField] GameObject[] FightCut;
    //[SerializeField] GameObject[] BloodCut;

    public GameObject idle;
    public bool isPlay = false;

    #region ��� ��������
    public void Player_Run() //����
    {
        character.sprite = motion[1];
    }

    public void Player_Re() //����
    {
        character.sprite = motion[2];
    }

    public void Player_De() //���ĵ�
    {
        character.sprite = motion[0];
    }
    #endregion

    void Start()
    {
        Material materialInstance = Instantiate(character.material);
        character.material = materialInstance;

        ScreenSize();
    }

    Sprite value_1 = null;
    bool resetSprite = false;
    float time_1 = 0;

    bool CameraAct = false;
    void Update()
    {
        if (playerchar != null)
        {
            if (character.sprite != value_1)
            {
                value_1 = character.sprite;
                resetSprite = true;
                time_1 = 0;
            }
            if (resetSprite)
            {
                time_1 += Time.deltaTime;
                if (time_1 > 1)
                {
                    resetSprite = false;
                    Player_illustOn(true, 0);
                    value_1 = character.sprite;
                }
            }
        }
        if (CameraAct)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z), Time.deltaTime * 0.1f);
        }
    }

    string card_type;
    string card_effect;
    int damage;

    #region �ػ� ����
    void ScreenSize()
    {
        float Ratio = (float)Screen.width / (float)Screen.height;
        if (Ratio > 1.65f)
        {
            ItemTarget.transform.position = ItemPoint[0];
            gameObject.GetComponent<Camera>().orthographicSize = 3.9f;
        }
        else if (Ratio < 1.65f)
        {
            ItemTarget.transform.position = ItemPoint[1];
            gameObject.GetComponent<Camera>().orthographicSize = 4.3f;
        }
    }
    #endregion
    #region �÷��̾ ���� �Ϲݰ���
    public IEnumerator playerattmotion(Mob mob, Card card, bool Desto = false)
    {
        if (mob.HP - mob.damage > -BuffManager.Inst.damage_player(mob, int.Parse(card.attack.text)))
        {
            isPlay = true;

            if (card != null)
            {
                damage = int.Parse(card.attack.text);
                card_type = card.type.text;
                card_effect = card.effect.text;
            }


            yield return new WaitForSeconds(0.2f);

            switch (Random.Range(0, 4))
            {
                case 0:
                    ClipManager.Inst.hit1();
                    break;
                case 1:
                    ClipManager.Inst.hit2();
                    break;
                case 2:
                    ClipManager.Inst.hit3();
                    break;
                case 3:
                    ClipManager.Inst.hit4();
                    break;
            }

            #region ���� ���� ��� - �̹���
            if (card_type.Contains("����"))
                Player_illustOn(false, 3);
            else if (card_type.Contains("Ÿ��"))
                Player_illustOn(false, 4);
            #endregion
            mob.illustOn(false, 0);
            mob.illustOn(false, 2);

            filpX_turn(mob);

            PlayerCharacter.Inst.TextBox.text = "";
            StartCoroutine(StopMotion(mob, card, false, Desto));
            //damage_eff -> ���� ���� ������ ��� 63��

            if (Desto)
            {
                mob.GetComponent<DeadEffect>().StartDissolve(2);
                mob.DeadHp();
                mob.part.Play();

                yield return new WaitForSeconds(0.5f);
                mob.Destro();
            }

            isPlay = false;
            CardManager.Inst.isUse = false;
        }
    }

    public IEnumerator playerattDestroy(Mob mob, Card card)
    {
        mob.illustOn(false, 0);
        mob.illustOn(false, 2);

        filpX_turn(mob);

        PlayerCharacter.Inst.TextBox.text = "";
        StartCoroutine(StopMotion(mob, card, false, true, false));

        mob.GetComponent<DeadEffect>().StartDissolve(2);
        mob.DeadHp();
        mob.part.Play();

        yield return new WaitForSeconds(0.5f);
        mob.Destro();
    }
    #endregion

    #region ���� �÷��̾ �Ϲݰ���
    public IEnumerator mobattmotion(Mob mob, bool Desto = false, bool mobTurn = true)
    {
        isPlay = true;

        yield return new WaitForSeconds(0.2f);

        if(mobTurn)
            filpX_turn(mob);

        Player_illustOn(false, 2);
        mob.illustOn(false, 3);

        StartCoroutine(StopMotion(mob, null, true, Desto));

        if (Desto)
        {
            playerchar.GetComponent<DeadEffect>().StartDissolve(1.5f);
            playerchar.GetComponent<PlayerCharacter>().DeadHp();
            playerchar.GetComponent<PlayerCharacter>().part.Play();

            yield return new WaitForSeconds(1f);
            playerchar.GetComponent<PlayerCharacter>().Destro();
        }

        isPlay = false;
        CardManager.Inst.isUse = false;
    }
    #endregion

    #region ���� ��ũ��Ʈ ���� �Լ���
    public void Player_illustOn(bool act, int num)
    {
        idle.SetActive(act);
        character.sprite = motion[num];
        if (act)
            character.color = new Color(0f, 0f, 0f, 0f);
        else
            character.color = new Color(1f, 1f, 1f, 1f);
    }

    public void DamageText(Mob mob, bool target_me, int dam, string effect, Color color)
    {
        GameObject damageText = Instantiate(Prefab);
        damageText.GetComponent<TextAni>().effect = effect.ToString();
        damageText.GetComponent<TextAni>().damage = dam.ToString();
        damageText.GetComponent<TextAni>().color = color;

        //HP�ֽ�ȭ
        playerchar.GetComponent<PlayerCharacter>().HPUpdate();
        if(mob != null)
            mob.HPUpdate();

        if (target_me)
        {
            damageText.transform.position = character.transform.position + new Vector3(0.2f, 0.1f, 0);
        }
        else
        {
            damageText.transform.position = mob.transform.position + new Vector3(-0.2f, 1.7f, 0);
        }
    }

    IEnumerator StopMotion(Mob mob, Card card, bool target_me, bool Desto, bool Dest = true)
    {
        Color color = new Color(1f, 150 / 255f, 150 / 255f, 1f);
        if (target_me)
        {
            if(playerchar.GetComponent<PlayerCharacter>().vulnerable != 0)
                DamageText(mob, target_me, mob.Mobdamage, "vulnerable", color);
            else
                DamageText(mob, target_me, mob.Mobdamage, "", Color.white);
        }
        else
        {
            if (Dest)
            {
                if (mob.vulnerable != 0)
                    DamageText(mob, target_me, BuffManager.Inst.damage_player(mob, int.Parse(card.attack.text)), "vulnerable", color);
                else
                    DamageText(mob, target_me, BuffManager.Inst.damage_player(mob, int.Parse(card.attack.text)), "", Color.white);
            }
        }

        if (Desto)
        {
            SetTimeScale(0.5f);
            #region ���� ��ġ ����
            Vector3 pos = gameObject.transform.position; //���� ��ġ ����
            Vector3 pos_player = playerchar.transform.position;
            Vector3 pos_mob = mob.transform.position;
            #endregion

            if(GameObject.Find("SubCanvas") != null)
                GameObject.Find("SubCanvas").SetActive(false);
            float orsize = gameObject.GetComponent<Camera>().orthographicSize;
            gameObject.GetComponent<Camera>().orthographicSize = 1;
            #region ī�޶� ���� and ������Ʈ�� ����

            Debug.Log("playerotate.transform.rotation.y = " + playerotate.transform.rotation.y);
            if (playerotate.transform.rotation.y == 0)
            {
                Debug.Log("���� �۵���");
                gameObject.transform.position += new Vector3(mob.transform.position.x - 1, -0.5f, 0);
                gameObject.transform.rotation = Quaternion.Euler(25, 0, -5);

                #region Ÿ���ΰ���?
                if (target_me)
                {
                    FightCut[0].SetActive(true);
                    //BloodCut[2].SetActive(true);
                    
                }
                else
                {
                    FightCut[1].SetActive(true);
                    //BloodCut[1].SetActive(true);
                    
                }
                #endregion
            }
            else if (playerotate.transform.rotation.y == 1)
            {
                Debug.Log("�Ʒ� �۵���");
                gameObject.transform.position += new Vector3(mob.transform.position.x + 1f, -0.5f, 0);
                gameObject.transform.rotation = Quaternion.Euler(25, 0, +5);

                #region Ÿ���ΰ���?
                if (target_me)
                {
                    FightCut[1].SetActive(true);
                    //BloodCut[3].SetActive(true);
                    //damageText.transform.position = character.transform.position + new Vector3(-0.2f, 0.1f, 0);
                }
                else
                {
                    FightCut[0].SetActive(true);
                    //BloodCut[0].SetActive(true);
                    //damageText.transform.position = mob.transform.position + new Vector3(0.2f, 0.4f, 0);
                }
                #endregion
            }

            Sub_StopMotion(mob, target_me);
            CameraAct = true;
            #endregion

            yield return new WaitForSeconds(0.2f);
            SetTimeScale(1f);
            CameraAct = false;

            #region Ÿ���ΰ���?
            if (FightCut[0].activeSelf)
                FightCut[0].SetActive(false);
            if (FightCut[1].activeSelf)
                FightCut[1].SetActive(false);
            /*
            for (int i = 0; i < BloodCut.Length; i++)
            {
                if (BloodCut[i].activeSelf)
                    BloodCut[i].SetActive(false);
            }
            */
            #endregion
            #region DOTween.Kill
            DOTween.Kill(gameObject.transform); //�����̴� �� ���
            DOTween.Kill(playerchar.transform); //�Ʒ� ��ġ ������ �۵� �Ŀ��� �̵��Ͽ�
            DOTween.Kill(mob.transform); //������ �ֱ⿡ �ۼ�
            #endregion
            #region ���� ��ġ �̵�
            gameObject.transform.position = pos; //���� ��ġ �̵�
            playerchar.transform.position = pos_player;
            mob.transform.position = pos_mob;
            #endregion
            gameObject.GetComponent<Camera>().orthographicSize = orsize;
            GameObject.Find("Canvas").transform.Find("SubCanvas").gameObject.SetActive(true);
            gameObject.transform.rotation = Quaternion.Euler(25, 0, 0);

        }

        if (target_me)
        {
            Debug.Log("1");
            StageBlock.Inst.Knockback_mob(mob, card, true);
        }
        else
        {
            if (card_effect.Contains("����"))
                StageBlock.Inst.heat_move(mob, true, StageBlock.Inst.Monster_back(mob));

            if (card_effect.Contains("�˹�"))
                StageBlock.Inst.Knockback_mob(mob, card, false, int.Parse(IndexOfEffect("�˹�")));

            if (card_effect.Contains("���"))
                mob.Vulner_buf(int.Parse(IndexOfEffect("���")));

            if (card_effect.Contains("��ȭ"))
                mob.Weak_buf(int.Parse(IndexOfEffect("��ȭ")));

            if (card_effect.Contains("����"))
                mob.Bleed_buf(int.Parse(IndexOfEffect("����")));
        }

        if (!target_me)
        {
            StageBlock.Inst.Move_Possible();
            MobManager.Inst.monsterStay();
        }
    }

    public string IndexOfEffect(string name)
    {
        int index = card_effect.IndexOf(name);
        string effect_num = card_effect.Substring(index + 2, 1);
        return effect_num;
    }

    void Sub_StopMotion(Mob mob, bool bo)
    {
        if (bo && playerchar.transform.rotation.y == 180)
        {
            Sub_DOMove_plus(mob);
        }
        else if (bo && playerchar.transform.rotation.y == 0)
        {
            Sub_DOMove_minus(mob);
        }
        else if (!bo && playerchar.transform.rotation.y == 0)
        {
            Sub_DOMove_plus(mob);
        }
        else if (!bo && playerchar.transform.rotation.y == 180)
        {
            Sub_DOMove_minus(mob);
        }
        Debug.Log(card_effect);
        if (card_effect == "" || card_effect == null)
            return;

        if (!card_effect.Contains("����"))
        {
            card_effect = "";
            damage = 0;
        }
    }

    void Sub_DOMove_plus(Mob mob) //���� �÷��� �� ������Ʈ ������  �����ʹ���
    {
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y, gameObject.transform.position.z), 1f);
        playerchar.transform.DOMove(new Vector3(playerchar.transform.position.x + 0.3f, playerchar.transform.position.y, playerchar.transform.position.z), 1f);
        mob.transform.DOMove(new Vector3(mob.transform.position.x + 1f, mob.transform.position.y, mob.transform.position.z), 2f);
    }

    void Sub_DOMove_minus(Mob mob) //���� �÷��� �� ������Ʈ ������  ���ʹ���
    {
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y, gameObject.transform.position.z), 1f);
        playerchar.transform.DOMove(new Vector3(playerchar.transform.position.x - 1f, playerchar.transform.position.y, playerchar.transform.position.z), 2f);
        mob.transform.DOMove(new Vector3(mob.transform.position.x - 0.3f, mob.transform.position.y, mob.transform.position.z), 1f);
    }

    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
        Time.fixedDeltaTime = 0.02f * time;
    }

    public void Stop()
    {
        /*
        character.sprite = motion[0]; //�⺻ �̹����� ����

        for (int i = 0; i < MobManager.Inst.InField.Count; i++)
            MobManager.Inst.InField[i].MotionChange(0); //�⺻ �̹����� ����
        */
        mob.Clear();
    }

    void filpX_turn(Mob mob)
    {
        if (character.transform.position.x < mob.transform.position.x)
        {
            playerchar.GetComponent<PlayerCharacter>().rotate.transform.rotation = Quaternion.Euler(0, 0, 0);
            mob.rotate.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            playerchar.GetComponent<PlayerCharacter>().rotate.transform.rotation = Quaternion.Euler(0, 180, 0);
            mob.rotate.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    #endregion
}

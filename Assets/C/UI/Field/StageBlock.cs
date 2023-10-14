using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class StageBlock : MonoBehaviour
{
    //-------------------------------------------------------------------------------------
    public static StageBlock Inst { get; private set; }
    void Awake() => Inst = this;
    //-------------------------------------------------------------------------------------
    public GameObject player;
    public List<GameObject> Mob;

    public GameObject[] situation; //현 상황
    public GameObject[] test_situation; //미리 예상하고 몬스터의 행동을 예지하기 위해

    [SerializeField] GameObject targetPrefab; //공격 가능한 범위 표시player
    [SerializeField] List<GameObject> targetList;

    [SerializeField] StoryMonsterSO smSO;

    public GameObject[] movenum;
    public List<Transform> room;
    [SerializeField] List<Transform> result; //MobManager에 전송용
    [SerializeField] List<FieldOn> field; //MobManager에 전송용
    //-------------------------------------------------------------------------------------
    void Start()
    {
        situation = new GameObject[7];
        test_situation = new GameObject[7];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int play_point = Array.IndexOf(StageBlock.Inst.situation, StageBlock.Inst.player);
            if (play_point != 0)
            {
                field[play_point - 1].usePart = true;
                field[play_point - 1].OnMouseDown();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int play_point = Array.IndexOf(StageBlock.Inst.situation, StageBlock.Inst.player);
            if (play_point != 6)
            {
                field[play_point + 1].usePart = true;
                field[play_point + 1].OnMouseDown();
            }
        }
    }

    public void Start_Player(int num, bool bo)
    {
        situation[num] = player;
        player.transform.position = new Vector3(room[num].position.x, player.transform.position.y, player.transform.position.z); ;

        if(!bo)
            player.GetComponent<PlayerCharacter>().rotate.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            player.GetComponent<PlayerCharacter>().rotate.transform.rotation = Quaternion.Euler(0, 180, 0);

        Invoke("Move_Possible", 0.1f);
    }

    public void Move_Possible()
    {
        Block_Clear();

        int play_point = Array.IndexOf(situation, player);

        if (movenum[0].activeSelf)
        {
            if (play_point != 6)
            {
                if (situation[play_point + 1] == null && movenum[0].activeSelf)
                    room[play_point + 1].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            if (play_point != 0)
            {
                if (situation[play_point - 1] == null && movenum[0].activeSelf)
                    room[play_point - 1].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }
    //-------------------------------------------------------------------------------------
    #region 카드 사거리 인식
    public bool Card_Range(Card card)
    {
        if (MobManager.Inst.select == null || card == null)
            return false;

        int play_point = Array.IndexOf(situation, player);
        int mob_point = Array.IndexOf(situation, MobManager.Inst.select.ReturnGameObject());

        int Distance = Mathf.Abs(Mathf.Abs(play_point) - Mathf.Abs(mob_point));

        if (card.range >= Distance)
        {
            return true;
        }
        if (card.range < Distance)
        {
            Debug.Log(Distance);
            PlayerCharacter.Inst.TextBox.text = "상대가 너무 멀리 있습니다.";
            return false;
        }
        return false;
    }

    public void TargetPoint(Card card)
    {
        Block_Clear();

        int play_point = Array.IndexOf(situation, player);
        for (int i = 0; i < Mob.Count; i++)
        {
            if (Mob[i] != null)
            {
                int point = Array.IndexOf(situation, Mob[i]);
                int ds = Mathf.Abs(Mathf.Abs(play_point) - Mathf.Abs(point));

                if (card.range >= ds)
                {
                    room[point].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

                    GameObject target = Instantiate(targetPrefab,
                        new Vector3(Mob[i].transform.position.x,
                        Mob[i].GetComponent<Mob>().ReturnSpriteXY("Y") / 4f - 0.5f,
                        Mob[i].transform.position.z), Quaternion.identity, Mob[i].transform);
                    targetList.Add(target);
                }
            }
        }
    }

    public void TargetPoint_Clear()
    {
        for (int i = 0; i < targetList.Count; i++)
            Destroy(targetList[i]);

        Move_Possible();
        targetList.RemoveRange(0, targetList.Count);
    }

    void Block_Clear()
    {
        for (int i = 0; i < room.Count; i++)
            room[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 70 / 255f);
    }
    #endregion
    //-------------------------------------------------------------------------------------
    #region 스테이션 예상 스크립트
    public void Test_St_Clear()
    {
        test_situation = (GameObject[])situation.Clone();
    }

    public bool Test_St(GameObject play, GameObject obj, int num)
    {
        bool bo;
        int play_po = Array.IndexOf(test_situation, play);
        int add = Array.IndexOf(test_situation, obj);

        if (play_po > add)
            bo = true;
        else
            bo = false;

        if (play.CompareTag("Player"))
            if (!bo) num *= -1;
        if (play.CompareTag("Monster"))
            if (bo) num *= -1;

        if (add + num == 7 || add + num == -1)
            return false;


        if (test_situation[add + num] == null)
        {
            return true;
        }
        return false;
    }

    public void Test_St_Real(GameObject play, GameObject obj, int num, bool change = false)
    {
        bool bo;
        int play_po = Array.IndexOf(test_situation, play);
        int add = Array.IndexOf(test_situation, obj);

        if (play_po > add)
            bo = true;
        else
            bo = false;

        if (play.CompareTag("Player"))
            if (!bo) num *= -1;
        if (play.CompareTag("Monster"))
            if (bo) num *= -1;

        if (add + num == 7 || add + num == -1)
            return;

        if (change)
            test_KnockBack_To_Mob(add, num);
        else
        {
            GameObject temp = test_situation[add];
            test_situation[add] = test_situation[add + num];
            test_situation[add + num] = temp;
        }
    }
    #endregion
    //-------------------------------------------------------------------------------------
    public void DestroyMob(GameObject obj)
    {
        int p_point = Array.IndexOf(situation, obj);
        int m_point = Mob.FindIndex(x => x == obj);

        if (p_point != -1)
        {
            situation[p_point] = null;
            this.Mob[m_point] = null;
        }

        Move_Possible();
        MobManager.Inst.monsterStay(); //행동 다시 계산하기 위해
    }
    //-------------------------------------------------------------------------------------
    #region 프리팹 추가 및 자리 매김
    public List<Transform> MobSpawnRoom(int num)
    {
        if (num == 0)
            Debug.Log("스폰되는 몬스터 리스트가 비어있습니다.");
        else if (num < 8)
            CountSize(num);
        else
            Debug.Log("스폰되는 몬스터가 너무 많습니다. = " + num + "마리");
        return result;
    }

    void CountSize(int num)
    {
        result.Clear();
        for (int i = 0; i < num; i++)
        {

            if (Mob[i] != null)
            {
                situation[i] = Mob[i];
                result.Add(room[i]);
                Player_Mob_Flip(situation[i]);
            }

        }
    }
    #endregion
    //-------------------------------------------------------------------------------------
    #region 플레이어및 몬스터의 이동 구현
    public bool CanMove = false;
    public void Move(int num, bool bo = true)
    {
        #region 플레이어의 기력 소모 이동
        if (bo)
        {
            CanMove = MoveNum();
            if (CanMove)
            {
                int p_point = Array.IndexOf(situation, player);
                if (situation[p_point + num] == null)
                {
                    GameObject temp;

                    temp = situation[p_point];
                    situation[p_point] = situation[p_point + num];
                    situation[p_point + num] = temp;

                    if (p_point < p_point + num)
                        player.GetComponent<PlayerCharacter>().rotate.transform.rotation = Quaternion.Euler(0, 0, 0);
                    else
                        player.GetComponent<PlayerCharacter>().rotate.transform.rotation = Quaternion.Euler(0, 180, 0);
                    player.transform.DOMove(new Vector3(room[p_point + num].position.x, player.transform.position.y, player.transform.position.z), 0.3f);
                }
                else
                    PlayerCharacter.Inst.TextBox.text = "이동이 불가능한 위치입니다.";

                CanMove = false;
            }
        }
        #endregion
        else
        {
            Move_mob(null, num);
        }

        Move_Possible();
        MobManager.Inst.monsterStay();
    }

    public void Move_mob(GameObject mob, int num)
    {
        GameObject temp;
        int m_point = 0;
        int p_point = 0;

        p_point = Array.IndexOf(situation, mob);

        if (mob.GetComponent<Mob>().rotate.transform.rotation.y == 1)
            m_point = p_point + num;
        else
            m_point = p_point - num;

        while (situation[m_point] != null)
        {
            if (p_point < m_point)
                m_point -= 1;
            else if (p_point > m_point)
                m_point += 1;
            else
            {
                Debug.Log("앞뒤로 막혀 이동할 수 없습니다.");
                return;
            }
        }

        if (situation[m_point] == null)
        {
            //situation 내의 요소들 순서 바꾸기
            temp = situation[p_point];
            situation[p_point] = situation[m_point];
            situation[m_point] = temp;

            mob.GetComponent<Mob>().MotionChange(1);//달리는 모습

            mob.transform.DOMove(new Vector3(room[m_point].position.x, mob.transform.position.y, mob.transform.position.z), 0.3f);
        }

        Move_Possible();
        //MobManager.Inst.monsterStay();
    }

    public bool MoveNum(bool play = true)
    {
        for (int i = movenum.Length; i > 0; i--)
        {
            if (movenum[i - 1].activeSelf)
            {
                movenum[i - 1].SetActive(false);
                return true;
            }
        }
        if(play)
            PlayerCharacter.Inst.TextBox.text = "이동 게이지가 부족합니다.";
        return false;
    }
    #endregion
    //-------------------------------------------------------------------------------------
    #region 플레이어 및 몬스터의 x축 반전
    public void Player_Mob_Flip(GameObject mob)
    {
        if (mob == null)
            return;
        
        bool bo = Player_Mob_Where(player, mob);

        if (bo)
            mob.GetComponent<Mob>().rotate.transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            mob.GetComponent<Mob>().rotate.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    #endregion
    //-------------------------------------------------------------------------------------
    #region 플레이어와 몬스터 위치 비교
    bool Player_Mob_Where(GameObject play, GameObject mob)
    {
        int play_point = Array.IndexOf(situation, play);
        int mob_point = Array.IndexOf(situation, mob);

        if (play_point > mob_point)
            return true;
        else
            return false;
    }
    #endregion
    #region 몬스터 위치
    public int Monster_back(Mob mob)
    {
        int point = Array.IndexOf(situation, mob.ReturnGameObject());

        return point;
    }
    #endregion
    #region 플레이어 위치
    public int Player_back()
    {
        int point = Array.IndexOf(situation, player);

        return point;
    }
    #endregion
    #region 특정 거리의 몬스터 호출
    public GameObject[] Monster_Index(int num)
    {
        GameObject[] temp = new GameObject[0];
        int play_point = Array.IndexOf(situation, player);
        int mob_point = 0;
        int Distance = 0;

        for (int i = 0; i < situation.Length; i++)
        {
            for(int j = 0; j < Mob.Count; j++)
            {
                if (Mob[j] != null)
                {
                    mob_point = Array.IndexOf(situation, Mob[j]);
                    Distance = Mathf.Abs(Mathf.Abs(play_point) - Mathf.Abs(mob_point));
                }
                if (Distance == num)
                {
                    temp = new GameObject[1];
                    temp[temp.Length - 1] = Mob[j];
                }
            }
        }

        return temp;
    }
    #endregion

    //-------------------------------------------------------------------------------------
    #region 넉백
    public void Knockback_mob(Mob mob, Card card, bool target, int num = 0)
    {
        int play_point = Array.IndexOf(situation, player);
        int mob_point = Array.IndexOf(situation, mob.ReturnGameObject());

        int knockback = 0;
        if (target)
        {
            if (num == 0)
                knockback = mob.knockback;
            else
                knockback = num;

            if (play_point > mob_point)
            {
                if (play_point + knockback < 7)
                    heat_move(mob, target, play_point + knockback);
                else
                {
                    StartCoroutine(cornered(mob, target, mob.Mobdamage, 6));
                    Debug.Log("궁지에몰림 실행");
                }
            }
            else
            {
                if (play_point - knockback > -1)
                    heat_move(mob, target, play_point - knockback);
                else
                {
                    StartCoroutine(cornered(mob, target, mob.Mobdamage, 0));
                    Debug.Log("궁지에몰림 실행");
                }
            }
        }
        else
        {
            knockback = num;

            if (play_point < mob_point)
            {
                if (mob_point + knockback < 7)
                    heat_move(mob, target, mob_point + knockback);
                else
                {
                    StartCoroutine(cornered(mob, target, int.Parse(card.attack.text), 6));
                    Debug.Log("궁지에몰림 실행");
                }
            }
            else
            {
                if (mob_point - knockback > -1)
                    heat_move(mob, target, mob_point - knockback);
                else
                {
                    StartCoroutine(cornered(mob, target, int.Parse(card.attack.text), 0));
                    Debug.Log("궁지에몰림 실행");
                }
            }
        }
        Move_Possible();
    }

    IEnumerator cornered(Mob mob, bool target, int dam, int num)
    {
        GameObject obj;

        if (target)
        {
            obj = player;
            dam = BuffManager.Inst.damage_mob(mob, mob.Mobdamage);
        }
        else
        {
            obj = mob.ReturnGameObject();
            dam = BuffManager.Inst.damage_player(mob, dam);
        }

        heat_move(mob, target, num);
        
        if (num == 6)
            obj.transform.DOMove(obj.transform.position + new Vector3(1f, 0, 0), 0.1f);
        else
            obj.transform.DOMove(obj.transform.position - new Vector3(1f, 0, 0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        
        
        obj.transform.DOMove(new Vector3(room[num].position.x, obj.transform.position.y, obj.transform.position.z), 0.1f);

        int crash = Mathf.CeilToInt(dam * 0.5f);

        Color color = new Color(120 / 255f, 120 / 255f, 120 / 255f, 1f);

        if (!target) //몬스터
        {
            mob.SetUpdate(null, crash, false);
            BattleCam.Inst.DamageText(mob, target, crash, "crash", color);
        }
        else
        {
            obj.GetComponent<PlayerCharacter>().SetUpdate(mob, crash, false);
            BattleCam.Inst.DamageText(mob, target, crash, "crash", color);
        }
    }

    public void heat_move(Mob mob, bool isPlayer,int num)
    {
        int p_point = Array.IndexOf(situation, player);
        int mob_point = Array.IndexOf(situation, mob.ReturnGameObject());
        GameObject temp;

        if (situation[num] == null)
        {//situation 내의 요소들 순서 바꾸기
            if (isPlayer)
            {
                if (PlayerCharacter.Inst.Item_HP > player.GetComponent<PlayerCharacter>().damage)
                {
                    temp = situation[p_point];
                    situation[p_point] = situation[num];
                    situation[num] = temp;

                    player.transform.DOMove(new Vector3(room[num].position.x, player.transform.position.y, player.transform.position.z), 0.3f);
                }
            }
            else
            {
                temp = situation[mob_point];
                situation[mob_point] = situation[num];
                situation[num] = temp;

                mob.transform.DOMove(new Vector3(room[num].position.x, mob.transform.position.y, mob.transform.position.z), 0.3f);
            }
        }
        else if (false)
        {
            if (situation[num].GetComponent<Mob>().HP <= situation[num].GetComponent<Mob>().damage)
            {
                temp = situation[p_point];
                situation[p_point] = situation[num];
                situation[num] = temp;

                player.transform.DOMove(new Vector3(room[num].position.x, player.transform.position.y, player.transform.position.z), 0.3f);
            }
        }
        else
        {
            if (isPlayer)
            {
                if (PlayerCharacter.Inst.Item_HP > player.GetComponent<PlayerCharacter>().damage)
                    KnockBack_To_Mob(p_point, num);
            }
            else
            {
                if (mob.HP > mob.damage)
                    KnockBack_To_Mob(mob_point, num);
            }
        }
        //MobManager.Inst.monsterStay();
    }
    #endregion
    //-------------------------------------------------------------------------------------
    #region 교체 넉백
    void test_KnockBack_To_Mob(int point, int num) //대상이 있는 상황에서 이동시 교체
    {
        GameObject temp;

        for (int i = point; i != num;)
        {
            if (i > num)
            {
                temp = test_situation[i];
                test_situation[i] = test_situation[i - 1];
                test_situation[i - 1] = temp;

                i--;
            }
            else
            {
                temp = test_situation[i];
                test_situation[i] = test_situation[i + 1];
                test_situation[i + 1] = temp;

                i++;
            }
        }
    }

    void KnockBack_To_Mob(int point, int num) //대상이 있는 상황에서 이동시 교체
    {
        GameObject temp;

        for (int i = point; i != num;)
        {
            if (i > num)
            {
                temp = situation[i];
                situation[i] = situation[i - 1];
                situation[i - 1] = temp;

                if (situation[i] != null)
                    situation[i].transform.DOMove(new Vector3(room[i].position.x, situation[i].transform.position.y, situation[i].transform.position.z), 0.3f);
                i--;
                if (situation[i] != null)
                    situation[i].transform.DOMove(new Vector3(room[i].position.x, situation[i].transform.position.y, situation[i].transform.position.z), 0.3f);
            }
            else
            {
                temp = situation[i];
                situation[i] = situation[i + 1];
                situation[i + 1] = temp;

                if(situation[i] != null)
                    situation[i].transform.DOMove(new Vector3(room[i].position.x, situation[i].transform.position.y, situation[i].transform.position.z), 0.3f);
                i++;
                if (situation[i] != null)
                    situation[i].transform.DOMove(new Vector3(room[i].position.x, situation[i].transform.position.y, situation[i].transform.position.z), 0.3f);
            }
        }

        //MobManager.Inst.monsterStay();
    }
    #endregion
    //-------------------------------------------------------------------------------------
}

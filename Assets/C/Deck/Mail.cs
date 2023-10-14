using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Mail : MonoBehaviour
{
    [SerializeField] DeckillustSO deckillustSO;

    [SerializeField] TMP_Text name;
    [SerializeField] SpriteRenderer illust;
    [SerializeField] GameObject Select;

    public Deck deck;
    public PRS originPRS;

    public int addrassnum;

    public void SetUp(Deck deck)
    {
        this.deck = deck;

        name.text = deck.name;
        addrassnum = deck.addrass;
        illust.sprite = deckillustSO.decks[deck.number].illust;
    }

    void Update()
    {
        if (Player.Inst.playerdata.ThisDeck == addrassnum)
            Select.SetActive(true);
        else
            Select.SetActive(false);

        if (Input.GetMouseButtonDown(1) && selectThis)
        {
            MenuMail.Inst.SetActivePanel(addrassnum, this);
        }
    }

    public void Destro()
    {
        Destroy(gameObject);
    }

    [SerializeField]  bool selectThis = false;
    void OnMouseOver()
    {
        selectThis = true;
    }

    void OnMouseExit()
    {
        selectThis = false;
    }

    void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            SaveThisName();
    }

    public void SaveThisName()
    {
        if (Player.Inst.playerdata.ThisDeck == addrassnum)
        {
            SceneManager.LoadScene("Deck");
        }
        else
        {
            Player.Inst.playerdata.ThisDeck = addrassnum;
            Player.Inst.Save();
        }
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

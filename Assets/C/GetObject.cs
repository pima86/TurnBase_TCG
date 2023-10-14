using UnityEngine;
using UnityEngine.EventSystems; //지시문 추가

public class GetObject : MonoBehaviour
{
    public int Point;
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point = Point;
        }
    }
}
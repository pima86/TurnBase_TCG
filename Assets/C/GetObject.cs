using UnityEngine;
using UnityEngine.EventSystems; //���ù� �߰�

public class GetObject : MonoBehaviour
{
    public int Point;
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject.Find("�ϴ� �������").GetComponent<TextBox>().Bracket_point = Point;
        }
    }
}
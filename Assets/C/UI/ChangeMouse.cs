using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMouse : MonoBehaviour
{
    [SerializeField] Texture2D cursorImg;
    [SerializeField] Texture2D cursorImg_click;

    void Start()
    {
        Cursor.SetCursor(Resize(cursorImg, 40, 40), Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseDown()
    {
        Cursor.SetCursor(Resize(cursorImg_click, 40, 40), Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseUp()
    {
        Cursor.SetCursor(Resize(cursorImg, 40, 40), Vector2.zero, CursorMode.ForceSoftware);
    }

    Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }
}

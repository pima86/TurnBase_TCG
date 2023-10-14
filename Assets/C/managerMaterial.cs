using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerMaterial : MonoBehaviour
{
    // Component
    private Renderer CCube;
    private Renderer EbCube;
    private Renderer ECube;
    private Renderer MCube;

    // Game Object
    public GameObject CharCube;
    public GameObject EyebrowsCube;
    public GameObject EyesCube;
    public GameObject MouthCube;

    // ½ºÅÄµå
    public Texture2D Nb1_1; //1¹ø
    public Texture2D Nb1_2; //¸Ó¸®¸¦ ½Î¸Å´Â
    public Texture2D Nb1_3; //´Ù½Ã

    //´«½ç
    public Texture2D Nb1_Eb_1; //Æ÷¹°¼±
    public Texture2D Nb1_Eb_2; //¿þÀÌºê

    //´«
    public Texture2D Nb1_E_1; //°¨Àº
    public Texture2D Nb1_E_2; //¾Æ·¡ Á¹¸°
    public Texture2D Nb1_E_3; //Á¤¸é Á¹¸°

    //ÀÔ
    public Texture2D Nb1_M_1; //¤¿
    public Texture2D Nb1_M_2; //¤Ó
    public Texture2D Nb1_M_3; //¤Ñ


    private Color32 colorCube;

    void Awake()
    {
        // Get Component
        CCube = CharCube.GetComponent<Renderer>();
        EbCube = EyebrowsCube.GetComponent<Renderer>();
        ECube = EyesCube.GetComponent<Renderer>();
        MCube = MouthCube.GetComponent<Renderer>();
    }

    private float E = 0;
    private float M = 0;
    private bool Nb1_E = false;
    private bool Nb1_M = false;

    void Update()
    {
        if (Nb1_E)
        {
            E += Time.deltaTime;
            if (E >= 3)
            {
                E = 0;
                StartCoroutine(CloseEyes());
            }
        }
        if (Nb1_M)
        {
            M += Time.deltaTime;
            if (M > 0.35f)
            {
                M = 0;
                StartCoroutine(TalkMouth());
            }
        }
    }

    IEnumerator CloseEyes()
    {
        ECube.material.SetTexture("_MainTex", Nb1_E_1);
        yield return new WaitForSeconds(0.1f);
        if (Eyes_N == 1) { N1_E_1(); }
        else if (Eyes_N == 2) { N1_E_2(); }
        else if (Eyes_N == 3) { N1_E_3(); }
    }
    IEnumerator TalkMouth()
    {
        N1_M_2();
        yield return new WaitForSeconds(0.1f);
        N1_M_1();
        yield return new WaitForSeconds(0.1f);
        N1_M_3();
    }

    public void Light()
    {
        // Change Color
        colorCube = new Color32(255, 255, 255, 255);
        CCube.material.SetColor("_Color", colorCube);
    }

    public void Dark()
    {
        // Change Color
        colorCube = new Color32(120, 120, 120, 255);
        CCube.material.SetColor("_Color", colorCube);
    }

    public void Deep_Dark()
    {
        // Change Color
        colorCube = new Color32(45, 45, 45, 255);
        CCube.material.SetColor("_Color", colorCube);
    }

    // Change Texture
    //½ºÅÄµå
    public void N1_1() { CCube.material.SetTexture("_MainTex", Nb1_1); }
    public void N1_2() { CCube.material.SetTexture("_MainTex", Nb1_2); }
    public void N1_3() { CCube.material.SetTexture("_MainTex", Nb1_3); }

    //´«½ç
    private bool EyebrowsSet = true;
    public void N1_Eb_0() { if (EyebrowsSet) { EyebrowsCube.SetActive(false); EyebrowsSet = false; } else { EyebrowsCube.SetActive(true); EyebrowsSet = true; } }
    public void N1_Eb_1() { EbCube.material.SetTexture("_MainTex", Nb1_Eb_1); }
    public void N1_Eb_2() { EbCube.material.SetTexture("_MainTex", Nb1_Eb_2); }

    //´«
    private int Eyes_N = 0;
    private bool EyesSet = true;
    public void N1_E_0() { if (EyesSet) { EyesCube.SetActive(false); EyesSet = false; } else { EyesCube.SetActive(true); EyesSet = true; } }
    public void N1_E_1() { ECube.material.SetTexture("_MainTex", Nb1_E_1); Eyes_N = 1; }
    public void N1_E_2() { ECube.material.SetTexture("_MainTex", Nb1_E_2); Eyes_N = 2; }
    public void N1_E_3() { ECube.material.SetTexture("_MainTex", Nb1_E_3); Eyes_N = 3; }

    //ÀÔ
    private bool MouthSet = true;
    public void N1_M_0() { if (MouthSet) { MouthCube.SetActive(false); MouthSet = false; } else { MouthCube.SetActive(true); MouthSet = true; } }
    public void N1_M_1() { MCube.material.SetTexture("_MainTex", Nb1_M_1); }
    public void N1_M_2() { MCube.material.SetTexture("_MainTex", Nb1_M_2); }
    public void N1_M_3() { MCube.material.SetTexture("_MainTex", Nb1_M_3); }

    //´«±ôºýÀÓ
    public void N1_E() { if (Nb1_E) { Nb1_E = false; } else { Nb1_E = true; } }

    //ÀÔ»µ²û
    public void N1_M(bool ee) { if (!ee) { Nb1_M = false; } else { Nb1_M = true; } }
}

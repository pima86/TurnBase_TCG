using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextAni : MonoBehaviour
{
    public static TextAni Inst { get; private set; }
    void Awake() => Inst = this;

    public float moveSpeed;
    public float alphaSpeed;
    public float destroyTime;
    [SerializeField] TMP_Text effect_text;
    [SerializeField] TMP_Text text;
    Color alpha;
    public string effect;
    public string damage;
    public Color color;

    void Start()
    {
        /*
        Material materialInstance = Instantiate(effect_text.material);
        Material materialInstance_1 = Instantiate(text.material);
        effect_text.material = materialInstance;
        text.material = materialInstance_1;
        */

        gameObject.GetComponent<Renderer>().sortingOrder = 1000;

        ThisText_option();

        alpha = text.color;

        Invoke("DestroyObejct", destroyTime);
    }

    public void ThisText_option()
    {
        effect_text.text = "";
        text.text = damage;
        //effect_text.color = color;
        text.color = color;
        //text.color = color;
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        if (gameObject.transform.position.y >= 2)
        {
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
            effect_text.color = alpha;
            text.color = alpha;
        }
    }

    private void DestroyObejct() =>   Destroy(gameObject);
}

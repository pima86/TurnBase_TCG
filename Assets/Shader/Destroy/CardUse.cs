using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUse : MonoBehaviour
{
    [SerializeField] Renderer renderer_1;
    [SerializeField] Renderer renderer_2;

    [SerializeField] Material material_on;
    [SerializeField] Material material_off;

    float dissolveAmount;
    float dissolveSpeed;
    bool isDissolving;

    void Start()
    {
        renderer_1.material = Instantiate(renderer_1.material);
        renderer_2.material = Instantiate(renderer_2.material);
    }

    void Update()
    {
        if (isDissolving && dissolveAmount > 0)
        {
            if (dissolveAmount > 5)
                dissolveAmount = dissolveAmount - (dissolveSpeed * 4 * Time.deltaTime);
            else
                dissolveAmount = dissolveAmount - (dissolveSpeed * 1f * Time.deltaTime);

            renderer_1.material.SetFloat("Amount", dissolveAmount);
            renderer_2.material.SetFloat("Amount", dissolveAmount);
        }
        else
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            //material_on.SetFloat("Amount", dissolveAmount); 
        }
    }

    public void StartDissolve(float dissolveSpeed)
    {
        renderer_1.material = material_on;
        renderer_2.material = material_on;
        isDissolving = true;
        dissolveAmount = 20;
        this.dissolveSpeed = dissolveSpeed;

        ClipManager.Inst.CardBurn();
    }

    public void StopDissolve(float dissolveSpeed)
    {
        renderer_1.material = material_off;
        renderer_2.material = material_off;
        isDissolving = false;
        this.dissolveSpeed = dissolveSpeed;
    }
}

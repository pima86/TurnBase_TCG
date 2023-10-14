using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour
{
    [SerializeField] Renderer renderer;

    [SerializeField] Material material_on;
    [SerializeField] Material material_off;

    float dissolveAmount;
    float dissolveSpeed;
    public bool isDissolving;

    Mob mob;

    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + dissolveSpeed * Time.deltaTime);
            material_on.SetFloat("Amount", dissolveAmount);
        }
        else 
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            //material_on.SetFloat("Amount", dissolveAmount); 
        }
    }

    public void StartDissolve(float dissolveSpeed)
    {
        renderer.material = material_on;
        isDissolving = true;
        this.dissolveSpeed = dissolveSpeed;
    }

    public void StopDissolve(float dissolveSpeed)
    {
        renderer.material = material_off;
        isDissolving = false;
        this.dissolveSpeed = dissolveSpeed;
    }
}

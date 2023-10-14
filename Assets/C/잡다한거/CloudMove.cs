using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMove : MonoBehaviour
{
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        MoveCloud();
    }

    void MoveCloud()
    {
        gameObject.transform.DOMove(new Vector3(num, gameObject.transform.position.y, gameObject.transform.position.z), 200f);
    }

    void Update()
    {
        if (gameObject.transform.position.x <= num + 10)
        {
            DOTween.Kill(gameObject.transform);
            gameObject.transform.position = new Vector3(26, gameObject.transform.position.y, gameObject.transform.position.z);
            MoveCloud();
        }
    }
}

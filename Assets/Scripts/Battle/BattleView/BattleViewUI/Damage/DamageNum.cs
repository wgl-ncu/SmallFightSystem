using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNum : MonoBehaviour
{
    public Animation anim;
    public Text text;
    private Transform followTar;

    public void OnAnimEnd()
    {
        Destroy(gameObject);
    }

    public void SetText(string text, Color color, Transform tar)
    {
        this.text.text = text;
        this.text.color = color;
        followTar = tar;
        SyncPos();

    }

    private void Update()
    {
        SyncPos();
    }

    private void SyncPos()
    {
        if (followTar != null)
        {
            transform.position = followTar.position;
        }
    }
}

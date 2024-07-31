using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Licia : MonoBehaviour
{
    public StoryScriptable story;
    public SpriteRenderer spriteRenderer;
    public float duration = 0.8f;
    // Update is called once per frame
    private void Start()
    {
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }
    void Update()
    {
        if (story.Stage2Check2)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void TurnOnAlpha()
    {
        spriteRenderer.DOFade(1, duration);
    }
}

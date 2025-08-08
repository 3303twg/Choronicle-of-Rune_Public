using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSort : MonoBehaviour
{
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // y 좌표가 낮을수록 앞에 오도록 음수로 변환
        //플레이어, npc, 몬스터
        sr.sortingOrder = -(int)(transform.position.y * 100);
    }
}

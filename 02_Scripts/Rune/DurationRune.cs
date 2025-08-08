using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "DurationRune", menuName = "Rune/Duration")]
public class DurationRune : Rune
{
    public float increaseDuration = 2f; // 지속시간 증가 값
    public override void Apply(Skill skill)
    {
        skill.MiddleSet(() =>
        {
            if (skill.skillAction.duration < skill.duration * increaseDuration )
            {
                skill.skillAction.duration *= increaseDuration;
            }

            if (skill.skillActionDevide == null) return;
            
            if (skill.skillActionDevide.duration < skill.duration * increaseDuration )
            {
                skill.skillActionDevide.duration *= increaseDuration;
            }
        });
    }
}


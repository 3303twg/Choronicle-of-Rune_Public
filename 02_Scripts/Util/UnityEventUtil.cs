using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventUtil
{
    public static Action GetEvnetAction(UnityEvent unityEvent, Component runtimeContext)
    {
        if (unityEvent == null || unityEvent.GetPersistentEventCount() == 0)
        {
            return null;
        }

        var prefabTarget = unityEvent.GetPersistentTarget(0);
        var methodName = unityEvent.GetPersistentMethodName(0);
        var targetType = prefabTarget.GetType();
        if (prefabTarget == null || string.IsNullOrEmpty(methodName))
        {
            return null;
        }

        // 런타임에 enemyController가 가진 prefabTarget 타입 컴포넌트 찾기
        var runtimeTarget = runtimeContext.GetComponent(targetType);
        if (runtimeTarget == null)
        {
            return null;
        }

        MethodInfo methodInfo = runtimeTarget.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (methodInfo == null)
        {
            return null;
        }

        // 매개변수가 없는 메서드인 경우에만 Action으로 변환 가능
        if (methodInfo.GetParameters().Length != 0)
        {
            Debug.LogWarning($"Method {methodName} has parameters and cannot be converted to Action without adapting.");
            return null;
        }

        // 델리게이트 생성
        return (Action)Delegate.CreateDelegate(typeof(Action), runtimeTarget, methodInfo);
    }
}

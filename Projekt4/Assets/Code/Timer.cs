using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public struct Timer
{
    private Action action;
    private float time;
    private readonly float end;
    public bool HasRan => time >= end;
    public void UpdateTimer(float timeSinceLastUpdate)
    {
        time += timeSinceLastUpdate;
        if(HasRan)
        {
            action();
        }
    }
    public void Reset()
    {
        time = 0f;
    }

    public Timer(Action action, float end)
    {
        this.action = action;
        this.end = end;
        this.time = 0f;
    }
}
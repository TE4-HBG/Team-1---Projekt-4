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
    private float end;
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
    public void Reset(Action action)
    {
        time = 0f;
        this.action = action;
    }
    public void Reset(float end)
    {
        time = 0f;
        this.end = end;
    }
    public void Reset(Action action, float end)
    {
        time = 0f;
        this.action = action;
        this.end = end;
    }
    public void Reset(float end, Action action)
    {
        time = 0f;
        this.action = action;
        this.end = end;
    }
    public Timer(Action action, float end)
    {
        this.action = action;
        this.end = end;
        this.time = 0f;
    }
}
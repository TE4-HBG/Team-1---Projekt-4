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
    public float time
    {
        get;
        private set;
    }
    private float end;
    public bool HasRan => time >= end;
    public void Update(float timeSinceLastUpdate)
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
    public void Set(Action action)
    {
        time = 0f;
        this.action = action;
    }
    public void Set(float end)
    {
        time = 0f;
        this.end = end;
    }
    public void Set(Action action, float end)
    {
        time = 0f;
        this.action = action;
        this.end = end;
    }
    public void Set(float end, Action action)
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
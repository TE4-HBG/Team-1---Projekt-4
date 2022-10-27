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
    public float end
    {
        get;
        private set;
    }
    public bool HasRan => time >= end;
    public bool paused;
    public void Update(float timeSinceLastUpdate)
    {
        if (!paused)
        {
            time += timeSinceLastUpdate;
            if (HasRan)
            {
                paused = true;
                // this was the action can ovveride the action
                action();
            }
        }
        
    }
    public void Reset()
    {
        time = 0f;
        paused = false;
    }
    public void Set(Action action)
    {
        time = 0f;
        this.action = action;
        paused = false;
    }
    public void Set(float end)
    {
        time = 0f;
        this.end = end;
        paused = false;
    }
    public void Set(Action action, float end)
    {
        time = 0f;
        this.action = action;
        this.end = end;
        paused = false;
    }
    public void Set(float end, Action action)
    {
        time = 0f;
        this.action = action;
        this.end = end;
        paused = false;
    }
    public Timer(bool paused)
    {
        this.action = null;
        this.end = 0f;
        this.time = 0f;
        this.paused = paused;
    }
    public Timer(Action action, float end, bool paused = false)
    {
        this.action = action;
        this.end = end;
        this.time = 0f;
        this.paused = paused;
    }
}
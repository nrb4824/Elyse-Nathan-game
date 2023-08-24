using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class SentryBaseState
{   
    protected GameObject gameObject;
    protected Transform transform;
    public abstract Type Tick();
    public SentryBaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }
    
}

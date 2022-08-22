using System.Collections.Generic;
using UnityEngine;
using System;

public class RealPuckManager : Singleton<RealPuckManager>
{
    private RealPuck _realPuck;

    public RealPuck GetRealPuck()
    {
        if (_realPuck == null){
            _realPuck = FindObjectOfType<RealPuck>();
        }
        return _realPuck;
    }
    
}

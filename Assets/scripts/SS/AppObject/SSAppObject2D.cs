using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.AppObject
{
    public abstract class SSAppObject2D : SSAppObject
    {
        //constructor 
        public SSAppObject2D(string name) : base(name)
        {
            this.mGameObject.layer = 5; 
        }
    }
}

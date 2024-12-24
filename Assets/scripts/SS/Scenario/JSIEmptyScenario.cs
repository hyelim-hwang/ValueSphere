using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;

namespace SS.Scenario
{
    public partial class SSEmptyScenario : XScenario
    {
        //singleton pattern 
        private static SSEmptyScenario mSingleton = null;
        public static SSEmptyScenario getSingleton()
        {
            Debug.Assert(SSEmptyScenario.mSingleton != null);
            return SSEmptyScenario.mSingleton;
        }
        public static SSEmptyScenario createSingleton(XApp app)
        {
            Debug.Assert(SSEmptyScenario.mSingleton == null);
            SSEmptyScenario.mSingleton = new SSEmptyScenario(app);
            return SSEmptyScenario.mSingleton;
        }

        private SSEmptyScenario(XApp app) : base(app)
        {
        }
        protected override void addScenes()
        {
            this.addScene(SSDefaultScenario.EmptyScene.createSingleton(this));
        }
    }
}


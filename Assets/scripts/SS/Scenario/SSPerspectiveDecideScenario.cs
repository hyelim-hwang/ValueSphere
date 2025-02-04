using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;

namespace SS.Scenario
{
    public partial class SSSperspectiveDecideScenario : XScenario
    {
        //singleton pattern
        private static SSSperspectiveDecideScenario mSingleton = null;
        public static SSSperspectiveDecideScenario getSingleton()
        {
            Debug.Assert(SSSperspectiveDecideScenario.mSingleton != null);
            return SSSperspectiveDecideScenario.mSingleton;
        }
        public static SSSperspectiveDecideScenario createSingleton(XApp app)
        {
            Debug.Assert(SSSperspectiveDecideScenario.mSingleton == null);
            SSSperspectiveDecideScenario.mSingleton =
                new SSSperspectiveDecideScenario(app);
            return SSSperspectiveDecideScenario.mSingleton;
        }

        private SSSperspectiveDecideScenario(XApp app) : base(app) {
            this.mManipulatingTouchmarks = new List<SSTouchMark>();
        }

        private List<SSTouchMark> mManipulatingTouchmarks = null;
        public List<SSTouchMark> getManipulatingTouchMarks() {
            return this.mManipulatingTouchmarks;
        }

        protected override void addScenes()
        {
            this.addScene(SSSperspectiveDecideScenario.
            CubeHandleReadyScene.createSingleton(this));
            this.addScene(SSSperspectiveDecideScenario.
            TumbleCameraScene.createSingleton(this));
            this.addScene(SSSperspectiveDecideScenario.
            DollyCameraScene.createSingleton(this));
            this.addScene(SSSperspectiveDecideScenario.
            ScaleCubeReadyScene.createSingleton(this));
            this.addScene(SSSperspectiveDecideScenario.
            ScaleCubeScene.createSingleton(this));
        }
    }
}


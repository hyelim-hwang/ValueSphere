using System.Collections.Generic;
using UnityEngine;
using X;

namespace SS.Scenario {
    public partial class SSNavigateScenario : XScenario {
        //singleton pattern
        private static SSNavigateScenario mSingleton = null;
        public static SSNavigateScenario getSingleton() {
            Debug.Assert(SSNavigateScenario.mSingleton != null);
            return SSNavigateScenario.mSingleton;
        }
        public static SSNavigateScenario createSingleton(XApp app) {
            Debug.Assert(SSNavigateScenario.mSingleton == null);
            SSNavigateScenario.mSingleton = new SSNavigateScenario(app);
            return SSNavigateScenario.mSingleton;
        }

        private SSNavigateScenario(XApp app) : base(app) {
            this.mManipulatingTouchmarks = new List<SSTouchMark>();
        }

        //fields
        private List<SSTouchMark> mManipulatingTouchmarks = null;
        public List<SSTouchMark> getManipulatingTouchMarks() {
            return this.mManipulatingTouchmarks;
        }

        protected override void addScenes() {
            this.addScene(
                SSNavigateScenario.RotateReadyScene.createSingleton(this));
            this.addScene(
                SSNavigateScenario.ZoomAndRotateScene.createSingleton(this));
        }
    }
}


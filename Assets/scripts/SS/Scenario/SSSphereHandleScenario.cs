using System.Collections.Generic;
using UnityEngine;
using X;

namespace SS.Scenario {
    public partial class SSSphereHandleScenario : XScenario {
        //singleton pattern
        private static SSSphereHandleScenario mSingleton = null;
        public static SSSphereHandleScenario getSingleton() {
            Debug.Assert(SSSphereHandleScenario.mSingleton != null);
            return SSSphereHandleScenario.mSingleton;
        }
        public static SSSphereHandleScenario createSingleton(XApp app) {
            Debug.Assert(SSSphereHandleScenario.mSingleton == null);
            SSSphereHandleScenario.mSingleton = new SSSphereHandleScenario(app);
            return SSSphereHandleScenario.mSingleton;
        }

        private SSSphereHandleScenario(XApp app) : base(app) {
            this.mManipulatingTouchmarks = new List<SSTouchMark>();
        }

        //fields
        private List<SSTouchMark> mManipulatingTouchmarks = null;
        public List<SSTouchMark> getManipulatingTouchMarks() {
            return this.mManipulatingTouchmarks;
        }

        protected override void addScenes() {
            // this.addScene(SSSphereHandleScenario.
            //     SphereGenerateScene.createSingleton(this));
            // this.addScene(SSSphereHandleScenario.
            //     SphereGenerateReadyScene.createSingleton(this));
            this.addScene(SSSphereHandleScenario.
                SphereMoveNScaleScene.createSingleton(this));
            this.addScene(SSSphereHandleScenario.
                MoveSphereScene.createSingleton(this));
            this.addScene(SSSphereHandleScenario.
                HandleHighlightScene.createSingleton(this));
            this.addScene(SSSphereHandleScenario.
                MoveEquatorScene.createSingleton(this));
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using X;

namespace SS.Scenario {
    public partial class SSStickHandleScenario : XScenario {
        //singleton pattern
        private static SSStickHandleScenario mSingleton = null;
        public static SSStickHandleScenario getSingleton() {
            Debug.Assert(SSStickHandleScenario.mSingleton != null);
            return SSStickHandleScenario.mSingleton;
        }
        public static SSStickHandleScenario createSingleton(XApp app) {
            Debug.Assert(SSStickHandleScenario.mSingleton == null);
            SSStickHandleScenario.mSingleton = new SSStickHandleScenario(app);
            return SSStickHandleScenario.mSingleton;
        }

        private SSStickHandleScenario(XApp app) : base(app) {
            this.mManipulatingTouchmarks = new List<SSTouchMark>();
        }

        //fields
        private List<SSTouchMark> mManipulatingTouchmarks = null;
        public List<SSTouchMark> getManipulatingTouchMarks() {
            return this.mManipulatingTouchmarks;
        }

        protected override void addScenes() {
            this.addScene(SSStickHandleScenario.
                TranslateStickScene.createSingleton(this));
            this.addScene(SSStickHandleScenario.
                SlideEdgeTopScene.createSingleton(this));
            this.addScene(SSStickHandleScenario.
                SlideEdgeBottomScene.createSingleton(this));
            this.addScene(SSStickHandleScenario.
                ConstructStickScene.createSingleton(this));
        }
    }
}
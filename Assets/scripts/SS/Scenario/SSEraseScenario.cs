using UnityEngine;
using X;

namespace SS.Scenario {
    public partial class SSEraseScenario : XScenario {
        //singleton pattern
        private static SSEraseScenario mSingleton = null;
        public static SSEraseScenario getSingleton() {
            Debug.Assert(SSEraseScenario.mSingleton != null);
            return SSEraseScenario.mSingleton;
        }
        public static SSEraseScenario createSingleton(XApp app) {
            Debug.Assert(SSEraseScenario.mSingleton == null);
            SSEraseScenario.mSingleton = new SSEraseScenario(app);
            return SSEraseScenario.mSingleton;
        }

        private SSEraseScenario(XApp app) : base(app) {}
        protected override void addScenes() {
            this.addScene(SSEraseScenario.EraseScene.createSingleton(this));
        }
    }
}


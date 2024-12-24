using UnityEngine;
using X;

namespace SS.Scenario {
    public partial class SSDefaultScenario : XScenario {
        //singleton pattern
        private static SSDefaultScenario mSingleton = null;
        public static SSDefaultScenario getSingleton() {
            Debug.Assert(SSDefaultScenario.mSingleton != null);
            return SSDefaultScenario.mSingleton;
        }
        public static SSDefaultScenario createSingleton(XApp app) {
            Debug.Assert(SSDefaultScenario.mSingleton == null);
            SSDefaultScenario.mSingleton = new SSDefaultScenario(app);
            return SSDefaultScenario.mSingleton;
        }

        private SSDefaultScenario(XApp app) : base(app) {}
        protected override void addScenes() {
            this.addScene(SSDefaultScenario.ReadyScene.createSingleton(this));
        }
    }
}


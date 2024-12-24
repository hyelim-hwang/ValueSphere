using UnityEngine;
using X;

namespace SS.Scenario {
    public partial class SSDrawScenario : XScenario {
        //singleton pattern
        private static SSDrawScenario mSingleton = null;
        public static SSDrawScenario getSingleton() {
            Debug.Assert(SSDrawScenario.mSingleton != null);
            return SSDrawScenario.mSingleton;
        }
        public static SSDrawScenario createSingleton(XApp app) {
            Debug.Assert(SSDrawScenario.mSingleton == null);
            SSDrawScenario.mSingleton = new SSDrawScenario(app);
            return SSDrawScenario.mSingleton;
        }

        private SSDrawScenario(XApp app) : base(app) {
        }
        protected override void addScenes() {
            this.addScene(SSDrawScenario.DrawScene.createSingleton(this));
        }
    }
}


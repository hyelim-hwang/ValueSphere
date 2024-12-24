using SS.Scenario;
using X;

namespace SS {
    public class SSScenarioMgr : XScenarioMgr {
        //constructor
        public SSScenarioMgr(SSApp app) : base(app) {}

        //methods
        protected override void addScenarios() {
            SSApp ss = (SSApp)this.mApp;
            this.addScenario(SSDefaultScenario.createSingleton(ss));
            this.addScenario(SSNavigateScenario.createSingleton(ss));
            this.addScenario(SSDrawScenario.createSingleton(ss));
            this.addScenario(SSSphereHandleScenario.createSingleton(ss));
            this.addScenario(SSEraseScenario.createSingleton(ss));
        }

        protected override void setInitCurScene() {
            this.setCurScene(SSDefaultScenario.ReadyScene.getSingleton());
        }
    }
}

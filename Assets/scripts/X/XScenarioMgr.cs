using System.Collections.Generic;

namespace X {
    public abstract class XScenarioMgr {
        //field
        protected XApp mApp = null;
        protected List<XScenario> mScenarios = null;
        protected XScene mCurScene = null;
        public XScene getCurScene() {
            return this.mCurScene;
        }
        public void setCurScene(XScene scene) {
            if (this.mCurScene != null) {
                this.mCurScene.wrapUp();
            }
            this.mCurScene = scene;
            this.mCurScene.getReady();
        }

        //protected constructor
        protected XScenarioMgr(XApp app) {
            this.mApp = app;
            this.mScenarios = new List<XScenario>();
            this.addScenarios();
            this.setInitCurScene();
        }

        //abstract method
        protected abstract void addScenarios();
        protected abstract void setInitCurScene();

        //concrete method
        protected void addScenario(XScenario scenario) {
            this.mScenarios.Add(scenario);
        }
    }
}
using System.Collections.Generic;

namespace X {
    public abstract class XScenario {
        //field
        protected XApp mApp = null;
        public XApp getApp() {
            return this.mApp;
        }
        protected List<XScene> mScenes = null;


        //constructor
        protected XScenario(XApp app) {
            this.mApp = app;
            this.mScenes = new List<XScene>();
            this.addScenes();
        }

        //abstract method
        protected abstract void addScenes();

        //concrete method
        protected void addScene(XScene scene) {
            this.mScenes.Add(scene);
        }
    }
}
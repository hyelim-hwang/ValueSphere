namespace X {
    public class XCmdToChangeScene : XLoggableCmd {

        //field
        private XScene mFromScene = null;
        private XScene mToScene = null;
        private XScene mReturnScene = null;

        //private constructor
        private XCmdToChangeScene(
            XApp app, XScene toScene, XScene returnScene) : base(app) {
            this.mFromScene = this.mApp.getScenarioMgr().getCurScene();
            this.mToScene = toScene;
            this.mReturnScene = returnScene;
        }

        //method
        public static bool execute(
            XApp app, XScene toScene, XScene returnScene) {
            XCmdToChangeScene cmd =
                new XCmdToChangeScene(app, toScene, returnScene);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            this.mToScene.setReturnScene(this.mReturnScene);
            this.mApp.getScenarioMgr().setCurScene(this.mToScene);
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("changeScene", this.GetType().Name);
            data.addMember("fromScene", this.GetType().Name);
            XScene curScene = this.mApp.getScenarioMgr().getCurScene();
            data.addMember("toScene", this.GetType().Name);
            data.addMember("returnScene", this.GetType().Name);
            return data;
        }
    }
}

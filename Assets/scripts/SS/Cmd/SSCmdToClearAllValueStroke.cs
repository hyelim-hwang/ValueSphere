using X;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToclearAllValueStroke : XLoggableCmd {
        //fields

        //private constructor
        private SSCmdToclearAllValueStroke(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToclearAllValueStroke cmd = new SSCmdToclearAllValueStroke(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp SS = (SSApp)this.mApp;
            // clear existing valueStrokes
            foreach (SSValueStroke vs in SS.getValueStrokeMgr().
                getValueStrokes()) {
                vs.destroyGameObject();
            } SS.getValueStrokeMgr().getValueStrokes().Clear();
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("clearAllValueStroke", this.GetType().Name);
            return data;
        }
    }
}
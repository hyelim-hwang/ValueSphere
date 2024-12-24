using System;
using X;

namespace SS.Cmd {
    public class SSCmdToRedo : XLoggableCmd {
        // private constructor
        private SSCmdToRedo(XApp app) : base(app) {}

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToRedo cmd = new SSCmdToRedo(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            return ss.getSnapshotMgr().redo();
        }

        protected override XJson createLogData() {
            SSApp ss = (SSApp)this.mApp;
            XJson data = new XJson();
            data.addMember("remainingUndoCount", SSCmdToUndo.
                calcRemainingUndoCount(ss));
            data.addMember("remainingRedoCount", SSCmdToUndo.
                calcRemainingRedoCount(ss));
            return data;
        }
    }
}

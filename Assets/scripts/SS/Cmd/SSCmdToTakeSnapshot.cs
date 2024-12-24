using X;

namespace SS.Cmd {
    public class SSCmdToTakeSnapshot : XLoggableCmd {
        // fields
        // ...

        // private constructor
        private SSCmdToTakeSnapshot(XApp app) : base(app) {
            // SSApp ss = (SSApp)this.mApp;
        }

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToTakeSnapshot cmd = new SSCmdToTakeSnapshot(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            return ss.getSnapshotMgr().takeSnapshot();
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

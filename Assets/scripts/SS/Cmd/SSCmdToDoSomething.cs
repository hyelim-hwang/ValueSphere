using X;

namespace SS.Cmd {
    public class SSCmdToDoSomething : XLoggableCmd {
        //fields

        //private constructor
        private SSCmdToDoSomething(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToDoSomething cmd = new SSCmdToDoSomething(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            throw new System.NotImplementedException();
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("doSomething", this.GetType().Name);
            return data;
        }
    }
}
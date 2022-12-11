using ControllerBaseLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerBaseLib
{
    public class OperationFinishedEventArgs
    {
        #region Prop

        public OperationState State { get; set; }

        public dynamic Type { get; set; }

        public Exception Exception { get; set; }

        List<String> AdditionalInfo { get; set; }

        public dynamic OperationResult { get; set; }

        #endregion

        #region Ctor

        public OperationFinishedEventArgs()
        {
            AdditionalInfo = new List<String>();
        }

        #endregion
    }
}

using ControllerBaseLib.Enums;
using System;

namespace ControllerBaseLib
{
    public class ControllerBase<TOperationType>
        where TOperationType : struct, Enum
    {
        #region Fields

        OperationState state;

        #endregion

        #region Ctor

        public ControllerBase()
        {
            state = new OperationState();
        }

        #endregion

        #region Methods

        public void ExecuteFunc(TOperationType operationType, Func<dynamic> func)
        {
            var e = ExeEnvelope(operationType, func);

            OperationFinished(e);
        }

        public OperationFinishedEventArgs<TOperationType> ExecuteFunc(Func<dynamic> func, TOperationType operationType)
        {
            return ExeEnvelope(operationType, func);
        }

        OperationFinishedEventArgs<TOperationType> ExeEnvelope(TOperationType operationType, Func<dynamic> func)
        {
            OperationFinishedEventArgs<TOperationType> OFEA = null;

           Exception ex = null;

            dynamic result = null;

            try
            {
                result = func.Invoke();

                state = OperationState.OpSucceded;
            }
            catch (Exception e)
            {
                ex = e;

                state = OperationState.OpFailed;
            }
            finally
            { 
                    OFEA = new OperationFinishedEventArgs<TOperationType>()
                    {
                        Exception = ex,
                        OperationResult = result,
                        State = state,
                        Type = operationType
                    };
            }

            return OFEA;
        }

        #endregion

        #region Events

        public event Action<OperationFinishedEventArgs<TOperationType>> OperationFinished;

        #endregion
    }
}
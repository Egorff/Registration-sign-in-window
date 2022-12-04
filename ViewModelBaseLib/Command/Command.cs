using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.Command.CommandBase;

namespace ViewModelBaseLib.Command
{
    public class Command : CommandBaseClass
    {
        #region Fields

        Action<object> m_Execute;

        Func<object, bool> m_CanExecute;

        #endregion


        #region Prop

        #endregion


        #region Ctor

        public Command(Action<object> execute, Func<object, bool> canExecute) 
        {
            m_CanExecute = canExecute;

            m_Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        #endregion


        #region Methods

        public override bool CanExecute(object? parameter)
        {
            return m_CanExecute?.Invoke(parameter) ?? true;


        }

        public override void Execute(object? parameter)
        {
            m_Execute(parameter);
        }

        #endregion

        


    }
}

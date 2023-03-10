// System.Windows.Forms.Command
using System;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    internal class Command : IDisposable
    {
        private static readonly Type CommandType = typeof(Control).Assembly.GetType(typeof(Command).FullName);
        private static readonly Reflection.PropertyInfo CommandIDProperty = CommandType.GetProperty(nameof(ID), typeof(Int32));
        private static readonly Reflection.MethodInfo CommandDisposeMethod = CommandType.GetMethod(nameof(Dispose), Type.EmptyTypes);

        private readonly Object cmd;

        public Command(ICommandExecutor target) => this.cmd = Activator.CreateInstance(CommandType, target);

        public Int32 ID => (Int32)CommandIDProperty.GetValue(this.cmd);

        public void Dispose() => CommandDisposeMethod.Invoke(this.cmd, null);
    }
}

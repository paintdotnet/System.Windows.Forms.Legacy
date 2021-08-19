// System.Windows.Forms.Command
using System;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    internal class Command : WeakReference
    {
        private static Command[] cmds;

        private static int icmdTry;

        private static object internalSyncObject = new object();

        private const int idMin = 256;

        private const int idLim = 65536;

        internal int id;

        public virtual int ID => id;

        public Command(ICommandExecutor target)
            : base(target, trackResurrection: false)
        {
            AssignID(this);
        }

        protected static void AssignID(Command cmd)
        {
            lock (internalSyncObject)
            {
                int num;
                if (cmds == null)
                {
                    cmds = new Command[20];
                    num = 0;
                }
                else
                {
                    int num2 = cmds.Length;
                    if (icmdTry >= num2)
                    {
                        icmdTry = 0;
                    }
                    num = icmdTry;
                    while (true)
                    {
                        if (num < num2)
                        {
                            if (cmds[num] == null)
                            {
                                break;
                            }
                            num++;
                            continue;
                        }
                        num = 0;
                        while (true)
                        {
                            if (num < icmdTry)
                            {
                                if (cmds[num] == null)
                                {
                                    break;
                                }
                                num++;
                                continue;
                            }
                            num = 0;
                            while (true)
                            {
                                if (num < num2)
                                {
                                    if (cmds[num].Target == null)
                                    {
                                        break;
                                    }
                                    num++;
                                    continue;
                                }
                                num = cmds.Length;
                                num2 = Math.Min(65280, 2 * num);
                                if (num2 <= num)
                                {
                                    GC.Collect();
                                    num = 0;
                                    while (true)
                                    {
                                        if (num < num2)
                                        {
                                            if (cmds[num] == null || cmds[num].Target == null)
                                            {
                                                break;
                                            }
                                            num++;
                                            continue;
                                        }
                                        throw new ArgumentException("CommandIdNotAllocated");
                                    }
                                }
                                else
                                {
                                    Command[] destinationArray = new Command[num2];
                                    Array.Copy(cmds, 0, destinationArray, 0, num);
                                    cmds = destinationArray;
                                }
                                break;
                            }
                            break;
                        }
                        break;
                    }
                }
                cmd.id = num + 256;
                cmds[num] = cmd;
                icmdTry = num + 1;
            }
        }

        public static bool DispatchID(int id)
        {
            return GetCommandFromID(id)?.Invoke() ?? false;
        }

        protected static void Dispose(Command cmd)
        {
            lock (internalSyncObject)
            {
                if (cmd.id >= 256)
                {
                    cmd.Target = null;
                    if (cmds[cmd.id - 256] == cmd)
                    {
                        cmds[cmd.id - 256] = null;
                    }
                    cmd.id = 0;
                }
            }
        }

        public virtual void Dispose()
        {
            if (id >= 256)
            {
                Dispose(this);
            }
        }

        public static Command GetCommandFromID(int id)
        {
            lock (internalSyncObject)
            {
                if (cmds == null)
                {
                    return null;
                }
                int num = id - 256;
                if (num < 0 || num >= cmds.Length)
                {
                    return null;
                }
                return cmds[num];
            }
        }

        public virtual bool Invoke()
        {
            object target = Target;
            if (!(target is ICommandExecutor))
            {
                return false;
            }
            ((ICommandExecutor)target).Execute();
            return true;
        }
    }
}

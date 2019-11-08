using System;
using SadConsole;
using SadConsole.Entities;
using SadConsole.Input;

namespace TextAdventure.Core.Entities
{
    public abstract class ReactiveToInputEntity : Entity
    {
        public Action<Entity, Keyboard> ProcessKeyboardFn;
        public Action<Entity, MouseConsoleState> ProcessMouseFn;

        public ReactiveToInputEntity(int width, int height, Action<Entity, Keyboard> processKeyboardFn = null, 
                                                            Action<Entity, MouseConsoleState> processMouseFn = null)
            : base(width, height)
        {
            if (UseKeyboard = processKeyboardFn != null)
            {
                this.ProcessKeyboardFn = processKeyboardFn;
            }

            if (UseMouse = processMouseFn != null)
            {
                this.ProcessMouseFn = processMouseFn;
            }
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            ProcessKeyboardFn?.Invoke(this, info);
            return base.ProcessKeyboard(info);
        }

        public override bool ProcessMouse(MouseConsoleState state)
        {
            ProcessMouseFn?.Invoke(this, state);
            return base.ProcessMouse(state);
        }
    }
}

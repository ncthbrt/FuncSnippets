using System;

namespace FuncSnippets
{
    public class Recursive<T>
    {
        private enum State
        {
            Recurse,
            Return
        }

        private readonly State _recursiveState;
        private readonly Func<Recursive<T>> _recursiveFunction;
        private readonly T _val;

        internal Recursive(T val)
        {
            _val = val;
            _recursiveFunction = default(Func<Recursive<T>>);
            _recursiveState = State.Return;
        }

        internal Recursive(Func<Recursive<T>> func)
        {
            _recursiveState = State.Recurse;
            _recursiveFunction = func;
            _val = default(T);
        }

        public static implicit operator T(Recursive<T> val)
        {
            var current = val;
            while (current._recursiveState != State.Return)
            {
                current = current._recursiveFunction();
            }
            return current._val;
        }

        public static implicit operator Recursive<T>(T val)=>new Recursive<T>(val);        
    }
}
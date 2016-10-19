using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace FuncSnippets
{
    public class ConditionalContinuation<T, TR>
    {
        internal Option<TR> Result { get; }
        public T Value { get; }

        public ConditionalContinuation(T value)
        {
            Value = value;
            Result = Option<TR>.None;
        }


        public ConditionalContinuation(ConditionalContinuation<T, TR> prevConditionalContinuation, TR result)
        {
            Value = prevConditionalContinuation.Value;
            Result = result;
        }

        public ConditionalContinuation(T value, TR result)
        {
            Value = value;
            Result = result;
        }

        public static implicit operator Option<TR>(ConditionalContinuation<T, TR> self) =>
            self.Result.Match<Option<TR>>
            (
                Some: (rez) => Optional(rez),
                None: () => Option<TR>.None
            );
    }
}
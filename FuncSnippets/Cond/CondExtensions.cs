using LanguageExt;
using static LanguageExt.Prelude;
using System.Threading.Tasks;
using static System.Threading.Tasks.Task;
using System;
using FuncSnippets;

namespace FuncSnippets
{
    public static class CondExtensions
    {

        #region Establishing the continuation
        public static ConditionalContinuation<T, TR> Cond<T, TR>(this T val, Func<T, bool> condition, Func<T, TR> returns) =>
            condition(val) ?
                new ConditionalContinuation<T, TR>(val, returns(val))
                : new ConditionalContinuation<T, TR>(val);

        public static async Task<ConditionalContinuation<T, TR>> Cond<T, TR>(this T val, Func<T, bool> condition, Func<T, Task<TR>> returns) =>
            condition(val) ?
                    new ConditionalContinuation<T, TR>(val, await returns(val)) 
                    : new ConditionalContinuation<T, TR>(val);

        public static async Task<ConditionalContinuation<T, TR>> Cond<T, TR>(this Task<T> val, Func<T, bool> condition, Func<T, Task<TR>> returns) =>
            await Cond<T, TR>(await val, condition, returns);

        public static async Task<ConditionalContinuation<T, TR>> Cond<T, TR>(this Task<T> val, Func<T, bool> condition, Func<T, TR> returns) =>
            Cond(await val, condition, returns);

        #endregion Establishing the continuation

        #region Continuing
        public static async Task<ConditionalContinuation<T, TR>> Cond<T, TR>(this Task<ConditionalContinuation<T, TR>> continuation, Func<T, bool> condition, Func<T, TR> returns) =>
            (await continuation).Cond(condition, returns);

        public static ConditionalContinuation<T, TR> Cond<T, TR>(this ConditionalContinuation<T, TR> continuation, Func<T, bool> condition, Func<T, TR> returns) =>
            continuation.Result.Match<ConditionalContinuation<T, TR>>
            (
                Some: (result) => new ConditionalContinuation<T, TR>(continuation.Value, result),
                None: () => Cond<T, TR>(continuation.Value, condition, returns)
            );


        public static Task<ConditionalContinuation<T, TR>> Cond<T, TR>(this ConditionalContinuation<T, TR> continuation, Func<T, bool> condition, Func<T, Task<TR>> returns) =>
            continuation.Result.Match
            (
                Some: result => FromResult(new ConditionalContinuation<T, TR>(continuation.Value, result)),
                None: () => Cond<T, TR>(continuation.Value, condition, returns)
            );
        public static async Task<ConditionalContinuation<T, TR>> Cond<T, TR>(this Task<ConditionalContinuation<T, TR>> continuation, Func<T, bool> condition, Func<T, Task<TR>> returns) =>
            await map((await continuation), conditionalContinuation => conditionalContinuation.Cond(condition, returns));


        #endregion Continuing
        #region Else
        public static async Task<TR> Else<T, TR>(this Task<ConditionalContinuation<T, TR>> continuation, Func<T, Task<TR>> returns) =>
            await (await continuation).Else(returns);

        public static Task<TR> Else<T, TR>(this ConditionalContinuation<T, TR> continuation, Func<T, Task<TR>> returns) =>
            continuation.Result.Match<Task<TR>>(Some: FromResult, None: async () => await returns(continuation.Value));

        public static async Task<TR> Else<T, TR>(this Task<ConditionalContinuation<T, TR>> continuationTask, Func<T, TR> returns) =>
            map(await continuationTask, continuation => continuation.Else(returns));
        public static TR Else<T, TR>(this ConditionalContinuation<T, TR> continuation, Func<T, TR> returns) =>
            continuation.Result.Match<TR>(Some: identity, None: () => returns(continuation.Value));
            
        #endregion Else

    }
}
using LanguageExt;
using static LanguageExt.Prelude;
using System.Threading.Tasks;
using static System.Threading.Tasks.Task;
using System;

namespace FuncSnippets
{
    public static class CondExtensions
    {
        public static async Task<ConditionalContinuation<T, TR>> CondClause<T, TR>(this T val, Func<T, bool> condition, Func<T, Task<TR>> returns) =>
            condition(val) ?
                new ConditionalContinuation<T, TR>(val, await returns(val))
                : new ConditionalContinuation<T, TR>(val);

        
        public static ConditionalContinuation<T,TR> CondClause<T,TR>(this T val, Func<T, bool> condition, Func<T, TR> returns) =>
             new ConditionalContinuation<T,TR>(val).CondClause<T,TR>(condition,(x)=>FromResult(returns(x)));
        
                
        // public static async Task<ConditionalContinuation<T, TR>> Cond<T, TR>(this Task<T> val) =>
        //      new ConditionalContinuation<T, TR>(await val);

        // public static ConditionalContinuation<T, TR> Cond<T, TR>(this T val) =>
        //      new ConditionalContinuation<T, TR>(val);
             

        public static async Task<ConditionalContinuation<T, TR>> CondClause<T, TR>(this Task<ConditionalContinuation<T, TR>> val, Func<T, bool> condition, Func<T, Task<TR>> returns) =>
            map(await val, v => v.CondClause(condition, returns));

        public static async Task<ConditionalContinuation<T, TR>> CondClause<T, TR>(this Task<ConditionalContinuation<T, TR>> val, Func<T, bool> condition, Func<T, TR> returns) =>
            map(await val, v => v.CondClause(condition, (u) => Task.FromResult(returns(u))));

        public static async Task<ConditionalContinuation<T, TR>> CondClause<T, TR>(this Task<ConditionalContinuation<T, TR>> conditionalContinuation, Func<T, bool> condition, Func<T, Task<TR>> returns) =>
            conditionalContinuation.Result
                .Match(
                    Some: (resultOfContinuation) => new ConditionalContinuation<T, TR>(await conditionalContinuation.Result, resultOfContinuation),
                    None: () => conditionalContinuation.Value.CondClause(condition, returns)
                );


        public static async Task<TR> Else<T, TR>(this Task<ConditionalContinuation<T, TR>> prevCase, Func<T, Task<TR>> returns) =>
            await (await prevCase).Result.Match(Some: identity, None: async () => await returns((await prevCase).Value));

        public static async Task<TR> Else<T, TR>(this Task<ConditionalContinuation<T, TR>> prevCase, Func<T, TR> returns) =>
            await (await prevCase).Result.Match(Some: identity, None: async () => returns((await prevCase).Value));

        public static Task<TR> Else<T, TR>(this ConditionalContinuation<T, TR> prevConditionalContinuation, Func<T, Task<TR>> returns) =>
            returns(prevConditionalContinuation.Value);
    }
}
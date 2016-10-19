using System;
using System.Diagnostics.Contracts;
using static LanguageExt.Prelude;

namespace FuncSnippets
{
    public static class Prelude
    {
        public static Recursive<T> returns<T>(T t) => new Recursive<T>(t);

        public static Recursive<A> recurse<A>(Func<Recursive<A>> func) =>
            new Recursive<A>(func);

        public static Recursive<B> recurse<A, B>(Func<A, Recursive<B>> func, A a) =>
            new Recursive<B>(() => func(a));

        public static Recursive<C> recurse<A, B, C>(Func<A, B, Recursive<C>> func, A a, B b) =>
            new Recursive<C>(() => func(a, b));

        public static Recursive<D> recurse<A, B, C, D>(Func<A, B, C, Recursive<D>> func, A a, B b, C c) =>
            new Recursive<D>(() => func(a, b, c));

        public static Recursive<E> recurse<A, B, C, D, E>(Func<A, B, C, D, Recursive<E>> func, A a, B b, C c, D d) =>
            new Recursive<E>(() => func(a, b, c, d));

        public static Recursive<F> recurse<A, B, C, D, E, F>(Func<A, B, C, D, E, Recursive<F>> func, A a, B b, C c, D d, E e) =>
            new Recursive<F>(() => func(a, b, c, d, e));

        public static Recursive<G> recurse<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, Recursive<G>> func, A a, B b, C c, D d, E e, F f) =>
            new Recursive<G>(() => func(a, b, c, d, e, f));

        public static Recursive<H> recurse<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, Recursive<H>> func, A a, B b, C c, D d, E e, F f, G g) =>
            new Recursive<H>(() => func(a, b, c, d, e, f, g));

        public static Recursive<I> recurse<A, B, C, D, E, F, G, H, I>(Func<A, B, C, D, E, F, G, H, Recursive<I>> func, A a, B b, C c, D d, E e, F f, G g, H h) =>
            new Recursive<I>(() => func(a, b, c, d, e, f, g, h));

        public static Recursive<J> recurse<A, B, C, D, E, F, G, H, I, J>(Func<A, B, C, D, E, F, G, H, I, Recursive<J>> func, A a, B b, C c, D d, E e, F f, G g, H h, I i) =>
            new Recursive<J>(() => func(a, b, c, d, e, f, g, h, i));

        public static Recursive<K> recurse<A, B, C, D, E, F, G, H, I, J, K>(Func<A, B, C, D, E, F, G, H, I, J, Recursive<K>> func, A a, B b, C c, D d, E e, F f, G g, H h, I i, J j) =>
            new Recursive<K>(() => func(a, b, c, d, e, f, g, h, i, j));

    }

}
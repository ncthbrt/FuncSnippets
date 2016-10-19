using System.Numerics;
using static FuncSnippets.Prelude;
using static System.Console;
using System.Threading.Tasks;
using System;
using static FuncSnippets.CondExtensions;

namespace FuncSnippets
{
    public class TestClass
    {

        public static BigInteger Fibonacci(BigInteger n) => Fibonacci(n, 1);

        public static Recursive<BigInteger> Fibonacci(BigInteger n, BigInteger product) =>
            n < 2 ?
                returns(product)
                : recurse(Fibonacci, n - 1, n * product);


        public static BigInteger NaiveFibonacci(BigInteger n, BigInteger product) =>
            n < 2 ?
                product
                : NaiveFibonacci(n - 1, n * product);


        public static void Main(string[] args)
        {
            #region Recursive Example
            {
                WriteLine(Fibonacci(12345));                
                //This will overflow the stack if you uncomment it
                //WriteLine(NaiveFibonacci(12345,1));
            }
            #endregion Recursive Example
            
            #region Cond Example
            {

                Random random=new Random();
                WriteLine(
                    (random.NextDouble())                   
                    .CondClause<double,string>(x=>x<0.25,(x)=>$"{x} is quite a low value. Please try again later")
                    .CondClause<double,string>(x=>x<0.5,(x)=>$"{x} is not quite half full")
                    .CondClause<double,string>(x=>x>=0.5 && x<=0.75,(x)=>$"{x} is half full or half empty")
                    .Else<double,string>((x)=>$"{x} is close to 1.0")
                );
                    
                                
                //This will overflow the stack if you uncomment it
                //WriteLine(NonTCOFibonacci(12345,1));
            }
            #endregion Recursive Example
            
        }

    }
}

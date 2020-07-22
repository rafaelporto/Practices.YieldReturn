using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

namespace YieldReturn
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Antes de chamar 'Foo'...");
            var foo = Foo();
            WriteLine("Depois de chamar 'Foo'...");

            using (var enumerator = foo.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var elem = enumerator.Current;
                    WriteLine($"Antes de imprimir 'elem' {elem}...");
                    WriteLine(elem);
                    WriteLine($"Depois de imprimir 'elem' {elem}...");
                }
            }
        }

        public static IEnumerable<int> Foo() => new MyEnumerable();
    }

    public class MyEnumerable : IEnumerable<int>, IDisposable
    {
        public void Dispose(){ }

        public IEnumerator<int> GetEnumerator() => new MyEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => new MyEnumerator();
    }

    public class MyEnumerator : IEnumerator<int>
    {
        public MyEnumerator()
        {
            WriteLine("Antes de iniciar o 'loop for' ...");
        }

        int _current = -1;
        public int Current => _current;

        object IEnumerator.Current => _current;

        public void Dispose()
        {
            WriteLine($"Depois de encerrar o 'loop for' ...");
        }

        public bool MoveNext()
        {
            if (_current >= 0)
                WriteLine($"Depois do 'yield return {_current}' ...");

            if (_current > 3) return false;

            _current++;
            WriteLine($"Antes do 'yield return {_current}' ...");
            return true;
        }

        public void Reset()
        {
            _current = -1;
        }
    }
}

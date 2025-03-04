using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LINQ
{
    class Program
    {
        //static void printList(List<string> a)
        //{ 
        //    foreach(var item in a)
        //        Console.WriteLine(item);

        //    Console.WriteLine();
        //}

        static void printList(List<string> a)
        {
            for (int i = 0; i < a.Count; i++)
                Console.WriteLine(a[i]);

            Console.WriteLine();
        }

        static void printQueue(Queue<string> a)
        {
            foreach(var i in a)
                Console.Write(i + " ");

            Console.WriteLine();
        }
        static void printStack(Stack<int> st)
        {
            foreach (int i in st)
                Console.Write(i + " ");

            Console.WriteLine();
        }

        static int calcEquation(Queue<string> equation)
        {
            Stack<int> calc = new Stack<int>();
            foreach (var sym in equation)
            {
                string op = "-+/*";
                if (!op.Contains(sym))
                {
                    calc.Push(Convert.ToInt32(sym.ToString()));
                }
                else
                {
                    int b = calc.Pop();
                    int a = calc.Pop();
                    switch (sym)
                    {
                        case "+": calc.Push(a + b); break;
                        case "-": calc.Push(a - b); break;
                        case "*": calc.Push(a * b); break;
                        case "/": calc.Push(a / b); break;
                    }
                }
            }
            return calc.Pop();
        }

        private static Queue<string> buildPostfix(string s)
        {
            Stack<string> stackOper = new Stack<string>();
            Queue<string> queueOPZ = new Queue<string>();

            Dictionary<string, int> priority_operations = new Dictionary<string, int> { { "+", 1 }, { "-", 1 }, { "*", 2 }, { "/", 2 }, {"(", 0 } };

            foreach (var sym in s)
            {
                if (sym == '(')
                    stackOper.Push(sym.ToString());
                else if (sym == ')')
                {
                    while (stackOper.Peek() != "(") { queueOPZ.Enqueue(stackOper.Pop()); }
                    stackOper.Pop(); // выкинули открывающуюся скобку
                }
                else if (priority_operations.ContainsKey(sym.ToString()))
                {
                    string cur_op = sym.ToString();
                    while (stackOper.Count > 0 && priority_operations[cur_op] <= priority_operations[stackOper.Peek()])
                    {
                        { queueOPZ.Enqueue(stackOper.Pop()); }
                    }
                    stackOper.Push(cur_op);
                }
                else
                {
                    queueOPZ.Enqueue(sym.ToString());
                }
            }
            while (stackOper.Count > 0) { queueOPZ.Enqueue(stackOper.Pop()); }
            return queueOPZ;
        }


        static void Main()
        {
            // построение постфиксной записи
            string s = "((((5+8)*(9-2))*(3-1)-((8+1)/2))/3)*(9-3)";
            Console.WriteLine(s);
            Queue<string> opz = buildPostfix(s);
            printQueue(opz);

            //List<string> equation = new List<string> { "13", "5", "+", "4", "7", "2", "1", "-", "*", "+", "*" };
            //// вычисление по постфиксной записи
            Console.WriteLine(calcEquation(opz));

        }

       
    }
}

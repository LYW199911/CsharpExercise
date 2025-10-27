using System;
using System.Threading;
using System.Threading.Tasks;

namespace UntitledApp
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            int aimPos = 1;

            //listenner
            _ = Task.Run(() =>
            {
                Console.ReadLine();
                cts.Cancel();
            });

            Console.WriteLine("射撃：");
            Random rand = new Random();
            var hitpoint = rand.Next(11, 21); // 11-20の間でランダムに命中点を決定
            for (int a = 1; a <= 30; a++)
            {  
                if (a == hitpoint)
                    Console.Write("*");
                else
                    Console.Write("-");;
            }            
            Console.WriteLine();

            try
            {
                for (aimPos = 1; aimPos <= 30; aimPos++)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    // 输出瞄准线
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i == aimPos)
                            Console.Write("^"); // 当前准星
                        else
                            Console.Write(" ");
                    }
                    Console.Write("\r"); // 回到行首
                    await Task.Delay(60, cts.Token);
                }
                Console.WriteLine("\nNo Shoot！");
                return 0;
            }
            catch (OperationCanceledException)
            {
                // 判断是否命中
                if (aimPos == hitpoint)
                    Console.WriteLine("\nHit!");
                else
                    Console.WriteLine("\nmiss!");
                return 1;
            }
        }
    }
}
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
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            Console.WriteLine("异步绘制三角形：");
            try
            {
                await DrawTriangleAsync(6, cts.Token); // 高
                Console.WriteLine("绘制完成！");
                return 0;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("操作被取消。");
                return 1;
            }
        }

        static async Task DrawTriangleAsync(int height, CancellationToken cancellationToken)
        {
            for (int row = 1; row <= height; row++)
            {
                int start = height - row + 1;
                int end = height + row - 1;

                for (int col = 1; col <= 2 * height - 1; col++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (col >= start && col <= end)
                        await OutputStarAsync(cancellationToken);
                    else
                        await OutputMinusAsync(cancellationToken);
                }
                await OutputNewLineAsync(cancellationToken);
            }
        }

        // 输出1秒星号
        static async Task OutputStarAsync(CancellationToken cancellationToken)
        {
            Console.Write("*");
            await Task.Delay(100, cancellationToken);
        }

        static async Task OutputMinusAsync(CancellationToken cancellationToken)
        {
            Console.Write("-");
            await Task.Delay(100, cancellationToken);
        }

        // 换行
        static async Task OutputNewLineAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine();
            await Task.Delay(100, cancellationToken);
        }
    }
}
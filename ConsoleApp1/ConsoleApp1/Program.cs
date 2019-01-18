using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Task t = BadAsync();
        t.Wait();
        Console.WriteLine("Task Status  :  {0}", t.Status);//获取任务状态
        Console.WriteLine("Task IsFaulted  :  {0}", t.IsFaulted);//task是否由于未经处理异常的原因而完成
    }
    static async Task BadAsync()
    {
        try
        {
            await Task.Run(() => { throw new Exception(); });
        }
        catch
        {
            Console.WriteLine("Exception in BadAsync");
        }
    }
}


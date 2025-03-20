using System;
using System.IO;
using ConsoleApp3;
using System.Threading;

class Program
{
    private static List<Thread> threadList = new List<Thread>();
    private static bool isRunning = false;
    private static readonly object locker = new object();
    List<string> proxies = new List<string>();

    static void Main(string[] args)
    {
        int threads = 1;
        int timeout = 5000;
        string proxyFile = string.Empty;
        int selectedOption = 0;

        while (true)
        {
            Console.Clear();
            DisplayMenu(selectedOption, threads, timeout, proxyFile, isRunning);

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = Math.Max(0, selectedOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = Math.Min(4, selectedOption + 1);
                    break;
                case ConsoleKey.Enter:
                    HandleSelection(ref threads, ref timeout, ref proxyFile, ref selectedOption);
                    break;
                case ConsoleKey.Escape:
                    StopWorker();
                    return;
            }
        }
    }

    static void DisplayMenu(int selectedOption, int threads, int timeout, string proxyFile, bool isRunning)
    {
        Console.WriteLine($"1. Number of threads: {threads} {(selectedOption == 0 ? ">" : " ")}");
        Console.WriteLine($"2. Timeout (ms): {timeout} {(selectedOption == 1 ? ">" : " ")}");
        Console.WriteLine($"3. Proxy file: {(string.IsNullOrEmpty(proxyFile) ? "No chosed" : proxyFile)} {(selectedOption == 2 ? ">" : " ")}");
        Console.WriteLine($"4. State: {(isRunning ? "Working" : "Not working")} {(selectedOption == 3 ? ">" : " ")}");
        Console.WriteLine($"5. {(!isRunning ? "Start" : "Stop")} {(selectedOption == 4 ? ">" : " ")}");
    }

    static void HandleSelection(ref int threads, ref int timeout, ref string proxyFile, ref int selectedOption)
    {
        switch (selectedOption)
        {
            case 0:
                threads = GetNumberInput("Set num of threads", 1, 400);
                break;
            case 1:
                timeout = GetNumberInput("Set timeout:", 1000, 60000);
                break;
            case 2:
                proxyFile = GetFilePath();
                break;
            case 3:
                // Показать текущее состояние
                Console.WriteLine($"\nState: {(isRunning ? "Working" : "Stopped")}");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                break;
            case 4:
                if (!isRunning)
                {
                    StartWorker(threads);
                }
                else
                {
                    StopWorker();
                }
                break;
        }
    }

    static void Work()
    {
        try
        {
            string str = string.Empty;
            string query = "SEARCH WHERE ";
            string proxy = string.Empty;
            int temp = 0;
            while (Loader.work)
            {
                if (!Loader.work)
                    break;

                doSomeThing(temp++, proxy);
                Thread.Sleep(100);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }

    private static void doSomeThing(int temp, string proxy)
{
        string host = string.Empty;
        string port = string.Empty;

        try
        {
            if (Loader.proxyList.Count > 0)
            {
                proxy = Loader.GetProxy();
            }
            Console.WriteLine("Each Thread Routine: [" + temp + "] Iteration");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing proxy: {ex.Message}");
        }
    
}

    static void StartWorker(int numThreads)
    {
        lock (locker)
        {
            if (isRunning)
                return;

            Loader.work = true;
            threadList.Clear();

            for (int thrcount = 0; thrcount < numThreads; thrcount++)
            {
                Thread th = new Thread(new ThreadStart(Work));
                th.IsBackground = true;
                th.Start();
               threadList.Add(th);
            }

            isRunning = true;
            Console.WriteLine("\nWork is started. Threads: " + numThreads);
            Console.ReadKey();
        }
    }

    static void StopWorker()
    {
        lock (locker)
        {
            if (!isRunning)
                return;

            Loader.work = false;
            foreach (Thread th in threadList)
            {
                if (th != null && th.IsAlive)
                {
                    th.Join(5000); 
                }
            }
            threadList.Clear();
            isRunning = false;
            Console.WriteLine("\nWork is stopped.");
            Console.ReadKey();
        }
    }
    
    static int GetNumberInput(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write($"\n{prompt} ({min}-{max}): ");
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine("Error");
        }
    }

    static string GetFilePath()
    {
        Console.Write("\nProxy path");
        string path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
            return string.Empty;

        if (File.Exists(path))
            return path;

        Console.WriteLine("No file entered");
        return string.Empty;
    }
}
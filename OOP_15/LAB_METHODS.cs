using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;
using System.Threading;
using System.IO;
using System.Reflection;

namespace OOP_15
{
    public static class LAB_METHODS
    {
        #region FIELDS
        static object locker = new object();
        public static StreamWriter fstream = new StreamWriter(@"..\Numbers_1.txt");
        public static Mutex mutex = new Mutex();
        #endregion

        #region METHODS
        public static void ProcessAnalyze()
        {
            List<Process> processes_list = new List<Process>(Process.GetProcesses());
            using (StreamWriter fstream = new StreamWriter(@"..\FileOut.txt"))
            {
                foreach (Process item in processes_list)
                {
                    WriteLine($"\n ID процесса:{item.Id}");
                    WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                    WriteLine($"Имя процесса:{item.ProcessName}");
                    WriteLine($"Приоритет процесса:{item.BasePriority}");


                    fstream.WriteLine($"\n ID процесса:{item.Id}");
                    fstream.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

                    try
                    {
                        WriteLine($"Время запуска процесса:{item.StartTime}");
                        WriteLine($"Время использования CPU: {DateTime.Now.Hour - item.StartTime.Hour}:" +
                            $"{(DateTime.Now.Minute > item.StartTime.Minute ? DateTime.Now.Minute - item.StartTime.Minute : item.StartTime.Minute - DateTime.Now.Minute)}:" +
                            $"{(DateTime.Now.Second > item.StartTime.Second ? DateTime.Now.Second - item.StartTime.Second : item.StartTime.Second - DateTime.Now.Second)}");

                        fstream.WriteLine($"Время запуска процесса:{item.StartTime}");
                        fstream.WriteLine($"Время использования CPU: {DateTime.Now.Hour - item.StartTime.Hour}:" +
                           $"{(DateTime.Now.Minute > item.StartTime.Minute ? DateTime.Now.Minute - item.StartTime.Minute : item.StartTime.Minute - DateTime.Now.Minute)}:" +
                           $"{(DateTime.Now.Second > item.StartTime.Second ? DateTime.Now.Second - item.StartTime.Second : item.StartTime.Second - DateTime.Now.Second)}");
                    }
                    catch (Exception ex)
                    {
                        WriteLine(ex.Message);
                    }

                    fstream.WriteLine($"Имя процесса:{item.ProcessName}");
                    fstream.WriteLine($"Приоритет процесса:{item.BasePriority}");
                    fstream.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

                    WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                }
                Process notepad = Process.Start("notepad", @"..\FileOut.txt");
                Console.ReadKey();
                notepad.Kill();
            }
        }
        public static void DomainAnalyze()
        {
            AppDomain CurrentDomain = AppDomain.CurrentDomain;
            WriteLine($"Имя домена: {CurrentDomain.FriendlyName}");
            WriteLine($"Детали конфигурации: имя приложения:{CurrentDomain.SetupInformation.ApplicationName,10};" +
                $"\n каталог приложения :{CurrentDomain.SetupInformation.ApplicationBase,5};");
            WriteLine("Все сборки:");
            foreach (Assembly item in CurrentDomain.GetAssemblies())
            {
                WriteLine(item);
            }

            AppDomain NewOne = AppDomain.CreateDomain("notepad");
            var domain = Thread.GetDomain();
            NewOne.Load(domain.GetAssemblies()[0].FullName);
            AppDomain.Unload(NewOne);
        }
        public static void ThreadAnalyze()
        {
            Thread thread = Thread.CurrentThread;
            int n;
            Console.Write("Введите значение n: ");
            n = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                Thread.Sleep(100);
                Console.Write(" " + i);

                if (i % 5 == 0)
                {
                    thread.Suspend();
                }
            }
            WriteLine($"\nСтатус потока:{thread.ThreadState}");
            WriteLine($"Имя потока:{thread.Name}");
            WriteLine($"Приоритет потока:{thread.Priority}");
            WriteLine($"ID потока:{thread.ManagedThreadId}");
            thread.Interrupt();
            WriteLine();
        }
        public static void Timer(object a)
        {
            WriteLine("\n▬▬▬▬▬▬▬▬▬ Сработал таймер ▬▬▬▬▬▬▬▬▬\n");
        }
        public static void OutputNumbers(object inputN)
        {
            lock (locker)
            {
                int n = (int)inputN;
                if (Thread.CurrentThread.Name == "1")
                    for (int i = 1; i <= n; i++)
                        if (i % 2 != 0)
                        {
                            fstream.Write($" {i,3} ");
                            Write($" {i,3} ");
                        }

                if (Thread.CurrentThread.Name == "2")
                    for (int i = 1; i <= n; i++)
                        if (i % 2 == 0)
                        {
                            fstream.Write($" {i,3} ");
                            Write($" {i,3} ");
                        }
            }

        }
        public static void OutputNumbersSinh(object inputN)
        {
            int n = (int)inputN;
            if (Thread.CurrentThread.Name == "1")
            {
                for (int i = 1; i <= n; i++)
                    if (i % 2 != 0)
                    {
                        mutex.WaitOne();
                        fstream.Write($" {i,3} ");
                        Write($" {i,3} ");
                        mutex.ReleaseMutex();
                    }
            }

            if (Thread.CurrentThread.Name == "2")
            {
                for (int i = 1; i <= n; i++)
                    if (i % 2 == 0)
                    {
                        mutex.WaitOne();
                        fstream.Write($" {i,3} ");
                        Write($" {i,3} ");
                        mutex.ReleaseMutex();
                    }
            }

        }
        #endregion

    }
}

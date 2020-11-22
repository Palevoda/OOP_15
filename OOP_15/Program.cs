using System;
using System.Diagnostics;
using static System.Console;
using System.Threading;
using static OOP_15.LAB_METHODS;

namespace OOP_15
{
    class Program
    {
 
        static void Main()
        {
            #region PROCESS
            ProcessAnalyze();
            #endregion

            #region DOMAIN
            DomainAnalyze();
            #endregion

            #region TIMER & THREAD
            TimerCallback tmr = new TimerCallback(Timer);
            Timer timer = new Timer(tmr, null, 0, 5000);

            Thread thread = new Thread(new ThreadStart(ThreadAnalyze)) { Name = "Имя_потока", Priority = ThreadPriority.Highest };
            thread.Start();

            while (thread.ThreadState != System.Threading.ThreadState.Stopped)
            {
                if (thread.ThreadState == System.Threading.ThreadState.Suspended)
                {
                    Thread.Sleep(500);
                    thread.Resume();
                }
            }
            #endregion

            #region EVEN & ODD
            Thread thread1 = new Thread(new ParameterizedThreadStart(OutputNumbers)) { Name = "1", Priority = ThreadPriority.Highest };
            Thread thread2 = new Thread(new ParameterizedThreadStart(OutputNumbers)) { Name = "2", Priority = ThreadPriority.AboveNormal };
            thread1.Start(15);
            thread2.Start(15);

            while (true)
            {
                if (thread1.IsAlive == false && thread2.IsAlive == false)
                {
                    fstream.WriteLine("\n══════════════════════");
                    WriteLine("\n▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                    Thread thread3 = new Thread(new ParameterizedThreadStart(OutputNumbersSinh)) { Name = "1" };
                    Thread thread4 = new Thread(new ParameterizedThreadStart(OutputNumbersSinh)) { Name = "2" };
                    thread3.Start(15);
                    thread4.Start(15);
                    while (true)
                    {
                        if (thread1.IsAlive == false && thread2.IsAlive == false && thread3.IsAlive == false && thread4.IsAlive == false)
                        {
                            fstream.Close();
                            break;
                        }
                    }
                    break;
                }
               
            }
            
            Process open_note =Process.Start("notepad", @"..\Numbers_1");
            ReadKey();
            if(open_note.HasExited != true)
            open_note.Kill();
            #endregion

            Console.ReadKey();
        }

       
    }
}

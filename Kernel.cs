using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.IO;
using Cosmos.System.Graphics;
using System.Threading;
using System.Drawing;
using Cosmos.Core;
using Cosmos.HAL.Drivers.PCI.Audio;
using Cosmos.System;
using LibDotNetParser;
using LibDotNetParser.CILApi;
using Console = System.Console;
using Programs;

namespace LunarOS
{
    public class Kernel : Sys.Kernel
    {
        public static bool video = false;
        public byte[] vrama = new byte[4194304];
        public byte[] vramb = new byte[4194304];
        public byte[] vramc = new byte[4194304];
        public byte[] vramd = new byte[4194304];
        public string cd = String.Empty;
        protected override void BeforeRun()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Lunar Boot Manager");
                    Console.WriteLine("");
                    Console.WriteLine("1) LunarOS");
                    Console.WriteLine("2) Recovery");
                    Console.Write("> ");
                    string key = Console.ReadKey().KeyChar.ToString();
                    if (key == "1")
                    {
                        break;
                    }
                    if (key == "2")
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Lunar Boot Manager");
                            Console.WriteLine("");
                            Console.WriteLine("1) Format Partition");
                            Console.WriteLine("2) Back");
                            Console.Write("> ");
                            key = Console.ReadKey().KeyChar.ToString();
                            if (key == "2")
                            {
                                break;
                            }
                        }
                    }
                }
                Console.WriteLine("Starting LunarOS...");
                Console.WriteLine("Initializing drivers...");
                //Programs.Programs.drvinit();
                Console.WriteLine("Initializing programs...");
                Programs.Programs.boot();
                // Console.Write("Password: ");
                // Console.ReadLine();
                /* if (!File.Exists("s.lsf"))
                {
                    Console.WriteLine("Settings file not found. Applying default settings.");
                    using (StreamWriter sw = File.CreateText("s.lsf"))
                    {
                        sw.WriteLine("bla");
                        sw.WriteLine("whi");
                        sw.WriteLine("EndOfLSF");
                    }
                }
                Console.WriteLine("Reading settings file...");
                string[] slist = new string[2];
                using (StreamReader sr = File.OpenText("s.lsf"))
                {
                    int i = 0;
                    string s;
                    while ((s = sr.ReadLine()) != "EndOfLSF")
                    {
                        Console.WriteLine(s);
                        slist[i] = s;
                        i++;
                    }
                }*/
            }
            catch (Exception e)
            {
                Crash(e.ToString());
            }
        }

        protected override void Run()
        {
            try
            {
                if (video)
                {
                    Drivers.Video.update();
                    Drivers.Video.drawScreen();
                }
                else
                {
                    Console.Write("> ");
                    var cmdline = Console.ReadLine();
                    userRun(cmdline);
                }
            }
            catch (Exception e)
            {
                Crash(e.ToString());
            }
        }
        public static void SystemErrorEvent(String e)
        {
            using (StreamWriter sw = File.CreateText("err.log"))
            {
                sw.WriteLine(e);
                Console.WriteLine(e);
            }
        }
        public static void SystemErrorEvent(Exception e)
        {
            using (StreamWriter sw = File.CreateText("err.log"))
            {
                sw.WriteLine(e.ToString());
                Console.WriteLine(e.ToString());
            }
        }
        public static Double Evaluate(String expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            return Convert.ToDouble(table.Compute(expression, String.Empty));
        }
        public static void Crash(String e)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine(e);
            while (true)
            {

            }
        }
        public static void StartCmd(String cmdline)
        {
            string[] cmdsplit = cmdline.Split(" ");
            var cmd = cmdsplit[0];
            var cmdargs = "";
            string[] cmdargst = null;
            var v = false;
            for (int i = 1; i < cmdsplit.Length; i++)
            {
                cmdargs += cmdsplit[i] + " ";
            }
            for (int i = 1; i < cmdsplit.Length; i++)
            {
                cmdargst[i - 1] = cmdsplit[i];
            }
            if (cmd == "reboot")
            {
                Sys.Power.Reboot();
                v = true;
            }
            if (cmd == "shutdown")
            {
                Sys.Power.Shutdown();
                v = true;
            }
            if (cmd == "clear")
            {
                Console.Clear();
                v = true;
            }
            if (cmd == "echo")
            {
                Console.WriteLine(cmdargs);
                v = true;
            }
            if (cmd == "crash")
            {
                Crash(cmdargs);
                v = true;
            }
            if (cmd == "lunarde")
            {
                //Console.WriteLine("This command is disabled.");
                LunarDE();
                v = true;
            }
            if (cmd == "ver")
            {
                Console.Write("LunarOS Build ");
                Console.WriteLine("030121.1"); // MMDDYY.B
                v = true;
            }
            if (cmd == "async")
            {
                Console.WriteLine("This command is disabled.");
                // StartCmdAsync(cmdargs);
                v = true;
            }
            if (cmd == "sinfo")
            {
                Console.WriteLine("Total RAM: " + Cosmos.Core.CPU.GetAmountOfRAM().ToString());
                Console.WriteLine("Used RAM: " + (Cosmos.Core.CPU.GetEndOfKernel() / 1048576).ToString());
                Console.WriteLine("CPU Vendor: " + Cosmos.Core.CPU.GetCPUVendorName());
                Console.WriteLine("Uptime: " + (Cosmos.Core.CPU.GetCPUUptime() / 1000000000).ToString());
                v = true;
            }
            if (cmd == "dir")
            {
                Console.WriteLine("Directory of 0:/");
                Console.WriteLine("");
                foreach (var item in Directory.GetDirectories("."))
                {
                    Console.Write(item);
                    Console.WriteLine(" (DIR)");
                }
                foreach (var item in Directory.GetFiles("."))
                {
                    Console.WriteLine(item);
                }
                v = true;
            }
            if (cmd == "cd")
            {
                try
                {
                    //native code???
                    //if (Directory.Exists(cmdargs))
                    //{
                    //Environment.CurrentDirectory += cmdargs;
                    //}
                }
                catch (Exception)
                {

                }
                v = true;
            }
            if (cmd == "md")
            {
                try
                {
                    Directory.CreateDirectory(cmdargs);
                }
                catch (Exception)
                {

                }
                v = true;
            }
            if (cmd == "lpm")
            {
                Lpm.cmd(cmdsplit[1],cmdsplit);
                v = true;
            }
            if (v)
            {
            }
            else
            {
                try
                {
                    byte[] fb = new byte[9999999999999999999];
                    using (BinaryReader reader = new BinaryReader(File.Open(cmd, FileMode.Open)))
                    {
                        fb[fb.Length + 1] = reader.ReadByte();
                    }
                    var fl = new DotNetFile(fb);
                    var vm = new DotNetVirtualMachine();
                    vm.SetMainExe(fl);
                    vm.Start();
                }
                catch (Exception e)
                {
                    SystemErrorEvent(e.ToString());
                }
            }
        }
        public static void Reboot()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.WriteLine("Restarting");
            Thread.Sleep(1500);
            Sys.Power.Reboot();
        }
        public static void Shutdown()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.WriteLine("Shutting down");
            Thread.Sleep(1500);
            Sys.Power.Shutdown();
        }
        protected static void LunarDE()
        {
            Drivers.Video.init();
            Drivers.Video.update();
        }
        public static void userRun(string cmd)
        {
            try
            {
                StartCmd(cmd);
            }
            catch (Exception e)
            {
                SystemErrorEvent(e.ToString());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sys = Cosmos.System;

namespace Programs
{

	public class Programs
	{
		public static void boot()
		{
			// program init code goes here
		}
		public static void drvinit()
		{
			Drivers.fsDrv();
			// driver init runs before boot()
			// put driver init stuff here
		}
	}
	public class Drivers
	{
		public static void fsDrv()
		{
			var fs = new Sys.FileSystem.CosmosVFS();
			Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
		}
	}
	/*
	public class LBC
	{
		public static void loadAndRun(String fn)
		{
			byte[] fb = new byte[2147483647];
			if (File.Exists(fn))
			{
				using (BinaryReader reader = new BinaryReader(File.Open(fn, FileMode.Open)))
				{
					fb[fb.Length + 1] = reader.ReadByte();
				}
				UInt32 pc = 0;
				byte ir = 0;
				bool run = true;
				while (run)
				{
					ir = fb[pc];
					pc++;
					if (pc == fb.Length)
					{
						break;
					}
					switch (ir)
					{
						case 0:
							break;
						case 1:
							Sys.Power.Shutdown();
							break;
						case 2:
							pc = BitConverter.ToUInt32(fb,(int)pc);
							break;
						case 3:
							run = false;
							break;
						case 4:
							byte[] outst = Encoding.ASCII.GetBytes(((char)fb[pc]).ToString());
							pc++;
							Console.WriteLine(Encoding.ASCII.GetString(outst));
							break;
						case 5:
							Console.WriteLine(fb[pc]);
							pc++;
							break;
						default:
							run = false;
							break;
					}
				}
			}
		}
	}
	*/
	public class Lpm
	{
		public static void cmd(string farg, string[] args)
        {
            if (farg == "install")
            {
				install(args);
				return;
            }
        }
		public static void cmd(string farg)
        {

        }
		public static void install(string[] args)
        {
			Console.WriteLine("The following packages will be installed:");
			//string[] pkgs = args.Where((item, index) => index != 0).ToArray();
			string[] pkgs = args;
			int i = 0;
            foreach (var pkg in args)
            {
				i++;
				Console.Write(" " + pkg);
            }
			Console.WriteLine();
			Console.Write("Do you want to install these " + pkgs.Length + " packages? (Y/N) ");
		    ConsoleKeyInfo yn = Console.ReadKey();
			string yns = yn.KeyChar.ToString();
            if (yns == "y")
            {
				Console.WriteLine("Installing...");
            }
            else
            {
				Console.WriteLine("Aborted.");
            }
        }
	}
}
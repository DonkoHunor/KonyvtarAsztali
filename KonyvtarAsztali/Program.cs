using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KonyvtarAsztali
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if(args.Contains("--stat"))
            {
                Statisztika stat = new Statisztika();
                stat.Run();
            }
            else
            {
				Application application = new Application();
				application.Run(new MainWindow());
			}		
            
		}
    }
}

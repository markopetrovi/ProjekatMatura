using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjekatMatura
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
	public class Ucenik
	{
		public int jezikMature = -1, tipMature = -1, prviPredmet = -1, treciPredmet = -1;
		public string ime = "", prezime = "", razred = "", skola = "";
		public bool IsEmpty()
		{
			return jezikMature == -1 && tipMature == -1 && prviPredmet == -1 && treciPredmet == -1 &&
					ime == "" && prezime == "" && razred == "" && skola == "";
		}
	}
}

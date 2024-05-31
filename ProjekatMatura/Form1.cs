using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskBand;
using System.IO;
using System.Collections;

namespace ProjekatMatura
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			drugiPredmet.SelectedIndex = 0;
			UpdateStatus();
			data = LoadUcenikList("data.csv");
			templates = LoadUcenikList("templates.csv");
			FetchStudent(0);
			UpdateSchoolList();
		}
		private void UpdateSchoolList()
		{
			skola.Items.Clear();
			foreach (Ucenik u in templates)
				skola.Items.Add(u.skola);
		}
		private List<Ucenik> templates;
		private readonly List<Ucenik> data;
		private int curId = 0;
		private List<Ucenik> LoadUcenikList(string path)
		{
			string data;
			List<Ucenik> l = new List<Ucenik>();

			try { data = File.ReadAllText(path); }
			catch (Exception) { return l; }
			string[] lines = data.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 1; i < lines.Length; i++)
				l.Add(LoadCsvObject(lines[i]));

			return l;
		}
		private void SaveUcenikList(string path, List<Ucenik> u)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Ime,Prezime,Razred,Skola,jezikMature,tipMature,prviPredmet,drugiPredmet,treciPredmet");
			foreach(Ucenik uc in u)
				sb.AppendLine(GenerateCsvLine(uc));
			File.WriteAllText(path, sb.ToString()); // TODO: Catch exception
		}
		private void drawButtonSign(Button button, PaintEventArgs e, string text)
		{
			Brush brush = new SolidBrush(Color.Black);
			SizeF textSize = e.Graphics.MeasureString(text, button.Font);

			// Calculate the scale factors
			float scaleX = (button.ClientSize.Width) / textSize.Width;
			float scaleY = (button.ClientSize.Height) / textSize.Height;

			e.Graphics.ScaleTransform(scaleX, scaleY);

			// Draw the string at the left edge of the button
			e.Graphics.DrawString(text, button.Font, brush, 0, 0);

			brush.Dispose();
		}
		private void last_Paint(object sender, PaintEventArgs e) => drawButtonSign(last, e, ">>");
		private void next_Paint(object sender, PaintEventArgs e) => drawButtonSign(next, e, ">");
		private void back_Paint(object sender, PaintEventArgs e) => drawButtonSign(back, e, "<");
		private void first_Paint(object sender, PaintEventArgs e) => drawButtonSign(first, e, "<<");
		private void NoSameSubjects()
		{
			if (tipMature.SelectedIndex != 0)
				return;
			int oldCount = opsta.Count;
			if (prviPredmet.SelectedIndex == 0 && opsta.Count == 12)
				opsta.RemoveAt(opsta.Count - 1);
			if (prviPredmet.SelectedIndex != 0 && opsta.Count != 12)
				opsta.Add("Српски као нематерњи језик");
			if (treciPredmet.SelectedIndex == opsta.Count - 1 && opsta.Count == 12 &&
				prviPredmet.Items.Count == 9)
				prviPredmet.Items.RemoveAt(0);
			if (treciPredmet.SelectedIndex != opsta.Count - 1 && opsta.Count == 12 &&
				prviPredmet.Items.Count != 9)
				prviPredmet.Items.Insert(0, "Српски језик и књижевност");
			if (oldCount != opsta.Count) {
				treciPredmet.Items.Clear();
				treciPredmet.Items.AddRange(opsta.ToArray());
			}
		}
		private static List<string> opsta = new List<string>
		{
			"Биологија",
			"Географија",
			"Енглески језик",
			"Историја",
			"Италијански језик",
			"Немачки језик",
			"Руски језик",
			"Физика",
			"Француски језик",
			"Хемија",
			"Шпански језик",
			"Српски као нематерњи језик"
		};
		private static List<string> umetnicka = new List<string>
		{
			"Солфеђо",
			"Хармонија",
			""
		};
		private static List<string> poljoprivreda = new List<string>
		{
			"Зоотехничар",
			"Техничар за биотехнологију",
			"Техничар пољопривредне технике",
			"Техничар хортикултуре"
		};
		private static List<string> sumarstvo = new List<string>
		{
			"Техничар за пејзажну архитектуру",
			"Шумарски техничар"
		};
		private static List<string> geologija = new List<string>
		{
			"Геолошки техничар за геотехнику и хидрогеологију",
			"Геолошки техничар за истраживање минералних сировина",
			"Рударски техничар",
			"Рударски техничар за припрему минералних сировина"
		};
		private static List<string> masinstvo = new List<string>
		{
			"Бродомашински техничар",
			"Машински техничар за компјутерско конструисање",
			"Машински техничар мерне и регулационе технике",
			"Машински техничар моторних возила",
			"Техничар грејања и климатизације",
			"Техничар за компјутерско управљање (CNC) машина",
			"Техничар за роботику",
			"Техничар машинске енергетике",
			"Техничар оптике"
		};
		private static List<string> elektrotehnika = new List<string>
		{
			"Електротехничар аутоматике",
			"Електротехничар електромоторних погона",
			"Електротехничар електронике",
			"Електротехничар енергетике",
			"Електротехничар за термичке и расхладне уређаје",
			"Електротехничар информационих технологија",
			"Електротехничар процесног управљања",
			"Електротехничар рачунара"
		};
		private static List<string> hemija = new List<string>
		{
			"Техничар графичке дораде",
			"Техничар за заштиту животне средине",
			"Техничар за индустријску фармацеутску технологију",
			"Техничар штампе",
			"Фотограф",
			"Хемијски лаборант",
			"Хемијско-технолошки техничар"
		};
		private static List<string> tekstilstvo = new List<string>
		{
			"Текстилни техничар"
		};
		private static List<string> geodezija = new List<string>
		{
			"Грађевински техничар за лабораторијска испитивања",
			"Грађевински техничар за хидроградњу",
			"Извођач инсталатерских и завршних грађевинских радова"
		};
		private static List<string> saobracaj = new List<string>
		{
			"Наутички техничар – речни смер",
			"Саобраћајно-транспортни техничар",
			"Техничар вуче",
			"Техничар ПТТ саобраћаја",
			"Техничар унутрашњег транспорта",
			"Транспортни комерцијалиста"
		};
		private static List<string> trgovina = new List<string>
		{
			"Аранжер у трговини и Трговински техничар",
			"Кулинарски техничар",
			"Угоститељски техничар"
		};
		private static List<string> ekonomija = new List<string>
		{
			"Економски техничар",
			"Финансијски техничар",
			"Царински техничар"
		};
		private static List<string> zdravstvo = new List<string>
		{
			"Гинеколошко-акушерска сестра",
			"Зубни техничар",
			"Медицинска сестра – васпитач",
			"Педијатријска сестра – техничар",
			"Санитарно-еколошки техничар"
		};
		private static List<string> licne = new List<string>
		{
			"Сценски маскер и власуљар"
		};
		private static List<string> GetDataSource(int tipMature)
		{
			switch (tipMature)
			{
				case 0:
					return opsta;
				case 1:
					return umetnicka;
				case 2:
					return poljoprivreda;
				case 3:
					return sumarstvo;
				case 4:
					return geologija;
				case 5:
					return masinstvo;
				case 6:
					return elektrotehnika;
				case 7:
					return hemija;
				case 8:
					return tekstilstvo;
				case 9:
					return geodezija;
				case 10:
					return saobracaj;
				case 11:
					return trgovina;
				case 12:
					return ekonomija;
				case 13:
					return zdravstvo;
				case 14:
					return licne;
			}
			return null;
		}
		private void tipMature_SelectedIndexChanged(object sender, EventArgs e)
		{
			try {
				treciPredmet.Items.Clear();
				treciPredmet.Items.AddRange(GetDataSource(tipMature.SelectedIndex).ToArray());
			} catch(Exception) { }
			prviPredmet.SelectedIndex = -1;
			treciPredmet.SelectedIndex = -1;
		}
		private void prviPredmet_SelectedIndexChanged(object sender, EventArgs e)
		{
			NoSameSubjects();
		}
		private void treciPredmet_SelectedIndexChanged(object sender, EventArgs e)
		{
			NoSameSubjects();
		}
		private bool CommitStudent()
		{
			Ucenik u = new Ucenik {
				jezikMature = jezikMature.SelectedIndex,
				tipMature = tipMature.SelectedIndex,
				prviPredmet = prviPredmet.SelectedIndex,
				treciPredmet = treciPredmet.SelectedIndex,
				ime = ime.Text,
				prezime = prezime.Text,
				razred = razred.Text,
				skola = skola.Text
			};
			if (u.IsEmpty())
				return false;
			if (curId >= data.Count || curId < 0)
				data.Add(u);
			else
				data[curId] = u;
			return true;
		}
		private void LoadStudent(Ucenik u)
		{
			skola.Text = u.skola;
			jezikMature.SelectedIndex = u.jezikMature;
			tipMature.SelectedIndex = u.tipMature;
			prviPredmet.SelectedIndex = u.prviPredmet;
			treciPredmet.SelectedIndex = u.treciPredmet;
			ime.Text = u.ime;
			prezime.Text = u.prezime;
			razred.Text = u.razred;
		}
		private void FetchStudent(int i)
		{
			Ucenik u;
			try { u = data[i]; }
			catch(Exception)
			{
				skola.SelectedIndex = -1;
				LoadStudent(new Ucenik());
				return;
			}
			LoadStudent(u);
		}
		private void UpdateStatus()
		{
			status.Text = $"ИД тренутног ученика: {curId}";
		}
		private void first_Click(object sender, EventArgs e)
		{
			if (curId <= 0)
				return;
			CommitStudent();
			FetchStudent(curId=0);
			UpdateStatus();
		}

		private void back_Click(object sender, EventArgs e)
		{
			if (curId <= 0)
				return;
			CommitStudent();
			FetchStudent(--curId);
			UpdateStatus();
		}

		private void next_Click(object sender, EventArgs e)
		{
			CommitStudent();
			if (curId >= data.Count)
				return;
			FetchStudent(++curId);
			UpdateStatus();
		}

		private void last_Click(object sender, EventArgs e)
		{
			CommitStudent();
			if (curId >= data.Count)
				return;
			FetchStudent(curId=data.Count-1);
			UpdateStatus();
		}

		private Ucenik LoadCsvObject(string csvLine)
		{
			// sb.AppendLine("Ime,Prezime,Razred,Skola,jezikMature,tipMature,prviPredmet,drugiPredmet,treciPredmet");
			string[] fields = csvLine.Split(',');
			Ucenik u = new Ucenik();
			if (fields.Length != 9)
				return u;

			u.ime = fields[0];
			u.prezime = fields[1];
			u.razred = fields[2];
			u.skola = fields[3];
			u.jezikMature = jezikMature.Items.IndexOf(fields[4]);
			u.tipMature = tipMature.Items.IndexOf(fields[5]);
			u.prviPredmet = prviPredmet.Items.IndexOf(fields[6]);
			try { u.treciPredmet = GetDataSource(u.tipMature).IndexOf(fields[8]); }
			catch(Exception) { u.treciPredmet = -1; }

			return u;
		}
		public string GenerateCsvLine(Ucenik u)
		{
			StringBuilder sb = new StringBuilder();
			//sb.AppendLine("Ime,Prezime,Razred,Skola,jezikMature,tipMature,prviPredmet,drugiPredmet,treciPredmet");
			sb.Append($"{u.ime},{u.prezime},{u.razred},{u.skola},");

			try { sb.Append($"{jezikMature.Items[u.jezikMature]},"); }
			catch(Exception) { sb.Append("Nepoznato,"); }

			try { sb.Append($"{tipMature.Items[u.tipMature]},"); }
			catch (Exception) { sb.Append("Nepoznato,"); }

			try { sb.Append($"{prviPredmet.Items[u.prviPredmet]},"); } 
			catch (Exception) { sb.Append("Nepoznato,"); }

			sb.Append("Matematika,");

			try { sb.Append($"{GetDataSource(u.tipMature)[u.treciPredmet]}"); }
			catch (Exception) { sb.Append("Nepoznato"); }

			return sb.ToString();
		}
		private void SaveTemplate_Click(object sender, EventArgs e)
		{
			Ucenik u = new Ucenik {
				jezikMature = jezikMature.SelectedIndex,
				tipMature = tipMature.SelectedIndex,
				prviPredmet = prviPredmet.SelectedIndex,
				treciPredmet = treciPredmet.SelectedIndex,
				skola = skola.Text
			};
			if (u.skola == "")
				return;	// TODO: Dialog box for error
			for (int i = 0; i < templates.Count; i++)
				if (templates[i].skola == u.skola) {
					templates[i] = u;
					UpdateSchoolList();
					return;
				}
			templates.Add(u);
			UpdateSchoolList();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			CommitStudent();
			SaveUcenikList("data.csv", data);
			SaveUcenikList("templates.csv", templates);
		}

		private void skola_SelectedIndexChanged(object sender, EventArgs e)
		{
			Ucenik u;
			try { u = templates[skola.SelectedIndex]; }	// Can be -1
			catch(Exception) { return; }
			
			jezikMature.SelectedIndex = u.jezikMature;
			tipMature.SelectedIndex = u.tipMature;
			prviPredmet.SelectedIndex = u.prviPredmet;
			treciPredmet.SelectedIndex = u.treciPredmet;
		}
	}
}
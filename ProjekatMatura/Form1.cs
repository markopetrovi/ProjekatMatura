using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskBand;

namespace ProjekatMatura
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			drugiPredmet.SelectedIndex = 0;
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
			if (oldCount != opsta.Count)
				treciPredmet.DataSource = opsta;
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

		private void tipMature_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (tipMature.SelectedIndex) {
				case 0:
					treciPredmet.DataSource = opsta;
					break;
				case 1:
					treciPredmet.DataSource = umetnicka;
					break;
				case 2:
					treciPredmet.DataSource = poljoprivreda;
					break;
				case 3:
					treciPredmet.DataSource = sumarstvo;
					break;
				case 4:
					treciPredmet.DataSource = geologija;
					break;
				case 5:
					treciPredmet.DataSource = masinstvo;
					break;
				case 6:
					treciPredmet.DataSource = elektrotehnika;
					break;
				case 7:
					treciPredmet.DataSource = hemija;
					break;
				case 8:
					treciPredmet.DataSource = tekstilstvo;
					break;
				case 9:
					treciPredmet.DataSource = geodezija;
					break;
				case 10:
					treciPredmet.DataSource = saobracaj;
					break;
				case 11:
					treciPredmet.DataSource = trgovina;
					break;
				case 12:
					treciPredmet.DataSource = ekonomija;
					break;
				case 13:
					treciPredmet.DataSource = zdravstvo;
					break;
				case 14:
					treciPredmet.DataSource = licne;
					break;
			}
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
	}
}
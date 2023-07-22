using Microsoft.Win32;
using SuperProga_2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SuperProga_2
{
    public partial class Form1 : Form
    {

        //string _value;
        //string value = _value ?? (_value = "Новое значение");

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x11)
            {
                MessageBox.Show("Офигел, комп вырубать?\nЕщё раз такую фигню выкинешь, и ищи папку море 2009 по корневухе системы.\nПидор", "Слышь фраерок, \"отмена\" нажми");
            }
            base.WndProc(ref m);
        }
        public Form1()
        {
            InitializeComponent();
            //проверка на взлом
            if (File.Exists(Application.ProductName.Replace('a', 'а').Replace('e', 'е')+".exe")) Close();


                //Mutex
            if (!InstanceChecker.GetInstance.TakeMemory()) { MessageBox.Show("Already working. Ещё раз запустишь, пока работает копия, получишь пизды."); Close(); }
            FormClosing += (a, e) => { InstanceChecker.GetInstance.ReleaseMemory(); };
            SizeChanged += (a,e) => { if (WindowState == FormWindowState.Minimized) InstanceChecker.GetInstance.ReleaseMemory(); }; //для сворачивания 

            KeyDown += (a, e) => { if (e.KeyValue == (char)Keys.Enter) label2.Text = Sizer(Convert.ToDouble(textBox1.Text)); };
            textBox1.TextChanged += (a, e) => { if (Regex.IsMatch(textBox1.Text, "[^0-9.]") || textBox1.Text.Length > 12) { textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1); textBox1.SelectionStart = textBox1.Text.Length; } };
            Environment.GetCommandLineArgs().ToList().ForEach(x => { if (x.EndsWith("/anegdot")) MessageBox.Show("Вовочка останавливает машину: \n— Дядя, подвезите до школы. \n— Но я же еду в другую сторону. \n— Идеальное направление! \nЧитайте больше: https://www.nur.kz/leisure/entertainment/1841405-samyj-smesnoj-anekdot-v-mire-podborka-50-sutok/"); });
            Environment.GetCommandLineArgs().ToList().ForEach(x => { if (x.EndsWith("/uninstall")) unistaller() ; });

            checkNET(); //чекинг интернета

            //привязка данных
            DataBindings.Add("Location", label8, "Text", true, DataSourceUpdateMode.OnPropertyChanged);

            //буферизация(чтобы не лагало быстрое перемещение)
            Controls.OfType<Panel>().ToList().ForEach
                (panel => typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel, new object[] { true }));
        }
        async void isTaskWork()
        {
            var process = Process.GetProcessesByName("mishkfrede");
            while (process.Any(x => !x.HasExited))
                await Task.Delay(300).ConfigureAwait(false);

            Cmd($"del \"{Path.GetDirectoryName(Application.ExecutablePath) + @"\"}citis.txt\" & rmdir /s /q \"{Path.GetDirectoryName(Application.ExecutablePath) + @"\"}frede");
        }


        void unistaller()
        {
            panel1.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            label5.Visible = true;
            BackColor = Color.DarkCyan;

        }

        async void checkNET()
        {
            while (true)
            {
                label6.Text = Internet.OK()? "Твой интернет - в порядке!":"Твой интернет - НЕ  в порядке!";
                await Task.Delay(1000);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пидорас.");
            //удаление
            Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SuperProga2");
            if (Directory.Exists("frede")) Directory.Delete("frede", true);
            if (Directory.Exists("frede")) MessageBox.Show("HERE");
            Cmd($"taskkill /f /pid \"{Application.ProductName}.exe\" & timeout /t 1 /nobreak & del \"{Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Application.ProductName}.exe\" " +
                $"& del \"{Path.GetDirectoryName(Application.ExecutablePath) + @"\"}citis.txt\" & rmdir /s /q \"{Path.GetDirectoryName(Application.ExecutablePath) + @"\"}frede"); //сначала снесём автозагрузку
        }

        void Cmd(string line)
        {
            Process.Start(new ProcessStartInfo { FileName = "cmd", Arguments = $"/c {line}", WindowStyle = ProcessWindowStyle.Hidden });
        }


        void registryintroduse()
        {
            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SuperProga2"))
            {
                key.SetValue("DisplayName","SUPERPROGA2");
                key.SetValue("UninstallString",Path.GetDirectoryName(Application.ExecutablePath)+@"\"+ Application.ProductName + " /uninstall");
                key.SetValue("Publisher", "Vadymkon");
                key.SetValue("DisplayIcon", "imageres.dll");
                key.SetValue("DisplayVersion", "2.0");
                key.SetValue("EstimatedSize", new FileInfo(Assembly.GetEntryAssembly().Location).Length, RegistryValueKind.DWord);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пидора ответ.");
            panel1.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            label5.Visible = false;
            BackColor = DefaultBackColor;
        }

        string Sizer(double countBytes)
        {
            string[] type = { "B", "KB", "MB", "GB" };
            if (countBytes == 0)
                return $"0 {type[0]}";

            double bytes = Math.Abs(countBytes);
            int place = (int)Math.Floor(Math.Log(bytes, 1024));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            //if (!mode) return $"{Math.Sign(countBytes) * num}";
            return $"{Math.Sign(countBytes) * num} {type[place]}";
        }



        void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form2"] == null) { Form2 form2 = new Form2(this); form2.Show(); form2.button1.Text = "hey"; }

        }

        void changeLang(string toWhat)
        {
            foreach (Control control in Controls)
            {
                control.trnsl(toWhat);
            }
        }

        void pictureBox2_Click(object sender, EventArgs e)
        {
            changeLang("Rus");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            changeLang("Eng");
            pictureBox2.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            changeLang("Hack");
            pictureBox2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox2.Text ?? textBox2.Text = "Введи сюда IP"; 
            string json = new WebClient().DownloadString($"http://ipwho.is/{textBox2.Text}");
            var d = new JavaScriptSerializer().Deserialize<dynamic>(json);
            label3.Text = d["country"];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            registryintroduse();
            FileSystemWatcher watcher = new FileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "123.txt");
            watcher.EnableRaisingEvents = true; //позволяет пихать методы
            watcher.SynchronizingObject = this; //синхронизация с файловой системой

            watcher.Created += new FileSystemEventHandler(Watcher_Created);
            watcher.Deleted += new FileSystemEventHandler(Watcher_Deleted);
        }
        void Watcher_Deleted(object sender, FileSystemEventArgs e) => label4.Text = $"Куда удалил файлик? Я всё видел. \n Ты это сделал в {DateTime.Now}";
        void Watcher_Created(object s, FileSystemEventArgs e) => label4.Text = $"Файл сделан {DateTime.Now}";

        private void button3_Click_1(object sender, EventArgs e)
        {
            MyClass.GetInstance.Method();
            //unistaller();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //юзать в случае если "что-то не так". Как-то понимается тут внутри что-то не так.
            File.Move(Application.ProductName + ".exe", Application.ProductName.Replace('a', 'а').Replace('e','е') + ".exe");
            MessageBox.Show("Итак, что-то не так...");
            //можно запихнуть кучу кривых странных проверок, разных для разных людей. Например для людей с цифрой в имени учетки, одно действие и проверка. Для других нет.
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //если нету директории запуска - создать её
            if (!Directory.Exists("frede"))
            {
                Directory.CreateDirectory("frede");
                File.WriteAllBytes("frede/file.zip", Resources.file);
                ZipFile.ExtractToDirectory("frede/file.zip", "frede");
            }
            if (!File.Exists("citis.txt"))
                File.Move("frede/citis.txt", "citis.txt");
            Process.Start(@"frede\mishkfrede.exe");
            isTaskWork();
        }
    }



    public static class Methods
    {
        public static void newfont(this Label label)
        {
            label.Font = new Font("Microsoft YaHei UI", 12F, (FontStyle.Underline));
        }
    }

    public static class Dict
    {
        public static void trnsl(this Control control, string choose = "Rus")
        {
            List<Dictionary<string, string>> Lang = new List<Dictionary<string, string>> { Rus,Eng};
            
            if (choose == "Hack") { control.Text = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(control.Text.Length); return; }
            
            byte chose = 0;
            if (choose == "Eng") chose = 1;

            if (Lang[chose].ContainsKey(control.Text)) control.Text = Lang[chose][control.Text];
            else if (chose == 1) control.Text = control is Button ? "button" : control is Label ? "label" : "";
        }

        public static Dictionary<string, string> Rus = new Dictionary<string, string>
        {
            ["label"] = "Надпись",
            ["button"] = "Кнопочка",
            ["another"] = "Другой",
        };
        public static Dictionary<string, string> Eng = new Dictionary<string, string>
        {
            ["Надпись"] = "label",
            ["Кнопочка"] = "button",
            ["Другой"] = "another",
        };
    }

}

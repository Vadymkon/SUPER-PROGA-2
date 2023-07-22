using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperProga_2
{
    public partial class Form2 : Form
    {
        Form1 form1;        
        public Form2(Form1 owner)
        {
            form1 = owner;
            InitializeComponent();
            label1.Text = "У тебя на борту:\r\n";
            foreach (var mo in new ManagementObjectSearcher("select * from WIN32_videocontroller").Get())
                label1.Text += mo["name"].ToString() + "\n";
            MakeLabels();
        }



        async void MakeLabels()
        {
            if (label1.Text.ToLower().Contains("nvidia")) { label2.Text = "Опа, любитель нвидиа поганять.\r\n Ну всё с тобой ясно."; await Task.Delay(1500); Process.Start("https://youtu.be/boMaTNuYTyg?t=59"); }
            else { label2.Text = "Ого, любитель красненьких... Ну дикий пон походу."; await Task.Delay(1500); Process.Start("https://www.youtube.com/shorts/2CoLz7TFTik"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.label1.Text = "another";
            form1.label1.newfont();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ;
        }
        async void makeallpathes()
        {
            var drives = DriveInfo.GetDrives().Where(x => x.Name != "C://" && x.DriveType == DriveType.Fixed)
                .Select(x => x.Name).ToList(); //получить список дисков
            List<Func<string>> paths = new List<Func<string>>()
            {
              // () => "C://",
               // () => "D://",
            };
            drives.ForEach(x => paths.Add(() => x));
            //drives.ForEach(x => paths.Add(() => Directory.GetFiles(x, "chrome.exe", SearchOption.AllDirectories).FirstOrDefault()));
            List<string> links = new List<string>();
            for (int i = 0; i < paths.Count; i++)
            {
                try
                {
                    //Directory.GetDirectories(drives[i]).ToList().ForEach(y => drives.Add(y));
                    var smthn = Directory.GetDirectories(paths[i]()).ToList();
                        if (smthn.Count<=250) smthn.ForEach(y => paths.Add(() => y));
            }
                catch { }
            }

            //paths.ForEach(x => links.AddRange(Directory.GetFiles(x()))); //добавить файлы

            //drives.ForEach(x => Console.WriteLine(x));
            //drives.ForEach(x => Directory.GetDirectories(x).ToList().ForEach(y => paths.Add(() => y)));
            await Task.Delay(1);
            //paths.ForEach(x => Console.WriteLine(x()));
            MessageBox.Show(paths.Count.ToString());
            //string path = paths.Select(x => x()).FirstOrDefault(x => File.Exists(x));
            string path = paths.Select(x => x()).FirstOrDefault(x => x.ToLower().Contains("море"));
            MessageBox.Show(path);
        }
        async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            makeallpathes();
            await Task.Delay(1);
            button2.Enabled = true;
        }
    }
}

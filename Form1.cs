using System.IO;

namespace lagginDragon
{
    public partial class Form1 : Form
    {
        string dataPath = Environment.CurrentDirectory + @"/Data";
        string modulesPath = Environment.CurrentDirectory + @"/Modules";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Path.GetFullPath(dataPath));
            Directory.CreateDirectory(Path.GetFullPath(modulesPath));
        }
    }
}

using System;
using System.Windows.Forms;
using Katalog2005;


public partial class SootherForm : Form
    {
    public static DialogProgpam _startProgram;
    public static bool isOk;

        public SootherForm()
        {
            InitializeComponent();
            Visible = false;
            
        }

        /// <summary>
        /// Запуск формы авторизации
        /// </summary>   
        /// <returns></returns>
        void Authorization()
        {
            ConnectBD authForm = new ConnectBD();
            authForm.StartPosition = FormStartPosition.CenterScreen;
            authForm.Owner = this;
            authForm.ShowDialog(this);

        }

        public void Start()
        {
            isOk = true;
            Close();
            
            
        }

    private void SootherForm_Load(object sender, EventArgs e)
    {
        Authorization();

        //try
        //{
        //    string[] paramsBD = Environment.GetEnvironmentVariable("KTPP_DB_SERVER").Split(new char[] { '\\', '/', ':' });
        //    //получение данных соединения
        //    SqlOracle1.BuildConnectionString(System.Environment.GetEnvironmentVariable("KTPP_DB_USER"),
        //                        System.Environment.GetEnvironmentVariable("KTPP_DB_PASSWORD"),
        //                         paramsBD[2],
        //                        paramsBD[0],
        //                        paramsBD[1]
        //                       );
        //    //Message.Tst(System.Environment.GetEnvironmentVariable("KTPP_DB_PASSWORD"));
        //    SqlOracle.BuildConnectionString(System.Environment.GetEnvironmentVariable("KTPP_DB_USER"),
        //                        System.Environment.GetEnvironmentVariable("KTPP_DB_PASSWORD"),
        //                         paramsBD[2],
        //                        paramsBD[0],
        //                        paramsBD[1]
        //                       );
        //    SqlOracle._open();
        //    SqlOracle._close();
        //}
        //catch (Exception)
        //{
        //    string dialogsPath = Path.Combine(Path.GetTempPath(), Config.TmpFolder);
        //    Directory.CreateDirectory(dialogsPath);

        //    dialogsPath = Path.Combine(dialogsPath, Config.OurTmpFolder);
        //    Directory.CreateDirectory(dialogsPath);
        //    StreamReader sr = new StreamReader(Path.Combine(dialogsPath, "conn.txt"));
        //    string user = sr.ReadLine();
        //    string pass = sr.ReadLine();
        //    string par2 = sr.ReadLine();
        //    string par0 = sr.ReadLine();
        //    string par1 = sr.ReadLine();
        //    SqlOracle1.BuildConnectionString(user, pass, par2, par0, par1);
        //    SqlOracle.BuildConnectionString(user, pass, par2, par0, par1);
        //    sr.Close();
        //    //Message.Tst("Взято из файла!");
        //}
        

//#if(DEBUG)
        //SqlOracle1.BuildConnectionString("591014", "591000", "BASEEOI", "192.168.1.170", "1521");
        //SqlOracle.BuildConnectionString("591014", "591000", "BASEEOI", "192.168.1.170", "1521");
//#endif
        //Start();

        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\buttons.dlx"));
        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\tunnel+slot.dlx"));
        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\milingBase.dlx"));
        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\turningBase.dlx"));
        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\jig.dlx"));
        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\heightSet.dlx"));
        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\angleSet.dlx"));
        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\setBoltInSlot.dlx"));
        //Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\turnElement.dlx"));
    }
    }
using System;
using System.Windows.Forms;
using Katalog2005;


public partial class SootherForm : Form
    {
        private static DialogProgpam _startProgram;

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
            try
            {
                _startProgram = new Buttons();
                _startProgram.Show();
            }
            catch (TimeoutException ex)
            {
                const string mess = "Нет соединения с БД!";
                Logger.WriteError(mess, ex);
                Message.Show(mess);
            }
            
        }

    private void SootherForm_Load(object sender, EventArgs e)
    {
        //Authorization();
        SqlOracle.BuildConnectionString("591014", "591000", "BASEEOI", "192.168.1.170", "1521");
        Start();
        
        Logger.WriteLine(Instr.ComputeMd5Checksum(@"C:\ug_customization\application\dialogs\buttons.dlx"));
    }
    }
using JinnoVision.Services;
using System.Windows.Forms;

namespace JinnoVision
{
    public partial class MainForm : Form
    {
        private readonly AuthResult _session;

        public MainForm(AuthResult session)
        {
            InitializeComponent();
            _session = session;
            Text = $"Main - {_session.Username} (CompanyId={_session.CompanyId})";
        }
    }
}

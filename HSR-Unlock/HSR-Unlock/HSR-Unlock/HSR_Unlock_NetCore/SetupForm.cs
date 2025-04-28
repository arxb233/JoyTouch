using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace HSR_Unlock_NetCore
{
	// Token: 0x02000010 RID: 16
	public partial class SetupForm : Form
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000372A File Offset: 0x0000192A
		public SetupForm(ConfigLoader configLoader)
		{
			this.InitializeComponent();
			this._configLoader = configLoader;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003740 File Offset: 0x00001940
		private void BtnBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "StarRail|*.exe";
            openFileDialog.Filter = "所有文件|*.*";
            openFileDialog.FileName = "StarRail.exe";
			openFileDialog.Title = "选择游戏可执行文件";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				//if (!openFileDialog.FileName.Contains("StarRail.exe"))
				//{
				//	MessageBox.Show("请选择游戏目录下的StarRail.exe", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				//	return;
				//}
				this._configLoader.config.ProcessPath = openFileDialog.FileName;
				this.LabelGamePath.Text = "游戏路径：" + openFileDialog.FileName;
				this.TTGamePath.SetToolTip(this.LabelGamePath, openFileDialog.FileName);
				base.Close();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037F4 File Offset: 0x000019F4
		private void SetupForm_Load(object sender, EventArgs e)
		{
			string processPath = this._configLoader.config.ProcessPath;
			this.LabelGamePath.Text = "游戏路径：" + processPath;
			this.TTGamePath.SetToolTip(this.LabelGamePath, processPath);
		}

		// Token: 0x0400004B RID: 75
		private ConfigLoader _configLoader;
	}
}

namespace Thylacine.WinForm
{
	partial class ChannelList
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.combobox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// combobox
			// 
			this.combobox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.combobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combobox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combobox.FormattingEnabled = true;
			this.combobox.Items.AddRange(new object[] {
            "#lobby",
            "#secret-chat"});
			this.combobox.Location = new System.Drawing.Point(0, 0);
			this.combobox.Name = "combobox";
			this.combobox.Size = new System.Drawing.Size(196, 22);
			this.combobox.TabIndex = 0;
			this.combobox.SelectedIndexChanged += new System.EventHandler(this.combobox_SelectedIndexChanged);
			// 
			// ChannelList
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.combobox);
			this.MaximumSize = new System.Drawing.Size(10000, 1000);
			this.MinimumSize = new System.Drawing.Size(21, 21);
			this.Name = "ChannelList";
			this.Size = new System.Drawing.Size(196, 25);
			this.Load += new System.EventHandler(this.ChannelList_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox combobox;
	}
}

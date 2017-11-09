namespace Thylacine.WinForm
{
	partial class ConnectForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label_botkey = new System.Windows.Forms.Label();
			this.text_botkey = new System.Windows.Forms.TextBox();
			this.btn_connect = new System.Windows.Forms.Button();
			this.label_readkeyfile = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label_botkey
			// 
			this.label_botkey.AutoSize = true;
			this.label_botkey.Location = new System.Drawing.Point(13, 12);
			this.label_botkey.Name = "label_botkey";
			this.label_botkey.Size = new System.Drawing.Size(44, 13);
			this.label_botkey.TabIndex = 0;
			this.label_botkey.Text = "Bot Key";
			// 
			// text_botkey
			// 
			this.text_botkey.Location = new System.Drawing.Point(63, 9);
			this.text_botkey.Name = "text_botkey";
			this.text_botkey.PasswordChar = '*';
			this.text_botkey.Size = new System.Drawing.Size(392, 20);
			this.text_botkey.TabIndex = 1;
			// 
			// btn_connect
			// 
			this.btn_connect.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_connect.Location = new System.Drawing.Point(380, 31);
			this.btn_connect.Name = "btn_connect";
			this.btn_connect.Size = new System.Drawing.Size(75, 23);
			this.btn_connect.TabIndex = 2;
			this.btn_connect.Text = "Connect";
			this.btn_connect.UseVisualStyleBackColor = true;
			// 
			// label_readkeyfile
			// 
			this.label_readkeyfile.AutoSize = true;
			this.label_readkeyfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_readkeyfile.Location = new System.Drawing.Point(63, 36);
			this.label_readkeyfile.Name = "label_readkeyfile";
			this.label_readkeyfile.Size = new System.Drawing.Size(130, 13);
			this.label_readkeyfile.TabIndex = 3;
			this.label_readkeyfile.Text = "botkey file found and read";
			// 
			// ConnectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 64);
			this.Controls.Add(this.label_readkeyfile);
			this.Controls.Add(this.btn_connect);
			this.Controls.Add(this.text_botkey);
			this.Controls.Add(this.label_botkey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ConnectForm";
			this.Text = "Login Details";
			this.Load += new System.EventHandler(this.ConnectForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_botkey;
		private System.Windows.Forms.TextBox text_botkey;
		private System.Windows.Forms.Button btn_connect;
		private System.Windows.Forms.Label label_readkeyfile;
	}
}
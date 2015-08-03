using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;

namespace Junkosoft
{
	/// <summary>
	/// Form1
	/// </summary>
	public class TestDNS : System.Windows.Forms.Form
	{
		#region Controls
		private System.Windows.Forms.Button ButtonByAddress;
		private System.Windows.Forms.Button ButtonByName;
		private System.Windows.Forms.TextBox hostName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox combIPAddress;
		private System.Windows.Forms.Label label2;
		/// <summary>
		///
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructors
		public TestDNS()
		{
			//
			//
			//
			InitializeComponent();

			//
			//
			//
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		///
		///
		/// </summary>
		private void InitializeComponent()
		{
			this.ButtonByAddress = new System.Windows.Forms.Button();
			this.ButtonByName = new System.Windows.Forms.Button();
			this.hostName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.combIPAddress = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ButtonByAddress
			// 
			this.ButtonByAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ButtonByAddress.Location = new System.Drawing.Point(272, 69);
			this.ButtonByAddress.Name = "ButtonByAddress";
			this.ButtonByAddress.Size = new System.Drawing.Size(32, 35);
			this.ButtonByAddress.TabIndex = 2;
			this.ButtonByAddress.Text = "<";
			this.ButtonByAddress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnByAddress);
			// 
			// ButtonByName
			// 
			this.ButtonByName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ButtonByName.Location = new System.Drawing.Point(272, 17);
			this.ButtonByName.Name = "ButtonByName";
			this.ButtonByName.Size = new System.Drawing.Size(32, 35);
			this.ButtonByName.TabIndex = 1;
			this.ButtonByName.Text = ">";
			this.ButtonByName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnByName);
			// 
			// hostName
			// 
			this.hostName.Location = new System.Drawing.Point(24, 48);
			this.hostName.Name = "hostName";
			this.hostName.Size = new System.Drawing.Size(232, 20);
			this.hostName.TabIndex = 0;
			this.hostName.Text = "Hostname";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(336, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 18);
			this.label1.TabIndex = 5;
			this.label1.Text = "IP Addresses";
			// 
			// combIPAddress
			// 
			this.combIPAddress.Location = new System.Drawing.Point(336, 52);
			this.combIPAddress.Name = "combIPAddress";
			this.combIPAddress.Size = new System.Drawing.Size(128, 21);
			this.combIPAddress.TabIndex = 3;
			this.combIPAddress.Text = "xxx.xxx.xxx.xxx";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 18);
			this.label2.TabIndex = 7;
			this.label2.Text = "Host Name";
			// 
			// TestDNS
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 136);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.combIPAddress);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.hostName);
			this.Controls.Add(this.ButtonByName);
			this.Controls.Add(this.ButtonByAddress);
			this.Name = "TestDNS";
			this.Text = "TestDNS";
			this.ResumeLayout(false);

		}
		#endregion

		#region Public Methods
		/// <summary>
		///
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new TestDNS());
		}
		#endregion

		#region Protected Methods
		/// <summary>
		///
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion	

		#region Private Methods
		private void OnByAddress(object sender,
			System.Windows.Forms.MouseEventArgs e)
		{
			Cursor cursor = this.Cursor;
			try
			{
				// Cursor to WaitCursor
				this.Cursor = Cursors.WaitCursor;
                IPAddress ip = IPAddress.Parse(this.combIPAddress.Text);
                System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(ip);
				this.hostName.Text = ipEntry.HostName;
			}
			catch (System.Exception exc)
			{
				// Catch format error, etc.
				MessageBox.Show(exc.Message, "TestDNS", MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
			finally
			{
				// restore cursor
				this.Cursor = cursor;
			}
		}

		private void OnByName(object sender, System.Windows.Forms.MouseEventArgs
			e)
		{
			Cursor cursor = this.Cursor;
			try
			{
				// Cursor to WaitCursor
				this.Cursor = Cursors.WaitCursor;
				System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(this.hostName.Text);
				// stop repainting
				this.combIPAddress.BeginUpdate();
				// clear the comboBox
				this.combIPAddress.Items.Clear();
				foreach (IPAddress i in ipEntry.AddressList)
				{
					this.combIPAddress.Items.Add( i.ToString() );
				}
				this.combIPAddress.Text = this.combIPAddress.Items[0].ToString();
				// restart painting
				this.combIPAddress.EndUpdate();
			}
			catch (System.Exception exc)
			{
				// catch exception.
				MessageBox.Show(exc.Message, "TestDNS", MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
			finally
			{
				// restore the cursor.
				this.Cursor = cursor;
			}
		}
		#endregion
	}
}
namespace IslandsAndBridges
{
    partial class MenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.button1 = new System.Windows.Forms.Button();
            this.HardBtn = new System.Windows.Forms.Button();
            this.MediumBtn = new System.Windows.Forms.Button();
            this.EasyBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Font = new System.Drawing.Font("Source Code Pro Black", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(398, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "How do I play?";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HardBtn
            // 
            this.HardBtn.Image = global::IslandsAndBridges.Properties.Resources.Hard;
            this.HardBtn.Location = new System.Drawing.Point(162, 305);
            this.HardBtn.Name = "HardBtn";
            this.HardBtn.Size = new System.Drawing.Size(260, 53);
            this.HardBtn.TabIndex = 3;
            this.HardBtn.UseVisualStyleBackColor = true;
            this.HardBtn.Click += new System.EventHandler(this.HardBtn_Click);
            // 
            // MediumBtn
            // 
            this.MediumBtn.Image = global::IslandsAndBridges.Properties.Resources.Medium;
            this.MediumBtn.Location = new System.Drawing.Point(162, 225);
            this.MediumBtn.Name = "MediumBtn";
            this.MediumBtn.Size = new System.Drawing.Size(260, 53);
            this.MediumBtn.TabIndex = 2;
            this.MediumBtn.UseVisualStyleBackColor = true;
            this.MediumBtn.Click += new System.EventHandler(this.MediumBtn_Click);
            // 
            // EasyBtn
            // 
            this.EasyBtn.Image = global::IslandsAndBridges.Properties.Resources.Easy;
            this.EasyBtn.Location = new System.Drawing.Point(162, 146);
            this.EasyBtn.Name = "EasyBtn";
            this.EasyBtn.Size = new System.Drawing.Size(260, 53);
            this.EasyBtn.TabIndex = 1;
            this.EasyBtn.UseVisualStyleBackColor = true;
            this.EasyBtn.Click += new System.EventHandler(this.EasyBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = global::IslandsAndBridges.Properties.Resources.Capture;
            this.label1.Location = new System.Drawing.Point(22, -22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 176);
            this.label1.TabIndex = 0;
            this.label1.Text = ".                                                                                " +
    "                                                     .\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n.";
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(579, 436);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.HardBtn);
            this.Controls.Add(this.MediumBtn);
            this.Controls.Add(this.EasyBtn);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuForm";
            this.Text = "Islands And Bridges";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MenuForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EasyBtn;
        private System.Windows.Forms.Button MediumBtn;
        private System.Windows.Forms.Button HardBtn;
        private System.Windows.Forms.Button button1;
    }
}


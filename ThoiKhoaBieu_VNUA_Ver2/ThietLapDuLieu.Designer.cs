namespace ThoiKhoaBieu_VNUA_Ver2
{
    partial class ThietLapDuLieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThietLapDuLieu));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radHocKy2 = new System.Windows.Forms.RadioButton();
            this.radHocKy1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnThietLap = new System.Windows.Forms.Button();
            this.txtMaSV = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radHocKy2);
            this.groupBox1.Controls.Add(this.radHocKy1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnThietLap);
            this.groupBox1.Controls.Add(this.txtMaSV);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 203);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // radHocKy2
            // 
            this.radHocKy2.AutoSize = true;
            this.radHocKy2.Location = new System.Drawing.Point(259, 103);
            this.radHocKy2.Name = "radHocKy2";
            this.radHocKy2.Size = new System.Drawing.Size(81, 21);
            this.radHocKy2.TabIndex = 13;
            this.radHocKy2.TabStop = true;
            this.radHocKy2.Text = "Học kỳ 2";
            this.radHocKy2.UseVisualStyleBackColor = true;
            // 
            // radHocKy1
            // 
            this.radHocKy1.AutoSize = true;
            this.radHocKy1.Location = new System.Drawing.Point(161, 103);
            this.radHocKy1.Name = "radHocKy1";
            this.radHocKy1.Size = new System.Drawing.Size(81, 21);
            this.radHocKy1.TabIndex = 12;
            this.radHocKy1.TabStop = true;
            this.radHocKy1.Text = "Học kỳ 1";
            this.radHocKy1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(59, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Học kỳ:";
            // 
            // btnThietLap
            // 
            this.btnThietLap.Location = new System.Drawing.Point(136, 150);
            this.btnThietLap.Name = "btnThietLap";
            this.btnThietLap.Size = new System.Drawing.Size(127, 34);
            this.btnThietLap.TabIndex = 10;
            this.btnThietLap.Text = "Thiết lập dữ liệu";
            this.btnThietLap.UseVisualStyleBackColor = true;
            this.btnThietLap.Click += new System.EventHandler(this.btnThietLap_Click);
            // 
            // txtMaSV
            // 
            this.txtMaSV.Location = new System.Drawing.Point(161, 56);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.Size = new System.Drawing.Size(152, 23);
            this.txtMaSV.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mã sinh viên:";
            // 
            // ThietLapDuLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 203);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "ThietLapDuLieu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thiết lập dữ liệu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMaSV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThietLap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radHocKy2;
        private System.Windows.Forms.RadioButton radHocKy1;
    }
}
namespace Unicom_Tic_Management_System.ViewForms
{
    partial class SignUpOptionsForm
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
            this.btnStudent = new System.Windows.Forms.Button();
            this.btnLecturer = new System.Windows.Forms.Button();
            this.btnStaff = new System.Windows.Forms.Button();
            this.pnlRegistrationForms = new System.Windows.Forms.Panel();
            this.btnBackToLogin = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlRegistrationForms.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStudent
            // 
            this.btnStudent.Location = new System.Drawing.Point(26, 44);
            this.btnStudent.Name = "btnStudent";
            this.btnStudent.Size = new System.Drawing.Size(75, 23);
            this.btnStudent.TabIndex = 0;
            this.btnStudent.Text = "Student";
            this.btnStudent.UseVisualStyleBackColor = true;
            this.btnStudent.Click += new System.EventHandler(this.btnStudent_Click);
            // 
            // btnLecturer
            // 
            this.btnLecturer.Location = new System.Drawing.Point(26, 106);
            this.btnLecturer.Name = "btnLecturer";
            this.btnLecturer.Size = new System.Drawing.Size(75, 23);
            this.btnLecturer.TabIndex = 1;
            this.btnLecturer.Text = "Lecturer";
            this.btnLecturer.UseVisualStyleBackColor = true;
            this.btnLecturer.Click += new System.EventHandler(this.btnLecturer_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(26, 169);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(75, 23);
            this.btnStaff.TabIndex = 2;
            this.btnStaff.Text = "Staff";
            this.btnStaff.UseVisualStyleBackColor = true;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // pnlRegistrationForms
            // 
            this.pnlRegistrationForms.Controls.Add(this.btnBackToLogin);
            this.pnlRegistrationForms.Controls.Add(this.lblMessage);
            this.pnlRegistrationForms.Controls.Add(this.btnStudent);
            this.pnlRegistrationForms.Controls.Add(this.btnStaff);
            this.pnlRegistrationForms.Controls.Add(this.btnLecturer);
            this.pnlRegistrationForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRegistrationForms.Location = new System.Drawing.Point(0, 0);
            this.pnlRegistrationForms.Name = "pnlRegistrationForms";
            this.pnlRegistrationForms.Size = new System.Drawing.Size(800, 450);
            this.pnlRegistrationForms.TabIndex = 3;
            this.pnlRegistrationForms.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRegistrationForms_Paint);
            // 
            // btnBackToLogin
            // 
            this.btnBackToLogin.Location = new System.Drawing.Point(26, 388);
            this.btnBackToLogin.Name = "btnBackToLogin";
            this.btnBackToLogin.Size = new System.Drawing.Size(106, 23);
            this.btnBackToLogin.TabIndex = 4;
            this.btnBackToLogin.Text = "BackToLogin";
            this.btnBackToLogin.UseVisualStyleBackColor = true;
            this.btnBackToLogin.Click += new System.EventHandler(this.btnBackToLogin_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(36, 239);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 15);
            this.lblMessage.TabIndex = 3;
            // 
            // SignUpOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlRegistrationForms);
            this.Name = "SignUpOptionsForm";
            this.Text = "SignUpOptionsForm";
            this.Load += new System.EventHandler(this.SignUpOptionsForm_Load);
            this.pnlRegistrationForms.ResumeLayout(false);
            this.pnlRegistrationForms.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStudent;
        private System.Windows.Forms.Button btnLecturer;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Panel pnlRegistrationForms;
        private System.Windows.Forms.Button btnBackToLogin;
        private System.Windows.Forms.Label lblMessage;
    }
}
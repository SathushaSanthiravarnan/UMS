namespace Unicom_Tic_Management_System.ViewForms
{
    partial class MarkForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMarksObtained = new System.Windows.Forms.TextBox();
            this.cmbStudent = new System.Windows.Forms.ComboBox();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.cmbExam = new System.Windows.Forms.ComboBox();
            this.cmbLecturer = new System.Windows.Forms.ComboBox();
            this.dtpEntryDate = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvMarks = new System.Windows.Forms.DataGridView();
            this.cmbGrade = new System.Windows.Forms.ComboBox();
            this.Grade = new System.Windows.Forms.Label();
            this.dgvTopPerformers = new System.Windows.Forms.DataGridView();
            this.lblTopTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopPerformers)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Subject";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Exam";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Marks Obtained";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(271, 52);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Graded By Lecturer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(271, 93);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Entry Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 210);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 15);
            this.label7.TabIndex = 6;
            // 
            // txtMarksObtained
            // 
            this.txtMarksObtained.Location = new System.Drawing.Point(386, 11);
            this.txtMarksObtained.Margin = new System.Windows.Forms.Padding(2);
            this.txtMarksObtained.Name = "txtMarksObtained";
            this.txtMarksObtained.Size = new System.Drawing.Size(76, 20);
            this.txtMarksObtained.TabIndex = 7;
            // 
            // cmbStudent
            // 
            this.cmbStudent.FormattingEnabled = true;
            this.cmbStudent.Location = new System.Drawing.Point(118, 11);
            this.cmbStudent.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStudent.Name = "cmbStudent";
            this.cmbStudent.Size = new System.Drawing.Size(92, 21);
            this.cmbStudent.TabIndex = 8;
            // 
            // cmbSubject
            // 
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(118, 49);
            this.cmbSubject.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(92, 21);
            this.cmbSubject.TabIndex = 9;
            // 
            // cmbExam
            // 
            this.cmbExam.FormattingEnabled = true;
            this.cmbExam.Location = new System.Drawing.Point(118, 87);
            this.cmbExam.Margin = new System.Windows.Forms.Padding(2);
            this.cmbExam.Name = "cmbExam";
            this.cmbExam.Size = new System.Drawing.Size(92, 21);
            this.cmbExam.TabIndex = 10;
            // 
            // cmbLecturer
            // 
            this.cmbLecturer.FormattingEnabled = true;
            this.cmbLecturer.Location = new System.Drawing.Point(386, 46);
            this.cmbLecturer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLecturer.Name = "cmbLecturer";
            this.cmbLecturer.Size = new System.Drawing.Size(92, 21);
            this.cmbLecturer.TabIndex = 11;
            // 
            // dtpEntryDate
            // 
            this.dtpEntryDate.Location = new System.Drawing.Point(386, 89);
            this.dtpEntryDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpEntryDate.Name = "dtpEntryDate";
            this.dtpEntryDate.Size = new System.Drawing.Size(151, 20);
            this.dtpEntryDate.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(74, 179);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 28);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(74, 236);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(74, 28);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(74, 299);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 28);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(74, 371);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 28);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvMarks
            // 
            this.dgvMarks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarks.Location = new System.Drawing.Point(109, 451);
            this.dgvMarks.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMarks.Name = "dgvMarks";
            this.dgvMarks.RowHeadersWidth = 51;
            this.dgvMarks.RowTemplate.Height = 24;
            this.dgvMarks.Size = new System.Drawing.Size(547, 125);
            this.dgvMarks.TabIndex = 17;
            this.dgvMarks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMarks_CellClick);
            // 
            // cmbGrade
            // 
            this.cmbGrade.FormattingEnabled = true;
            this.cmbGrade.Location = new System.Drawing.Point(118, 129);
            this.cmbGrade.Name = "cmbGrade";
            this.cmbGrade.Size = new System.Drawing.Size(121, 21);
            this.cmbGrade.TabIndex = 19;
            // 
            // Grade
            // 
            this.Grade.AutoSize = true;
            this.Grade.Location = new System.Drawing.Point(21, 129);
            this.Grade.Name = "Grade";
            this.Grade.Size = new System.Drawing.Size(41, 15);
            this.Grade.TabIndex = 20;
            this.Grade.Text = "Grade";
            // 
            // dgvTopPerformers
            // 
            this.dgvTopPerformers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopPerformers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopPerformers.Location = new System.Drawing.Point(377, 147);
            this.dgvTopPerformers.Name = "dgvTopPerformers";
            this.dgvTopPerformers.ReadOnly = true;
            this.dgvTopPerformers.RowHeadersWidth = 51;
            this.dgvTopPerformers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopPerformers.Size = new System.Drawing.Size(240, 150);
            this.dgvTopPerformers.TabIndex = 21;
            this.dgvTopPerformers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTopPerformers_CellFormatting);
            // 
            // lblTopTitle
            // 
            this.lblTopTitle.AutoSize = true;
            this.lblTopTitle.Location = new System.Drawing.Point(274, 164);
            this.lblTopTitle.Name = "lblTopTitle";
            this.lblTopTitle.Size = new System.Drawing.Size(95, 15);
            this.lblTopTitle.TabIndex = 22;
            this.lblTopTitle.Text = "Top Performers ";
            // 
            // MarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 596);
            this.Controls.Add(this.lblTopTitle);
            this.Controls.Add(this.dgvTopPerformers);
            this.Controls.Add(this.Grade);
            this.Controls.Add(this.cmbGrade);
            this.Controls.Add(this.dgvMarks);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtpEntryDate);
            this.Controls.Add(this.cmbLecturer);
            this.Controls.Add(this.cmbExam);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.cmbStudent);
            this.Controls.Add(this.txtMarksObtained);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MarkForm";
            this.Text = "MarkForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopPerformers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMarksObtained;
        private System.Windows.Forms.ComboBox cmbStudent;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.ComboBox cmbExam;
        private System.Windows.Forms.ComboBox cmbLecturer;
        private System.Windows.Forms.DateTimePicker dtpEntryDate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvMarks;
        private System.Windows.Forms.ComboBox cmbGrade;
        private System.Windows.Forms.Label Grade;
        private System.Windows.Forms.DataGridView dgvTopPerformers;
        private System.Windows.Forms.Label lblTopTitle;
    }
}
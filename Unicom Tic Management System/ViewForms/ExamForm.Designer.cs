namespace Unicom_Tic_Management_System.ViewForms
{
    partial class ExamForm
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
            this.txtExamName = new System.Windows.Forms.TextBox();
            this.txtMaxMarks = new System.Windows.Forms.TextBox();
            this.dgvExams = new System.Windows.Forms.DataGridView();
            this.dtpExamDate = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSubjectId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            this.SuspendLayout();
            // 
            // txtExamName
            // 
            this.txtExamName.Location = new System.Drawing.Point(135, 31);
            this.txtExamName.Name = "txtExamName";
            this.txtExamName.Size = new System.Drawing.Size(100, 20);
            this.txtExamName.TabIndex = 0;
            // 
            // txtMaxMarks
            // 
            this.txtMaxMarks.Location = new System.Drawing.Point(135, 112);
            this.txtMaxMarks.Name = "txtMaxMarks";
            this.txtMaxMarks.Size = new System.Drawing.Size(100, 20);
            this.txtMaxMarks.TabIndex = 1;
            // 
            // dgvExams
            // 
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExams.Location = new System.Drawing.Point(239, 269);
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.RowHeadersWidth = 51;
            this.dgvExams.Size = new System.Drawing.Size(240, 150);
            this.dgvExams.TabIndex = 2;
            this.dgvExams.SelectionChanged += new System.EventHandler(this.dgvExams_SelectionChanged);
            // 
            // dtpExamDate
            // 
            this.dtpExamDate.Location = new System.Drawing.Point(135, 156);
            this.dtpExamDate.Name = "dtpExamDate";
            this.dtpExamDate.Size = new System.Drawing.Size(200, 20);
            this.dtpExamDate.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(394, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(394, 73);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(394, 140);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Exam Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Max Marks";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Exam Date";
            // 
            // txtSubjectId
            // 
            this.txtSubjectId.Location = new System.Drawing.Point(135, 73);
            this.txtSubjectId.Name = "txtSubjectId";
            this.txtSubjectId.Size = new System.Drawing.Size(100, 20);
            this.txtSubjectId.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Subject";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // ExamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSubjectId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtpExamDate);
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.txtMaxMarks);
            this.Controls.Add(this.txtExamName);
            this.Name = "ExamForm";
            this.Text = "ExamForm";
            this.Load += new System.EventHandler(this.ExamForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExamName;
        private System.Windows.Forms.TextBox txtMaxMarks;
        private System.Windows.Forms.DataGridView dgvExams;
        private System.Windows.Forms.DateTimePicker dtpExamDate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSubjectId;
        private System.Windows.Forms.Label label4;
    }
}
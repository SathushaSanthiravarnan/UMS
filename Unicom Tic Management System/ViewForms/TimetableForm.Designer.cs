namespace Unicom_Tic_Management_System.ViewForms
{
    partial class TimetableForm
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
            this.cmbRoom = new System.Windows.Forms.ComboBox();
            this.cmbDay = new System.Windows.Forms.ComboBox();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.cmbTimeSlot = new System.Windows.Forms.ComboBox();
            this.cmbActivityType = new System.Windows.Forms.ComboBox();
            this.cmbCourse = new System.Windows.Forms.ComboBox();
            this.cmbLecturer = new System.Windows.Forms.ComboBox();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.txtAcademicYear = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvTimetables = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimetables)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbRoom
            // 
            this.cmbRoom.FormattingEnabled = true;
            this.cmbRoom.Location = new System.Drawing.Point(94, 158);
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.Size = new System.Drawing.Size(121, 24);
            this.cmbRoom.TabIndex = 0;
            this.cmbRoom.SelectedIndexChanged += new System.EventHandler(this.cmbRoom_SelectedIndexChanged);
            // 
            // cmbDay
            // 
            this.cmbDay.FormattingEnabled = true;
            this.cmbDay.Location = new System.Drawing.Point(94, 258);
            this.cmbDay.Name = "cmbDay";
            this.cmbDay.Size = new System.Drawing.Size(121, 24);
            this.cmbDay.TabIndex = 1;
            // 
            // cmbSubject
            // 
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(94, 57);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(121, 24);
            this.cmbSubject.TabIndex = 2;
            // 
            // cmbTimeSlot
            // 
            this.cmbTimeSlot.FormattingEnabled = true;
            this.cmbTimeSlot.Location = new System.Drawing.Point(94, 312);
            this.cmbTimeSlot.Name = "cmbTimeSlot";
            this.cmbTimeSlot.Size = new System.Drawing.Size(121, 24);
            this.cmbTimeSlot.TabIndex = 3;
            // 
            // cmbActivityType
            // 
            this.cmbActivityType.FormattingEnabled = true;
            this.cmbActivityType.Location = new System.Drawing.Point(94, 363);
            this.cmbActivityType.Name = "cmbActivityType";
            this.cmbActivityType.Size = new System.Drawing.Size(121, 24);
            this.cmbActivityType.TabIndex = 4;
            // 
            // cmbCourse
            // 
            this.cmbCourse.FormattingEnabled = true;
            this.cmbCourse.Location = new System.Drawing.Point(94, 12);
            this.cmbCourse.Name = "cmbCourse";
            this.cmbCourse.Size = new System.Drawing.Size(121, 24);
            this.cmbCourse.TabIndex = 5;
            // 
            // cmbLecturer
            // 
            this.cmbLecturer.FormattingEnabled = true;
            this.cmbLecturer.Location = new System.Drawing.Point(94, 106);
            this.cmbLecturer.Name = "cmbLecturer";
            this.cmbLecturer.Size = new System.Drawing.Size(121, 24);
            this.cmbLecturer.TabIndex = 6;
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(94, 204);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(121, 24);
            this.cmbGroup.TabIndex = 7;
            // 
            // txtAcademicYear
            // 
            this.txtAcademicYear.Location = new System.Drawing.Point(109, 416);
            this.txtAcademicYear.Name = "txtAcademicYear";
            this.txtAcademicYear.Size = new System.Drawing.Size(121, 22);
            this.txtAcademicYear.TabIndex = 8;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(479, 106);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(98, 30);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(479, 194);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(98, 33);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(479, 52);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(98, 29);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvTimetables
            // 
            this.dgvTimetables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimetables.Location = new System.Drawing.Point(262, 244);
            this.dgvTimetables.Name = "dgvTimetables";
            this.dgvTimetables.RowHeadersWidth = 51;
            this.dgvTimetables.RowTemplate.Height = 24;
            this.dgvTimetables.Size = new System.Drawing.Size(729, 390);
            this.dgvTimetables.TabIndex = 12;
            this.dgvTimetables.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTimetables_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Course";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Subject";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Lecturer\t";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Room";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Group";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "DayOfWeek";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "TimeSlot";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 366);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "ActivityType";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 422);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "AcademicYear";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(479, 158);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 22);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "      CLEAR";
            this.btnClear.TextChanged += new System.EventHandler(this.btnClear_TextChanged);
            // 
            // TimetableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 688);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTimetables);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtAcademicYear);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.cmbLecturer);
            this.Controls.Add(this.cmbCourse);
            this.Controls.Add(this.cmbActivityType);
            this.Controls.Add(this.cmbTimeSlot);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.cmbDay);
            this.Controls.Add(this.cmbRoom);
            this.Name = "TimetableForm";
            this.Text = "TimetableForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimetables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRoom;
        private System.Windows.Forms.ComboBox cmbDay;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.ComboBox cmbTimeSlot;
        private System.Windows.Forms.ComboBox cmbActivityType;
        private System.Windows.Forms.ComboBox cmbCourse;
        private System.Windows.Forms.ComboBox cmbLecturer;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.TextBox txtAcademicYear;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvTimetables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox btnClear;
    }
}
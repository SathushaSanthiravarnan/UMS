namespace Unicom_Tic_Management_System
{
    partial class ActivityLogForm
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
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.btnSearchByAction = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnFilterByDate = new System.Windows.Forms.Button();
            this.txtSearchAction = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLogs
            // 
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Location = new System.Drawing.Point(30, 162);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.RowHeadersWidth = 51;
            this.dgvLogs.RowTemplate.Height = 24;
            this.dgvLogs.Size = new System.Drawing.Size(683, 276);
            this.dgvLogs.TabIndex = 0;
            // 
            // btnSearchByAction
            // 
            this.btnSearchByAction.Location = new System.Drawing.Point(200, 22);
            this.btnSearchByAction.Name = "btnSearchByAction";
            this.btnSearchByAction.Size = new System.Drawing.Size(120, 23);
            this.btnSearchByAction.TabIndex = 1;
            this.btnSearchByAction.Text = "SearchByAction";
            this.btnSearchByAction.UseVisualStyleBackColor = true;
            this.btnSearchByAction.Click += new System.EventHandler(this.btnSearchByAction_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(376, 22);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnFilterByDate
            // 
            this.btnFilterByDate.Location = new System.Drawing.Point(614, 118);
            this.btnFilterByDate.Name = "btnFilterByDate";
            this.btnFilterByDate.Size = new System.Drawing.Size(99, 23);
            this.btnFilterByDate.TabIndex = 3;
            this.btnFilterByDate.Text = "Filter by Date";
            this.btnFilterByDate.UseVisualStyleBackColor = true;
            this.btnFilterByDate.Click += new System.EventHandler(this.btnFilterByDate_Click);
            // 
            // txtSearchAction
            // 
            this.txtSearchAction.Location = new System.Drawing.Point(41, 22);
            this.txtSearchAction.Name = "txtSearchAction";
            this.txtSearchAction.Size = new System.Drawing.Size(100, 22);
            this.txtSearchAction.TabIndex = 4;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(57, 81);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(38, 16);
            this.lblFrom.TabIndex = 5;
            this.lblFrom.Text = "From";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(101, 81);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 22);
            this.dtpStart.TabIndex = 6;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(403, 81);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 22);
            this.dtpEnd.TabIndex = 7;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(373, 81);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(24, 16);
            this.lblTo.TabIndex = 8;
            this.lblTo.Text = "To";
            // 
            // ActivityLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.txtSearchAction);
            this.Controls.Add(this.btnFilterByDate);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSearchByAction);
            this.Controls.Add(this.dgvLogs);
            this.Name = "ActivityLogForm";
            this.Text = "ActivityLogForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Button btnSearchByAction;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnFilterByDate;
        private System.Windows.Forms.TextBox txtSearchAction;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblTo;
    }
}
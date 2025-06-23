namespace Unicom_Tic_Management_System.ViewForms
{
    partial class NicDetailForm
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
            this.lblNic = new System.Windows.Forms.Label();
            this.txtNic = new System.Windows.Forms.TextBox();
            this.chkIsUsed = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvNIC = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNIC)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNic
            // 
            this.lblNic.AutoSize = true;
            this.lblNic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNic.Location = new System.Drawing.Point(87, 88);
            this.lblNic.Name = "lblNic";
            this.lblNic.Size = new System.Drawing.Size(43, 22);
            this.lblNic.TabIndex = 0;
            this.lblNic.Text = "NIC";
            // 
            // txtNic
            // 
            this.txtNic.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNic.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNic.Location = new System.Drawing.Point(175, 88);
            this.txtNic.Name = "txtNic";
            this.txtNic.Size = new System.Drawing.Size(168, 27);
            this.txtNic.TabIndex = 1;
            // 
            // chkIsUsed
            // 
            this.chkIsUsed.AutoSize = true;
            this.chkIsUsed.Font = new System.Drawing.Font("Lucida Sans Unicode", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsUsed.Location = new System.Drawing.Point(379, 93);
            this.chkIsUsed.Name = "chkIsUsed";
            this.chkIsUsed.Size = new System.Drawing.Size(77, 22);
            this.chkIsUsed.TabIndex = 2;
            this.chkIsUsed.Text = "IsUsed";
            this.chkIsUsed.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(224, 176);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(95, 36);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(347, 176);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(91, 36);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(464, 176);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(98, 36);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvNIC
            // 
            this.dgvNIC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNIC.Location = new System.Drawing.Point(58, 232);
            this.dgvNIC.Name = "dgvNIC";
            this.dgvNIC.RowHeadersWidth = 51;
            this.dgvNIC.RowTemplate.Height = 24;
            this.dgvNIC.Size = new System.Drawing.Size(639, 217);
            this.dgvNIC.TabIndex = 6;
            this.dgvNIC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNIC_CellContentClick);
            this.dgvNIC.SelectionChanged += new System.EventHandler(this.dgvNIC_SelectionChanged);
            // 
            // NicDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvNIC);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.chkIsUsed);
            this.Controls.Add(this.txtNic);
            this.Controls.Add(this.lblNic);
            this.Name = "NicDetailForm";
            this.Text = "NicDetailForm";
            this.Load += new System.EventHandler(this.NicDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNIC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNic;
        private System.Windows.Forms.TextBox txtNic;
        private System.Windows.Forms.CheckBox chkIsUsed;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvNIC;
    }
}
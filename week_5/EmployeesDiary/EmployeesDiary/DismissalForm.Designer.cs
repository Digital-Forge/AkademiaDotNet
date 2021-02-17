
namespace EmployeesDiary
{
    partial class DismissalForm
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
            this.btDismissal = new System.Windows.Forms.Button();
            this.dtpWhen = new System.Windows.Forms.DateTimePicker();
            this.lbWho = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btDismissal
            // 
            this.btDismissal.Location = new System.Drawing.Point(12, 124);
            this.btDismissal.Name = "btDismissal";
            this.btDismissal.Size = new System.Drawing.Size(200, 23);
            this.btDismissal.TabIndex = 0;
            this.btDismissal.Text = "Dismissal";
            this.btDismissal.UseVisualStyleBackColor = true;
            this.btDismissal.Click += new System.EventHandler(this.btDismissal_Click);
            // 
            // dtpWhen
            // 
            this.dtpWhen.Location = new System.Drawing.Point(12, 29);
            this.dtpWhen.Name = "dtpWhen";
            this.dtpWhen.Size = new System.Drawing.Size(200, 20);
            this.dtpWhen.TabIndex = 1;
            this.dtpWhen.ValueChanged += new System.EventHandler(this.dtpWhen_ValueChanged);
            // 
            // lbWho
            // 
            this.lbWho.AutoSize = true;
            this.lbWho.Location = new System.Drawing.Point(12, 9);
            this.lbWho.Name = "lbWho";
            this.lbWho.Size = new System.Drawing.Size(35, 13);
            this.lbWho.TabIndex = 2;
            this.lbWho.Text = "label1";
            // 
            // DismissalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 159);
            this.Controls.Add(this.lbWho);
            this.Controls.Add(this.dtpWhen);
            this.Controls.Add(this.btDismissal);
            this.MaximumSize = new System.Drawing.Size(242, 198);
            this.MinimumSize = new System.Drawing.Size(242, 198);
            this.Name = "DismissalForm";
            this.Text = "Dismissal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btDismissal;
        private System.Windows.Forms.DateTimePicker dtpWhen;
        private System.Windows.Forms.Label lbWho;
    }
}
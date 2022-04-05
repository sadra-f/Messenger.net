
namespace Messenger.Client.src.Forms {
    partial class frmCreateGroup {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tbGroupDesc = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.tbGroupName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbGroupDesc
            // 
            this.tbGroupDesc.Location = new System.Drawing.Point(12, 83);
            this.tbGroupDesc.MaxLength = 128;
            this.tbGroupDesc.Multiline = true;
            this.tbGroupDesc.Name = "tbGroupDesc";
            this.tbGroupDesc.Size = new System.Drawing.Size(509, 38);
            this.tbGroupDesc.TabIndex = 11;
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCreate.Location = new System.Drawing.Point(427, 27);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(94, 28);
            this.btnCreate.TabIndex = 10;
            this.btnCreate.Text = "Create Group";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // tbGroupName
            // 
            this.tbGroupName.Location = new System.Drawing.Point(12, 31);
            this.tbGroupName.MaxLength = 32;
            this.tbGroupName.Name = "tbGroupName";
            this.tbGroupName.Size = new System.Drawing.Size(271, 20);
            this.tbGroupName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Group Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "GroupName";
            // 
            // frmCreateGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 136);
            this.Controls.Add(this.tbGroupDesc);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.tbGroupName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCreateGroup";
            this.Text = "frmCreateGroup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbGroupDesc;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox tbGroupName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
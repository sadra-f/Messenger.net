
namespace Messenger.Client.src.Forms {
    partial class frmGroupInfo {
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveDesc = new System.Windows.Forms.Button();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.tbCreatorUsername = new System.Windows.Forms.TextBox();
            this.tbGName = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbMemebers = new System.Windows.Forms.ListBox();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnRemoveMember = new System.Windows.Forms.Button();
            this.btnLeaveGroup = new System.Windows.Forms.Button();
            this.btnCloseAndLEave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveDesc);
            this.splitContainer1.Panel1.Controls.Add(this.tbDesc);
            this.splitContainer1.Panel1.Controls.Add(this.tbCreatorUsername);
            this.splitContainer1.Panel1.Controls.Add(this.tbGName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(861, 459);
            this.splitContainer1.SplitterDistance = 147;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 109);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Creator";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // btnSaveDesc
            // 
            this.btnSaveDesc.Enabled = false;
            this.btnSaveDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSaveDesc.Location = new System.Drawing.Point(728, 58);
            this.btnSaveDesc.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDesc.Name = "btnSaveDesc";
            this.btnSaveDesc.Size = new System.Drawing.Size(129, 28);
            this.btnSaveDesc.TabIndex = 1;
            this.btnSaveDesc.Text = "Save Description";
            this.btnSaveDesc.UseVisualStyleBackColor = true;
            // 
            // tbDesc
            // 
            this.tbDesc.Enabled = false;
            this.tbDesc.Location = new System.Drawing.Point(110, 47);
            this.tbDesc.Margin = new System.Windows.Forms.Padding(4);
            this.tbDesc.Multiline = true;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.Size = new System.Drawing.Size(577, 51);
            this.tbDesc.TabIndex = 0;
            // 
            // tbCreatorUsername
            // 
            this.tbCreatorUsername.Enabled = false;
            this.tbCreatorUsername.Location = new System.Drawing.Point(110, 106);
            this.tbCreatorUsername.Margin = new System.Windows.Forms.Padding(4);
            this.tbCreatorUsername.Name = "tbCreatorUsername";
            this.tbCreatorUsername.Size = new System.Drawing.Size(132, 23);
            this.tbCreatorUsername.TabIndex = 0;
            // 
            // tbGName
            // 
            this.tbGName.Enabled = false;
            this.tbGName.Location = new System.Drawing.Point(110, 15);
            this.tbGName.Margin = new System.Windows.Forms.Padding(4);
            this.tbGName.Name = "tbGName";
            this.tbGName.Size = new System.Drawing.Size(359, 23);
            this.tbGName.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(861, 307);
            this.splitContainer2.SplitterDistance = 38;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Memebers :";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbMemebers);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btnAddMember);
            this.splitContainer3.Panel2.Controls.Add(this.btnRemoveMember);
            this.splitContainer3.Panel2.Controls.Add(this.btnLeaveGroup);
            this.splitContainer3.Panel2.Controls.Add(this.btnCloseAndLEave);
            this.splitContainer3.Size = new System.Drawing.Size(861, 264);
            this.splitContainer3.SplitterDistance = 722;
            this.splitContainer3.TabIndex = 2;
            // 
            // lbMemebers
            // 
            this.lbMemebers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMemebers.FormattingEnabled = true;
            this.lbMemebers.ItemHeight = 16;
            this.lbMemebers.Location = new System.Drawing.Point(0, 0);
            this.lbMemebers.Name = "lbMemebers";
            this.lbMemebers.Size = new System.Drawing.Size(722, 264);
            this.lbMemebers.TabIndex = 0;
            // 
            // btnAddMember
            // 
            this.btnAddMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnAddMember.Location = new System.Drawing.Point(19, 55);
            this.btnAddMember.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(100, 28);
            this.btnAddMember.TabIndex = 2;
            this.btnAddMember.Text = "Add";
            this.btnAddMember.UseVisualStyleBackColor = true;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // btnRemoveMember
            // 
            this.btnRemoveMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnRemoveMember.Location = new System.Drawing.Point(19, 91);
            this.btnRemoveMember.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveMember.Name = "btnRemoveMember";
            this.btnRemoveMember.Size = new System.Drawing.Size(100, 28);
            this.btnRemoveMember.TabIndex = 3;
            this.btnRemoveMember.Text = "Remove";
            this.btnRemoveMember.UseVisualStyleBackColor = true;
            // 
            // btnLeaveGroup
            // 
            this.btnLeaveGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnLeaveGroup.Location = new System.Drawing.Point(19, 127);
            this.btnLeaveGroup.Margin = new System.Windows.Forms.Padding(4);
            this.btnLeaveGroup.Name = "btnLeaveGroup";
            this.btnLeaveGroup.Size = new System.Drawing.Size(100, 28);
            this.btnLeaveGroup.TabIndex = 4;
            this.btnLeaveGroup.Text = "Leave";
            this.btnLeaveGroup.UseVisualStyleBackColor = true;
            // 
            // btnCloseAndLEave
            // 
            this.btnCloseAndLEave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCloseAndLEave.Location = new System.Drawing.Point(12, 175);
            this.btnCloseAndLEave.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseAndLEave.Name = "btnCloseAndLEave";
            this.btnCloseAndLEave.Size = new System.Drawing.Size(115, 28);
            this.btnCloseAndLEave.TabIndex = 5;
            this.btnCloseAndLEave.Text = "Close And Leave";
            this.btnCloseAndLEave.UseVisualStyleBackColor = true;
            // 
            // frmGroupInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 459);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmGroupInfo";
            this.Text = "Group";
            this.Load += new System.EventHandler(this.frmGroupInfo_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveDesc;
        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.TextBox tbCreatorUsername;
        private System.Windows.Forms.TextBox tbGName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox lbMemebers;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnRemoveMember;
        private System.Windows.Forms.Button btnLeaveGroup;
        private System.Windows.Forms.Button btnCloseAndLEave;
    }
}
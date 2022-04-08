
namespace Messenger.Client.src.Forms {
    partial class frmHome {
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.btnNewChat = new System.Windows.Forms.Button();
            this.btnChat = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbGroups = new System.Windows.Forms.ListBox();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.btnOpenGroup = new System.Windows.Forms.Button();
            this.lbContacts = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 36;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblUsername);
            this.panel1.Controls.Add(this.btnNewChat);
            this.panel1.Controls.Add(this.btnChat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 36);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hello ";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(47, 13);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(53, 13);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "username";
            // 
            // btnNewChat
            // 
            this.btnNewChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnNewChat.Location = new System.Drawing.Point(619, 7);
            this.btnNewChat.Name = "btnNewChat";
            this.btnNewChat.Size = new System.Drawing.Size(84, 23);
            this.btnNewChat.TabIndex = 2;
            this.btnNewChat.Text = "New Chat";
            this.btnNewChat.UseVisualStyleBackColor = true;
            this.btnNewChat.Click += new System.EventHandler(this.btnNewChat_Click);
            // 
            // btnChat
            // 
            this.btnChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnChat.Location = new System.Drawing.Point(709, 7);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(84, 23);
            this.btnChat.TabIndex = 3;
            this.btnChat.Text = "Open Chat";
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbGroups);
            this.panel2.Controls.Add(this.btnNewGroup);
            this.panel2.Controls.Add(this.btnOpenGroup);
            this.panel2.Controls.Add(this.lbContacts);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 413);
            this.panel2.TabIndex = 0;
            // 
            // lbGroups
            // 
            this.lbGroups.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lbGroups.FormattingEnabled = true;
            this.lbGroups.ItemHeight = 16;
            this.lbGroups.Location = new System.Drawing.Point(0, 233);
            this.lbGroups.Name = "lbGroups";
            this.lbGroups.Size = new System.Drawing.Size(800, 180);
            this.lbGroups.TabIndex = 1;
            this.lbGroups.DoubleClick += new System.EventHandler(this.lbGroups_DoubleClick);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnNewGroup.Location = new System.Drawing.Point(622, 203);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(81, 23);
            this.btnNewGroup.TabIndex = 2;
            this.btnNewGroup.Text = "New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // btnOpenGroup
            // 
            this.btnOpenGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnOpenGroup.Location = new System.Drawing.Point(709, 203);
            this.btnOpenGroup.Name = "btnOpenGroup";
            this.btnOpenGroup.Size = new System.Drawing.Size(84, 23);
            this.btnOpenGroup.TabIndex = 3;
            this.btnOpenGroup.Text = "Open Group";
            this.btnOpenGroup.UseVisualStyleBackColor = true;
            this.btnOpenGroup.Click += new System.EventHandler(this.btnOpenGroup_Click);
            // 
            // lbContacts
            // 
            this.lbContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lbContacts.FormattingEnabled = true;
            this.lbContacts.ItemHeight = 16;
            this.lbContacts.Location = new System.Drawing.Point(0, 0);
            this.lbContacts.Name = "lbContacts";
            this.lbContacts.Size = new System.Drawing.Size(800, 196);
            this.lbContacts.TabIndex = 0;
            this.lbContacts.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmHome";
            this.Text = "frmHome";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHome_FormClosing);
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNewChat;
        private System.Windows.Forms.Button btnChat;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbContacts;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.ListBox lbGroups;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.Button btnOpenGroup;
    }
}
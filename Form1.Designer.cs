namespace _241018_CaroChess_WinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlChessBoard = new Panel();
            pngPicture = new Panel();
            ptbAvatar = new PictureBox();
            panel3 = new Panel();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnLAN = new Button();
            txbIP = new TextBox();
            ptbMark = new PictureBox();
            txbPlayerName = new TextBox();
            pgbCoolDown = new ProgressBar();
            timerCoolDown = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            pngPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptbAvatar).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptbMark).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.CausesValidation = false;
            label1.Font = new Font("Ravie", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(16, 229);
            label1.Name = "label1";
            label1.Size = new Size(347, 38);
            label1.TabIndex = 5;
            label1.Text = "5 in a Line to WIN";
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.BorderStyle = BorderStyle.FixedSingle;
            pnlChessBoard.Location = new Point(12, 12);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(661, 663);
            pnlChessBoard.TabIndex = 0;
            // 
            // pngPicture
            // 
            pngPicture.Controls.Add(ptbAvatar);
            pngPicture.Location = new Point(679, 12);
            pngPicture.Name = "pngPicture";
            pngPicture.Size = new Size(369, 385);
            pngPicture.TabIndex = 1;
            // 
            // ptbAvatar
            // 
            ptbAvatar.Anchor = AnchorStyles.None;
            ptbAvatar.BackgroundImage = (Image)resources.GetObject("ptbAvatar.BackgroundImage");
            ptbAvatar.BackgroundImageLayout = ImageLayout.Stretch;
            ptbAvatar.Location = new Point(0, 3);
            ptbAvatar.Name = "ptbAvatar";
            ptbAvatar.Size = new Size(366, 379);
            ptbAvatar.TabIndex = 0;
            ptbAvatar.TabStop = false;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.Controls.Add(label4);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(btnLAN);
            panel3.Controls.Add(txbIP);
            panel3.Controls.Add(ptbMark);
            panel3.Controls.Add(txbPlayerName);
            panel3.Controls.Add(pgbCoolDown);
            panel3.Location = new Point(679, 403);
            panel3.Name = "panel3";
            panel3.Size = new Size(369, 272);
            panel3.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Lucida Handwriting", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Purple;
            label4.Location = new Point(16, 104);
            label4.Name = "label4";
            label4.Size = new Size(31, 23);
            label4.TabIndex = 8;
            label4.Text = "IP";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Lucida Handwriting", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Purple;
            label3.Location = new Point(3, 68);
            label3.Name = "label3";
            label3.Size = new Size(66, 23);
            label3.TabIndex = 7;
            label3.Text = "Timer";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Lucida Handwriting", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Purple;
            label2.Location = new Point(3, 26);
            label2.Name = "label2";
            label2.Size = new Size(71, 23);
            label2.TabIndex = 6;
            label2.Text = "Player";
            // 
            // btnLAN
            // 
            btnLAN.BackColor = Color.Lavender;
            btnLAN.Font = new Font("Cooper Black", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLAN.ForeColor = Color.DarkCyan;
            btnLAN.Location = new Point(112, 166);
            btnLAN.Name = "btnLAN";
            btnLAN.Size = new Size(164, 46);
            btnLAN.TabIndex = 4;
            btnLAN.Text = "LAN";
            btnLAN.UseVisualStyleBackColor = false;
            // 
            // txbIP
            // 
            txbIP.BackColor = Color.PeachPuff;
            txbIP.BorderStyle = BorderStyle.None;
            txbIP.Font = new Font("Forte", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txbIP.ForeColor = Color.DeepSkyBlue;
            txbIP.Location = new Point(80, 97);
            txbIP.Name = "txbIP";
            txbIP.Size = new Size(138, 30);
            txbIP.TabIndex = 3;
            txbIP.Text = "127.0.0.1";
            txbIP.TextAlign = HorizontalAlignment.Center;
            // 
            // ptbMark
            // 
            ptbMark.Location = new Point(238, 35);
            ptbMark.Name = "ptbMark";
            ptbMark.Size = new Size(116, 109);
            ptbMark.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbMark.TabIndex = 2;
            ptbMark.TabStop = false;
            // 
            // txbPlayerName
            // 
            txbPlayerName.BackColor = Color.Cornsilk;
            txbPlayerName.BorderStyle = BorderStyle.None;
            txbPlayerName.Font = new Font("Forte", 13F);
            txbPlayerName.ForeColor = Color.Navy;
            txbPlayerName.Location = new Point(80, 26);
            txbPlayerName.Name = "txbPlayerName";
            txbPlayerName.ReadOnly = true;
            txbPlayerName.Size = new Size(138, 30);
            txbPlayerName.TabIndex = 1;
            txbPlayerName.TextAlign = HorizontalAlignment.Center;
            // 
            // pgbCoolDown
            // 
            pgbCoolDown.Location = new Point(80, 62);
            pgbCoolDown.Name = "pgbCoolDown";
            pgbCoolDown.Size = new Size(138, 29);
            pgbCoolDown.TabIndex = 0;
            // 
            // timerCoolDown
            // 
            timerCoolDown.Tick += timerCoolDown_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 680);
            Controls.Add(panel3);
            Controls.Add(pngPicture);
            Controls.Add(pnlChessBoard);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "CaroChess LAN";
            pngPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ptbAvatar).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ptbMark).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlChessBoard;
        private Panel pngPicture;
        private Panel panel3;
        private PictureBox ptbAvatar;
        private Button btnLAN;
        private TextBox txbIP;
        private PictureBox ptbMark;
        private TextBox txbPlayerName;
        private ProgressBar pgbCoolDown;
        private Label label2;
        private Label label4;
        private Label label3;
        private System.Windows.Forms.Timer timerCoolDown;
    }
}

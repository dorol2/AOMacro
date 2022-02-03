namespace MacroExample.View
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.TestCaptureButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.XPositionTextbox = new System.Windows.Forms.TextBox();
            this.XPositionLabel = new System.Windows.Forms.Label();
            this.YPositionLabel = new System.Windows.Forms.Label();
            this.YPositionTextbox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TestCaptureButton
            // 
            this.TestCaptureButton.Location = new System.Drawing.Point(13, 13);
            this.TestCaptureButton.Name = "TestCaptureButton";
            this.TestCaptureButton.Size = new System.Drawing.Size(150, 23);
            this.TestCaptureButton.TabIndex = 0;
            this.TestCaptureButton.Text = "TestCaptureButton";
            this.TestCaptureButton.UseVisualStyleBackColor = true;
            this.TestCaptureButton.Click += new System.EventHandler(this.TestCaptureButtonClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(676, 381);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // XPositionTextbox
            // 
            this.XPositionTextbox.Location = new System.Drawing.Point(75, 429);
            this.XPositionTextbox.Multiline = true;
            this.XPositionTextbox.Name = "XPositionTextbox";
            this.XPositionTextbox.Size = new System.Drawing.Size(118, 32);
            this.XPositionTextbox.TabIndex = 3;
            this.XPositionTextbox.Text = "0";
            // 
            // XPositionLabel
            // 
            this.XPositionLabel.AutoSize = true;
            this.XPositionLabel.Location = new System.Drawing.Point(11, 432);
            this.XPositionLabel.Name = "XPositionLabel";
            this.XPositionLabel.Size = new System.Drawing.Size(58, 12);
            this.XPositionLabel.TabIndex = 2;
            this.XPositionLabel.Text = "XPosition";
            this.XPositionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // YPositionLabel
            // 
            this.YPositionLabel.AutoSize = true;
            this.YPositionLabel.Location = new System.Drawing.Point(12, 466);
            this.YPositionLabel.Name = "YPositionLabel";
            this.YPositionLabel.Size = new System.Drawing.Size(58, 12);
            this.YPositionLabel.TabIndex = 5;
            this.YPositionLabel.Text = "YPosition";
            this.YPositionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // YPositionTextbox
            // 
            this.YPositionTextbox.Location = new System.Drawing.Point(75, 466);
            this.YPositionTextbox.Multiline = true;
            this.YPositionTextbox.Name = "YPositionTextbox";
            this.YPositionTextbox.Size = new System.Drawing.Size(118, 32);
            this.YPositionTextbox.TabIndex = 4;
            this.YPositionTextbox.Text = "-15";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(439, 429);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(122, 23);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "StartButton";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(567, 429);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(122, 23);
            this.StopButton.TabIndex = 7;
            this.StopButton.Text = "StopButton";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 504);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.YPositionLabel);
            this.Controls.Add(this.YPositionTextbox);
            this.Controls.Add(this.XPositionLabel);
            this.Controls.Add(this.XPositionTextbox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TestCaptureButton);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TestCaptureButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox XPositionTextbox;
        public System.Windows.Forms.Label XPositionLabel;
        public System.Windows.Forms.Label YPositionLabel;
        private System.Windows.Forms.TextBox YPositionTextbox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
    }
}


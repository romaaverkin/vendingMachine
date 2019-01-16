namespace FlowLayoutPanel
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.paymentLabel = new System.Windows.Forms.Label();
            this.buyButton = new System.Windows.Forms.Button();
            this.currentBalanceVendingMachineLabel = new System.Windows.Forms.Label();
            this.selectDrinkButton = new System.Windows.Forms.Label();
            this.yourСhangelabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(15, 50);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(180, 300);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(223, 50);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(180, 300);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // paymentLabel
            // 
            this.paymentLabel.AutoSize = true;
            this.paymentLabel.Location = new System.Drawing.Point(260, 18);
            this.paymentLabel.Name = "paymentLabel";
            this.paymentLabel.Size = new System.Drawing.Size(93, 13);
            this.paymentLabel.TabIndex = 2;
            this.paymentLabel.Text = "Вы внесли 0 руб.";
            // 
            // buyButton
            // 
            this.buyButton.Enabled = false;
            this.buyButton.Location = new System.Drawing.Point(172, 369);
            this.buyButton.Name = "buyButton";
            this.buyButton.Size = new System.Drawing.Size(75, 23);
            this.buyButton.TabIndex = 3;
            this.buyButton.Text = "Купить";
            this.buyButton.UseVisualStyleBackColor = true;
            this.buyButton.Click += new System.EventHandler(this.buyButton_Click);
            // 
            // currentBalanceVendingMachineLabel
            // 
            this.currentBalanceVendingMachineLabel.AutoSize = true;
            this.currentBalanceVendingMachineLabel.Location = new System.Drawing.Point(12, 449);
            this.currentBalanceVendingMachineLabel.Name = "currentBalanceVendingMachineLabel";
            this.currentBalanceVendingMachineLabel.Size = new System.Drawing.Size(0, 13);
            this.currentBalanceVendingMachineLabel.TabIndex = 4;
            // 
            // selectDrinkButton
            // 
            this.selectDrinkButton.AutoSize = true;
            this.selectDrinkButton.Location = new System.Drawing.Point(52, 18);
            this.selectDrinkButton.Name = "selectDrinkButton";
            this.selectDrinkButton.Size = new System.Drawing.Size(101, 13);
            this.selectDrinkButton.TabIndex = 5;
            this.selectDrinkButton.Text = "Выберите напиток";
            // 
            // yourСhangelabel
            // 
            this.yourСhangelabel.AutoSize = true;
            this.yourСhangelabel.Location = new System.Drawing.Point(220, 449);
            this.yourСhangelabel.Name = "yourСhangelabel";
            this.yourСhangelabel.Size = new System.Drawing.Size(0, 13);
            this.yourСhangelabel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 646);
            this.Controls.Add(this.yourСhangelabel);
            this.Controls.Add(this.selectDrinkButton);
            this.Controls.Add(this.currentBalanceVendingMachineLabel);
            this.Controls.Add(this.buyButton);
            this.Controls.Add(this.paymentLabel);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label paymentLabel;
        private System.Windows.Forms.Button buyButton;
        private System.Windows.Forms.Label currentBalanceVendingMachineLabel;
        private System.Windows.Forms.Label selectDrinkButton;
        private System.Windows.Forms.Label yourСhangelabel;
    }
}


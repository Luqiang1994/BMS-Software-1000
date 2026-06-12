namespace BMS_Read_Write_1000
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            dgvStatusInfo = new DataGridView();
            cm_Name1 = new DataGridViewTextBoxColumn();
            cm_Value1 = new DataGridViewTextBoxColumn();
            dgvCellVoltages = new DataGridView();
            colBalance = new DataGridViewTextBoxColumn();
            cm_Name = new DataGridViewTextBoxColumn();
            cm_Value = new DataGridViewTextBoxColumn();
            groupBox17 = new GroupBox();
            txtTotalCapacity = new TextBox();
            txtBatteryCycles = new TextBox();
            tx_Current = new TextBox();
            tx_TotalVoltage = new TextBox();
            label62 = new Label();
            label61 = new Label();
            label21 = new Label();
            label24 = new Label();
            groupBox16 = new GroupBox();
            comboSlaveAddress = new ComboBox();
            button11 = new Button();
            label60 = new Label();
            groupBox5 = new GroupBox();
            button10 = new Button();
            btnConnect = new Button();
            comboBaud = new ComboBox();
            label25 = new Label();
            comboPort = new ComboBox();
            label = new Label();
            tabPage2 = new TabPage();
            btnWriteConfig = new Button();
            btnReadConfig = new Button();
            groupBox15 = new GroupBox();
            button9 = new Button();
            button8 = new Button();
            button6 = new Button();
            button4 = new Button();
            button7 = new Button();
            button5 = new Button();
            button3 = new Button();
            groupBox14 = new GroupBox();
            button2 = new Button();
            button1 = new Button();
            richTextBox1 = new RichTextBox();
            comboFwBaudRate = new ComboBox();
            label59 = new Label();
            groupBox13 = new GroupBox();
            label58 = new Label();
            comboDischargeCalibration = new ComboBox();
            label57 = new Label();
            comboChargeCalibration = new ComboBox();
            groupBox12 = new GroupBox();
            label56 = new Label();
            comboMosOverTempProtect = new ComboBox();
            comboMosOverTempRestore = new ComboBox();
            label55 = new Label();
            groupBox11 = new GroupBox();
            label11 = new Label();
            label10 = new Label();
            label12 = new Label();
            label13 = new Label();
            comboDischargeUnderTempRestore = new ComboBox();
            comboDischargeUnderTempProtect = new ComboBox();
            comboDischargeOverTempRestore = new ComboBox();
            comboDischargeOverTempProtect = new ComboBox();
            comboChargeUnderTempRestore = new ComboBox();
            comboChargeUnderTempProtect = new ComboBox();
            comboChargeOverTempRestore = new ComboBox();
            comboChargeOverTempProtect = new ComboBox();
            label51 = new Label();
            label54 = new Label();
            groupBox10 = new GroupBox();
            comboFunctionParamSet = new ComboBox();
            label9 = new Label();
            comboSelfConsumptionPower = new ComboBox();
            label8 = new Label();
            comboSleepLeakageCurrent = new ComboBox();
            label7 = new Label();
            groupBox9 = new GroupBox();
            label53 = new Label();
            comboBALP = new ComboBox();
            label52 = new Label();
            comboBALV = new ComboBox();
            groupBox3 = new GroupBox();
            comboDC2Delay = new ComboBox();
            comboDC2 = new ComboBox();
            comboDC1Delay = new ComboBox();
            comboDC1 = new ComboBox();
            comboCCDelay = new ComboBox();
            comboCC = new ComboBox();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            groupBox2 = new GroupBox();
            comboUCDelay = new ComboBox();
            comboUCR = new ComboBox();
            comboUCV = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            comboOCDelay = new ComboBox();
            comboOCR = new ComboBox();
            comboOCV = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            label26 = new Label();
            groupBox4 = new GroupBox();
            lblFirmwareVersion = new Label();
            lblHardwareVersion = new Label();
            lblWarning = new Label();
            label38 = new Label();
            label36 = new Label();
            label34 = new Label();
            label33 = new Label();
            label31 = new Label();
            label29 = new Label();
            label27 = new Label();
            panelDischargeMos = new Panel();
            panelChargeDischarge = new Panel();
            panelUnderVoltage = new Panel();
            panelUnderTemp = new Panel();
            label32 = new Label();
            panelChargeMos = new Panel();
            label30 = new Label();
            panelOverCurrent = new Panel();
            label28 = new Label();
            panelOverVoltage = new Panel();
            label23 = new Label();
            panelOverTemp = new Panel();
            labelSoc = new Label();
            label20 = new Label();
            progressBarSoc = new ProgressBar();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStatusInfo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCellVoltages).BeginInit();
            groupBox17.SuspendLayout();
            groupBox16.SuspendLayout();
            groupBox5.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox15.SuspendLayout();
            groupBox14.SuspendLayout();
            groupBox13.SuspendLayout();
            groupBox12.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(711, 582);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dgvStatusInfo);
            tabPage1.Controls.Add(dgvCellVoltages);
            tabPage1.Controls.Add(groupBox17);
            tabPage1.Controls.Add(groupBox16);
            tabPage1.Controls.Add(groupBox5);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(703, 552);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "实时监控";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvStatusInfo
            // 
            dgvStatusInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStatusInfo.Columns.AddRange(new DataGridViewColumn[] { cm_Name1, cm_Value1 });
            dgvStatusInfo.Location = new Point(492, 7);
            dgvStatusInfo.Name = "dgvStatusInfo";
            dgvStatusInfo.RowHeadersVisible = false;
            dgvStatusInfo.Size = new Size(205, 537);
            dgvStatusInfo.TabIndex = 12;
            // 
            // cm_Name1
            // 
            cm_Name1.HeaderText = "名称";
            cm_Name1.Name = "cm_Name1";
            // 
            // cm_Value1
            // 
            cm_Value1.HeaderText = "当前值";
            cm_Value1.Name = "cm_Value1";
            // 
            // dgvCellVoltages
            // 
            dgvCellVoltages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCellVoltages.Columns.AddRange(new DataGridViewColumn[] { colBalance, cm_Name, cm_Value });
            dgvCellVoltages.Location = new Point(238, 6);
            dgvCellVoltages.Name = "dgvCellVoltages";
            dgvCellVoltages.RowHeadersVisible = false;
            dgvCellVoltages.Size = new Size(248, 538);
            dgvCellVoltages.TabIndex = 11;
            // 
            // colBalance
            // 
            colBalance.HeaderText = "均衡";
            colBalance.Name = "colBalance";
            colBalance.Width = 60;
            // 
            // cm_Name
            // 
            cm_Name.HeaderText = "名称";
            cm_Name.Name = "cm_Name";
            // 
            // cm_Value
            // 
            cm_Value.HeaderText = "当前值";
            cm_Value.Name = "cm_Value";
            // 
            // groupBox17
            // 
            groupBox17.Controls.Add(txtTotalCapacity);
            groupBox17.Controls.Add(txtBatteryCycles);
            groupBox17.Controls.Add(tx_Current);
            groupBox17.Controls.Add(tx_TotalVoltage);
            groupBox17.Controls.Add(label62);
            groupBox17.Controls.Add(label61);
            groupBox17.Controls.Add(label21);
            groupBox17.Controls.Add(label24);
            groupBox17.Location = new Point(6, 212);
            groupBox17.Name = "groupBox17";
            groupBox17.Size = new Size(226, 123);
            groupBox17.TabIndex = 10;
            groupBox17.TabStop = false;
            groupBox17.Text = "电池信息";
            // 
            // txtTotalCapacity
            // 
            txtTotalCapacity.Location = new Point(116, 95);
            txtTotalCapacity.Name = "txtTotalCapacity";
            txtTotalCapacity.ReadOnly = true;
            txtTotalCapacity.Size = new Size(100, 23);
            txtTotalCapacity.TabIndex = 4;
            // 
            // txtBatteryCycles
            // 
            txtBatteryCycles.Location = new Point(116, 68);
            txtBatteryCycles.Name = "txtBatteryCycles";
            txtBatteryCycles.ReadOnly = true;
            txtBatteryCycles.Size = new Size(100, 23);
            txtBatteryCycles.TabIndex = 4;
            // 
            // tx_Current
            // 
            tx_Current.Location = new Point(116, 42);
            tx_Current.Name = "tx_Current";
            tx_Current.ReadOnly = true;
            tx_Current.Size = new Size(100, 23);
            tx_Current.TabIndex = 4;
            // 
            // tx_TotalVoltage
            // 
            tx_TotalVoltage.Location = new Point(116, 16);
            tx_TotalVoltage.Name = "tx_TotalVoltage";
            tx_TotalVoltage.ReadOnly = true;
            tx_TotalVoltage.Size = new Size(100, 23);
            tx_TotalVoltage.TabIndex = 4;
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Location = new Point(38, 71);
            label62.Name = "label62";
            label62.Size = new Size(72, 17);
            label62.TabIndex = 3;
            label62.Text = "总容量(AH):";
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Location = new Point(39, 98);
            label61.Name = "label61";
            label61.Size = new Size(71, 17);
            label61.TabIndex = 3;
            label61.Text = "总循环次数:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(47, 19);
            label21.Name = "label21";
            label21.Size = new Size(63, 17);
            label21.TabIndex = 3;
            label21.Text = "总电压(V):";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(47, 45);
            label24.Name = "label24";
            label24.Size = new Size(63, 17);
            label24.TabIndex = 3;
            label24.Text = "总电流(A):";
            // 
            // groupBox16
            // 
            groupBox16.Controls.Add(comboSlaveAddress);
            groupBox16.Controls.Add(button11);
            groupBox16.Controls.Add(label60);
            groupBox16.Location = new Point(6, 158);
            groupBox16.Name = "groupBox16";
            groupBox16.Size = new Size(226, 48);
            groupBox16.TabIndex = 9;
            groupBox16.TabStop = false;
            groupBox16.Text = "地址选择";
            // 
            // comboSlaveAddress
            // 
            comboSlaveAddress.FormattingEnabled = true;
            comboSlaveAddress.Location = new Point(47, 16);
            comboSlaveAddress.Name = "comboSlaveAddress";
            comboSlaveAddress.Size = new Size(59, 25);
            comboSlaveAddress.TabIndex = 1;
            // 
            // button11
            // 
            button11.Location = new Point(122, 13);
            button11.Name = "button11";
            button11.Size = new Size(90, 29);
            button11.TabIndex = 4;
            button11.Text = "停止监控";
            button11.UseVisualStyleBackColor = true;
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.Location = new Point(6, 19);
            label60.Name = "label60";
            label60.Size = new Size(35, 17);
            label60.TabIndex = 0;
            label60.Text = "地址:";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(button10);
            groupBox5.Controls.Add(btnConnect);
            groupBox5.Controls.Add(comboBaud);
            groupBox5.Controls.Add(label25);
            groupBox5.Controls.Add(comboPort);
            groupBox5.Controls.Add(label);
            groupBox5.Location = new Point(6, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(226, 146);
            groupBox5.TabIndex = 8;
            groupBox5.TabStop = false;
            groupBox5.Text = "串口设置";
            // 
            // button10
            // 
            button10.Location = new Point(122, 105);
            button10.Name = "button10";
            button10.Size = new Size(92, 29);
            button10.TabIndex = 4;
            button10.Text = "关闭串口";
            button10.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(16, 105);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(90, 29);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "搜索设备";
            btnConnect.UseVisualStyleBackColor = true;
            // 
            // comboBaud
            // 
            comboBaud.FormattingEnabled = true;
            comboBaud.Items.AddRange(new object[] { "2400", "4800", "9600", "19200", "38400", "57600", "115200" });
            comboBaud.Location = new Point(57, 65);
            comboBaud.Name = "comboBaud";
            comboBaud.Size = new Size(157, 25);
            comboBaud.TabIndex = 3;
            comboBaud.Text = "9600";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(6, 68);
            label25.Name = "label25";
            label25.Size = new Size(47, 17);
            label25.TabIndex = 2;
            label25.Text = "波特率:";
            // 
            // comboPort
            // 
            comboPort.FormattingEnabled = true;
            comboPort.Location = new Point(57, 27);
            comboPort.Name = "comboPort";
            comboPort.Size = new Size(157, 25);
            comboPort.TabIndex = 1;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(6, 30);
            label.Name = "label";
            label.Size = new Size(35, 17);
            label.TabIndex = 0;
            label.Text = "串口:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnWriteConfig);
            tabPage2.Controls.Add(btnReadConfig);
            tabPage2.Controls.Add(groupBox15);
            tabPage2.Controls.Add(groupBox14);
            tabPage2.Controls.Add(groupBox13);
            tabPage2.Controls.Add(groupBox12);
            tabPage2.Controls.Add(groupBox11);
            tabPage2.Controls.Add(groupBox10);
            tabPage2.Controls.Add(groupBox9);
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(703, 552);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "参数配置";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnWriteConfig
            // 
            btnWriteConfig.Location = new Point(397, 468);
            btnWriteConfig.Name = "btnWriteConfig";
            btnWriteConfig.Size = new Size(93, 31);
            btnWriteConfig.TabIndex = 18;
            btnWriteConfig.Text = "写入参数";
            btnWriteConfig.UseVisualStyleBackColor = true;
            btnWriteConfig.Click += BtnWriteConfig_Click;
            // 
            // btnReadConfig
            // 
            btnReadConfig.Location = new Point(182, 468);
            btnReadConfig.Name = "btnReadConfig";
            btnReadConfig.Size = new Size(93, 31);
            btnReadConfig.TabIndex = 19;
            btnReadConfig.Text = "读取参数";
            btnReadConfig.UseVisualStyleBackColor = true;
            btnReadConfig.Click += BtnReadConfig_Click;
            // 
            // groupBox15
            // 
            groupBox15.Controls.Add(button9);
            groupBox15.Controls.Add(button8);
            groupBox15.Controls.Add(button6);
            groupBox15.Controls.Add(button4);
            groupBox15.Controls.Add(button7);
            groupBox15.Controls.Add(button5);
            groupBox15.Controls.Add(button3);
            groupBox15.Location = new Point(455, 292);
            groupBox15.Name = "groupBox15";
            groupBox15.Size = new Size(216, 151);
            groupBox15.TabIndex = 17;
            groupBox15.TabStop = false;
            groupBox15.Text = "命令测试";
            // 
            // button9
            // 
            button9.Location = new Point(58, 111);
            button9.Name = "button9";
            button9.Size = new Size(95, 23);
            button9.TabIndex = 1;
            button9.Text = "恢复出厂设置";
            button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(115, 82);
            button8.Name = "button8";
            button8.Size = new Size(96, 23);
            button8.TabIndex = 0;
            button8.Text = "电流归零";
            button8.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(115, 54);
            button6.Name = "button6";
            button6.Size = new Size(96, 23);
            button6.TabIndex = 0;
            button6.Text = "充放关";
            button6.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(115, 24);
            button4.Name = "button4";
            button4.Size = new Size(96, 23);
            button4.TabIndex = 0;
            button4.Text = "充关放开";
            button4.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(13, 82);
            button7.Name = "button7";
            button7.Size = new Size(96, 23);
            button7.TabIndex = 0;
            button7.Text = "退出手动模式";
            button7.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(13, 54);
            button5.Name = "button5";
            button5.Size = new Size(96, 23);
            button5.TabIndex = 0;
            button5.Text = "充放开";
            button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(13, 24);
            button3.Name = "button3";
            button3.Size = new Size(96, 23);
            button3.TabIndex = 0;
            button3.Text = "充开放关";
            button3.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            groupBox14.Controls.Add(button2);
            groupBox14.Controls.Add(button1);
            groupBox14.Controls.Add(richTextBox1);
            groupBox14.Controls.Add(comboFwBaudRate);
            groupBox14.Controls.Add(label59);
            groupBox14.Location = new Point(455, 96);
            groupBox14.Name = "groupBox14";
            groupBox14.Size = new Size(216, 190);
            groupBox14.TabIndex = 16;
            groupBox14.TabStop = false;
            groupBox14.Text = "程序下载";
            // 
            // button2
            // 
            button2.Location = new Point(105, 154);
            button2.Name = "button2";
            button2.Size = new Size(105, 23);
            button2.TabIndex = 3;
            button2.Text = "程序下载到BMS";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(6, 155);
            button1.Name = "button1";
            button1.Size = new Size(93, 23);
            button1.TabIndex = 3;
            button1.Text = "载入BIN文件";
            button1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(6, 47);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(204, 103);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "请选择BIN文件";
            // 
            // comboFwBaudRate
            // 
            comboFwBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            comboFwBaudRate.FormattingEnabled = true;
            comboFwBaudRate.Items.AddRange(new object[] { "9600" });
            comboFwBaudRate.Location = new Point(115, 16);
            comboFwBaudRate.Name = "comboFwBaudRate";
            comboFwBaudRate.Size = new Size(95, 25);
            comboFwBaudRate.TabIndex = 1;
            // 
            // label59
            // 
            label59.AutoSize = true;
            label59.Location = new Point(6, 19);
            label59.Name = "label59";
            label59.Size = new Size(83, 17);
            label59.TabIndex = 0;
            label59.Text = "端口下载速率:";
            // 
            // groupBox13
            // 
            groupBox13.Controls.Add(label58);
            groupBox13.Controls.Add(comboDischargeCalibration);
            groupBox13.Controls.Add(label57);
            groupBox13.Controls.Add(comboChargeCalibration);
            groupBox13.Location = new Point(455, 6);
            groupBox13.Name = "groupBox13";
            groupBox13.Size = new Size(216, 84);
            groupBox13.TabIndex = 15;
            groupBox13.TabStop = false;
            groupBox13.Text = "电流校正";
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.Location = new Point(13, 54);
            label58.Name = "label58";
            label58.Size = new Size(86, 17);
            label58.TabIndex = 0;
            label58.Text = "放电校正(mA):";
            // 
            // comboDischargeCalibration
            // 
            comboDischargeCalibration.FormattingEnabled = true;
            comboDischargeCalibration.Location = new Point(131, 51);
            comboDischargeCalibration.Name = "comboDischargeCalibration";
            comboDischargeCalibration.Size = new Size(79, 25);
            comboDischargeCalibration.TabIndex = 2;
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.Location = new Point(13, 28);
            label57.Name = "label57";
            label57.Size = new Size(86, 17);
            label57.TabIndex = 0;
            label57.Text = "充电校正(mA):";
            // 
            // comboChargeCalibration
            // 
            comboChargeCalibration.FormattingEnabled = true;
            comboChargeCalibration.Location = new Point(131, 20);
            comboChargeCalibration.Name = "comboChargeCalibration";
            comboChargeCalibration.Size = new Size(79, 25);
            comboChargeCalibration.TabIndex = 2;
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(label56);
            groupBox12.Controls.Add(comboMosOverTempProtect);
            groupBox12.Controls.Add(comboMosOverTempRestore);
            groupBox12.Controls.Add(label55);
            groupBox12.Location = new Point(223, 357);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(226, 86);
            groupBox12.TabIndex = 14;
            groupBox12.TabStop = false;
            groupBox12.Text = "MOS超温";
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.Location = new Point(30, 21);
            label56.Name = "label56";
            label56.Size = new Size(84, 17);
            label56.TabIndex = 4;
            label56.Text = "MOS超温(℃):";
            // 
            // comboMosOverTempProtect
            // 
            comboMosOverTempProtect.Font = new Font("Microsoft YaHei UI", 7F);
            comboMosOverTempProtect.FormattingEnabled = true;
            comboMosOverTempProtect.Location = new Point(120, 18);
            comboMosOverTempProtect.Name = "comboMosOverTempProtect";
            comboMosOverTempProtect.Size = new Size(94, 22);
            comboMosOverTempProtect.TabIndex = 1;
            // 
            // comboMosOverTempRestore
            // 
            comboMosOverTempRestore.Font = new Font("Microsoft YaHei UI", 7F);
            comboMosOverTempRestore.FormattingEnabled = true;
            comboMosOverTempRestore.Location = new Point(120, 42);
            comboMosOverTempRestore.Name = "comboMosOverTempRestore";
            comboMosOverTempRestore.Size = new Size(94, 22);
            comboMosOverTempRestore.TabIndex = 1;
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Location = new Point(6, 46);
            label55.Name = "label55";
            label55.Size = new Size(108, 17);
            label55.TabIndex = 9;
            label55.Text = "MOS超温恢复(℃):";
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(label11);
            groupBox11.Controls.Add(label10);
            groupBox11.Controls.Add(label12);
            groupBox11.Controls.Add(label13);
            groupBox11.Controls.Add(comboDischargeUnderTempRestore);
            groupBox11.Controls.Add(comboDischargeUnderTempProtect);
            groupBox11.Controls.Add(comboDischargeOverTempRestore);
            groupBox11.Controls.Add(comboDischargeOverTempProtect);
            groupBox11.Controls.Add(comboChargeUnderTempRestore);
            groupBox11.Controls.Add(comboChargeUnderTempProtect);
            groupBox11.Controls.Add(comboChargeOverTempRestore);
            groupBox11.Controls.Add(comboChargeOverTempProtect);
            groupBox11.Controls.Add(label51);
            groupBox11.Controls.Add(label54);
            groupBox11.Location = new Point(223, 210);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(226, 141);
            groupBox11.TabIndex = 13;
            groupBox11.TabStop = false;
            groupBox11.Text = "温度保护";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(164, 19);
            label11.Name = "label11";
            label11.Size = new Size(32, 17);
            label11.TabIndex = 0;
            label11.Text = "放电";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(89, 19);
            label10.Name = "label10";
            label10.Size = new Size(32, 17);
            label10.TabIndex = 0;
            label10.Text = "充电";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(30, 43);
            label12.Name = "label12";
            label12.Size = new Size(55, 17);
            label12.TabIndex = 4;
            label12.Text = "高温(℃):";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 68);
            label13.Name = "label13";
            label13.Size = new Size(79, 17);
            label13.TabIndex = 9;
            label13.Text = "高温恢复(℃):";
            // 
            // comboDischargeUnderTempRestore
            // 
            comboDischargeUnderTempRestore.Font = new Font("Microsoft YaHei UI", 7F);
            comboDischargeUnderTempRestore.FormattingEnabled = true;
            comboDischargeUnderTempRestore.Location = new Point(151, 112);
            comboDischargeUnderTempRestore.Name = "comboDischargeUnderTempRestore";
            comboDischargeUnderTempRestore.Size = new Size(56, 22);
            comboDischargeUnderTempRestore.TabIndex = 1;
            // 
            // comboDischargeUnderTempProtect
            // 
            comboDischargeUnderTempProtect.Font = new Font("Microsoft YaHei UI", 7F);
            comboDischargeUnderTempProtect.FormattingEnabled = true;
            comboDischargeUnderTempProtect.Location = new Point(151, 87);
            comboDischargeUnderTempProtect.Name = "comboDischargeUnderTempProtect";
            comboDischargeUnderTempProtect.Size = new Size(56, 22);
            comboDischargeUnderTempProtect.TabIndex = 1;
            // 
            // comboDischargeOverTempRestore
            // 
            comboDischargeOverTempRestore.Font = new Font("Microsoft YaHei UI", 7F);
            comboDischargeOverTempRestore.FormattingEnabled = true;
            comboDischargeOverTempRestore.Location = new Point(151, 63);
            comboDischargeOverTempRestore.Name = "comboDischargeOverTempRestore";
            comboDischargeOverTempRestore.Size = new Size(56, 22);
            comboDischargeOverTempRestore.TabIndex = 1;
            // 
            // comboDischargeOverTempProtect
            // 
            comboDischargeOverTempProtect.Font = new Font("Microsoft YaHei UI", 7F);
            comboDischargeOverTempProtect.FormattingEnabled = true;
            comboDischargeOverTempProtect.Location = new Point(151, 39);
            comboDischargeOverTempProtect.Name = "comboDischargeOverTempProtect";
            comboDischargeOverTempProtect.Size = new Size(56, 22);
            comboDischargeOverTempProtect.TabIndex = 1;
            // 
            // comboChargeUnderTempRestore
            // 
            comboChargeUnderTempRestore.Font = new Font("Microsoft YaHei UI", 7F);
            comboChargeUnderTempRestore.FormattingEnabled = true;
            comboChargeUnderTempRestore.Location = new Point(89, 112);
            comboChargeUnderTempRestore.Name = "comboChargeUnderTempRestore";
            comboChargeUnderTempRestore.Size = new Size(56, 22);
            comboChargeUnderTempRestore.TabIndex = 1;
            // 
            // comboChargeUnderTempProtect
            // 
            comboChargeUnderTempProtect.Font = new Font("Microsoft YaHei UI", 7F);
            comboChargeUnderTempProtect.FormattingEnabled = true;
            comboChargeUnderTempProtect.Location = new Point(89, 87);
            comboChargeUnderTempProtect.Name = "comboChargeUnderTempProtect";
            comboChargeUnderTempProtect.Size = new Size(56, 22);
            comboChargeUnderTempProtect.TabIndex = 1;
            // 
            // comboChargeOverTempRestore
            // 
            comboChargeOverTempRestore.Font = new Font("Microsoft YaHei UI", 7F);
            comboChargeOverTempRestore.FormattingEnabled = true;
            comboChargeOverTempRestore.Location = new Point(89, 63);
            comboChargeOverTempRestore.Name = "comboChargeOverTempRestore";
            comboChargeOverTempRestore.Size = new Size(56, 22);
            comboChargeOverTempRestore.TabIndex = 1;
            // 
            // comboChargeOverTempProtect
            // 
            comboChargeOverTempProtect.Font = new Font("Microsoft YaHei UI", 7F);
            comboChargeOverTempProtect.FormattingEnabled = true;
            comboChargeOverTempProtect.Location = new Point(89, 39);
            comboChargeOverTempProtect.Name = "comboChargeOverTempProtect";
            comboChargeOverTempProtect.Size = new Size(56, 22);
            comboChargeOverTempProtect.TabIndex = 1;
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.Location = new Point(30, 92);
            label51.Name = "label51";
            label51.Size = new Size(55, 17);
            label51.TabIndex = 6;
            label51.Text = "低温(℃):";
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.Location = new Point(6, 117);
            label54.Name = "label54";
            label54.Size = new Size(79, 17);
            label54.TabIndex = 5;
            label54.Text = "低温恢复(℃):";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(comboFunctionParamSet);
            groupBox10.Controls.Add(label9);
            groupBox10.Controls.Add(comboSelfConsumptionPower);
            groupBox10.Controls.Add(label8);
            groupBox10.Controls.Add(comboSleepLeakageCurrent);
            groupBox10.Controls.Add(label7);
            groupBox10.Location = new Point(223, 96);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(226, 108);
            groupBox10.TabIndex = 12;
            groupBox10.TabStop = false;
            groupBox10.Text = "其他";
            // 
            // comboFunctionParamSet
            // 
            comboFunctionParamSet.FormattingEnabled = true;
            comboFunctionParamSet.Location = new Point(136, 73);
            comboFunctionParamSet.Name = "comboFunctionParamSet";
            comboFunctionParamSet.Size = new Size(78, 25);
            comboFunctionParamSet.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(33, 76);
            label9.Name = "label9";
            label9.Size = new Size(83, 17);
            label9.TabIndex = 0;
            label9.Text = "功能参数设置:";
            // 
            // comboSelfConsumptionPower
            // 
            comboSelfConsumptionPower.FormattingEnabled = true;
            comboSelfConsumptionPower.Location = new Point(136, 42);
            comboSelfConsumptionPower.Name = "comboSelfConsumptionPower";
            comboSelfConsumptionPower.Size = new Size(78, 25);
            comboSelfConsumptionPower.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(42, 45);
            label8.Name = "label8";
            label8.Size = new Size(74, 17);
            label8.TabIndex = 0;
            label8.Text = "自耗电(mA):";
            // 
            // comboSleepLeakageCurrent
            // 
            comboSleepLeakageCurrent.FormattingEnabled = true;
            comboSleepLeakageCurrent.Location = new Point(136, 11);
            comboSleepLeakageCurrent.Name = "comboSleepLeakageCurrent";
            comboSleepLeakageCurrent.Size = new Size(78, 25);
            comboSleepLeakageCurrent.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(30, 19);
            label7.Name = "label7";
            label7.Size = new Size(86, 17);
            label7.TabIndex = 0;
            label7.Text = "休眠电流(mA):";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(label53);
            groupBox9.Controls.Add(comboBALP);
            groupBox9.Controls.Add(label52);
            groupBox9.Controls.Add(comboBALV);
            groupBox9.Location = new Point(223, 6);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(226, 84);
            groupBox9.TabIndex = 11;
            groupBox9.TabStop = false;
            groupBox9.Text = "均衡参数";
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.Location = new Point(6, 54);
            label53.Name = "label53";
            label53.Size = new Size(110, 17);
            label53.TabIndex = 1;
            label53.Text = "均衡开启压差(mV):";
            // 
            // comboBALP
            // 
            comboBALP.FormattingEnabled = true;
            comboBALP.Location = new Point(116, 51);
            comboBALP.Name = "comboBALP";
            comboBALP.Size = new Size(98, 25);
            comboBALP.TabIndex = 2;
            // 
            // label52
            // 
            label52.AutoSize = true;
            label52.Location = new Point(6, 23);
            label52.Name = "label52";
            label52.Size = new Size(110, 17);
            label52.TabIndex = 1;
            label52.Text = "均衡开启电压(mV):";
            // 
            // comboBALV
            // 
            comboBALV.FormattingEnabled = true;
            comboBALV.Location = new Point(116, 20);
            comboBALV.Name = "comboBALV";
            comboBALV.Size = new Size(98, 25);
            comboBALV.TabIndex = 2;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(comboDC2Delay);
            groupBox3.Controls.Add(comboDC2);
            groupBox3.Controls.Add(comboDC1Delay);
            groupBox3.Controls.Add(comboDC1);
            groupBox3.Controls.Add(comboCCDelay);
            groupBox3.Controls.Add(comboCC);
            groupBox3.Controls.Add(label19);
            groupBox3.Controls.Add(label18);
            groupBox3.Controls.Add(label17);
            groupBox3.Controls.Add(label16);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(label14);
            groupBox3.Location = new Point(8, 235);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(209, 208);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "过流保护";
            // 
            // comboDC2Delay
            // 
            comboDC2Delay.FormattingEnabled = true;
            comboDC2Delay.Location = new Point(123, 171);
            comboDC2Delay.Name = "comboDC2Delay";
            comboDC2Delay.Size = new Size(78, 25);
            comboDC2Delay.TabIndex = 10;
            // 
            // comboDC2
            // 
            comboDC2.FormattingEnabled = true;
            comboDC2.Location = new Point(123, 140);
            comboDC2.Name = "comboDC2";
            comboDC2.Size = new Size(78, 25);
            comboDC2.TabIndex = 10;
            // 
            // comboDC1Delay
            // 
            comboDC1Delay.FormattingEnabled = true;
            comboDC1Delay.Location = new Point(123, 109);
            comboDC1Delay.Name = "comboDC1Delay";
            comboDC1Delay.Size = new Size(78, 25);
            comboDC1Delay.TabIndex = 10;
            // 
            // comboDC1
            // 
            comboDC1.FormattingEnabled = true;
            comboDC1.Location = new Point(121, 78);
            comboDC1.Name = "comboDC1";
            comboDC1.Size = new Size(78, 25);
            comboDC1.TabIndex = 10;
            // 
            // comboCCDelay
            // 
            comboCCDelay.FormattingEnabled = true;
            comboCCDelay.Location = new Point(121, 47);
            comboCCDelay.Name = "comboCCDelay";
            comboCCDelay.Size = new Size(78, 25);
            comboCCDelay.TabIndex = 10;
            // 
            // comboCC
            // 
            comboCC.FormattingEnabled = true;
            comboCC.Location = new Point(121, 16);
            comboCC.Name = "comboCC";
            comboCC.Size = new Size(78, 25);
            comboCC.TabIndex = 10;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(4, 179);
            label19.Name = "label19";
            label19.Size = new Size(104, 17);
            label19.TabIndex = 7;
            label19.Text = "放电过流2延时(s):";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(15, 148);
            label18.Name = "label18";
            label18.Size = new Size(93, 17);
            label18.TabIndex = 8;
            label18.Text = "放电过流2(mA):";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(7, 117);
            label17.Name = "label17";
            label17.Size = new Size(104, 17);
            label17.TabIndex = 5;
            label17.Text = "放电过流1延时(s):";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(18, 81);
            label16.Name = "label16";
            label16.Size = new Size(93, 17);
            label16.TabIndex = 6;
            label16.Text = "放电过流1(mA):";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(18, 50);
            label15.Name = "label15";
            label15.Size = new Size(97, 17);
            label15.TabIndex = 9;
            label15.Text = "充电过流延时(s):";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(29, 19);
            label14.Name = "label14";
            label14.Size = new Size(86, 17);
            label14.TabIndex = 4;
            label14.Text = "充电过流(mA):";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(comboUCDelay);
            groupBox2.Controls.Add(comboUCR);
            groupBox2.Controls.Add(comboUCV);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(6, 120);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(211, 109);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "过放保护";
            // 
            // comboUCDelay
            // 
            comboUCDelay.FormattingEnabled = true;
            comboUCDelay.Location = new Point(123, 78);
            comboUCDelay.Name = "comboUCDelay";
            comboUCDelay.Size = new Size(78, 25);
            comboUCDelay.TabIndex = 1;
            // 
            // comboUCR
            // 
            comboUCR.FormattingEnabled = true;
            comboUCR.Location = new Point(123, 47);
            comboUCR.Name = "comboUCR";
            comboUCR.Size = new Size(78, 25);
            comboUCR.TabIndex = 1;
            // 
            // comboUCV
            // 
            comboUCV.FormattingEnabled = true;
            comboUCV.Location = new Point(123, 16);
            comboUCV.Name = "comboUCV";
            comboUCV.Size = new Size(78, 25);
            comboUCV.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(44, 81);
            label6.Name = "label6";
            label6.Size = new Size(73, 17);
            label6.TabIndex = 0;
            label6.Text = "过放延时(s):";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 53);
            label5.Name = "label5";
            label5.Size = new Size(110, 17);
            label5.TabIndex = 0;
            label5.Text = "过放恢复电压(mV):";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(54, 19);
            label4.Name = "label4";
            label4.Size = new Size(59, 17);
            label4.TabIndex = 0;
            label4.Text = "过放电压:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 46);
            label3.Name = "label3";
            label3.Size = new Size(0, 17);
            label3.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboOCDelay);
            groupBox1.Controls.Add(comboOCR);
            groupBox1.Controls.Add(comboOCV);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label26);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(211, 108);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "过充保护";
            // 
            // comboOCDelay
            // 
            comboOCDelay.FormattingEnabled = true;
            comboOCDelay.Location = new Point(123, 78);
            comboOCDelay.Name = "comboOCDelay";
            comboOCDelay.Size = new Size(78, 25);
            comboOCDelay.TabIndex = 1;
            // 
            // comboOCR
            // 
            comboOCR.FormattingEnabled = true;
            comboOCR.Location = new Point(123, 47);
            comboOCR.Name = "comboOCR";
            comboOCR.Size = new Size(78, 25);
            comboOCR.TabIndex = 1;
            // 
            // comboOCV
            // 
            comboOCV.FormattingEnabled = true;
            comboOCV.Location = new Point(123, 16);
            comboOCV.Name = "comboOCV";
            comboOCV.Size = new Size(78, 25);
            comboOCV.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 50);
            label2.Name = "label2";
            label2.Size = new Size(110, 17);
            label2.TabIndex = 0;
            label2.Text = "过充恢复电压(mV):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 81);
            label1.Name = "label1";
            label1.Size = new Size(73, 17);
            label1.TabIndex = 0;
            label1.Text = "过充延时(s):";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(51, 24);
            label26.Name = "label26";
            label26.Size = new Size(59, 17);
            label26.TabIndex = 0;
            label26.Text = "过充电压:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(lblFirmwareVersion);
            groupBox4.Controls.Add(lblHardwareVersion);
            groupBox4.Controls.Add(lblWarning);
            groupBox4.Controls.Add(label38);
            groupBox4.Controls.Add(label36);
            groupBox4.Controls.Add(label34);
            groupBox4.Controls.Add(label33);
            groupBox4.Controls.Add(label31);
            groupBox4.Controls.Add(label29);
            groupBox4.Controls.Add(label27);
            groupBox4.Controls.Add(panelDischargeMos);
            groupBox4.Controls.Add(panelChargeDischarge);
            groupBox4.Controls.Add(panelUnderVoltage);
            groupBox4.Controls.Add(panelUnderTemp);
            groupBox4.Controls.Add(label32);
            groupBox4.Controls.Add(panelChargeMos);
            groupBox4.Controls.Add(label30);
            groupBox4.Controls.Add(panelOverCurrent);
            groupBox4.Controls.Add(label28);
            groupBox4.Controls.Add(panelOverVoltage);
            groupBox4.Controls.Add(label23);
            groupBox4.Controls.Add(panelOverTemp);
            groupBox4.Controls.Add(labelSoc);
            groupBox4.Controls.Add(label20);
            groupBox4.Controls.Add(progressBarSoc);
            groupBox4.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            groupBox4.Location = new Point(720, 28);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(215, 552);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "状态";
            // 
            // lblFirmwareVersion
            // 
            lblFirmwareVersion.AutoSize = true;
            lblFirmwareVersion.Font = new Font("Microsoft YaHei UI", 9F);
            lblFirmwareVersion.Location = new Point(108, 502);
            lblFirmwareVersion.Name = "lblFirmwareVersion";
            lblFirmwareVersion.Size = new Size(18, 17);
            lblFirmwareVersion.TabIndex = 5;
            lblFirmwareVersion.Text = "--";
            // 
            // lblHardwareVersion
            // 
            lblHardwareVersion.AutoSize = true;
            lblHardwareVersion.Font = new Font("Microsoft YaHei UI", 9F);
            lblHardwareVersion.Location = new Point(108, 475);
            lblHardwareVersion.Name = "lblHardwareVersion";
            lblHardwareVersion.Size = new Size(18, 17);
            lblHardwareVersion.TabIndex = 5;
            lblHardwareVersion.Text = "--";
            // 
            // lblWarning
            // 
            lblWarning.AutoSize = true;
            lblWarning.Font = new Font("Microsoft YaHei UI", 9F);
            lblWarning.Location = new Point(81, 338);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(20, 17);
            lblWarning.TabIndex = 5;
            lblWarning.Text = "无";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font("Microsoft YaHei UI", 9F);
            label38.Location = new Point(43, 502);
            label38.Name = "label38";
            label38.Size = new Size(59, 17);
            label38.TabIndex = 5;
            label38.Text = "软件版本:";
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("Microsoft YaHei UI", 9F);
            label36.Location = new Point(43, 475);
            label36.Name = "label36";
            label36.Size = new Size(59, 17);
            label36.TabIndex = 5;
            label36.Text = "硬件版本:";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Font = new Font("Microsoft YaHei UI", 9F);
            label34.Location = new Point(43, 338);
            label34.Name = "label34";
            label34.Size = new Size(35, 17);
            label34.TabIndex = 5;
            label34.Text = "告警:";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("Microsoft YaHei UI", 9F);
            label33.Location = new Point(59, 307);
            label33.Name = "label33";
            label33.Size = new Size(61, 17);
            label33.TabIndex = 4;
            label33.Text = "放电MOS";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("Microsoft YaHei UI", 9F);
            label31.Location = new Point(59, 260);
            label31.Name = "label31";
            label31.Size = new Size(49, 17);
            label31.TabIndex = 4;
            label31.Text = "充/放电";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Microsoft YaHei UI", 9F);
            label29.Location = new Point(59, 212);
            label29.Name = "label29";
            label29.Size = new Size(32, 17);
            label29.TabIndex = 4;
            label29.Text = "欠压";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Microsoft YaHei UI", 9F);
            label27.Location = new Point(59, 166);
            label27.Name = "label27";
            label27.Size = new Size(32, 17);
            label27.TabIndex = 4;
            label27.Text = "欠温";
            // 
            // panelDischargeMos
            // 
            panelDischargeMos.BackColor = Color.FromArgb(255, 255, 128);
            panelDischargeMos.Location = new Point(43, 312);
            panelDischargeMos.Name = "panelDischargeMos";
            panelDischargeMos.Size = new Size(10, 10);
            panelDischargeMos.TabIndex = 3;
            // 
            // panelChargeDischarge
            // 
            panelChargeDischarge.BackColor = Color.FromArgb(255, 255, 128);
            panelChargeDischarge.Location = new Point(43, 265);
            panelChargeDischarge.Name = "panelChargeDischarge";
            panelChargeDischarge.Size = new Size(10, 10);
            panelChargeDischarge.TabIndex = 3;
            // 
            // panelUnderVoltage
            // 
            panelUnderVoltage.BackColor = Color.FromArgb(255, 255, 128);
            panelUnderVoltage.Location = new Point(43, 217);
            panelUnderVoltage.Name = "panelUnderVoltage";
            panelUnderVoltage.Size = new Size(10, 10);
            panelUnderVoltage.TabIndex = 3;
            // 
            // panelUnderTemp
            // 
            panelUnderTemp.BackColor = Color.FromArgb(255, 255, 128);
            panelUnderTemp.Location = new Point(43, 171);
            panelUnderTemp.Name = "panelUnderTemp";
            panelUnderTemp.Size = new Size(10, 10);
            panelUnderTemp.TabIndex = 3;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Microsoft YaHei UI", 9F);
            label32.Location = new Point(59, 282);
            label32.Name = "label32";
            label32.Size = new Size(61, 17);
            label32.TabIndex = 4;
            label32.Text = "充电MOS";
            // 
            // panelChargeMos
            // 
            panelChargeMos.BackColor = Color.FromArgb(255, 255, 128);
            panelChargeMos.Location = new Point(43, 287);
            panelChargeMos.Name = "panelChargeMos";
            panelChargeMos.Size = new Size(10, 10);
            panelChargeMos.TabIndex = 3;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Microsoft YaHei UI", 9F);
            label30.Location = new Point(59, 235);
            label30.Name = "label30";
            label30.Size = new Size(32, 17);
            label30.TabIndex = 4;
            label30.Text = "过流";
            // 
            // panelOverCurrent
            // 
            panelOverCurrent.BackColor = Color.FromArgb(255, 255, 128);
            panelOverCurrent.Location = new Point(43, 240);
            panelOverCurrent.Name = "panelOverCurrent";
            panelOverCurrent.Size = new Size(10, 10);
            panelOverCurrent.TabIndex = 3;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Microsoft YaHei UI", 9F);
            label28.Location = new Point(59, 187);
            label28.Name = "label28";
            label28.Size = new Size(32, 17);
            label28.TabIndex = 4;
            label28.Text = "过压";
            // 
            // panelOverVoltage
            // 
            panelOverVoltage.BackColor = Color.FromArgb(255, 255, 128);
            panelOverVoltage.Location = new Point(43, 192);
            panelOverVoltage.Name = "panelOverVoltage";
            panelOverVoltage.Size = new Size(10, 10);
            panelOverVoltage.TabIndex = 3;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Microsoft YaHei UI", 9F);
            label23.Location = new Point(59, 141);
            label23.Name = "label23";
            label23.Size = new Size(32, 17);
            label23.TabIndex = 4;
            label23.Text = "过温";
            // 
            // panelOverTemp
            // 
            panelOverTemp.BackColor = Color.FromArgb(255, 255, 128);
            panelOverTemp.Location = new Point(43, 146);
            panelOverTemp.Name = "panelOverTemp";
            panelOverTemp.Size = new Size(10, 10);
            panelOverTemp.TabIndex = 3;
            // 
            // labelSoc
            // 
            labelSoc.AutoSize = true;
            labelSoc.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            labelSoc.ForeColor = Color.FromArgb(0, 192, 0);
            labelSoc.Location = new Point(81, 84);
            labelSoc.Name = "labelSoc";
            labelSoc.Size = new Size(23, 17);
            labelSoc.TabIndex = 2;
            labelSoc.Text = "---";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(40, 84);
            label20.Name = "label20";
            label20.Size = new Size(35, 17);
            label20.TabIndex = 2;
            label20.Text = "电量:";
            // 
            // progressBarSoc
            // 
            progressBarSoc.Location = new Point(40, 34);
            progressBarSoc.Name = "progressBarSoc";
            progressBarSoc.Size = new Size(130, 30);
            progressBarSoc.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(947, 584);
            Controls.Add(groupBox4);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "BMS 监控系统 V1.0";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStatusInfo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCellVoltages).EndInit();
            groupBox17.ResumeLayout(false);
            groupBox17.PerformLayout();
            groupBox16.ResumeLayout(false);
            groupBox16.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox15.ResumeLayout(false);
            groupBox14.ResumeLayout(false);
            groupBox14.PerformLayout();
            groupBox13.ResumeLayout(false);
            groupBox13.PerformLayout();
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            groupBox11.ResumeLayout(false);
            groupBox11.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label24;
        private Label label21;
        private GroupBox groupBox9;
        private Label label53;
        private ComboBox comboBALP;
        private Label label52;
        private ComboBox comboBALV;
        private GroupBox groupBox3;
        private ComboBox comboDC2Delay;
        private ComboBox comboDC2;
        private ComboBox comboDC1Delay;
        private ComboBox comboDC1;
        private ComboBox comboCCDelay;
        private ComboBox comboCC;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private GroupBox groupBox2;
        private ComboBox comboUCDelay;
        private ComboBox comboUCR;
        private ComboBox comboUCV;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private GroupBox groupBox1;
        private ComboBox comboOCDelay;
        private ComboBox comboOCR;
        private ComboBox comboOCV;
        private Label label2;
        private Label label1;
        private Label label26;
        private GroupBox groupBox11;
        private Label label11;
        private Label label10;
        private Label label12;
        private Label label13;
        private Label label51;
        private Label label54;
        private GroupBox groupBox10;
        private ComboBox comboFunctionParamSet;
        private Label label9;
        private ComboBox comboSelfConsumptionPower;
        private Label label8;
        private ComboBox comboSleepLeakageCurrent;
        private Label label7;
        private ComboBox comboDischargeUnderTempRestore;
        private ComboBox comboDischargeUnderTempProtect;
        private ComboBox comboDischargeOverTempRestore;
        private ComboBox comboDischargeOverTempProtect;
        private ComboBox comboChargeUnderTempRestore;
        private ComboBox comboChargeUnderTempProtect;
        private ComboBox comboChargeOverTempRestore;
        private ComboBox comboChargeOverTempProtect;
        private GroupBox groupBox13;
        private Label label57;
        private GroupBox groupBox12;
        private Label label56;
        private ComboBox comboMosOverTempProtect;
        private ComboBox comboMosOverTempRestore;
        private Label label55;
        private GroupBox groupBox14;
        private ComboBox comboFwBaudRate;
        private Label label59;
        private Label label58;
        private ComboBox comboDischargeCalibration;
        private ComboBox comboChargeCalibration;
        private RichTextBox richTextBox1;
        private GroupBox groupBox15;
        private Button button8;
        private Button button6;
        private Button button4;
        private Button button7;
        private Button button5;
        private Button button3;
        private Button button2;
        private Button button1;
        private GroupBox groupBox16;
        private ComboBox comboSlaveAddress;
        private Button button11;
        private Label label60;
        private GroupBox groupBox5;
        private Button button10;
        private Button btnConnect;
        private ComboBox comboBaud;
        private Label label25;
        private ComboBox comboPort;
        private Label label;
        private Button button9;
        private GroupBox groupBox17;
        private Label label62;
        private Label label61;
        private TextBox txtTotalCapacity;
        private TextBox txtBatteryCycles;
        private TextBox tx_Current;
        private TextBox tx_TotalVoltage;
        private Button btnWriteConfig;
        private Button btnReadConfig;
        private GroupBox groupBox4;
        private Label labelSoc;
        private Label label20;
        private ProgressBar progressBarSoc;
        private Label label31;
        private Label label29;
        private Label label27;
        private Panel panelChargeDischarge;
        private Panel panelUnderVoltage;
        private Panel panelUnderTemp;
        private Label label30;
        private Panel panelOverCurrent;
        private Label label28;
        private Panel panelOverVoltage;
        private Label label23;
        private Panel panelOverTemp;
        private DataGridView dgvCellVoltages;
        private Label lblFirmwareVersion;
        private Label lblHardwareVersion;
        private Label lblWarning;
        private Label label38;
        private Label label36;
        private Label label34;
        private Label label33;
        private Panel panelDischargeMos;
        private Label label32;
        private Panel panelChargeMos;
        private DataGridView dgvStatusInfo;
        private DataGridViewTextBoxColumn cm_Name1;
        private DataGridViewTextBoxColumn cm_Value1;
        private DataGridViewTextBoxColumn colBalance;
        private DataGridViewTextBoxColumn cm_Name;
        private DataGridViewTextBoxColumn cm_Value;
    }
}

﻿namespace Capa_Presentacion
{
    partial class Cosechas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExportar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvCultivo = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.IDCultivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cultivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Plantado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cosecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscarUnCultivo = new System.Windows.Forms.TextBox();
            this.dtpPlantado = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCultivo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(144)))));
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.ForeColor = System.Drawing.Color.Black;
            this.btnExportar.Location = new System.Drawing.Point(529, 51);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(155, 34);
            this.btnExportar.TabIndex = 68;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 25);
            this.label1.TabIndex = 64;
            this.label1.Text = "Administrar Cosechas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 65;
            this.label2.Text = "Buscar un cultivo";
            // 
            // dgvCultivo
            // 
            this.dgvCultivo.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvCultivo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCultivo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCultivo.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvCultivo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCultivo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCultivo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCultivo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCultivo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDCultivo,
            this.Usuario,
            this.Cultivo,
            this.Plantado,
            this.Cosecha,
            this.Cantidad,
            this.Estado});
            this.dgvCultivo.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCultivo.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCultivo.DoubleBuffered = true;
            this.dgvCultivo.EnableHeadersVisualStyles = false;
            this.dgvCultivo.HeaderBgColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCultivo.HeaderForeColor = System.Drawing.Color.DimGray;
            this.dgvCultivo.Location = new System.Drawing.Point(34, 96);
            this.dgvCultivo.MultiSelect = false;
            this.dgvCultivo.Name = "dgvCultivo";
            this.dgvCultivo.ReadOnly = true;
            this.dgvCultivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvCultivo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCultivo.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCultivo.RowHeadersVisible = false;
            this.dgvCultivo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCultivo.Size = new System.Drawing.Size(650, 467);
            this.dgvCultivo.TabIndex = 67;
            // 
            // IDCultivo
            // 
            this.IDCultivo.DataPropertyName = "IDCultivo";
            this.IDCultivo.HeaderText = "IDCultivo";
            this.IDCultivo.Name = "IDCultivo";
            this.IDCultivo.ReadOnly = true;
            // 
            // Usuario
            // 
            this.Usuario.DataPropertyName = "Usuario_Cultivo";
            this.Usuario.HeaderText = "Usuario";
            this.Usuario.Name = "Usuario";
            this.Usuario.ReadOnly = true;
            // 
            // Cultivo
            // 
            this.Cultivo.DataPropertyName = "Cultivo";
            this.Cultivo.HeaderText = "Cultivo";
            this.Cultivo.Name = "Cultivo";
            this.Cultivo.ReadOnly = true;
            this.Cultivo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Plantado
            // 
            this.Plantado.DataPropertyName = "Fecha_Plantado";
            dataGridViewCellStyle3.Format = "D";
            dataGridViewCellStyle3.NullValue = null;
            this.Plantado.DefaultCellStyle = dataGridViewCellStyle3;
            this.Plantado.HeaderText = "Fecha Plantado";
            this.Plantado.Name = "Plantado";
            this.Plantado.ReadOnly = true;
            // 
            // Cosecha
            // 
            this.Cosecha.DataPropertyName = "Fecha_Cosecha";
            dataGridViewCellStyle4.Format = "D";
            dataGridViewCellStyle4.NullValue = null;
            this.Cosecha.DefaultCellStyle = dataGridViewCellStyle4;
            this.Cosecha.HeaderText = "Fecha Cosecha";
            this.Cosecha.Name = "Cosecha";
            this.Cosecha.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            // 
            // txtBuscarUnCultivo
            // 
            this.txtBuscarUnCultivo.Location = new System.Drawing.Point(207, 65);
            this.txtBuscarUnCultivo.Name = "txtBuscarUnCultivo";
            this.txtBuscarUnCultivo.Size = new System.Drawing.Size(301, 20);
            this.txtBuscarUnCultivo.TabIndex = 66;
            this.txtBuscarUnCultivo.TextChanged += new System.EventHandler(this.txtBuscarUnCultivo_TextChanged);
            // 
            // dtpPlantado
            // 
            this.dtpPlantado.CustomFormat = "";
            this.dtpPlantado.Location = new System.Drawing.Point(207, 590);
            this.dtpPlantado.Name = "dtpPlantado";
            this.dtpPlantado.Size = new System.Drawing.Size(205, 20);
            this.dtpPlantado.TabIndex = 69;
            this.dtpPlantado.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // Cosechas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 681);
            this.Controls.Add(this.dtpPlantado);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvCultivo);
            this.Controls.Add(this.txtBuscarUnCultivo);
            this.Name = "Cosechas";
            this.Text = "Cosechas";
            this.Load += new System.EventHandler(this.Cosechas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCultivo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuCustomDataGrid dgvCultivo;
        private System.Windows.Forms.TextBox txtBuscarUnCultivo;
        private System.Windows.Forms.DateTimePicker dtpPlantado;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCultivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cultivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plantado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cosecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
    }
}
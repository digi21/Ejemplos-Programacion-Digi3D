namespace DigiNG.IO.Gps
{
    partial class FormularioGps
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
            this.botonConectar = new System.Windows.Forms.Button();
            this.botonDato = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLongitud = new System.Windows.Forms.TextBox();
            this.txtLatitud = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAltitud = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.botonTentativo = new System.Windows.Forms.Button();
            this.botonCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // botonConectar
            // 
            this.botonConectar.Location = new System.Drawing.Point(13, 13);
            this.botonConectar.Name = "botonConectar";
            this.botonConectar.Size = new System.Drawing.Size(181, 23);
            this.botonConectar.TabIndex = 0;
            this.botonConectar.Text = "Conectar con GPS";
            this.botonConectar.UseVisualStyleBackColor = true;
            this.botonConectar.Click += new System.EventHandler(this.botonConectar_Click);
            // 
            // botonDato
            // 
            this.botonDato.Appearance = System.Windows.Forms.Appearance.Button;
            this.botonDato.AutoSize = true;
            this.botonDato.Location = new System.Drawing.Point(13, 42);
            this.botonDato.Name = "botonDato";
            this.botonDato.Size = new System.Drawing.Size(40, 23);
            this.botonDato.TabIndex = 2;
            this.botonDato.Text = "Dato";
            this.botonDato.UseVisualStyleBackColor = true;
            this.botonDato.CheckedChanged += new System.EventHandler(this.botonDato_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Longitud";
            // 
            // txtLongitud
            // 
            this.txtLongitud.Location = new System.Drawing.Point(68, 72);
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.ReadOnly = true;
            this.txtLongitud.Size = new System.Drawing.Size(126, 20);
            this.txtLongitud.TabIndex = 6;
            // 
            // txtLatitud
            // 
            this.txtLatitud.Location = new System.Drawing.Point(68, 97);
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.ReadOnly = true;
            this.txtLatitud.Size = new System.Drawing.Size(126, 20);
            this.txtLatitud.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Latitud";
            // 
            // txtAltitud
            // 
            this.txtAltitud.Location = new System.Drawing.Point(68, 123);
            this.txtAltitud.Name = "txtAltitud";
            this.txtAltitud.ReadOnly = true;
            this.txtAltitud.Size = new System.Drawing.Size(126, 20);
            this.txtAltitud.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Altitud";
            // 
            // botonTentativo
            // 
            this.botonTentativo.Location = new System.Drawing.Point(61, 42);
            this.botonTentativo.Name = "botonTentativo";
            this.botonTentativo.Size = new System.Drawing.Size(61, 23);
            this.botonTentativo.TabIndex = 13;
            this.botonTentativo.Text = "Tentativo";
            this.botonTentativo.UseVisualStyleBackColor = true;
            this.botonTentativo.Click += new System.EventHandler(this.botonTentativo_Click);
            // 
            // botonCancelar
            // 
            this.botonCancelar.Location = new System.Drawing.Point(128, 42);
            this.botonCancelar.Name = "botonCancelar";
            this.botonCancelar.Size = new System.Drawing.Size(67, 23);
            this.botonCancelar.TabIndex = 14;
            this.botonCancelar.Text = "Cancelar";
            this.botonCancelar.UseVisualStyleBackColor = true;
            this.botonCancelar.Click += new System.EventHandler(this.botonCancelar_Click);
            // 
            // FormularioGps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 165);
            this.Controls.Add(this.botonCancelar);
            this.Controls.Add(this.botonTentativo);
            this.Controls.Add(this.txtAltitud);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLatitud);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLongitud);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.botonDato);
            this.Controls.Add(this.botonConectar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormularioGps";
            this.Text = "Gps";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botonConectar;
        private System.Windows.Forms.CheckBox botonDato;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLongitud;
        private System.Windows.Forms.TextBox txtLatitud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAltitud;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button botonTentativo;
        private System.Windows.Forms.Button botonCancelar;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace TP1
{
    public partial class FormCalculadora : Form
    {
        StringBuilder Historial = new StringBuilder();  
        public FormCalculadora()
        {
            InitializeComponent();
        }

        private void MiCalculadora_Load(object sender, EventArgs e)
        {
            btnLimpiar_Click(sender, e);
        }             
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            this.btnConvertirABinario.Enabled = true;
            this.btnConvertirADecimal.Enabled = true;
        }
        private void Limpiar()
        {
            this.txtNumero1.Text = "";
            this.txtNumero2.Text = "";
            this.lblResultado.Text = "";
            this.cmbOperador.SelectedIndex = 0;            
        }

        private double Operar(string numero1, string numero2, string operador)
        {
            Operando operando1 = new Operando(numero1);
            Operando operando2 = new Operando(numero2);            
            char operadorChar;
            char.TryParse(operador, out operadorChar);          
            
            double resultado = Calculadora.Operar(operando1, operando2, operadorChar);       

            return resultado;            
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            string operando1 = this.txtNumero1.Text;
            string operando2 = this.txtNumero2.Text;
            string operador = this.cmbOperador.Text;

          if(string.IsNullOrWhiteSpace(operando1) ||
             string.IsNullOrWhiteSpace(operando2) ||
             string.IsNullOrWhiteSpace(operador))
            {
                MessageBox.Show("Faltan datos","Operación fallida",MessageBoxButtons.RetryCancel ,MessageBoxIcon.Warning);
            }
          else
            {
            double resultado = Operar(operando1, operando2, operador);

            this.lblResultado.Text = resultado.ToString();

            Historial.AppendLine($"{operando1} {operador} {operando2} = {resultado.ToString()}");

            this.lstOperaciones.Items.Add(Historial);
            Historial.Clear();

            }

        }

        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            string resultado = this.lblResultado.Text;            
            string resultadoEnBinario;       

            if(!(string.IsNullOrWhiteSpace(resultado)))
            {
                resultadoEnBinario = Operando.BinarioDecimal(resultado);
                this.lblResultado.Text = resultadoEnBinario;
                Historial.AppendLine($"{resultado} = {resultadoEnBinario}");
                this.lstOperaciones.Items.Add(Historial);
                Historial.Clear();
                this.btnConvertirABinario.Enabled = true;
                this.btnConvertirADecimal.Enabled = false;
            }
            else
            {
                MessageBox.Show("No se calculó el resultado","Operación fallida",MessageBoxButtons.RetryCancel ,MessageBoxIcon.Warning);
            }
        }

        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            string resultado = this.lblResultado.Text;
            string resultadoEnBinario;

            if (!(string.IsNullOrWhiteSpace(resultado)) && resultado.All(char.IsDigit))
            {
                resultadoEnBinario = Operando.DecimalBinario(resultado);
                this.lblResultado.Text = resultadoEnBinario;
                Historial.AppendLine($"{resultado} = {resultadoEnBinario}");
                this.lstOperaciones.Items.Add(Historial);
                Historial.Clear();
                this.btnConvertirABinario.Enabled = false;
                this.btnConvertirADecimal.Enabled = true;
            }
            else
            {
                MessageBox.Show("No se calculó el resultado", "Operación fallida", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }

        }        

        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de querer salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            
        }
    }
}

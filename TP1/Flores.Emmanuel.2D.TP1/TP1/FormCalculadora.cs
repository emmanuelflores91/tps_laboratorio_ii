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

        public FormCalculadora()
        {
            InitializeComponent();
        }

        private void MiCalculadora_Load(object sender, EventArgs e)
        {
            btnLimpiar_Click(sender, e);
            this.alertIcon.Visible = false;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            this.btnConvertirABinario.Enabled = false;
            this.btnConvertirADecimal.Enabled = false;
            this.alertIcon.Visible = false;
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
            StringBuilder Historial = new StringBuilder();

            string operando1 = this.txtNumero1.Text.Replace('.', ',');

            string operando2 = this.txtNumero2.Text.Replace('.', ',');

            string operador = this.cmbOperador.Text;
            this.lblResultado.ForeColor = Color.DarkCyan;

            if (string.IsNullOrWhiteSpace(operando1) ||
               string.IsNullOrWhiteSpace(operando2) ||
               string.IsNullOrWhiteSpace(operador))
            {
                this.lblResultado.ForeColor = Color.DarkRed;
                this.lblResultado.Text = "Faltan datos.";
                this.btnConvertirADecimal.Enabled = false;
                this.btnConvertirABinario.Enabled = false;
                this.alertIcon.Visible = true;
            }
            else
            {
                if (double.TryParse(operando1, out double auxiliar) &&
                    double.TryParse(operando2, out auxiliar))
                {
                    double resultado = Operar(operando1, operando2, operador);
                    if (resultado == double.MinValue)
                    {
                        this.lblResultado.ForeColor = Color.DarkRed;
                        this.lblResultado.Text = "No se admite división por cero.";
                        this.alertIcon.Visible = true;
                        this.btnConvertirABinario.Enabled = false;
                        this.btnConvertirADecimal.Enabled = false;
                    }
                    else
                    {
                        this.lblResultado.Text = resultado.ToString();

                        Historial.AppendLine($"{operando1} {operador} {operando2} = {resultado.ToString()}");

                        this.lstOperaciones.Items.Add(Historial);
                        Historial.Clear();
                        this.btnConvertirABinario.Enabled = true;
                        this.btnConvertirADecimal.Enabled = false;
                        this.alertIcon.Visible = false;
                    }

                }
                else
                {
                    this.lblResultado.ForeColor = Color.DarkRed;
                    this.lblResultado.Text = "Se ingresaron caracteres no admitidos.";
                    this.btnConvertirADecimal.Enabled = false;
                    this.btnConvertirABinario.Enabled = false;
                    this.alertIcon.Visible = true;
                }
            }

        }

        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            StringBuilder Historial = new StringBuilder();
            string resultado = this.lblResultado.Text;
            string resultadoEnDecimal;

            resultadoEnDecimal = Operando.BinarioDecimal(resultado);

            this.lblResultado.Text = resultadoEnDecimal;
            Historial.AppendLine($"{resultado} = {resultadoEnDecimal}");
            this.lstOperaciones.Items.Add(Historial);
            Historial.Clear();
            this.btnConvertirABinario.Enabled = true;
            this.btnConvertirADecimal.Enabled = false;

        }

        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            StringBuilder Historial = new StringBuilder();
            string resultado = this.lblResultado.Text;
            string resultadoEnBinario;

            resultadoEnBinario = Operando.DecimalBinario(resultado);

            this.lblResultado.Text = resultadoEnBinario;
            Historial.AppendLine($"{resultado} = {resultadoEnBinario}");
            this.lstOperaciones.Items.Add(Historial);
            Historial.Clear();
            this.btnConvertirABinario.Enabled = false;
            this.btnConvertirADecimal.Enabled = true;


        }

        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de querer salir?", 
                                         "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }

        }
    }
}

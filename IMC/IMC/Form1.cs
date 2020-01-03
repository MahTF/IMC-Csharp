using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMC
{
    public partial class Form1 : Form
    {
        double altura, peso, calculo;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                VerificarCampos(); 
                
                calculo = (peso / (altura * altura))*10000;
                lblResultado.Visible = true; //Deixa o resultado visível.
                
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("O campo PESO e ALTURA são obrigatórios e não devem permanecer vazios!", "Campo vazio!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAltura.Text = "0";
                txtPeso.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lblResultado.Text = "Seu Indice de Massa Corporal é:\n" + calculo.ToString("N2");
                CalcularClassificacao(calculo);
            }
        }

        /// <summary>
        /// Este método verifica se os campos estão irregulares e pede para efetuar a correção. 
        /// </summary>
        private void VerificarCampos()
        {
            if(String.IsNullOrEmpty(txtAltura.Text) || String.IsNullOrEmpty(txtPeso.Text))
            {
                throw new ArgumentNullException();
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(txtPeso.Text, "[^0-9]"))
            {
                throw new Exception("O campo Peso deve ser preenchido somente com números");
            }
            if(System.Text.RegularExpressions.Regex.IsMatch(txtAltura.Text, "[^0-9]"))
            {
                throw new Exception("O campo Altura deve ser preenchido somente com números");
            }

            altura = double.Parse(txtAltura.Text.ToString());
            peso = double.Parse(txtPeso.Text.ToString());

            //De acordo com pesquisa realizada, a menor pessoa do mundo mede 62.8 cm e a mais alta 2,51
            if (altura < 62 || altura > 260)
            {
                throw new Exception("Acreditamos que sua altura esteja errada, favor verificar!");
            }
            
        }

        /// <summary>
        /// Este método realiza a classificação de risco.
        /// </summary>
        /// <param name="resultadoConta">Este valor é o resultado do cálculo do IMC. (Peso / altura²)</param>
        private void CalcularClassificacao(double resultadoConta)
        {
            if (resultadoConta != 0)
            {
                lblClassificacao.Visible = true;
            }

            //Valores retirados de https://pt.wikipedia.org/wiki/%C3%8Dndice_de_massa_corporal 
            if (resultadoConta < 17)
            {
                lblClassificacao.Text = "Muito abaixo do peso";
                lblClassificacao.ForeColor = Color.Red;
                return;
            }
            if(resultadoConta >= 17 && resultadoConta < 18.5)
            {
                lblClassificacao.Text = "Abaixo do peso";
                lblClassificacao.ForeColor = Color.Orange;
                return;
            }

            if (resultadoConta >= 18.5 && resultadoConta < 25)
            {
                lblClassificacao.Text = "Peso normal";
                lblClassificacao.ForeColor = Color.Green;
                return;
            }
            if (resultadoConta >= 25 && resultadoConta < 30)
            {
                lblClassificacao.Text = "Acima do peso";
                lblClassificacao.ForeColor = Color.Blue;
                return;
            }
            if (resultadoConta >= 30 && resultadoConta < 35)
            {
                lblClassificacao.Text = "Obesidade";
                lblClassificacao.ForeColor = Color.Purple;
                return;
            }
            if (resultadoConta >= 35)
            {
                lblClassificacao.Text = "Obesidade II";
                lblClassificacao.ForeColor = Color.MidnightBlue;
                if(resultadoConta >= 40)
                {
                    lblClassificacao.Text = "Obesidade III";
                    MessageBox.Show("Aconselho a procura de um profissional para aconselhamento!", "Estado grave!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
        }
    }
}

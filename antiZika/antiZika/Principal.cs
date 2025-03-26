using System.Security.Cryptography;

namespace antiZika
{
    public partial class Principal : Form
    {

        private readonly string _conexao = antiZika.Properties.Settings.Default.Conexao;
        public Principal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Abrir Windows Explorer ( File Dialog )
            using (OpenFileDialog dialogoAbrirArquivo = new OpenFileDialog()) 
            {
                dialogoAbrirArquivo.Filter = "Todos os arquivos (*.*)|*.*";

                if(dialogoAbrirArquivo.ShowDialog() == DialogResult.OK)
                {
                    // Armazena o caminho do arquivo
                    string caminhoArquivo = dialogoAbrirArquivo.FileName;
                    
                    //Calcula a hash
                    string hashSHA256 = CalculaHashSHA256(caminhoArquivo);
                    Clipboard.SetText(hashSHA256);
                    MessageBox.Show("Hash Colado na area de transferencia");
                }

            }
        }

        // Calcular Hash SHA256
        private string CalculaHashSHA256(string caminhoArquivo)
        {
            using (FileStream fs = File.OpenRead(caminhoArquivo))// Abre o arquivo
            using (SHA256 sha256 = SHA256.Create())
            {
                // Computa o hash do arquivo e converter os bytes para string
                byte[] hashBytes = sha256.ComputeHash(fs);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}

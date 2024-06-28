using System;
using System.Data;
using System.Data.SqlClient;


namespace factura_windows_form
{
    public partial class Form1 : Form
    {

        SqlConnection conexion = new SqlConnection("Data Source,; Inicial Catalog = facturacion;" + "Integridad Security = true");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            verdatos();
        }

        private void verdatos()
        {
            try
            {
                conexion.Open();
                string consulta = ("Select ITEM,CANTIDAD,DESCRIPCION,PU, PU*CANTIDAD as total from factura");
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "facturacion");
                dataGridView2.DataSource = dataSet.Tables["factura"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally { conexion.Close(); }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                foreach (DataRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow && !String.IsNullOrEmpty(row.Cells[0].Value.ToString))
                    {
                        int item = int.Parse(row.Cells[0].Value.ToString());
                        int cantidad = int.Parse(row.Cells[1].Value.ToString());
                        int descripcion = int.Parse(row.Cells[2].Value.ToString());
                        int PU = double.Parse(row.Cells[3].Value.ToString());

                        string consulta = "INSERT INTO factura (ITEM, CANTIDAD, DESCRIPCION, PU) VALUES (@item, @cantidad, @descripcion, @PU)";
                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@item", item);
                        comando.Parameters.AddWithValue("@cantidad", cantidad);
                        comando.Parameters.AddWithValue("@descripcion", descripcion);
                        comando.Parameters.AddWithValue("@PU", PU);
                        comando.ExecuteNonQuery();

                    }
                }
                MessageBox.Show("Guardado con exito");
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR AL GUARDAR LOS DATOS: " + ex.Message);
            }

            finally
            {
                conexion.Close();
                verdatos();
            }
        }
    }
}
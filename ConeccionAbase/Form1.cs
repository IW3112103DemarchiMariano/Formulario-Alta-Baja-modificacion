using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConeccionAbase
{
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection("server=DESKTOP-A56APQQ\\SQLEXPRESS; database=Despensa; integrated security = true ");
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            SqlCommand commando = new SqlCommand("Select  distinct distribuidora from Productos", conexion);
            conexion.Open();
            SqlDataReader registro = commando.ExecuteReader();
            while (registro.Read())
            {
                cboProductos.Items.Add(registro["distribuidora"].ToString());
            }
            conexion.Close();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("Select * from Productos", conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            ListaData.DataSource = tabla;

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {

            try
            {
                conexion.Open();
                MessageBox.Show("Conectado");
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string query = "insert into Productos (nombre,distribuidora,precio,cantidad)VALUES (@nombre,@distribuidora,@precio,@cantidad)";
            conexion.Open();
            SqlCommand comando = new SqlCommand(query, conexion);
            
            comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
            comando.Parameters.AddWithValue("@distribuidora", cboProductos.Text);
            comando.Parameters.AddWithValue("@precio", txtPrecio.Text);
            comando.Parameters.AddWithValue("@cantidad", txtUnidades.Text);
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Insertado");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Productos set nombre = @nombre, distribuidora= @distribuidora, precio= @precio, cantidad= @cantidad where id_producto = @id_producto";
            conexion.Open();
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id_producto", txtId.Text);

            comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
            comando.Parameters.AddWithValue("@distribuidora", cboProductos.Text);
            comando.Parameters.AddWithValue("@precio", txtPrecio.Text);
            comando.Parameters.AddWithValue("@cantidad", txtUnidades.Text);

            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Modificado");
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string query = "DELETE from Productos where id_producto=@id_producto";
            conexion.Open();
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id_producto", txtId.Text);
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Borrado");
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("Select * from Productos where id_producto = @id_producto", conexion);
            comando.Parameters.AddWithValue("@id_producto", txtId.Text);
            conexion.Open();
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                txtNombre.Text = registro["nombre"].ToString();
                cboProductos .Text = registro["distribuidora"].ToString();
                txtPrecio.Text = registro["precio"].ToString();
                txtUnidades.Text = registro["cantidad"].ToString();

            }
            conexion.Close();


        }
    }
}

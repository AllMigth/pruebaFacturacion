namespace FacturacionCliente
{
    partial class FormProductos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvProductos = new DataGridView();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            btnConfirmar = new Button();
            lstCarrito = new ListBox();
            btnIrAdmin = new Button();
            btnEliminarProducto = new Button();
            btnModificarStock = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            SuspendLayout();

            // dgvProductos
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(12, 12);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(270, 150);
            dgvProductos.TabIndex = 0;

            // nudCantidad
            nudCantidad.Location = new Point(132, 168);
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(120, 23);
            nudCantidad.TabIndex = 1;

            // btnAgregar
            btnAgregar.Location = new Point(112, 209);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(140, 23);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Agregar al carrito";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;

            // btnConfirmar
            btnConfirmar.Location = new Point(112, 238);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(140, 23);
            btnConfirmar.TabIndex = 3;
            btnConfirmar.Text = "Confirmar compra";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;

            // lstCarrito
            lstCarrito.FormattingEnabled = true;
            lstCarrito.ItemHeight = 15;
            lstCarrito.Location = new Point(12, 279);
            lstCarrito.Name = "lstCarrito";
            lstCarrito.Size = new Size(270, 94);
            lstCarrito.TabIndex = 4;

            // btnIrAdmin
            btnIrAdmin.Location = new Point(12, 379);
            btnIrAdmin.Name = "btnIrAdmin";
            btnIrAdmin.Size = new Size(270, 30);
            btnIrAdmin.TabIndex = 5;
            btnIrAdmin.Text = "Administrar productos";
            btnIrAdmin.UseVisualStyleBackColor = true;
            btnIrAdmin.Click += btnIrAdmin_Click;

            // btnEliminarProducto
            btnEliminarProducto.Location = new Point(12, 420);
            btnEliminarProducto.Name = "btnEliminarProducto";
            btnEliminarProducto.Size = new Size(130, 30);
            btnEliminarProducto.TabIndex = 6;
            btnEliminarProducto.Text = "Eliminar producto";
            btnEliminarProducto.UseVisualStyleBackColor = true;
            btnEliminarProducto.Click += btnEliminarProducto_Click;

            // btnModificarStock
            btnModificarStock.Location = new Point(152, 420);
            btnModificarStock.Name = "btnModificarStock";
            btnModificarStock.Size = new Size(130, 30);
            btnModificarStock.TabIndex = 7;
            btnModificarStock.Text = "Modificar stock";
            btnModificarStock.UseVisualStyleBackColor = true;
            btnModificarStock.Click += btnModificarStock_Click;

            // FormProductos
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 470);
            Controls.Add(btnModificarStock);
            Controls.Add(btnEliminarProducto);
            Controls.Add(btnIrAdmin);
            Controls.Add(lstCarrito);
            Controls.Add(btnConfirmar);
            Controls.Add(btnAgregar);
            Controls.Add(nudCantidad);
            Controls.Add(dgvProductos);
            Name = "FormProductos";
            Text = "Productos";
            Load += FormProductos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvProductos;
        private NumericUpDown nudCantidad;
        private Button btnAgregar;
        private Button btnConfirmar;
        private ListBox lstCarrito;
        private Button btnIrAdmin;
        private Button btnEliminarProducto;
        private Button btnModificarStock;
    }
}

namespace Calendar_App;

partial class Form2
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //inicializamos los elementos.
        btnAnterior = new Button();
        btnSiguiente = new Button();
        btnAtras = new Button();

        //Agregamos las propiedades a los elementos.
        btnAnterior.Location = new Point(602, 400);
        btnAnterior.Name = "btnAnterior";
        btnAnterior.BackColor = SystemColors.Control;
        btnAnterior.Size = new Size(75, 23);
        btnAnterior.Text = "Anterior";
        btnAnterior.UseVisualStyleBackColor = true;
        btnAnterior.Click += btnAnterior_Click;

        btnSiguiente.Location = new Point(687, 400);
        btnSiguiente.Name = "btnSiguiente";
        btnSiguiente.BackColor = SystemColors.Control;
        btnSiguiente.Size = new Size(75, 23);
        btnSiguiente.Text = "Siguiente";
        btnSiguiente.UseVisualStyleBackColor = true;
        btnSiguiente.Click += btnSiguiente_Click;

        btnAtras.Location = new Point(3, 3);
        btnAtras.Name = "btnAtras";
        btnAtras.BackColor = SystemColors.Control;
        btnAtras.Size = new Size(30,23);
        btnAtras.Text = "<--";
        btnAtras.UseVisualStyleBackColor=true ;
        btnAtras.Click += btnAtras_Click;

        //Agregamos a la vistalos elementos.
        Controls.Add(btnAnterior);
        Controls.Add(btnSiguiente);
        Controls.Add(btnAtras);
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "CalendarApp";
        Load += Form2_Load;
        FormClosing += Form2_Closing;
    }

    private Button btnAnterior;
    private Button btnSiguiente;
    private Button btnAtras;
    #endregion
}


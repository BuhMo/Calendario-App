namespace Calendar_App;

partial class Form1{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing){
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
    private void InitializeComponent(){
        //Inicializamos los elementos.
        cbYears = new ComboBox();
        cbMonths = new ComboBox();
        cbDays = new ComboBox();
        tittle = new Label();
        txtYears = new Label();
        txtMonths = new Label();
        txtDays = new Label();
        txtNumCant = new Label();
        cantNum = new TextBox();
        btnSend = new Button();
        SuspendLayout();

        //Agregamos las propiedades de los elementos.
        cbYears.FormattingEnabled = true;
        cbYears.Location = new Point(505, 209);
        cbYears.Name = "cbYears";
        cbYears.Size = new Size(121, 23);
        cbYears.SelectedIndexChanged += cbYears_SelectedIndexChanged;

        cbMonths.FormattingEnabled = true;
        cbMonths.Location = new Point(179, 209);
        cbMonths.Name = "cbMonths";
        cbMonths.Size = new Size(121, 23);
        cbMonths.SelectedIndexChanged += cbMonths_SelectedIndexChanged;

        cbDays.FormattingEnabled = true;
        cbDays.Location = new Point(339, 209);
        cbDays.Name = "cbDays";
        cbDays.Size = new Size(121, 23);

        txtYears.AutoSize = true;
        txtYears.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        txtYears.Location = new Point(505, 180);
        txtYears.Name = "txtYears";
        txtYears.Size = new Size(56, 21);
        txtYears.Text = "Año:";

        txtMonths.AutoSize = true;
        txtMonths.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        txtMonths.Location = new Point(179, 180);
        txtMonths.Name = "txtMonths";
        txtMonths.Size = new Size(56, 21);
        txtMonths.Text = "Mes: ";

        txtDays.AutoSize = true;
        txtDays.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        txtDays.Location = new Point(339, 180);
        txtDays.Name = "txtDays";
        txtDays.Size = new Size(56, 21);
        txtDays.Text = "Día: ";

        tittle.AutoSize = true;
        tittle.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
        tittle.Location = new Point(330, 50);
        tittle.Name = "txtMont";
        tittle.Size = new Size(183, 40);
        tittle.Text = "Calendario";

        txtNumCant.AutoSize= true;
        txtNumCant.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        txtNumCant.Location = new Point(220, 290);
        txtNumCant.Name = "txtNumCant";
        txtNumCant.Size = new Size(150, 25);
        txtNumCant.Text = "¿Cuantos días quiere ver? : ";

        cantNum.Location = new Point(420, 287);
        cantNum.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        cantNum.Name = "cantNum";
        cantNum.Size = new Size(100, 23);
        cantNum.KeyPress += cantNum_KeyPress;

        btnSend.Location = new Point(687, 397);
        btnSend.Name = "btnSend";
        btnSend.Size = new Size(75, 23);
        btnSend.Text = "Send";
        btnSend.UseVisualStyleBackColor = true;
        btnSend.Click += btnSend_Click;

        //Agregamos los elementos al formulario.
        Controls.Add(cbYears);
        Controls.Add(cbMonths);
        Controls.Add(cbDays);
        Controls.Add(txtYears);
        Controls.Add(txtMonths);
        Controls.Add(txtDays);
        Controls.Add(tittle);
        Controls.Add(txtNumCant);
        Controls.Add(cantNum);
        Controls.Add(btnSend);
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        BackColor = Color.FromArgb( 236, 240, 241 );
        this.Text = "Calendar Menu";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }
    
    //Se crean globalmente los elementos.
    private ComboBox cbYears;
    private ComboBox cbMonths;
    private ComboBox cbDays;
    private Label tittle;
    private Label txtYears;
    private Label txtMonths;
    private Label txtDays;
    private Label txtNumCant;
    private TextBox cantNum;
    private Button btnSend;

    #endregion
}

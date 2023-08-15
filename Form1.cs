namespace Calendar_App;

public partial class Form1 : Form{
    private int maxNum;
    private int[] cantAnio  = {2022, 2023, 2024};

    public Form1(){
        InitializeComponent();
        addData();
    }

    private void addData(){
        //Agregamos años al combobox
        for (int i = 0; i < cantAnio.Length ; i++) {
            cbYears.Items.Add(cantAnio[i]);
        }

        //Agregamos los meses del combox
        for (int i = 1; i < 13; i++) {
            cbMonths.Items.Add(i);
        }

        //Desabilitamos los combobox de mes y de día para que se habiliten por orden y así
        //obtener los días del mes
        cbMonths.Enabled = false;
        cbDays.Enabled = false;
    }

    private void addDays(int anno, int mes){
        switch (cbMonths.SelectedItem) {
                case 02:
                    //Verificamos si el año es bisiesto para 
                    if (((int)cbYears.SelectedItem % 4 == 0 && (int)cbYears.SelectedItem % 100 != 0) || ((int)cbYears.SelectedItem % 400 == 0)){
                        cbDays.Items.Clear();
                        for (int i = 1; i <= 29; i++){
                            cbDays.Items.Add(i);
                        }
                    } else {
                        cbDays.Items.Clear();
                        for (int i = 1; i < 29; i++){
                            cbDays.Items.Add(i);
                        }
                    }
                    break;
                case 04 or 06 or 09 or 11:
                    cbDays.Items.Clear();
                    for (int i = 1; i < 31; i++){
                        cbDays.Items.Add(i);
                    }
                    break;
                default:
                    cbDays.Items.Clear();
                    for (int i = 1; i <= 31; i++){
                        cbDays.Items.Add(i);
                    }
                    break;
            }
    }

    private int diasMeses(int mes, int anno){
        int[] meses30 = {4,6,9,11};
        if (mes == 2){
            if ((anno % 4 == 0 && anno % 100 != 0) || (anno % 400 == 0)){
                return 29;
            } else {
                return 28;
            }
        } else if (Array.Exists(meses30, m => m == mes)) {
            return 30;
        } else {
            return 31;
        }
    }

    private int diasNum(int anno, int mes, int dia, int cant) {
        int anio = cant/365, cont = 0, nAnio, mN = diasMeses(mes, anno) - dia + 1, rest = cant - mN, nDias = rest;
        
        
        if (anio > 0){
            for (int i = 0; i < anio; i++) {
                nAnio = cantAnio[i];
                for (int j = mes + 1; j <= 12; j++) {
                    cont = diasMeses(j, nAnio);
                    nDias += cont;
                }
            }
        } else {
            if (mes < 12) {
                for (int i = mes + 1; i <= 12; i++) {
                cont = diasMeses(i, anno);
                nDias += cont;
                }
            }
        }

        return nDias;
    }

    private void cbYears_SelectedIndexChanged(object sender, EventArgs e){
        try{
            if (cbYears.SelectedItem != null){
                cbMonths.Enabled = true;
                if (cbMonths.SelectedItem != null){
                    addDays((int)cbYears.SelectedItem, (int)cbMonths.SelectedItem);
                }
            }
        }
        catch (System.Exception){
            
            throw;
        }
    }

    private void cbMonths_SelectedIndexChanged(object sender, EventArgs e){
        //Ahora con el evento de selccionar un index y un item del combobox de mes
        //obtenemos los días a mostrar para ese mes
        //Verificamos que los campos del mes y el año no sean nulos antes de comenzar
        if (cbMonths.SelectedItem != null && cbYears.SelectedItem != null) {
            //Usamos un switch case para los casos de los meses de 30, 31 y 28/29 días
            addDays((int)cbYears.SelectedItem, (int)cbMonths.SelectedItem);
            cbDays.Enabled = true;
        }
    }

    private void cantNum_KeyPress(object sender, KeyPressEventArgs e){
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
            e.Handled = true;
        }
        
    }

    private void btnSend_Click(object sender, EventArgs e){
        if (cbYears.SelectedItem == null || cbMonths.SelectedItem == null || cbDays.SelectedItem == null || cantNum.Text == null ) {
            String message = "Llené el campo faltante: " ;
            String tittle = "Falta(n) Valor(es)";
            MessageBox.Show(message, tittle);
        } else {
            int initialMonth = (int)cbMonths.SelectedItem;
            int initialDay = (int)cbDays.SelectedItem;
            int initialYear = (int)cbYears.SelectedItem;
            int nDias = Convert.ToInt32(cantNum.Text);
            maxNum = diasNum(initialYear, initialMonth, initialDay, (int)nDias);

            if ((int)nDias > maxNum){
                String message = "El número de días a mostrar pedido es mayor a la cantidad que se puede mostrar. Ingrese otro valor";
                String tittle = "Valor grande";
                MessageBox.Show(message, tittle);
                cantNum.Clear();
                cbYears.Enabled = false;
                cbMonths.Enabled = false;
                cbDays.Enabled = false;
                cbYears.Enabled = true;

            } else {
                Form2 genCal = new Form2(initialYear, initialMonth, initialDay, (int)nDias);
                genCal.Show();
                this.Hide();
            }
        }

    }

    private void Form1_Load(object sender, EventArgs e){
        //Evento para cargar los paneles.
    }
}

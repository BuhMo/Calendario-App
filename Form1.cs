namespace Calendar_App;

public partial class Form1 : Form{
    //Varables globales para uso de la lógica
    private int maxNum;
    private int[] cantAnio  = {2022, 2023, 2024};

    public Form1(){
        InitializeComponent();
        addData();
    }

    private void addData(){
        //Agregar los años al combobox.
        for (int i = 0; i < cantAnio.Length ; i++) {
            cbYears.Items.Add(cantAnio[i]);
        }

        //Agregar los meses del combox.
        for (int i = 1; i < 13; i++) {
            cbMonths.Items.Add(i);
        }

        //Desabilitar comboBox del día y del mes para activarlos por eventos a ambos.
        //Los datos de los días se agregan con el evento del combobox del mes.
        cbMonths.Enabled = false;
        cbDays.Enabled = false;
    }

    private void addDays(int anno, int mes){
        //Método para agregar los días al combobox correpondiente.
        switch (cbMonths.SelectedItem) {
            //Dependiendo del caso agregará tantos días como el mes tenga.
                case 02:
                    //Verificamos si el año es bisiesto para los días de febrero.
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
        //Método para regresar la cantidad de días de un mes.
        //Esté y el otro método se diferencean que el anterior agrega a un combobox
        //mientras que esté método regresa la cantidad de días.

        //Arreglo con los meses con 30 días.
        int[] meses30 = {4,6,9,11};

        //Vemos si el mes es febrero o un mes de 30 días. Si no es ninguno será de 31 días.
        if (mes == 2){
            //Verificamos si el año es viciesto.
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
        //Método que regresa la cantidad maxima de días.
        //Sirve para verificar si el número ingresado en el textbox no exede.

        //Inicializamos las varibales necesarias.
        int anio = cant/365, cont = 0, nAnio =  anno, mN = diasMeses(mes, anno) - dia + 1, 
        rest = cant - mN, nDias = rest, month = mes;
        
        //Verificamos si la cantidad el mas de un año
        if (anio > 0){
            for (int i = 0; i < anio; i++) {
                nAnio = cantAnio[i];
                for (int j = mes + 1; j <= 12; j++) {
                    cont = diasMeses(j, nAnio);
                    nDias += cont;
                }
            }
        } else {
            //Si no verificamos si es diciembre para calcular con el otro año.
            //Me acabo de percatar que que si es octubre o noviembre y pide más días
            //de los que le queda al año, por lo tal dara error. Chale
            if (mes == 12) {
                month = 1;
                nAnio ++;
            } else {
                month++;
            }
            for (int i = month; i <= 12; i++) {
                cont = diasMeses(i, nAnio);
                nDias += cont;
            }
        }

        return nDias;
    }

    private void cbYears_SelectedIndexChanged(object sender, EventArgs e){
        //Evento del comboBox años.
        //Vemos si se seleccionó un item del combobox si, si activamos el
        //el combobox del mes.
        //Aparte si se seleccionan ambos se recarga los items del combobox de
        //días. Esto por el año viciesto.
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
        //Evento del combobox de meses.
        //Verificamos que el año y el mes estén seleccionados para poder usarse. 
        //Si, es redundante, pero causaba error el bóton al regresar.
        if (cbMonths.SelectedItem != null && cbYears.SelectedItem != null) {
            //Agregamos los días con el método y activamos el combobox de días.
            addDays((int)cbYears.SelectedItem, (int)cbMonths.SelectedItem);
            cbDays.Enabled = true;
        }
    }

    private void cantNum_KeyPress(object sender, KeyPressEventArgs e){
        //Evento del textbox de la cantidad de días para ver.
        //El evento es para validar que solo sean números.
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
            e.Handled = true;
        }
        
    }

    private void btnSend_Click(object sender, EventArgs e){
        //Evento cuando se presione el botón.
        //Validamos que todos los elementos (ComboBox y TextBox) no sean nulos.
        //En caso de que uno sea nulo saldrá un mensaje para corregirlo.
        if (cbYears.SelectedItem == null || cbMonths.SelectedItem == null || cbDays.SelectedItem == null || cantNum.Text == null ) {
            String message = "Llené el campo faltante: " ;
            String tittle = "Falta(n) Valor(es)";
            MessageBox.Show(message, tittle);
        } else {
            //Si todo salió bien guardamos los valores en variables para 
            //enviarlas al constructor del segundo formulario.
            int initialMonth = (int)cbMonths.SelectedItem;
            int initialDay = (int)cbDays.SelectedItem;
            int initialYear = (int)cbYears.SelectedItem;
            int nDias = Convert.ToInt32(cantNum.Text);
            maxNum = diasNum(initialYear, initialMonth, initialDay, (int)nDias);

            //Antes de enviarlas verificamos que la cantidad puesta no exceda la maxima.
            if ((int)nDias > maxNum){
                //En caso de ser mayor, se lanzará un mensaje diciendo que es mayor a lo posible.
                String message = "El número de días a mostrar pedido es mayor a la cantidad que se puede mostrar. Ingrese otro valor";
                String tittle = "Valor grande";
                MessageBox.Show(message, tittle);
                cantNum.Clear();
                cbYears.Enabled = false;
                cbMonths.Enabled = false;
                cbDays.Enabled = false;
                cbYears.Enabled = true;

            } else {
                //Si todo salió bien se llama al constructor y se "cierra", o se oculta, esté formulario.
                Form2 genCal = new Form2(initialYear, initialMonth, initialDay, (int)nDias);
                genCal.Show();
                this.Hide();
            }
        }

    }

    private void Form1_Load(object sender, EventArgs e){
        //Evento para cargar los paneles.
    }

    private void Form1_Closing(object sender, FormClosingEventArgs e){
        //Evento para que si se cierre la aplicación
        //A veces fuinciona, al parecer.
        Application.Exit();
    }
}

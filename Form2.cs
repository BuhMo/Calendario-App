using Accessibility;

namespace Calendar_App;
    public partial class Form2:Form{    
    private string[] diasSemana = {"Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"};
    private int index;
    private Boolean pressBtn;
    private List<FlowLayoutPanel> listaCalen = new List<FlowLayoutPanel>();
    List<FlowLayoutPanel> listaPanels = new List<FlowLayoutPanel>();
    public Form2(int anno, int mes, int dia, int cantDias){
        pressBtn = false;
        InitializeComponent();
        listaCalen = testCa(anno, mes, dia, cantDias);
    }

    private int calcularPrimerDiaMes(int dia, int mes, int anno){
        //Aplicando el algoritmo de zeller
        //
        int a = (14 - mes)/12;
        int y = anno - a;
        int m = mes + 12 * a - 2;
        //
        int h = (dia + y + y/4 - y/100 + y/400 + (31*m)/12) % 7;
        //
        return h;
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

    private String nameMonth(int mes){
        string[] nombreMes = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre" , "Diciembre"};
        string nombre = "";
        for (int i = 0; i < nombreMes.Length; i++) {
            if ( i == mes - 1) {
                nombre = nombreMes[i];
                break;
            }
        }
        return nombre;
    }

    private void painting(int anno, int numMes, int mes, int dia, int ranDia, int resto, int contadorMes, int totalMeses, int ban, Label label){
        //Primero conocemos cuantos días tiene el mes a pintar y el día de la semana que tendrá el día en cuestión.
        int p = calcularPrimerDiaMes(dia, numMes, anno);

        //Verificamos con un if que la fecha está dentro del rango dado pór el usuario y saber si se pinta de gris o de colores bien aca.
        if ((ranDia <= dia && numMes == mes && ban == 0 ) || (contadorMes != totalMeses && numMes != mes) || (resto >= dia && contadorMes == totalMeses && numMes != mes)) {
            //Ahora, vemos en que día de la semana cae la fecha dada para saber de que color tiene que ir pintada.
            if ( p == 0 || p == 6){
                label.BackColor = Color.FromArgb(247, 220, 111);
            } else {
                label.BackColor = Color.FromArgb(82, 190, 128);
            }

            //Por último, verificamos si la fecha no cae en ninguna de las siguientes fechas. En el caso de si, se repintara para
            //dar a entender que es día festivo.
            if ((numMes == 1 && dia == 1) || (numMes == 2 && dia == 5) || (numMes == 3 && dia == 18) || 
                (numMes == 5 && dia == 1) || (numMes == 9 && dia == 16) || (numMes == 11 && dia == 20) || 
                (numMes == 12 && dia == 25)){
                label.BackColor = Color.FromArgb(235, 152, 78);
            }
        } else {
            label.BackColor = Color.FromArgb(128, 139, 150);
        }
    }

    private int cantidadAnnios(int annio, int mes, int dia, int cant){
        int diasRestantes = cant - (diasMeses(mes, annio) - dia + 1);
        int annios = 0, cont=0, nAnnio = annio, month = mes;

        if (mes == 12 && cant > 31){
            month = 0;
            annios++;
        }
            //Son más de un año. Ahora calculamos la cantidad de años que son
        for (int i = month + 1; i <= 12; i++){
            cont = diasMeses(i, nAnnio);
            if (diasRestantes > cont){
                diasRestantes -= cont;
            } else {
                annios++;
                break;
            }
            if (i == 12 && diasRestantes > 0) {
                month = 0;
                i = 1;
                nAnnio++;
                annios++;
            }
            
        } 

        return annios;
    }

    private List<FlowLayoutPanel> testCa(int anno, int mes, int dia, int cant){
        //Primero conocemos en que día de la semana comienza el mes y cuantos días tiene ese mes.
        //int primerDia = calcularPrimerDiaMes(1, mes, anno);
        int diasXMes = diasMeses(mes, anno);
        //iniciamos un contador con los meses a mostras. Empezamos por defecto en uno.
        int check = diasXMes - dia + 1 , diasRest = cant - check, cAnn =  cantidadAnnios(anno, mes, dia, cant), contMes = 1, month = mes;
        int cont = 0, day = dia, ban = 0;
        int[] listaResi = new int[13];
        List<FlowLayoutPanel> listSave = new List<FlowLayoutPanel>();


        //For para crear la cantidad de paneles por meses y años solicitados por el usuario.
        for (int nAnnios = 0; nAnnios < cAnn; nAnnios++){
            listaResi[month] = day;
            int nMeses = 1;
            //Verificamos que tantos meses abarcara el calendario.
            try {
                if(check < cant){
                    //Son más de un mes.
                    for (int i = month + 1; i <= 12; i++){
                        cont = diasMeses(i, anno+nAnnios);
                        if (diasRest > cont) {
                            nMeses++;
                            listaResi[i]=cont;
                            diasRest -= cont;
                        } else {
                            nMeses++;
                            listaResi[i]=diasRest;
                            //month = 1;
                            //day = cont - diasRest + 1;
                            break;
                        }
                    }
                } else {
                    //Es Solo un mes. Prueba.
                    //nMeses=1;
                    listaResi[month] = cant;
                }
            } catch (Exception ex) {
                //MessageBox.Show(ex.Message);
            }

            for (int i = 0; i < nMeses; i++){
                //Creamos un FlowLayout como lienzo, o base, del calendario. Aunque los botones quedan fuera por 
                //lo que redimencionarlo quedaría descartado(?).
                FlowLayoutPanel flow1 = new FlowLayoutPanel();
                flow1.Size = new Size(800, 350);
                flow1.Location = new Point(0, 30);
                flow1.BackColor = SystemColors.Control;

                //Cramos un label que dirá el nombre y año del mes en cuestión.
                Label labelMes = new Label();
                labelMes.Text = nameMonth(i+month) + " " + (anno+nAnnios);
                labelMes.TextAlign = ContentAlignment.MiddleCenter;
                labelMes.Font = new Font("Arial", 16, FontStyle.Bold);
                labelMes.Size = new Size(200, 40);  //Tamaño del label.
                labelMes.Location = new Point(300, 2); //Ubicación del Label.
                flow1.Controls.Add(labelMes);

                //Creamos el Table Layout que contendra el calendario.
                TableLayoutPanel baseCalendar = new TableLayoutPanel();
                baseCalendar.Size = new Size(800, 350);
                baseCalendar.Location = new Point(3, 60);
                baseCalendar.RowCount = 7;
                baseCalendar.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                //Añadimos el tamaño que tendra cada fila por porcentaje.
                for (int j = 0 ;j < 7;++j ){
                    baseCalendar.RowStyles.Add(new RowStyle (SizeType.AutoSize, 100f / 7));
                }

                //Añadimos las columnas
                for(int col = 0; col <7 ; col++){
                    Label labelCol = new Label();
                    labelCol.Text = diasSemana[col];
                    labelCol.TextAlign = ContentAlignment.MiddleCenter;
                    labelCol.Dock = DockStyle.Fill;
                    labelCol.Font = new Font("Arial", 10, FontStyle.Bold);
                    baseCalendar.Controls.Add(labelCol , col, 0 );
                }

                //Agregamos los labels de los días al calendario y llamamos al método para pintarlos
                int contador = 1;
                int bDia = calcularPrimerDiaMes(1, i+month, anno+nAnnios);
                int bMes = diasMeses(i+month, anno+nAnnios);
                try {
                    for (int row = 1; row < 7; row++) {
                        for (int col = 0; col < 7; col++) {
                            if ((row == 1 && col < bDia) || contador > bMes) {
                                baseCalendar.Controls.Add(new Label(), col, row);
                            } else {
                                if (nMeses == 1 && cant < diasXMes && contador > cant){
                                    ban = 1;
                                }
                                Label nLabel = new Label();
                                nLabel.Text = contador.ToString();
                                nLabel.TextAlign = ContentAlignment.MiddleCenter;
                                nLabel.Dock = DockStyle.Fill;
                                nLabel.Margin = new Padding(2);
                                painting(anno+nAnnios, month+i, mes, contador, dia, listaResi[month+i], contMes + i, nMeses, ban, nLabel);
                                nLabel.BorderStyle = BorderStyle.FixedSingle;
                                baseCalendar.Controls.Add(nLabel, col, row);
                                contador++;
                            }
                        }
                    }
                } catch (Exception ex){
                    MessageBox.Show(ex.Message);
                }
                //Agregamos los dos paneles creados.
                flow1.Controls.Add(baseCalendar);
                Controls.Add(flow1);
                listSave.Add(flow1);
            }
            if ((month == 12 || cAnn > 1)&& check < cant){
                month = 1;
                day = 31;
                diasRest -= day;
            }
        }
        return listSave;
    }

    private void btnAnterior_Click(object sender, EventArgs e){
        //Evento para que el botón "anterior" pueda ir al anterior panel.
        if (index > 0){
            listaPanels[--index].BringToFront();
        }
    }

    private void btnSiguiente_Click(object sender, EventArgs e){
        //Evento para que el botón "siguiente" pueda ir al siguiente panel.
        if (index < listaPanels.Count -1){
            listaPanels[++index].BringToFront();
        }
    }

    private void btnAtras_Click(object sender, EventArgs e){
        listaPanels.Clear();
        index = 0;
        pressBtn = true;
        this.Close();
        Form1 form1 = new Form1();
        form1.Show();
    }

    private void Form2_Load(object sender, EventArgs e){
        //Evento para cargar los paneles.
        foreach (var item in listaCalen){
            listaPanels.Add(item);
        }
        listaPanels[index].BringToFront();
    }
    

    private void Form2_Closing(object sender, FormClosingEventArgs e){
        if (e.CloseReason == CloseReason.UserClosing && pressBtn == false){
            Application.Exit();
        }
    }
}

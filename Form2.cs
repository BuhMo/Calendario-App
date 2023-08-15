using Accessibility;

namespace Calendar_App;
    public partial class Form2:Form{
    //Variables globales.
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
        //Método para calcular el primer día del mes utilizando la congruencia de zeller.

        //Aplicando el algoritmo de zeller.
        //Variables para el algoritmo.
        int a = (14 - mes)/12;
        int y = anno - a;
        int m = mes + 12 * a - 2;

        //Formula para calcular el algoritmo.
        int h = (dia + y + y/4 - y/100 + y/400 + (31*m)/12) % 7;

        return h;
    }

    private int diasMeses(int mes, int anno){
        //Método para regresar la cantidad de días de un mes.

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

    private String nameMonth(int mes){
        //Método que sirve para regresar el nombre del mes.
        //Se usa para el label que contiene el nombre del mes.

        string[] nombreMes = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre" , "Diciembre"};
        string nombre = "";

        //Usamos un for para recorrer el arreglo que contiene los nombres.
        for (int i = 0; i < nombreMes.Length; i++) {
            //Luego validamos que el mes del for coincida con el mes dado
            //al constructor y regresar el nombre.
            if ( i == mes - 1) {
                nombre = nombreMes[i];
                break;
            }
        }

        return nombre;
    }

    private void painting(int anno, int numMes, int mes, int dia, int ranDia, int resto, int contadorMes, int totalMeses, int ban, Label label){
        //Método para saber que label pintar de que color. Esto usando sirtas condiciones antes de pintar.

        //Primero conocemos en que día de la semana cae el día dado.
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
            //Si no coincide con una fecha dentro del rango con el "if" pintamos de color gris el label
            label.BackColor = Color.FromArgb(128, 139, 150);
        }
    }

    private int cantidadAnnios(int annio, int mes, int dia, int cant){
        //Método para saber si la fecha dada y los días pedidos exceden al año 
        //y así saber cuantos años se usaran.

        //Primero creamos las variables locales.
        int diasRestantes = cant - (diasMeses(mes, annio) - dia + 1);
        int annios = 0, cont=0, nAnnio = annio, month = mes;

        //Verificamos si el mes dado es diciembre y la cantidad es mayor al mes.
        //En caso que si, podemos decir que excede al restante del año y así saber
        //cuantos años se usaran en el "algoritmo".
        if (mes == 12 && cant > 31){
            month = 0;
            annios++;
        }

        //Ahora calculamos con el mes siguiente al dado y la cantidad de dias cuantos años es.
        //Esto hace que si la fecha y los días sean mayores a diciembre, el ultimo mes, reinicie el contador
        //y así sumar otro año. Si no se ejecutare el for hasta caer en el else y ser un año.
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
        //Método, o "algoritmo", para generar los meses para el calendario.

        //Conocemos los días del mes con el método "diasMeses".
        int diasXMes = diasMeses(mes, anno);

        //Creamos las variables locales que se usaran dentro de todo el método y se
        //les asigna sus respectivos valores. Algunas varaibles se quedarán estaticos
        //Para llevar un control, mientras que otros serán "dinamicos".
        int check = diasXMes - dia + 1 , diasRest = cant - check, cAnn =  cantidadAnnios(anno, mes, dia, cant), contMes = 1, month = mes;
        int cont = 0, day = dia, ban = 0;
        //Arreglo de enteros para guardar la cantidad de días a pintar por cada mes.
        int[] listaResi = new int[13];

        //Creamos una lista para guardar los paneles creados.
        List<FlowLayoutPanel> listSave = new List<FlowLayoutPanel>();

        //Primer for que establece cuantos años se usaran para el calendario.
        for (int nAnnios = 0; nAnnios < cAnn; nAnnios++){
            //Guardamos en el mes y el día dado.
            //Este se usa para llevar un rango de que días se pintaran del mes.
            listaResi[month] = day;
            int nMeses = 1;

            //Calculamos los meses que abarca el calendario.
            try {
                if(check < cant){
                    //Si entra, significa que es más de un mes.
                    for (int i = month + 1; i <= 12; i++){
                        cont = diasMeses(i, anno+nAnnios);
                        if (diasRest > cont) {
                            //Control de los días que faltan restandolos a los dias del mes.
                            //Luego, se agrega al arreglo el mes y la cantidad de días a pintar.
                            nMeses++;
                            listaResi[i]=cont;
                            diasRest -= cont;
                        } else {
                            //Cuando los días ya no excedan se le suma el restante de ese mes
                            //y se suma otro mes.
                            nMeses++;
                            listaResi[i]=diasRest;
                            break;
                        }
                    }
                } else {
                    //Se agrega la cantidad restante al mes. Prueba, creo que ni hace nada.
                    listaResi[month] = cant;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
            //Segundo for para los meses que se usaran para el calendario.
            //Teniendo en cuenta los meses calculados antes, podemos saber
            //cuantos meses se crearan.
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
                baseCalendar.RowCount = 8;
                baseCalendar.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                //Añadimos el tamaño que tendra cada fila por porcentaje.
                for (int j = 0 ;j < 8;++j ){
                    baseCalendar.RowStyles.Add(new RowStyle (SizeType.Percent, 100f / 8));
                }

                //Añadimos las columnas al tablelayout del calendario.
                for(int col = 0; col <7 ; col++){
                    Label labelCol = new Label();
                    labelCol.Text = diasSemana[col];
                    labelCol.TextAlign = ContentAlignment.MiddleCenter;
                    labelCol.Dock = DockStyle.Fill;
                    labelCol.Font = new Font("Arial", 10, FontStyle.Bold);
                    baseCalendar.Controls.Add(labelCol , col, 0 );
                }

                //Agregamos los labels de los días al calendario y llamamos al método para pintarlos.
                //Creamos variables temporales para cada mes. Siven para saber el numero del día,
                //el día de la semana y la canttidad de días del mes. 
                int contador = 1;
                int bDia = calcularPrimerDiaMes(1, i+month, anno+nAnnios);
                int bMes = diasMeses(i+month, anno+nAnnios);

                try {
                    //For para seleccionar la fila.
                    for (int row = 1; row < 8; row++) {
                        //Segundo for agregar por las columnas los labels de los días.
                        for (int col = 0; col < 7; col++) {
                            //If para agregar labels en blanco para los dias antes y despues del mes.
                            if ((row == 1 && col < bDia) || contador > bMes) {
                                baseCalendar.Controls.Add(new Label(), col, row);
                            } else {
                                //Si lo anterior no se cumple, entonces estamos dentro del mes y
                                //podemos agregar los labels del mes.
                                //Primero verificamos si solo se vera un mes y la cantidad de días
                                //dados son menor a la cantidad de días del mes.
                                if (nMeses == 1 && cant < diasXMes && contador > cant){
                                    //Esto por un error en mi lógica del pintar donde si era un solo
                                    //mes siempre pintaria todo y no logre pensar otra solución.
                                    ban = 1;
                                }
                                //Si es más de un mes ejecutamos todo a la normalidad y creamos los labels
                                //y llamamos al mpetodo de pintar. 
                                Label nLabel = new Label();
                                nLabel.Text = contador.ToString();
                                nLabel.TextAlign = ContentAlignment.MiddleCenter;
                                nLabel.Dock = DockStyle.Fill;
                                nLabel.Margin = new Padding(2);
                                //Para el método usamos demasiadas variables. Esto es para llevar un rango de las fechas dadas
                                //y así pintar correctamente. Esto se podría mejorar.
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
                
                //Agregamos los dos paneles creados a cada panel y luego a la lista.
                flow1.Controls.Add(baseCalendar);
                Controls.Add(flow1);
                listSave.Add(flow1);
            }

            //En el caso de que el mes dado es el ultimo del año, o si son mas de un año y quedan días a llenar, se reinicia el mes
            if ((month == 12 || cAnn > 1)&& check < cant){
                month = 1;
                day = 31;
                diasRest -= day;
            }
        }
        return listSave;
    }

    private void btnAnterior_Click(object sender, EventArgs e){
        //Evento para que el botón "anterior".
        //Si el index es mayor a 0 retrocede el index. 
        //Esto hace que muestre el panel anterior de la lista.
        if (index > 0){
            listaPanels[--index].BringToFront();
        }
    }

    private void btnSiguiente_Click(object sender, EventArgs e){
        //Evento para que el botón "siguiente".
        //Si el index de la lista es menor aumenta el index para mostrar el siguiente panel.
        if (index < listaPanels.Count -1){
            listaPanels[++index].BringToFront();
        }
    }

    private void btnAtras_Click(object sender, EventArgs e){
        //Evento del botón "atras".
        //Este evetno sirve para regresar al formulario 1 (El menu principal).
        //También, limpía la lista de paneles, regresa el index a 0 y "cierra esté formulario"
        listaPanels.Clear();
        index = 0;
        pressBtn = true;
        this.Close();
        Form1 form1 = new Form1();
        form1.Show();
    }

    private void Form2_Load(object sender, EventArgs e){
        //Evento para cargar los paneles.
        //Este evento sirve para cargar los paneles creados con el "algoritmo"
        //Y luego los agrega a la lista. Pensadolo esto es algo redundante. Ya que de una lista agregamos de otra lista...
        foreach (var item in listaCalen){
            listaPanels.Add(item);
        }
        listaPanels[index].BringToFront();
    }
    
    private void Form2_Closing(object sender, FormClosingEventArgs e){
        //Evento de cerrado de esté formulario.
        //Hace que si el usuario cerro el formulario no usando el boton "atras" cierra la aplicación.
        //Al parecer algo esta mal.
        if (e.CloseReason == CloseReason.UserClosing && pressBtn == false){
            Application.Exit();
        }
    }
}

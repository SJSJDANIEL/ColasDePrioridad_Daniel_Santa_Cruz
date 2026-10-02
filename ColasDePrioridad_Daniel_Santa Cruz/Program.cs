using System;
using System.Collections.Generic;

namespace ColasDePrioridad_Daniel_Santa_Cruz
{
    
    class Ticket
    {
        public string codigo;
        public string cliente;
        public string descripcion;
        public int prioridad;
    }

    class Program
    {
        
        static List<Ticket> listaHeap = new List<Ticket>();

        
        static int correlativo = 1;

        static void Main(string[] args)
        {
            int opcion = 0;

            while (opcion != 7)
            {
                Console.Clear();
                Console.WriteLine("====================================================");
                Console.WriteLine("     SISTEMA DE GESTIÓN DE TICKETS DE SOPORTE");
                Console.WriteLine("                COLA DE PRIORIDAD");
                Console.WriteLine("====================================================\n");
                Console.WriteLine("[1] Registrar Ticket");
                Console.WriteLine("[2] Mostrar Siguiente Ticket");
                Console.WriteLine("[3] Atender Ticket");
                Console.WriteLine("[4] Mostrar Cola de Prioridad");
                Console.WriteLine("[5] Buscar Ticket");
                Console.WriteLine("[6] Mostrar Cantidad de Tickets");
                Console.WriteLine("[7] Salir\n");
                Console.WriteLine("----------------------------------------------------");
                Console.Write("Seleccione una opción: ");

                string entrada = Console.ReadLine();
                int.TryParse(entrada, out opcion);

                if (opcion == 1)
                {
                    RegistrarTicket();
                }
                else if (opcion == 2)
                {
                    MostrarSiguiente();
                }
                else if (opcion == 3)
                {
                    AtenderTicket();
                }
                else if (opcion == 4)
                {
                    MostrarCola();
                }
                else if (opcion == 5)
                {
                    BuscarTicket();
                }
                else if (opcion == 6)
                {
                    MostrarCantidad();
                }
                else if (opcion == 7)
                {
                    Console.WriteLine("\n¡Saliendo del programa!");
                }
                else
                {
                    Console.WriteLine("\nOpción no válida. Presione Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        // 1. REGISTRAR TICKET (INSERCIÓN + BUBBLE UP)
        static void RegistrarTicket()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                 REGISTRO DE TICKET");
            Console.WriteLine("====================================================\n");

            Ticket t = new Ticket();

            // Formato de código TCK0001, TCK0002, 
            if (correlativo < 10)
            {
                t.codigo = "TCK000" + correlativo;
            }
            else if (correlativo < 100)
            {
                t.codigo = "TCK00" + correlativo;
            }
            else if (correlativo < 1000)
            {
                t.codigo = "TCK0" + correlativo;
            }
            else
            {
                t.codigo = "TCK" + correlativo;
            }

            Console.WriteLine("Código del Ticket : " + t.codigo);

            Console.Write("Cliente           : ");
            t.cliente = Console.ReadLine();

            Console.Write("Descripción       : ");
            t.descripcion = Console.ReadLine();

            int prio = 0;
            while (prio < 1 || prio > 5)
            {
                Console.Write("Prioridad (1-5)   : ");
                int.TryParse(Console.ReadLine(), out prio);

                if (prio < 1 || prio > 5)
                {
                    Console.WriteLine("Error: La prioridad debe estar entre 1 y 5.");
                }
            }
            t.prioridad = prio;

            // Se inserta al final del Heap
            listaHeap.Add(t);

            // Reorganizar hacia arriba (Bubble Up)
            int pos = listaHeap.Count - 1;
            while (pos > 0)
            {
                int padre = (pos - 1) / 2;

                if (listaHeap[pos].prioridad < listaHeap[padre].prioridad)
                {
                    // Intercambiar elementos
                    Ticket temp = listaHeap[pos];
                    listaHeap[pos] = listaHeap[padre];
                    listaHeap[padre] = temp;

                    pos = padre;
                }
                else
                {
                    break;
                }
            }

            correlativo++;

            Console.WriteLine("\n----------------------------------------------------");
            Console.WriteLine("Ticket registrado exitosamente.");
            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        // 2. MOSTRAR SIGUIENTE TICKET (PEEK)
        static void MostrarSiguiente()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("             SIGUIENTE TICKET A ATENDER");
            Console.WriteLine("====================================================\n");

            if (listaHeap.Count == 0)
            {
                Console.WriteLine("No hay tickets en la cola de prioridad.");
            }
            else
            {
                Ticket siguiente = listaHeap[0]; // Elemento en la raíz

                Console.WriteLine("RESULTADO DE LA CONSULTA");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Código del Ticket : " + siguiente.codigo);
                Console.WriteLine("Cliente           : " + siguiente.cliente);
                Console.WriteLine("Descripción       : " + siguiente.descripcion);
                Console.WriteLine("Prioridad         : " + FormatearPrioridad(siguiente.prioridad));
            }

            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        // 3. ATENDER TICKET (EXTRACT MIN + BUBBLE DOWN)
        static void AtenderTicket()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                   ATENDER TICKET");
            Console.WriteLine("====================================================\n");

            if (listaHeap.Count == 0)
            {
                Console.WriteLine("No hay tickets registrados para atender.");
            }
            else
            {
                Ticket atendido = listaHeap[0];

                // Mover el último elemento a la raíz y eliminar el último
                Ticket ultimo = listaHeap[listaHeap.Count - 1];
                listaHeap.RemoveAt(listaHeap.Count - 1);

                if (listaHeap.Count > 0)
                {
                    listaHeap[0] = ultimo;

                    // Reorganizar hacia abajo (Bubble Down)
                    int pos = 0;
                    while (true)
                    {
                        int izq = (2 * pos) + 1;
                        int der = (2 * pos) + 2;
                        int menor = pos;

                        if (izq < listaHeap.Count && listaHeap[izq].prioridad < listaHeap[menor].prioridad)
                        {
                            menor = izq;
                        }

                        if (der < listaHeap.Count && listaHeap[der].prioridad < listaHeap[menor].prioridad)
                        {
                            menor = der;
                        }

                        if (menor != pos)
                        {
                            Ticket temp = listaHeap[pos];
                            listaHeap[pos] = listaHeap[menor];
                            listaHeap[menor] = temp;

                            pos = menor;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Console.WriteLine("RESULTADO DE LA OPERACIÓN");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Código del Ticket : " + atendido.codigo);
                Console.WriteLine("Cliente           : " + atendido.cliente);
                Console.WriteLine("Descripción       : " + atendido.descripcion);
                Console.WriteLine("Prioridad         : " + FormatearPrioridad(atendido.prioridad));
                Console.WriteLine("\n----------------------------------------------------");
                Console.WriteLine("Ticket atendido correctamente.");
            }

            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        // 4. MOSTRAR COLA DE PRIORIDAD
        static void MostrarCola()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("              COLA DE PRIORIDAD ACTUAL");
            Console.WriteLine("====================================================\n");

            if (listaHeap.Count == 0)
            {
                Console.WriteLine("La cola de prioridad se encuentra vacía.");
            }
            else
            {
                Console.WriteLine("Posición   Ticket       Cliente         Prioridad");
                Console.WriteLine("----------------------------------------------------");

                for (int i = 0; i < listaHeap.Count; i++)
                {
                    Console.WriteLine(i + "          " + listaHeap[i].codigo + "      " + listaHeap[i].cliente + "       " + listaHeap[i].prioridad);
                }

                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Total de Tickets: " + listaHeap.Count);
            }

            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        // 5. BUSCAR TICKET
        static void BuscarTicket()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                 BÚSQUEDA DE TICKET");
            Console.WriteLine("====================================================\n");

            Console.Write("Ingrese el código del ticket: ");
            string codigoBuscado = Console.ReadLine();

            Ticket encontrado = null;

            for (int i = 0; i < listaHeap.Count; i++)
            {
                if (listaHeap[i].codigo.ToLower() == codigoBuscado.ToLower())
                {
                    encontrado = listaHeap[i];
                    break;
                }
            }

            Console.WriteLine("\nRESULTADO DE LA BÚSQUEDA");
            Console.WriteLine("----------------------------------------------------");

            if (encontrado != null)
            {
                Console.WriteLine("Código del Ticket : " + encontrado.codigo);
                Console.WriteLine("Cliente           : " + encontrado.cliente);
                Console.WriteLine("Descripción       : " + encontrado.descripcion);
                Console.WriteLine("Prioridad         : " + FormatearPrioridad(encontrado.prioridad));
            }
            else
            {
                Console.WriteLine("No se encontró ningún ticket con el código '" + codigoBuscado + "'.");
            }

            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        // 6. MOSTRAR CANTIDAD DE TICKETS
        static void MostrarCantidad()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                CANTIDAD DE TICKETS");
            Console.WriteLine("====================================================\n");
            Console.WriteLine("TOTAL DE TICKETS REGISTRADOS");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Cantidad de Tickets : " + listaHeap.Count);
            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        // Función auxiliar para texto de la prioridad
        static string FormatearPrioridad(int p)
        {
            if (p == 1) return "1 (Crítica)";
            if (p == 2) return "2 (Alta)";
            if (p == 3) return "3 (Media)";
            if (p == 4) return "4 (Baja)";
            if (p == 5) return "5 (Muy Baja)";
            return p.ToString();
        }

        // Pausa para regresar al menú
        static void Pausar()
        {
            Console.WriteLine("\nPresione Enter para regresar al menú...");
            Console.ReadLine();
        }
    }
}
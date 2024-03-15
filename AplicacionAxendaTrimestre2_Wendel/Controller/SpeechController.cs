using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Speech.Synthesis;

namespace AplicacionAxendaTrimestre2_Wendel.Controller
{
    public class SpeechController
    {

        public SpeechSynthesizer speech = new SpeechSynthesizer();
        public SpeechSynthesizer speech2 = new SpeechSynthesizer();

        public void LeerContacto(Contacto contacto)
        {
            speech.Rate = -2;


            if (contacto != null)
            {
                speech.SpeakAsync("Datos del contacto seleccionado:");
                speech.SpeakAsync("Nombre: " + contacto.FirstName);
                speech.SpeakAsync("Apellidos: " + contacto.LastName);
                speech.SpeakAsync("Apodo: " + contacto.Nickname);

                // Leer todos los correos electrónicos del contacto
                foreach (var email in contacto.Emails)
                {
                    speech.SpeakAsync("Email: " + email.Address);
                }

                speech.SpeakAsync("Dirección: " + contacto.Address);
                speech.SpeakAsync("Edad: " + contacto.Age);
                speech.SpeakAsync("Fecha de nacimiento: " + contacto.BirthDate);
                speech.SpeakAsync("Tipo de contacto: " + contacto.ContactType);

                // Leer todos los números de teléfono del contacto
                foreach (var phoneNumber in contacto.Numbers)
                {
                    speech.SpeakAsync("Teléfono: " + phoneNumber.Number);
                }

                speech.SpeakAsync("Nota: " + contacto.Note);
            }
            else
            {
                speech.SpeakAsync("No se ha seleccionado ningún contacto de la lista.");
            }

        }

        // Función para pausar la voz
        public void PausarLectura()
        {
            if (speech != null && speech.State == SynthesizerState.Speaking)
            {
                speech.Pause();
                speech2.SpeakAsync("Pausado");
            }
        }

        // Función para continuar la voz
        public async void ContinuarLectura()
        {
            if (speech != null && speech.State == SynthesizerState.Paused)
            {
                speech2.SpeakAsync("Continuando...");
                // Esperar un segundo para no mezclar las voces
                await Task.Delay(1000);
                speech.Resume();
            }
        }


    }

    }


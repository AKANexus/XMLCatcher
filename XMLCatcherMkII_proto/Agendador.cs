using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.TaskScheduler;

namespace XMLCatcherMkII_Installer
{
    class Agendador
    {
        public Agendador()
        {

        }

        public void Tarefas()
        {
            try
            {
                if (!VerificaTarefa()) CriaTarefa();
                else VerificaTarefaAtiva();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CriaTarefa()
        {
            TaskDefinition td = TaskService.Instance.NewTask();
            td.RegistrationInfo.Description = "Executa backup das pastas monitoradas de XML.";
            td.RegistrationInfo.Author = "Trilha Informática";
            td.Settings.StartWhenAvailable = true;


            DailyTrigger dt = td.Triggers.Add(new DailyTrigger(1));
            dt.Repetition.Interval = TimeSpan.FromHours(4);

            td.Actions.Add(new ExecAction("notepad.exe", "", null));

            const string taskName = @"TrilhaInformatica\BackupXML";
            try
            {
                TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool VerificaTarefa()
        {
            TaskService ts = new TaskService();
            Task t = ts.GetTask(@"TrilhaInformatica\BackupXML");
            if (t is null) return false;
            else return true;
        }

        public void VerificaTarefaAtiva()
        {
            TaskService ts = new TaskService();
            Task t = ts.GetTask(@"TrilhaInformatica\BackupXML");
            if (t is null) return;
            t.Enabled = true;
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace XMLCatcherMkII
{
    public class LicencaDeUso
    {
        public string[] VerificarSerialOnline(string serial, string CNPJ)
        {
            using (var CerealConn = new MySqlConnection("server=turkeyshit.mysql.dbaas.com.br;user id=turkeyshit;password=Pfes2018;persistsecurityinfo=True;database=turkeyshit;ConvertZeroDateTime=True"))
            using (var CerealComm = new MySqlCommand())
            {
                var result = new string[3];
                CerealComm.Connection = CerealConn;
                CerealComm.CommandType = CommandType.Text;
                CerealComm.Parameters.AddWithValue("@SERIAL", @$"%{serial.Substring(3)}");
                CerealComm.CommandText = "SELECT * FROM LICENCAS_CLIENTE WHERE PK_SERIAL LIKE @SERIAL";
                try
                {
                    CerealConn.Open();
                }
                catch (MySqlException)
                {
                    result[0] = "-100";
                    result[1] = "Falha ao comunicar com o servidor de licenca";
                    result[2] = "";
                    return result;
                }
                var informacao = CerealComm.ExecuteReader();
                var table = new DataTable();
                table.Load(informacao);
                if (table.Rows.Count == 0)
                {
                    result[0] = "-200";
                    result[1] = "Serial inválido";
                    result[2] = "";
                    return result;
                }
                if (CerealConn.State == ConnectionState.Open) CerealConn.Close();
                CerealConn.Close();
                DateTime _validade = (DateTime)table.Rows[0]["VALIDADE"];
                int _tolerancia = (int)table.Rows[0]["TOLERANCIA"];
                string _cnpj = (string)table.Rows[0]["CNPJ_CPF"];
                string _status = (string)table.Rows[0]["STATUS"];
                switch (_status)
                {
                    case "P":
                        result[0] = "-305";
                        result[1] = "Pendente";
                        result[2] = table.Rows[0].IsNull("MOTIV_BLOQUEIO") ? "" : (string)table.Rows[0]["MOTIV_BLOQUEIO"];
                        return result;
                    case "I":
                        result[0] = "-400";
                        result[1] = "Inativo";
                        result[2] = "";
                        return result;
                    case "B":
                        result[0] = "-205";
                        result[1] = "Bloqueado";
                        result[2] = (string)table.Rows[0]["MOTIV_BLOQUEIO"];
                        return result;
                    case "A":
                        CerealComm.Dispose();
                        CerealConn.Dispose();
                        result[0] = "100";
                        result[1] = "";
                        result[2] = "";
                        return result;
                    default:
                        result[0] = "-999";
                        result[1] = "Algum erro";
                        result[2] = "";
                        return result;
                }

            }
        }
    }
}

using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Database
{
    private string _connectionPath;
    private IDbConnection _connection;

    public Database()
    {
        this._connectionPath = $"URI=file:{Application.dataPath}/GameDb.s3db"; 
        this._connection = new SqliteConnection(_connectionPath);

        //DropTableRanking();
        CreateTableRanking();
    }

    private void PostQuery(string query)
    {
        try
        {
            _connection.Open();

            IDbCommand command = _connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteReader();

            command.Dispose();
            command = null;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
        finally
        {
            _connection.Close();
        }
    }

    private void CreateTableRanking()
    {
        string query = 
            "CREATE TABLE IF NOT EXISTS Ranking (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Name VARCHAR(200) NOT NULL," +
                "Score INTEGER NOT NULL)";
        PostQuery(query);
    }

    private void DropTableRanking()
    {
        string query = "DROP TABLE IF EXISTS Ranking";
        PostQuery(query);
    }

    public void AddRankingRecord(RankingModel record)
    {
        string query = $"INSERT INTO Ranking " +
                        $"(Name, Score) " +
                        $"VALUES ('{record.Name}', {record.Score})";
        PostQuery(query);
    }

    public List<RankingModel> GetAllRecords()
    {
        List<RankingModel> records = new List<RankingModel>();

        try
        {
            _connection.Open();

            IDbCommand command = _connection.CreateCommand();
            string query = "SELECT Name, Score FROM Ranking";
            command.CommandText = query;

            IDataReader response = command.ExecuteReader();
            while (response.Read())
            {
                RankingModel record = new RankingModel(
                    response.GetString(0),
                    response.GetInt32(1));

                records.Add(record);
                Debug.Log($"Record found: {record.Name}, {record.Score}");
            }

            response.Close();
            response = null;

            command.Dispose();
            command = null;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            _connection.Close();
        }

        return records;
    }
}

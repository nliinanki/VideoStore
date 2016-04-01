using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void sp_NumberOfMoviesByCategory ()
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand moviesByGenre = new SqlCommand();

            moviesByGenre.CommandText = "SELECT Genre.GenreName, Count(MovieTitle) AS Num FROM MovieInfo INNER JOIN MovieGenre ON MovieInfo.MovieID = MovieGenre.MovieID INNER JOIN Genre ON MovieGenre.GenreID = Genre.GenreID GROUP BY GenreName";
            SqlContext.Pipe.ExecuteAndSend(moviesByGenre);
        }

    }
}

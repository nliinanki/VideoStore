using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void sp_SpecificMemberMoviesToReturn ()
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand moviesByGenre = new SqlCommand();

            moviesByGenre.CommandText = "SELECT CONCAT(Member.FirstName, ' ', Member.LastName) AS Name, MovieInfo.MovieTitle, DiscStorage.CopyType, StartDate, ReturnDate, Sum(CostPerDay) AS Cost, Count(RentOrder.DiscID) AS Num FROM RentOrder INNER JOIN Member ON RentOrder.MemberID = Member.MemberID INNER JOIN DiscStorage ON RentOrder.DiscID = DiscStorage.DiscID INNER JOIN MovieInfo ON DiscStorage.MovieID = MovieInfo.MovieID WHERE FirstName = 'Adam' AND ReturnDate IS NULL GROUP BY LastName, FirstName, MovieTitle, StartDate, ReturnDate, CopyType";
            SqlContext.Pipe.ExecuteAndSend(moviesByGenre);
        }

    }
}

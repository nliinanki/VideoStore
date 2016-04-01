using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void sp_MoviesToReturnAtDate (DateTime DT)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {

            SqlCommand CheckSpecificDate = new SqlCommand();
            SqlParameter selectYearParam = new SqlParameter("@year", SqlDbType.Int);
            SqlParameter selectMonthParam = new SqlParameter("@month", SqlDbType.Int);
            SqlParameter selectDayParam = new SqlParameter("@day", SqlDbType.Int);

            selectYearParam.Value = DT.Year;
            selectMonthParam.Value = DT.Month;
            selectDayParam.Value = DT.Day;

            CheckSpecificDate.Parameters.Add(selectYearParam);
            CheckSpecificDate.Parameters.Add(selectMonthParam);
            CheckSpecificDate.Parameters.Add(selectDayParam);

            CheckSpecificDate.CommandText = "SELECT StartDate, MovieTitle, RentOrder.DiscID, CONCAT(FirstName, ' ', LastName) AS Name, ReturnDate FROM RentOrder INNER JOIN Member ON RentOrder.MemberID = Member.MemberID INNER JOIN DiscStorage ON RentOrder.DiscID = DiscStorage.DiscID INNER JOIN MovieInfo ON DiscStorage.MovieID = MovieInfo.MovieID WHERE StartDate = DATETIMEFROMPARTS(@year, @month, @day, 0, 0, 0, 0) ORDER BY StartDate";
            SqlContext.Pipe.ExecuteAndSend(CheckSpecificDate);
        }

    }
}

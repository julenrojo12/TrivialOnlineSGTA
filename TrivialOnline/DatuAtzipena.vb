Imports System.Data.SqlClient

Public Class DatuAtzipena

	Private Shared conn As SqlConnection
	Private Shared cmd As SqlCommand


	Private Sub New()
	End Sub


	Public Shared Function Konektatu()
		Try
			conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Erlantz\source\repos\AzureSQLraWebFormularioa\DatuAtzipenekoak\DB_LAB05.mdf;Integrated Security=True")
			conn.Open()
		Catch ex As SqlException
			Throw New KonexioErrorea
		End Try

	End Function

	Public Shared Function ItxiKonexioa()
		conn.Close()
	End Function

	Public Shared Function ErabiltzaileaTxertatu(Email As String, Izena As String, Abizena As String, GalderaEzkutua As String, Erantzuna As String, EgiaztatzeZenbakia As Integer, Egiaztatua As Boolean, Mota As String, Pasahitza As String) As Integer
		Try
			cmd = New SqlCommand("INSERT INTO Erabiltzaileak(email, izena, abizena, galderaEzkutua, erantzuna, egiaztatzeZenbakia, egiaztatua, erabiltzaileMota, pasahitza) VALUES ('" & Email & "', '" & Izena & "', '" & Abizena & "', '" & GalderaEzkutua & "', '" & Erantzuna & "', '" & EgiaztatzeZenbakia & "', '" & Egiaztatua & "', '" & Mota & "', '" & Pasahitza & "')", conn)
			Dim zenb As Integer = cmd.ExecuteNonQuery()
			cmd.Dispose()
			Return zenb
		Catch ex As SqlException
			Console.Write(ex.Message)
			Throw New TxertatzeErrorea
		End Try
	End Function

	Public Shared Function ErabiltzaileaLortu(Email As String) As SqlDataReader
		Try
			Dim rdr As SqlDataReader
			cmd = New SqlCommand("SELECT * FROM Erabiltzaileak WHERE email='" + Email + "'", conn)
			rdr = cmd.ExecuteReader
			cmd.Dispose()
			Return rdr
		Catch ex As SqlException
			Throw New IrakurtzeErrorea
		End Try

	End Function

	Public Shared Function ErabiltzailearenPasahitzaAldatu(Email As String, Pasahitza As String) As Integer
		Try
			cmd = New SqlCommand("UPDATE Erabiltzaileak SET pasahitza='" + Pasahitza + "' WHERE email='" + Email + "'", conn)
			Dim zenb As Integer = cmd.ExecuteNonQuery()
			cmd.Dispose()
			Return zenb
		Catch ex As SqlException
			Throw New EguneratzeErrorea
		End Try

	End Function

End Class

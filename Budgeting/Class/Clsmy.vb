Imports System.Data
Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class Clsmy

    Private ReadOnly FileConf As String = Application.StartupPath & "\autocon.dll"

    Public Shared Function open_conn_excel(ByVal lokasi As String) As OleDbConnection

        Dim cn = New OleDbConnection
        Try

            'Dim myconnectionstring As String = String.Format("Provider=SQLOLEDB;Server={0};Database={1};Uid={2};Pwd={3};", mloc, mdbase, muser, mpwd)

            cn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + lokasi + ";Extended Properties=Excel 12.0;")
            cn.Open()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

        Return cn

    End Function
    Public Shared Function open_conn() As OleDbConnection


        Dim mloc As String = ""
        Dim mdbase As String = ""
        Dim muser As String = ""
        Dim mpwd As String = ""

        Dim cn = New OleDbConnection

        Try

            Dim fileReader As StreamReader = New StreamReader(Application.StartupPath & "\autocon.dll")

            For i As Integer = 0 To 3

                Select Case i
                    Case 0
                        mloc = fileReader.ReadLine
                    Case 1
                        mdbase = fileReader.ReadLine
                    Case 2
                        muser = fileReader.ReadLine
                    Case 3
                        mpwd = fileReader.ReadLine
                End Select

            Next

            If mloc.Trim.Length > 0 Then
                mloc = passwordDecrypt(mloc, keylok)
            End If

            If mdbase.Trim.Length > 0 Then
                mdbase = passwordDecrypt(mdbase, keylok)
            End If

            If muser.Trim.Length > 0 Then
                muser = passwordDecrypt(muser, keylok)
            End If

            If mpwd.Trim.Length > 0 Then
                mpwd = passwordDecrypt(mpwd, keylok)
            End If

            fileReader.Close()

            Dim myconnectionstring As String = String.Format("Provider=SQLNCLI10;Server={0};Database={1};Uid={2};Pwd={3};MARS Connection=True;", mloc, mdbase, muser, mpwd)
            'Dim myconnectionstring As String = String.Format("Provider=SQLNCLI10;Server={0};Database={1};Uid={2};Pwd={3};", mloc, mdbase, muser, mpwd)

            cn.ConnectionString = myconnectionstring
            cn.Open()

        Catch ex As OleDb.OleDbException
            Throw New Exception(ex.ToString)
        End Try

        Return cn

    End Function

    Public Shared Function open_conn_test(ByVal lokasi As String, ByVal datab As String, ByVal suser As String, ByVal pawd As String) As OleDbConnection


        Dim cn = New OleDbConnection

        Try


            Dim myconnectionstring As String = String.Format("Provider=SQLNCLI10;Server={0};Database={1};Uid={2};Pwd={3};;MARS Connection=True;", lokasi, datab, suser, pawd)

            cn.ConnectionString = myconnectionstring
            cn.Open()

        Catch ex As OleDb.OleDbException
            Throw New Exception(ex.ToString)
        End Try

        Return cn

    End Function

    Public Shared Function GetDataSet(ByVal SQL As String, ByVal cn As OleDbConnection) As DataSet

        Dim adapter As New OleDbDataAdapter(SQL, cn)
        Dim myData As New DataSet
        adapter.Fill(myData)

        adapter.Dispose()

        Return myData
    End Function

    Public Shared Function GetDataSet(ByVal Cmd As OleDbCommand, ByVal cn As OleDbConnection) As DataSet

        Dim adapter As New OleDbDataAdapter(Cmd)
        Dim myData As New DataSet
        adapter.Fill(myData)

        adapter.Dispose()

        Return myData
    End Function

    Private Function GenerateHash(ByVal SourceText As String) As String
        'Create an encoding object to ensure the encoding standard for the source text
        Dim Ue As New UnicodeEncoding()
        'Retrieve a byte array based on the source text
        Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
        'Instantiate an MD5 Provider object
        Dim Md5 As New MD5CryptoServiceProvider()
        'Compute the hash value from the source
        Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
        'And convert it to String format for return
        Return Convert.ToBase64String(ByteHash)
    End Function

    Public Shared Sub InsertToLog(ByVal cn As OleDbConnection, ByVal kodemenu As String, ByVal isinsert As Int32, _
                                  ByVal isupdate As Int32, ByVal isdelete As Int32, ByVal isprint As Int32, ByVal nobukti As String, _
                                  ByVal info2 As String, ByVal sqltrans As OleDbTransaction)

        Dim sinfo2 As String = ""
        If info2.Trim.Length > 0 Then
            If IsDate(info2) Then
                sinfo2 = convert_date_to_eng(info2)
            Else
                sinfo2 = info2
            End If
        End If

        Dim sql As String = String.Format("insert into ms_logact (userid,kodemenu,isinsert,isupdate,isdelete,isprint,nobukti,waktu,info2) values(" & _
                                    "'{0}','{1}',{2},{3},{4},{5},'{6}','{7}','{8}')", userprog, kodemenu, isinsert, isupdate, isdelete, isprint, nobukti, convert_datetime_to_eng(Date.Now), sinfo2)

        If IsNothing(sqltrans) Then
            Using comd As OleDbCommand = New OleDbCommand(sql, cn)
                comd.ExecuteNonQuery()
            End Using
        Else
            Using comd1 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                comd1.ExecuteNonQuery()
            End Using
        End If


    End Sub

 

    Public Shared Function WriteContent(ByVal content As String, ByVal sdbase As String, ByVal suser As String, ByVal spwd As String) As String
        Dim fileWriter As StreamWriter
        Try


            content = passwordEncrypt(content, keylok)
            sdbase = passwordEncrypt(sdbase, keylok)
            suser = passwordEncrypt(suser, keylok)
            spwd = passwordEncrypt(spwd, keylok)

            fileWriter = New StreamWriter(Application.StartupPath & "\autocon.dll")
            fileWriter.WriteLine(content)
            fileWriter.WriteLine(sdbase)
            fileWriter.WriteLine(suser)
            fileWriter.WriteLine(spwd)
            fileWriter.Close()

            Return "ok"

        Catch x As Exception
            Throw New Exception(x.ToString)
        End Try
    End Function

  
End Class

Imports DevExpress.XtraSplashScreen
Imports System.Globalization

Imports System.Windows.Forms
Imports System.Reflection

Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Imports System.Data
Imports System.Data.OleDb
Imports Budgeting.Clsmy

Module Mdlmy

    Public userprog, pwd As String
    Public stbalik_opex As Integer
    Public initial_user As String
    Public dtmenu As DataTable
    Public dtmenu2 As DataTable

    Public tglperiod1, tglperiod2 As String
    Public idbu_ku As String
    Public namabu_ku As String
    Public idbu_defaultku As String
    Public st_divku As Integer
    Public st_alluserku As Integer
    Public st_crossbudget As Integer

    Public dsinvoice_ku As DataSet

    Public keylok As String = "maduaja1234567800*@#%^&"

    Public Class ObjectFinder
        Public Shared Function CreateObjectInstance(ByVal objectName As String) As Object
            ' Creates and returns an instance of any object in the assembly by its type name.

            Dim obj As Object

            Try
                If objectName.LastIndexOf(".") = -1 Then
                    'Appends the root namespace if not specified.
                    objectName = String.Format("{0}.{1}", [Assembly].GetEntryAssembly.GetName.Name, objectName)
                End If

                obj = [Assembly].GetEntryAssembly.CreateInstance(objectName)

            Catch ex As Exception
                obj = Nothing
            End Try
            Return obj

        End Function

        Public Shared Function CreateForm(ByVal formName As String) As Form
            ' Return the instance of the form by specifying its name.
            Return DirectCast(CreateObjectInstance(formName), Form)
        End Function

    End Class

    Public Sub open_wait()

        SplashScreenManager.ShowForm(futama1, GetType(waitf), True, True, False)
        ' SplashScreenManager.Default.SetWaitFormDescription("lagi ngetest")

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub open_wait(ByVal capt As String)

        SplashScreenManager.ShowForm(futama1, GetType(waitf), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription(capt)

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub SetWaitDialog(ByVal capt As String)

        ' SplashScreenManager.ShowForm(futama, GetType(waitf), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription(capt)

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub open_wait2(ByVal formm As Form)
        SplashScreenManager.ShowForm(formm, GetType(waitf), True, True, False)
    End Sub

    Public Sub close_wait()
        SplashScreenManager.CloseForm(False)
    End Sub

    Public Function convert_date_to_eng(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function

    Public Function convert_datetime_to_eng(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function

    Public Function convert_datetime_to_ind_HHmm(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("dd/MM/yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function

    Public Function convert_date_to_ind(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, Date).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("id-ID"))

        Return valdate

    End Function


    Public Function passwordEncrypt(ByVal inText As String, ByVal key As String) As String
        Dim bytesBuff As Byte() = Encoding.Unicode.GetBytes(inText)
        Using aes__1 As Aes = Aes.Create()
            Dim crypto As New Rfc2898DeriveBytes(key, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            aes__1.Key = crypto.GetBytes(32)
            aes__1.IV = crypto.GetBytes(16)
            Using mStream As New MemoryStream()
                Using cStream As New CryptoStream(mStream, aes__1.CreateEncryptor(), CryptoStreamMode.Write)
                    cStream.Write(bytesBuff, 0, bytesBuff.Length)
                    cStream.Close()
                End Using
                inText = Convert.ToBase64String(mStream.ToArray())
            End Using
        End Using
        Return inText
    End Function

    Public Function passwordDecrypt(ByVal cryptTxt As String, ByVal key As String) As String
        cryptTxt = cryptTxt.Replace(" ", "+")
        Dim bytesBuff As Byte() = Convert.FromBase64String(cryptTxt)
        Using aes__1 As Aes = Aes.Create()
            Dim crypto As New Rfc2898DeriveBytes(key, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            aes__1.Key = crypto.GetBytes(32)
            aes__1.IV = crypto.GetBytes(16)
            Using mStream As New MemoryStream()
                Using cStream As New CryptoStream(mStream, aes__1.CreateDecryptor(), CryptoStreamMode.Write)
                    cStream.Write(bytesBuff, 0, bytesBuff.Length)
                    cStream.Close()
                End Using
                cryptTxt = Encoding.Unicode.GetString(mStream.ToArray())
            End Using
        End Using
        Return cryptTxt
    End Function


End Module

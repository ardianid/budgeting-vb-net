Imports System.Drawing.Printing
Imports DevExpress.XtraReports.UI
Public Class r_bukti_pengajuan

    Private nobukti As String

    Private Sub XrSubreport2_BeforePrint(sender As Object, e As PrintEventArgs) Handles XrSubreport2.BeforePrint

        nobukti = GetCurrentColumnValue("nobukti")

        '   CType((CType(sender, XRSubreport)).ReportSource.DataSource
        'CType((CType(sender, XRSubreport)).ReportSource, r_bukti_pengajuan2).Parameters("cnobukti").Value = GetCurrentColumnValue("nobukti")

    End Sub

End Class
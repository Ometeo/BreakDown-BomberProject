Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports EnvDTE90a
Imports EnvDTE100
Imports System.Diagnostics

Public Module AddDocumentHeader
    Sub AddDocumentHeader()
        Dim document As Document
        Dim version As String = "1.0.0"
        Dim myName As String = "Thierry Destribats"
        document = DTE.ActiveDocument
        Dim tmp As FileVersionInfo


        Dim Matching As String = "/* --------------------------Header-------------------------------------"
        document.Selection.StartOfDocument()
        document.Selection.LineDown(True, 2)
        Dim content As String = document.Selection.Text
        'Check for already hedader exist or not. Here I am only checking with Copyright Text with in second line of code.
        Dim MatchFound = System.Text.RegularExpressions.Regex.IsMatch(content, Matching)
        If (Not MatchFound) Then
            document.Selection.StartOfDocument()
            document.Selection.LineUp()
            document.Selection.Text = "/* --------------------------Header-------------------------------------"
            document.Selection.NewLine()
            document.Selection.Text = "File : " + document.Name
            document.Selection.NewLine()
            document.Selection.Text = "Description : "
            document.Selection.NewLine()
            document.Selection.Text = "Version : " + version
            document.Selection.NewLine()
            document.Selection.Text = "Created Date : " + Date.Now.ToString()
            document.Selection.NewLine()
            document.Selection.Text = "Created by : " + myName
            document.Selection.NewLine()
            document.Selection.Text = "Modification Date : " + Date.Now.ToString()
            document.Selection.NewLine()
            document.Selection.Text = "Modified by : " + myName
            document.Selection.NewLine()
            document.Selection.Text = "------------------------------------------------------------------------ */"
            document.Selection.NewLine()
            document.Selection.NewLine()
        Else
            document.Selection.StartOfDocument()
            document.Selection.LineDown(False, 6)
            document.Selection.LineDown(True, 3)
            document.Selection.Text = " * Modification Date : " + Date.Now.ToString()
            document.Selection.NewLine()
            document.Selection.Text = "Modified by : " + myName
            document.Selection.NewLine()
            document.Selection.Text = "------------------------------------------------------------------------ */"
            document.Selection.NewLine()
        End If
    End Sub

End Module

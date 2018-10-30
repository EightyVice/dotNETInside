
Public Class Form1


    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim opf As New OpenFileDialog
        opf.Multiselect = False

        If opf.ShowDialog() = DialogResult.OK Then
            Dim asman = New AssemblyManager(opf.FileName)
            TreeView1.Nodes.Clear()
            Me.Text = Me.Text & " - " & opf.FileName
            For Each cls In asman.GetListOfClasses
                Dim l As New List(Of TreeNode)
                Dim members = asman.GetClassMembers(cls)
                Dim methods = asman.GetClassMethods(cls)
                For Each m In members
                    If langcomb.SelectedIndex = 0 Then
                        l.Add(New TreeNode("Dim " & m.Name & " As " & m.FieldType.Name, 1, 1) With {.ContextMenuStrip = copymenu})
                    Else
                        l.Add(New TreeNode(m.FieldType.Name & " " & m.Name & ";", 1, 1) With {.ContextMenuStrip = copymenu})
                    End If
                Next
                For Each meth In methods
                    For Each pi In asman.GetMethodParameters(meth)
                        Dim parms As String = "("
                        If langcomb.SelectedIndex = 0 Then
                            parms = parms & "ByVal " & pi.Name & " As " & pi.ParameterType.Name & ")" & " As " & meth.ReturnType.Name
                            l.Add(New TreeNode(meth.Name & parms, 2, 2) With {.ContextMenuStrip = copymenu})
                        ElseIf langcomb.SelectedIndex = 1 Then
                            parms = " " & parms & pi.ParameterType.Name & " " & pi.Name & ");"
                            l.Add(New TreeNode(meth.ReturnType.Name & " " & meth.Name & parms, 2, 2) With {.ContextMenuStrip = copymenu})
                        End If
                    Next
                Next
                Dim nd = l.ToArray()
                TreeView1.Nodes.Add(New TreeNode("Class " & cls.Name, nd) With {.ContextMenuStrip = copymenu})
            Next
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        langcomb.SelectedIndex = 0
        If Date.Today.Year = 2018 Then
            abouttxt.Text = ".NETInside " & My.Application.Info.Version.ToString & " Copyrights (c) 2018 Zeyad Ahmed"
        Else
            abouttxt.Text = ".NETInside " & My.Application.Info.Version.ToString & " Copyrights (c) 2018 - " & Date.Today.Year & " Zeyad Ahmed"
        End If

    End Sub

    Private Sub abouttxt_Click(sender As Object, e As EventArgs) Handles abouttxt.Click
        Process.Start("https://github.com/ZeyadAhmed/dotNETInside")
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Clipboard.SetText(TreeView1.SelectedNode.Text)
    End Sub
End Class

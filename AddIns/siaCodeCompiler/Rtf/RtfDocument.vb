'*******************************************************************************
' *   This file is part of NRtfTree Library.
' *
' *   NRtfTree Library is free software; you can redistribute it and/or modify
' *   it under the terms of the GNU Lesser General Public License as published by
' *   the Free Software Foundation; either version 3 of the License, or
' *   (at your option) any later version.
' *
' *   NRtfTree Library is distributed in the hope that it will be useful,
' *   but WITHOUT ANY WARRANTY; without even the implied warranty of
' *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' *   GNU Lesser General Public License for more details.
' *
' *   You should have received a copy of the GNU Lesser General Public License
' *   along with this program. If not, see <http://www.gnu.org/licenses/>.
' *******************************************************************************


'*******************************************************************************
' * Library:		NRtfTree
' * Version:     v0.3.0
' * Date:		02/09/2007
' * Copyright:   2007 Salvador Gomez
' * E-mail:      sgoliver.net@gmail.com
' * Home Page:	http://www.sgoliver.net
' * SF Project:	http://nrtftree.sourceforge.net
' *				http://sourceforge.net/projects/nrtftree
' * Class:		RtfDocument
' * Description:	Clase para la generaci�n de documentos RTF.
' * *****************************************************************************


Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports siaCodeCompiler.Net.Sgoliver.NRtfTree.Core

Namespace Net.Sgoliver.NRtfTree
	Namespace Util
		''' <summary>
		''' Clase para la generaci�n de documentos RTF.
		''' </summary>
		Public Class RtfDocument
			#Region "Atributos privados"

			''' <summary>
			''' Ruta del fichero a generar.
			''' </summary>
			Private path As String

			''' <summary>
			''' Codificaci�n del documento.
			''' </summary>
			Private encoding As Encoding

			''' <summary>
			''' Tabla de fuentes del documento.
			''' </summary>
			Private fontTable As RtfFontTable

			''' <summary>
			''' Tabla de colores del documento.
			''' </summary>
			Private colorTable As RtfColorTable

			''' <summary>
			''' �rbol RTF del documento.
			''' </summary>
			Private tree As RtfTree

			''' <summary>
			''' Grupo principal del documento.
			''' </summary>
			Private mainGroup As RtfTreeNode

			''' <summary>
			''' Formato actual del texto.
			''' </summary>
			Private currentFormat As RtfTextFormat

			#End Region

			#Region "Constructores"

			''' <summary>
			''' Constructor de la clase RtfDocument.
			''' </summary>
			''' <param name="path">Ruta del fichero a generar.</param>
			''' <param name="enc">Codificaci�n del documento a generar.</param>
			Public Sub New(path As String, enc As Encoding)
				Me.path = path
				Me.encoding = enc

				fontTable = New RtfFontTable()
				fontTable.AddFont("Arial")
				'Default font
				colorTable = New RtfColorTable()
				colorTable.AddColor(Color.Black)
				'Default color
				currentFormat = Nothing

				tree = New RtfTree()
				mainGroup = New RtfTreeNode(RtfNodeType.Group)

				InitializeTree()
			End Sub

			''' <summary>
			''' Constructor de la clase RtfDocument. Se utilizar� la codificaci�n por defecto del sistema.
			''' </summary>
			''' <param name="path">Ruta del fichero a generar.</param>
			Public Sub New(path As String)

				Me.New(path, Encoding.[Default])
			End Sub

			#End Region

			#Region "Metodos Publicos"

			''' <summary>
			''' Cierra el documento RTF.
			''' </summary>
			Public Sub Close()
				InsertFontTable()
				InsertColorTable()
				InsertGenerator()

				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "par", False, 0))
				tree.RootNode.AppendChild(mainGroup)

				tree.SaveRtf(path)
			End Sub

			''' <summary>
			''' Inserta un fragmento de texto en el documento con un formato de texto determinado.
			''' </summary>
			''' <param name="text">Texto a insertar.</param>
			''' <param name="format">Formato del texto a insertar.</param>
			Public Sub AddText(text As String, format As RtfTextFormat)
				UpdateFontTable(format)
				UpdateColorTable(format)

				InsertFormat(format)

				InsertText(text)
			End Sub

			''' <summary>
			''' Inserta un fragmento de texto en el documento con el formato de texto actual.
			''' </summary>
			''' <param name="text">Texto a insertar.</param>
			Public Sub AddText(text As String)
				InsertText(text)
			End Sub

			''' <summary>
			''' Inserta un salto de l�nea en el documento.
			''' </summary>
			Public Sub AddNewLine()
				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "par", False, 0))
			End Sub

			''' <summary>
			''' Inserta una imagen en el documento.
			''' </summary>
			''' <param name="path">Ruta de la imagen a insertar.</param>
			''' <param name="width">Ancho deseado de la imagen en el documento.</param>
			''' <param name="height">Alto deseado de la imagen en el documento.</param>
			Public Sub AddImage(path As String, width As Integer, height As Integer)
				Dim fStream As FileStream = Nothing
				Dim br As BinaryReader = Nothing

				Try
					Dim data As Byte() = Nothing

					Dim fInfo As New FileInfo(path)
					Dim numBytes As Long = fInfo.Length

					fStream = New FileStream(path, FileMode.Open, FileAccess.Read)
					br = New BinaryReader(fStream)

					data = br.ReadBytes(CInt(numBytes))

					Dim hexdata As New StringBuilder()

					For i As Integer = 0 To data.Length - 1
						hexdata.Append(GetHexa(data(i)))
					Next

					Dim img As Image = Image.FromFile(path)

					Dim imgGroup As New RtfTreeNode(RtfNodeType.Group)
					imgGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "pict", False, 0))

					Dim format As String = ""
					If path.ToLower().EndsWith("wmf") Then
						format = "emfblip"
					Else
						format = "jpegblip"
					End If

					imgGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, format, False, 0))


					imgGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "picw", True, img.Width * 20))
					imgGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "pich", True, img.Height * 20))
					imgGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "picwgoal", True, width * 20))
					imgGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "pichgoal", True, height * 20))
					imgGroup.AppendChild(New RtfTreeNode(RtfNodeType.Text, hexdata.ToString(), False, 0))

					mainGroup.AppendChild(imgGroup)
				Finally
					br.Close()
					fStream.Close()
				End Try
			End Sub

			#End Region

			#Region "Metodos Privados"

			''' <summary>
			''' Obtiene el c�digo hexadecimal de un entero.
			''' </summary>
			''' <param name="code">N�mero entero.</param>
			''' <returns>C�digo hexadecimal del entero pasado como par�metro.</returns>
			Private Function GetHexa(code As Byte) As String
				Dim hexa As String = Convert.ToString(code, 16)

				If hexa.Length = 1 Then
					hexa = "0" & hexa
				End If

				Return hexa
			End Function

			''' <summary>
			''' Inserta el c�digo RTF de la tabla de fuentes en el documento.
			''' </summary>
			Private Sub InsertFontTable()
				Dim ftGroup As New RtfTreeNode(RtfNodeType.Group)

				ftGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "fonttbl", False, 0))

				For i As Integer = 0 To fontTable.Count - 1
					Dim ftFont As New RtfTreeNode(RtfNodeType.Group)
					ftFont.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "f", True, i))
					ftFont.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "fnil", False, 0))
					ftFont.AppendChild(New RtfTreeNode(RtfNodeType.Text, fontTable(i) + ";", False, 0))

					ftGroup.AppendChild(ftFont)
				Next

				mainGroup.InsertChild(5, ftGroup)
			End Sub

			''' <summary>
			''' Inserta el c�digo RTF de la tabla de colores en el documento.
			''' </summary>
			Private Sub InsertColorTable()
				Dim ctGroup As New RtfTreeNode(RtfNodeType.Group)

				ctGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "colortbl", False, 0))

				For i As Integer = 0 To colorTable.Count - 1
					ctGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "red", True, colorTable(i).R))
					ctGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "green", True, colorTable(i).G))
					ctGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "blue", True, colorTable(i).B))
					ctGroup.AppendChild(New RtfTreeNode(RtfNodeType.Text, ";", False, 0))
				Next

				mainGroup.InsertChild(6, ctGroup)
			End Sub

			''' <summary>
			''' Inserta el c�digo RTF de la aplicaci�n generadora del documento.
			''' </summary>
			Private Sub InsertGenerator()
				Dim genGroup As New RtfTreeNode(RtfNodeType.Group)

				genGroup.AppendChild(New RtfTreeNode(RtfNodeType.Control, "*", False, 0))
				genGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "generator", False, 0))
				genGroup.AppendChild(New RtfTreeNode(RtfNodeType.Text, "NRtfTree Library 1.3.0;", False, 0))

				mainGroup.InsertChild(7, genGroup)
			End Sub

			''' <summary>
			''' Inserta todos los nodos de texto y control necesarios para representar un texto determinado.
			''' </summary>
			''' <param name="text">Texto a insertar.</param>
			Private Sub InsertText(text As String)
				Dim i As Integer = 0
				Dim code As Integer = 0

				While i < text.Length
					code = [Char].ConvertToUtf32(text, i)

					If code >= 32 AndAlso code < 128 Then
						Dim s As New StringBuilder("")

						While i < text.Length AndAlso code >= 32 AndAlso code < 128
							s.Append(text(i))

							i += 1

							If i < text.Length Then
								code = [Char].ConvertToUtf32(text, i)
							End If
						End While

						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Text, s.ToString(), False, 0))
					Else
						Dim bytes As Byte() = encoding.GetBytes(New Char() {text(i)})

						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Control, "'", True, bytes(0)))

						i += 1
					End If
				End While
			End Sub

			''' <summary>
			''' Actualiza la tabla de fuentes con una nueva fuente si es necesario.
			''' </summary>
			''' <param name="format"></param>
			Private Sub UpdateFontTable(format As RtfTextFormat)
				If fontTable.IndexOf(format.font) = -1 Then
					fontTable.AddFont(format.font)
				End If
			End Sub

			''' <summary>
			''' Actualiza la tabla de colores con un nuevo color si es necesario.
			''' </summary>
			''' <param name="format"></param>
			Private Sub UpdateColorTable(format As RtfTextFormat)
				If colorTable.IndexOf(format.color) = -1 Then
					colorTable.AddColor(format.color)
				End If
			End Sub

			''' <summary>
			''' Inserta las claves RTF necesarias para representar el formato de texto pasado como par�metro.
			''' </summary>
			''' <param name="format">Formato de texto a representar.</param>
			Private Sub InsertFormat(format As RtfTextFormat)
				If currentFormat IsNot Nothing Then
					'Font Color
					If format.color.ToArgb() <> currentFormat.color.ToArgb() Then
						currentFormat.color = format.color

						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "cf", True, colorTable.IndexOf(format.color)))
					End If

					'Font Name
					If format.size <> currentFormat.size Then
						currentFormat.size = format.size

						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "fs", True, format.size * 2))
					End If

					'Font Size
					If format.font <> currentFormat.font Then
						currentFormat.font = format.font

						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "f", True, fontTable.IndexOf(format.font)))
					End If

					'Bold
					If format.bold <> currentFormat.bold Then
						currentFormat.bold = format.bold

						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "b", If(format.bold, False, True), 0))
					End If

					'Italic
					If format.italic <> currentFormat.italic Then
						currentFormat.italic = format.italic

						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "i", If(format.italic, False, True), 0))
					End If

					'Underline
					If format.underline <> currentFormat.underline Then
						currentFormat.underline = format.underline

						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "ul", If(format.underline, False, True), 0))
					End If
				Else
					'currentFormat == null
					mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "cf", True, colorTable.IndexOf(format.color)))
					mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "fs", True, format.size * 2))
					mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "f", True, fontTable.IndexOf(format.font)))

					If format.bold Then
						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "b", False, 0))
					End If

					If format.italic Then
						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "i", False, 0))
					End If

					If format.underline Then
						mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "ul", False, 0))
					End If

					currentFormat = New RtfTextFormat()
					currentFormat.color = format.color
					currentFormat.size = format.size
					currentFormat.font = format.font
					currentFormat.bold = format.bold
					currentFormat.italic = format.italic
					currentFormat.underline = format.underline
				End If
			End Sub

			''' <summary>
			''' Inicializa el arbol RTF con todas las claves de la cabecera del documento.
			''' </summary>
			Private Sub InitializeTree()
				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "rtf", True, 1))
				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "ansi", False, 0))
				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "ansicpg", True, encoding.CodePage))
				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "deff", True, 0))
				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "deflang", True, CultureInfo.CurrentCulture.LCID))

				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "viewkind", True, 4))
				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "uc", True, 1))
				mainGroup.AppendChild(New RtfTreeNode(RtfNodeType.Keyword, "pard", False, 0))
			End Sub

			#End Region
		End Class
	End Namespace
End Namespace

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
' * Class:		RtfTree
' * Description:	Representa un documento RTF en forma de �rbol.
' * *****************************************************************************


Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Drawing
Imports siaCodeCompiler.Net.Sgoliver.NRtfTree.Util

Namespace Net.Sgoliver.NRtfTree
	Namespace Core
		''' <summary>
		''' Reresenta la estructura en forma de �rbol de un documento RTF.
		''' </summary>
		Public Class RtfTree
			#Region "Atributos privados"

			''' <summary>
			''' Nodo ra�z del documento RTF.
			''' </summary>
			Private m_rootNode As RtfTreeNode
			''' <summary>
			''' Fichero/Cadena de entrada RTF
			''' </summary>
			Private m_rtf As TextReader
			''' <summary>
			''' Analizador l�xico para RTF
			''' </summary>
			Private lex As RtfLex
			''' <summary>
			''' Token actual
			''' </summary>
			Private tok As RtfToken
			''' <summary>
			''' Profundidad del nodo actual
			''' </summary>
			Private level As Integer
			''' <summary>
			''' Indica si se decodifican los caracteres especiales (\') uni�ndolos a nodos de texto contiguos.
			''' </summary>
			Private m_mergeSpecialCharacters As Boolean

			#End Region

			#Region "Contructores"

			''' <summary>
			''' Constructor de la clase RtfTree.
			''' </summary>
			Public Sub New()
				'Se crea el nodo ra�z del documento
				m_rootNode = New RtfTreeNode(RtfNodeType.Root, "ROOT", False, 0)

				m_rootNode.Tree = Me

				' Inicializados por defecto 


				'Se inicializa la propiedad mergeSpecialCharacters

					'Se inicializa la profundidad actual
					'level = 0;
				m_mergeSpecialCharacters = False
			End Sub

			#End Region

			#Region "M�todos P�blicos"

			''' <summary>
			''' Carga un fichero en formato RTF.
			''' </summary>
			''' <param name="path">Ruta del fichero con el documento.</param>
			''' <returns>Se devuelve el valor 0 en caso de no producirse ning�n error en la carga del documento.
			''' En caso contrario se devuelve el valor -1.</returns>
			Public Function LoadRtfFile(path As String) As Integer
				'Resultado de la carga
				Dim res As Integer = 0

				'Se abre el fichero de entrada
				m_rtf = New StreamReader(path)

				'Se crea el analizador l�xico para RTF
				lex = New RtfLex(m_rtf)

				'Se carga el �rbol con el contenido del documento RTF
				res = parseRtfTree()

				'Se cierra el stream
				m_rtf.Close()

				'Se devuelve el resultado de la carga
				Return res
			End Function

			''' <summary>
			''' Carga una cadena de Texto con formato RTF.
			''' </summary>
			''' <param name="text">Cadena de Texto que contiene el documento.</param>
			''' <returns>Se devuelve el valor 0 en caso de no producirse ning�n error en la carga del documento.
			''' En caso contrario se devuelve el valor -1.</returns>
			Public Function LoadRtfText(text As String) As Integer
				'Resultado de la carga
				Dim res As Integer = 0

				'Se abre el fichero de entrada
				m_rtf = New StringReader(text)

				'Se crea el analizador l�xico para RTF
				lex = New RtfLex(m_rtf)

				'Se carga el �rbol con el contenido del documento RTF
				res = parseRtfTree()

				'Se cierra el stream
				m_rtf.Close()

				'Se devuelve el resultado de la carga
				Return res
			End Function

			''' <summary>
			''' Escribe el c�digo RTF del documento a un fichero.
			''' </summary>
			''' <param name="filePath">Ruta del fichero a generar con el documento RTF.</param>
			Public Sub SaveRtf(filePath As String)
				'Stream de salida
				Dim sw As New StreamWriter(filePath)

				'Se trasforma el �rbol RTF a Texto y se escribe al fichero
				sw.Write(Me.RootNode.Rtf)

				'Se cierra el fichero
				sw.Flush()
				sw.Close()
			End Sub

			''' <summary>
			''' Devuelve una representaci�n Textual del documento cargado.
			''' </summary>
			''' <returns>Cadena de caracteres con la representaci�n del documento.</returns>
			Public Overloads Overrides Function ToString() As String
				Dim res As String = ""

				res = toStringInm(m_rootNode, 0, False)

				Return res
			End Function

			''' <summary>
			''' Devuelve una representaci�n Textual del documento cargado. A�ade el tipo de nodo a la izquierda del contenido del nodo.
			''' </summary>
			''' <returns>Cadena de caracteres con la representaci�n del documento.</returns>
			Public Function ToStringEx() As String
				Dim res As String = ""

				res = toStringInm(m_rootNode, 0, True)

				Return res
			End Function

			''' <summary>
			''' Devuelve la tabla de fuentes del documento RTF.
			''' </summary>
			''' <returns>Tabla de fuentes del documento RTF</returns>
			Public Function GetFontTable() As [String]()
				Dim tabla As New ArrayList()
				Dim tablaFuentes As [String]()

				'Nodo raiz del documento
				Dim root As RtfTreeNode = Me.m_rootNode

				'Grupo principal del documento
				Dim nprin As RtfTreeNode = root.FirstChild

				'Buscamos la tabla de fuentes en el �rbol
				Dim enc As Boolean = False
				Dim i As Integer = 0
				Dim ntf As New RtfTreeNode()
				'Nodo con la tabla de fuentes
				While Not enc AndAlso i < nprin.ChildNodes.Count
					If nprin.ChildNodes(i).NodeType = RtfNodeType.Group AndAlso nprin.ChildNodes(i).FirstChild.NodeKey = "fonttbl" Then
						enc = True
						ntf = nprin.ChildNodes(i)
					End If

					i += 1
				End While

				'Rellenamos el array de fuentes
				For j As Integer = 1 To ntf.ChildNodes.Count - 1
					Dim fuente As RtfTreeNode = ntf.ChildNodes(j)

					Dim nombreFuente As String = Nothing

					For Each nodo As RtfTreeNode In fuente.ChildNodes
						If nodo.NodeType = RtfNodeType.Text Then
							nombreFuente = nodo.NodeKey.Substring(0, nodo.NodeKey.Length - 1)
						End If
					Next

					tabla.Add(nombreFuente)
				Next

				'Convertimos el ArrayList en un array tradicional
				tablaFuentes = New [String](tabla.Count - 1) {}

				For c As Integer = 0 To tabla.Count - 1
					tablaFuentes(c) = DirectCast(tabla(c), [String])
				Next

				Return tablaFuentes
			End Function

			''' <summary>
			''' Devuelve la tabla de colores del documento RTF.
			''' </summary>
			''' <returns>Tabla de colores del documento RTF</returns>
			Public Function GetColorTable() As Color()
				Dim tabla As New ArrayList()
				Dim tablaColores As Color()

				'Nodo raiz del documento
				Dim root As RtfTreeNode = Me.m_rootNode

				'Grupo principal del documento
				Dim nprin As RtfTreeNode = root.FirstChild

				'Buscamos la tabla de colores en el �rbol
				Dim enc As Boolean = False
				Dim i As Integer = 0
				Dim ntc As New RtfTreeNode()
				'Nodo con la tabla de fuentes
				While Not enc AndAlso i < nprin.ChildNodes.Count
					If nprin.ChildNodes(i).NodeType = RtfNodeType.Group AndAlso nprin.ChildNodes(i).FirstChild.NodeKey = "colortbl" Then
						enc = True
						ntc = nprin.ChildNodes(i)
					End If

					i += 1
				End While

				'Rellenamos el array de colores
				Dim rojo As Integer = 0
				Dim verde As Integer = 0
				Dim azul As Integer = 0

				'A�adimos el color por defecto, en este caso el negro.
				'tabla.Add(Color.FromArgb(rojo,verde,azul));

				For j As Integer = 1 To ntc.ChildNodes.Count - 1
					Dim nodo As RtfTreeNode = ntc.ChildNodes(j)

					If nodo.NodeType = RtfNodeType.Text AndAlso nodo.NodeKey.Trim() = ";" Then
						tabla.Add(Color.FromArgb(rojo, verde, azul))

						rojo = 0
						verde = 0
						azul = 0
					ElseIf nodo.NodeType = RtfNodeType.Keyword Then
						Select Case nodo.NodeKey
							Case "red"
								rojo = nodo.Parameter
								Exit Select
							Case "green"
								verde = nodo.Parameter
								Exit Select
							Case "blue"
								azul = nodo.Parameter
								Exit Select
						End Select
					End If
				Next

				'Convertimos el ArrayList en un array tradicional
				tablaColores = New Color(tabla.Count - 1) {}

				For c As Integer = 0 To tabla.Count - 1
					tablaColores(c) = CType(tabla(c), Color)
				Next

				Return tablaColores
			End Function

			''' <summary>
			''' Devuelve la informaci�n contenida en el grupo "\info" del documento RTF.
			''' </summary>
			''' <returns>Objeto InfoGroup con la informaci�n del grupo "\info" del documento RTF.</returns>
			Public Function GetInfoGroup() As InfoGroup
				Dim info As InfoGroup = Nothing

				Dim infoNode As RtfTreeNode = Me.RootNode.SelectSingleNode("info")

				'Si existe el nodo "\info" exraemos toda la informaci�n.
				If infoNode IsNot Nothing Then
					Dim auxnode As RtfTreeNode = Nothing

					info = New InfoGroup()

					'Title
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("title"))) IsNot Nothing Then
						info.Title = auxnode.NextSibling.NodeKey
					End If

					'Subject
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("subject"))) IsNot Nothing Then
						info.Subject = auxnode.NextSibling.NodeKey
					End If

					'Author
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("author"))) IsNot Nothing Then
						info.Author = auxnode.NextSibling.NodeKey
					End If

					'Manager
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("manager"))) IsNot Nothing Then
						info.Manager = auxnode.NextSibling.NodeKey
					End If

					'Company
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("company"))) IsNot Nothing Then
						info.Company = auxnode.NextSibling.NodeKey
					End If

					'Operator
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("operator"))) IsNot Nothing Then
						info.[Operator] = auxnode.NextSibling.NodeKey
					End If

					'Category
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("category"))) IsNot Nothing Then
						info.Category = auxnode.NextSibling.NodeKey
					End If

					'Keywords
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("keywords"))) IsNot Nothing Then
						info.Keywords = auxnode.NextSibling.NodeKey
					End If

					'Comments
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("comment"))) IsNot Nothing Then
						info.Comment = auxnode.NextSibling.NodeKey
					End If

					'Document comments
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("doccomm"))) IsNot Nothing Then
						info.DocComment = auxnode.NextSibling.NodeKey
					End If

					'Hlinkbase (The base address that is used for the path of all relative hyperlinks inserted in the document)
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("hlinkbase"))) IsNot Nothing Then
						info.HlinkBase = auxnode.NextSibling.NodeKey
					End If

					'Version
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("version"))) IsNot Nothing Then
						info.Version = auxnode.Parameter
					End If

					'Internal Version
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("vern"))) IsNot Nothing Then
						info.InternalVersion = auxnode.Parameter
					End If

					'Editing Time
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("edmins"))) IsNot Nothing Then
						info.EditingTime = auxnode.Parameter
					End If

					'Number of Pages
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("nofpages"))) IsNot Nothing Then
						info.NumberOfPages = auxnode.Parameter
					End If

					'Number of Chars
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("nofchars"))) IsNot Nothing Then
						info.NumberOfChars = auxnode.Parameter
					End If

					'Number of Words
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("nofwords"))) IsNot Nothing Then
						info.NumberOfWords = auxnode.Parameter
					End If

					'Id
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("id"))) IsNot Nothing Then
						info.Id = auxnode.Parameter
					End If

					'Creation DateTime
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("creatim"))) IsNot Nothing Then
						info.CreationTime = parseDateTime(auxnode.ParentNode)
					End If

					'Revision DateTime
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("revtim"))) IsNot Nothing Then
						info.RevisionTime = parseDateTime(auxnode.ParentNode)
					End If

					'Last Print Time
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("printim"))) IsNot Nothing Then
						info.LastPrintTime = parseDateTime(auxnode.ParentNode)
					End If

					'Backup Time
					If (InlineAssignHelper(auxnode, Me.m_rootNode.SelectSingleNode("buptim"))) IsNot Nothing Then
						info.BackupTime = parseDateTime(auxnode.ParentNode)
					End If
				End If

				Return info
			End Function

			''' <summary>
			''' Devuelve la tabla de c�digos con la que est� codificado el documento RTF.
			''' </summary>
			''' <returns>Tabla de c�digos del documento RTF. Si no est� especificada en el documento se devuelve la tabla de c�digos actual del sistema.</returns>
			Public Function GetEncoding() As Encoding
				'Contributed by Jan Stuchl�k

				Dim encoding__1 As Encoding = Encoding.[Default]

				Dim cpNode As RtfTreeNode = RootNode.SelectSingleNode("ansicpg")

				If cpNode IsNot Nothing Then
					encoding__1 = Encoding.GetEncoding(cpNode.Parameter)
				End If

				Return encoding__1
			End Function

			#End Region

			#Region "M�todos Privados"

			''' <summary>
			''' Analiza el documento y lo carga con estructura de �rbol.
			''' </summary>
			''' <returns>Se devuelve el valor 0 en caso de no producirse ning�n error en la carga del documento.
			''' En caso contrario se devuelve el valor -1.</returns>
			Private Function parseRtfTree() As Integer
				'Resultado de la carga del documento
				Dim res As Integer = 0

				'Codificaci�n por defecto del documento
				Dim encoding__1 As Encoding = Encoding.[Default]

				'Nodo actual
				Dim curNode As RtfTreeNode = m_rootNode

				'Nuevos nodos para construir el �rbol RTF
				Dim newNode As RtfTreeNode = Nothing

				'Se obtiene el primer token
				tok = lex.NextToken()

				While tok.Type <> RtfTokenType.Eof
					Select Case tok.Type
						Case RtfTokenType.GroupStart
							newNode = New RtfTreeNode(RtfNodeType.Group, "GROUP", False, 0)
							curNode.AppendChild(newNode)
							curNode = newNode
							level += 1
							Exit Select
						Case RtfTokenType.GroupEnd
							curNode = curNode.ParentNode
							level -= 1
							Exit Select
						Case RtfTokenType.Keyword, RtfTokenType.Control, RtfTokenType.Text
							If m_mergeSpecialCharacters Then
								'Contributed by Jan Stuchl�k
								Dim isText As Boolean = tok.Type = RtfTokenType.Text OrElse (tok.Type = RtfTokenType.Control AndAlso tok.Key = "'")
								If curNode.LastChild IsNot Nothing AndAlso (curNode.LastChild.NodeType = RtfNodeType.Text AndAlso isText) Then
									If tok.Type = RtfTokenType.Text Then
										curNode.LastChild.NodeKey += tok.Key
										Exit Select
									End If
									If tok.Type = RtfTokenType.Control AndAlso tok.Key = "'" Then
										curNode.LastChild.NodeKey += DecodeControlChar(tok.Parameter, encoding__1)
										Exit Select
									End If
								Else
									'Primer caracter especial \'
									If tok.Type = RtfTokenType.Control AndAlso tok.Key = "'" Then
										newNode = New RtfTreeNode(RtfNodeType.Text, DecodeControlChar(tok.Parameter, encoding__1), False, 0)
										curNode.AppendChild(newNode)
										Exit Select
									End If
								End If
							End If

							newNode = New RtfTreeNode(tok)
							curNode.AppendChild(newNode)

							If m_mergeSpecialCharacters Then
								'Contributed by Jan Stuchl�k
								If level = 1 AndAlso newNode.NodeType = RtfNodeType.Keyword AndAlso newNode.NodeKey = "ansicpg" Then
									encoding__1 = Encoding.GetEncoding(newNode.Parameter)
								End If
							End If

							Exit Select
						Case Else
							res = -1
							Exit Select
					End Select

					'Se obtiene el siguiente token
					tok = lex.NextToken()
				End While

				'Si el nivel actual no es 0 ( == Algun grupo no est� bien formado )
				If level <> 0 Then
					res = -1
				End If

				'Se devuelve el resultado de la carga
				Return res
			End Function

			''' <summary>
			''' Decodifica un caracter especial indicado por su c�digo decimal
			''' </summary>
			''' <param name="code">C�digo del caracter especial (\')</param>
			''' <param name="enc">Codificaci�n utilizada para decodificar el caracter especial.</param>
			''' <returns>Caracter especial decodificado.</returns>
			Private Shared Function DecodeControlChar(code As Integer, enc As Encoding) As String
				'Contributed by Jan Stuchl�k
				Return enc.GetString(New Byte() {CByte(code)})
			End Function

			''' <summary>
			''' M�todo auxiliar para generar la representaci�n Textual del documento RTF.
			''' </summary>
			''' <param name="curNode">Nodo actual del �rbol.</param>
			''' <param name="level">Nivel actual en �rbol.</param>
			''' <param name="showNodeTypes">Indica si se mostrar� el tipo de cada nodo del �rbol.</param>
			''' <returns>Representaci�n Textual del nodo 'curNode' con nivel 'level'</returns>
			Private Function toStringInm(curNode As RtfTreeNode, level As Integer, showNodeTypes As Boolean) As String
				Dim res As New StringBuilder()

				Dim children As RtfNodeCollection = curNode.ChildNodes

				For i As Integer = 0 To level - 1
					res.Append("  ")
				Next

				If curNode.NodeType = RtfNodeType.Root Then
					res.Append("ROOT" & vbCr & vbLf)
				ElseIf curNode.NodeType = RtfNodeType.Group Then
					res.Append("GROUP" & vbCr & vbLf)
				Else
					If showNodeTypes Then
						res.Append(curNode.NodeType)
						res.Append(": ")
					End If

					res.Append(curNode.NodeKey)

					If curNode.HasParameter Then
						res.Append(" ")
						res.Append(Convert.ToString(curNode.Parameter))
					End If

					res.Append(vbCr & vbLf)
				End If

				For Each node As RtfTreeNode In children
					res.Append(toStringInm(node, level + 1, showNodeTypes))
				Next

				Return res.ToString()
			End Function

			''' <summary>
			''' Parsea una fecha con formato "\yr2005\mo12\dy2\hr22\min56\sec15"
			''' </summary>
			''' <param name="group">Grupo RTF con la fecha.</param>
			''' <returns>Objeto DateTime con la fecha leida.</returns>
			Private Shared Function parseDateTime(group As RtfTreeNode) As DateTime
				Dim dt As DateTime

				Dim year As Integer = 0, month As Integer = 0, day As Integer = 0, hour As Integer = 0, min As Integer = 0, sec As Integer = 0

				For Each node As RtfTreeNode In group.ChildNodes
					Select Case node.NodeKey
						Case "yr"
							year = node.Parameter
							Exit Select
						Case "mo"
							month = node.Parameter
							Exit Select
						Case "dy"
							day = node.Parameter
							Exit Select
						Case "hr"
							hour = node.Parameter
							Exit Select
						Case "min"
							min = node.Parameter
							Exit Select
						Case "sec"
							sec = node.Parameter
							Exit Select
					End Select
				Next

				dt = New DateTime(year, month, day, hour, min, sec)

				Return dt
			End Function

			''' <summary>
			''' Extrae el texto de un �rbol RTF.
			''' </summary>
			''' <returns>Texto plano del documento.</returns>
			Private Function ConvertToText() As String
				Dim pardNode As RtfTreeNode = Me.RootNode.FirstChild.SelectSingleChildNode("pard")

				Dim pPard As Integer = Me.RootNode.FirstChild.ChildNodes.IndexOf(pardNode)

				Dim enc As Encoding = Me.GetEncoding()

				Return ConvertToTextAux(Me.RootNode.FirstChild, pPard, enc)
			End Function

			''' <summary>
			''' Extrae el texto de un nodo RTF (Auxiliar de ConvertToText())
			''' </summary>
			''' <param name="curNode">Nodo actual.</param>
			''' <param name="prim">Nodo a partir del que convertir.</param>
			''' <param name="enc">Codificaci�n del documento.</param>
			''' <returns>Texto plano del documento.</returns>
			Private Function ConvertToTextAux(curNode As RtfTreeNode, prim As Integer, enc As Encoding) As String
				Dim res As New StringBuilder("")

				Dim nprin As RtfTreeNode = curNode

				Dim nodo As New RtfTreeNode()

				For i As Integer = prim To nprin.ChildNodes.Count - 1
					nodo = nprin.ChildNodes(i)

					If nodo.NodeType = RtfNodeType.Group Then
						res.Append(ConvertToTextAux(nodo, 0, enc))
					ElseIf nodo.NodeType = RtfNodeType.Control Then
						If nodo.NodeKey = "'" Then
							res.Append(DecodeControlChar(nodo.Parameter, enc))
						End If
					ElseIf nodo.NodeType = RtfNodeType.Text Then
						res.Append(nodo.NodeKey)
					ElseIf nodo.NodeType = RtfNodeType.Keyword Then
						If nodo.NodeKey.Equals("par") Then
							res.AppendLine("")
						End If
					End If
				Next

				Return res.ToString()
			End Function

			#End Region

			#Region "Propiedades"

			''' <summary>
			''' Devuelve el nodo ra�z del �rbol del documento.
			''' </summary>
			Public ReadOnly Property RootNode() As RtfTreeNode
				Get
					'Se devuelve el nodo ra�z del documento
					Return m_rootNode
				End Get
			End Property

			''' <summary>
			''' Devuelve el Texto RTF del documento.
			''' </summary>
			Public ReadOnly Property Rtf() As String
				Get
					'Se devuelve el Texto RTF del documento completo
					Return m_rootNode.Rtf
				End Get
			End Property

			''' <summary>
			''' Indica si se decodifican los caracteres especiales (\') uni�ndolos a nodos de texto contiguos.
			''' </summary>
			Public Property MergeSpecialCharacters() As Boolean
				Get
					Return m_mergeSpecialCharacters
				End Get
				Set
					m_mergeSpecialCharacters = value
				End Set
			End Property

			''' <summary>
			''' Devuelve el texto plano del documento.
			''' </summary>
			Public ReadOnly Property Text() As String
				Get
					Return ConvertToText()
				End Get
			End Property
			Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
				target = value
				Return value
			End Function

			#End Region
		End Class
	End Namespace
End Namespace
